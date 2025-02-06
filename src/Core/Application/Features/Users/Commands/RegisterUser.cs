using Application.Interfaces.Repository;
using Application.Wrappers.Results;
using AutoMapper;
using Domain.Entities;
using Mailing;
using MediatR;
using MimeKit;
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
        private readonly IMailService _mailService;

        public RegisterUserHandler(IMapper mapper, IUserRepository userRepository, IMailService mailService)
        {
            _mapper = mapper;
            _userRepository = userRepository;
            _mailService = mailService;
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



            var toEmailList = new List<MailboxAddress>();


            toEmailList.Add(new MailboxAddress(name: user.FirstName + " " + user.LastName, address: user.Email));

            await _mailService.SendEmailAsync(new Mail
            {
                ToList = toEmailList,
                BccList = null,
                Subject = "Your Registration is Completed - Welcome!",
                HtmlBody = $"Hello {user.FirstName}, <br> <br> Your account has been successfully created! You can now start using all the features that Octapull has to offer."
            });


            return new SuccessResult("You have successfully registered, please check your mailbox.");
        }
    }
}