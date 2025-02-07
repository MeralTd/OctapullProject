using Domain.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Security.Encryption;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Cryptography;
using System.Text;

namespace Security.JWT;

public class JwtHelper : ITokenHelper
{
    private IConfiguration Configuration { get; }
    private readonly TokenOptions _tokenOptions;
    private DateTime _accessTokenExpiration;

    public JwtHelper(IConfiguration configuration)
    {
        Configuration = configuration;
        const string configurationSection = "TokenOptions";
        _tokenOptions = Configuration.GetSection(configurationSection).Get<TokenOptions>() ??
                        throw new NullReferenceException(
                            $"\"{configurationSection}\" section cannot found in configuration.");
    }

    public AccessToken CreateToken2(User user)
    {
        var key = new byte[64]; // 512 bit için 64 byte
        using var rng = RandomNumberGenerator.Create();
        rng.GetBytes(key);
        var keyBase64 = Convert.ToBase64String(key);

        _accessTokenExpiration = DateTime.Now.AddMinutes(_tokenOptions.AccessTokenExpiration);
        var securityKey = SecurityKeyHelper.CreateSecurityKey(keyBase64);
        var signingCredentials = SigningCredentialsHelper.CreateSigningCredentials(securityKey);
        var jwt = CreateJwtSecurityToken(_tokenOptions, user, signingCredentials);
        JwtSecurityTokenHandler jwtSecurityTokenHandler;
        jwtSecurityTokenHandler = new();
        var token = jwtSecurityTokenHandler.WriteToken(jwt);

        return new AccessToken { Token = token, Expiration = _accessTokenExpiration };
    }


    public AccessToken CreateToken(User user)
    {

        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_tokenOptions.SecurityKey));

        // Signing credentials oluşturuluyor
        var signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        // Token expiration (geçerlilik süresi)
        _accessTokenExpiration = DateTime.Now.AddMinutes(_tokenOptions.AccessTokenExpiration);



        // JWT token oluşturuluyor
        var jwt = new JwtSecurityToken(
            _tokenOptions.Issuer,  // Geçerli issuer (yayıncı)
            _tokenOptions.Audience,  // Geçerli audience (hedef kitle)
            expires: _accessTokenExpiration,
            signingCredentials: signingCredentials
        );

        // Token yazma
        var jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
        var token = jwtSecurityTokenHandler.WriteToken(jwt);

        return new AccessToken { Token = token, Expiration = _accessTokenExpiration };
    }

    public RefreshToken CreateRefreshToken(User user)
    {
        RefreshToken refreshTokenEntity = new()
        {
            UserId = user.Id,
            Token = RandomRefreshToken(),
            Expires = DateTime.UtcNow.AddDays(7),
            //CreatedByIp = ipAddress
        };

        return refreshTokenEntity;
    }

    private JwtSecurityToken CreateJwtSecurityToken(TokenOptions tokenOptions, User user, SigningCredentials signingCredentials)
    {
        JwtSecurityToken jwt = new(tokenOptions.Issuer, tokenOptions.Audience, expires: _accessTokenExpiration,
            notBefore: DateTime.Now, signingCredentials: signingCredentials);
        return jwt;
    }


    private static string RandomRefreshToken()
    {
        var numberByte = new byte[32];
        using var random = RandomNumberGenerator.Create();
        random.GetBytes(numberByte);
        return Convert.ToBase64String(numberByte);
    }
}