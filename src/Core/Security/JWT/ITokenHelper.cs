using Domain.Entities;

namespace Security.JWT;

public interface ITokenHelper
{
    AccessToken CreateToken(User user);
    RefreshToken CreateRefreshToken(User user, string ipAddress);
}