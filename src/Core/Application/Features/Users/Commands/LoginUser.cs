using Application.Features.Authorizations.Dtos;
using Application.Interfaces.Repository;
using Application.Pipelines.Transaction;
using Application.Wrappers.Results;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Security.Hashing;
using Security.JWT;

namespace Application.Features.Authorizations.Commands;

public class LoginUser : IRequest<IDataResult<LoggedResponseDto>>, ITransactionalRequest
{
    public string Email { get; set; }
    public string Password { get; set; }
    public string IpAddress { get; set; }

    public class LoginUserHandler : IRequestHandler<LoginUser, IDataResult<LoggedResponseDto>>
    {
        private readonly IUserRepository _userRepository;
        private readonly ITokenHelper _tokenHelper;
        private readonly IRefreshTokenRepository _refreshTokenRepository;
        private readonly TokenOptions _tokenOptions;

        public LoginUserHandler(IUserRepository userRepository, ITokenHelper tokenHelper, IRefreshTokenRepository refreshTokenRepository, TokenOptions tokenOptions)
        {
            _userRepository = userRepository;
            _tokenHelper = tokenHelper;
            _refreshTokenRepository = refreshTokenRepository;
            _tokenOptions = tokenOptions;
        }

        public async Task<IDataResult<LoggedResponseDto>> Handle(LoginUser request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetAsync(x => x.Email == request.Email, cancellationToken);
            if (user == null)
                return new ErrorDataResult<LoggedResponseDto>("User not found");

            if (!HashingHelper.VerifyPasswordHash(request.Password, user!.PasswordHash, user.PasswordSalt))
                return new ErrorDataResult<LoggedResponseDto>("Email or password is wrong");


            var accessToken = _tokenHelper.CreateToken(user);
            var createdRefreshToken = _tokenHelper.CreateRefreshToken(user, request.IpAddress);
            var addedRefreshToken = await _refreshTokenRepository.AddAsync(createdRefreshToken);

            List<RefreshToken> refreshTokens = await _refreshTokenRepository
                .Query()
                .Where(
                    r =>
                        r.UserId == user.Id
                        && r.Revoked == null
                        && r.Expires >= DateTime.UtcNow
                        && r.CreatedDate.AddDays(_tokenOptions.RefreshTokenTTL) <= DateTime.UtcNow
                )
                .ToListAsync();

            await _refreshTokenRepository.DeleteRangeAsync(refreshTokens);

            LoggedResponseDto loggedResponse = new();
            loggedResponse.AccessToken = accessToken;
            loggedResponse.RefreshToken = addedRefreshToken;
            return new SuccessDataResult<LoggedResponseDto>(loggedResponse);
        }
    }
}