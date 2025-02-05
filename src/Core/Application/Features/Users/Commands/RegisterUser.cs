using Application.Interfaces.Repository;
using Application.Wrappers.Results;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Security.Hashing;

namespace Application.Features.Authorizations.Commands;

public class RegisterUser : IRequest<IResponseResult>
{
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public required string Email { get; set; }
    public required string PhoneNumber { get; set; }
    public required string Password { get; set; }
    public string? ProfilUrl { get; set; }


    public class RegisterUserHandler : IRequestHandler<RegisterUser, IResponseResult>
    {
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;

        public RegisterUserHandler(IMapper mapper, IUserRepository userRepository)
        {
            _mapper = mapper;
            _userRepository = userRepository;
        }

        public async Task<IResponseResult> Handle(RegisterUser request, CancellationToken cancellationToken)
        {
            var userControl = await _userRepository.GetAsync(x => x.Email == request.Email);
            if (userControl != null)
                return new ErrorResult("Already registered with this email address");



            var user = _mapper.Map<User>(request);
            HashingHelper.CreatePasswordHash(request.Password, out var passwordHash, out var passwordSalt);
            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;
            await _userRepository.AddAsync(user);



            return new SuccessResult("You have successfully registered, please check your mailbox.");
        }
    }
}