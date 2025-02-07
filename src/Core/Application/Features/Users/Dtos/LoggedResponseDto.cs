using Application.Features.Users.Dtos;
using Domain.Entities;
using Domain.Enums.UserEnums;
using Security.JWT;

namespace Application.Features.Authorizations.Dtos;

public class LoggedResponseDto
{
    public AccessToken? AccessToken { get; set; }
    public RefreshToken? RefreshToken { get; set; }
    public UserDto? User { get; set; }

    public AuthenticatorType? RequiredAuthenticatorType { get; set; }
    public LoggedHttpResponse ToHttpResponse() => new() { AccessToken = AccessToken, RequiredAuthenticatorType = RequiredAuthenticatorType };

    public class LoggedHttpResponse
    {
        public AccessToken? AccessToken { get; set; }
        public AuthenticatorType? RequiredAuthenticatorType { get; set; }
    }
}