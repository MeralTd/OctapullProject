using Microsoft.Extensions.DependencyInjection;
using Security.JWT;

namespace Security;

public static class SecurityServiceRegistration
{
    public static IServiceCollection AddSecurityServices(this IServiceCollection services)
    {
        services.AddScoped<ITokenHelper, JwtHelper>();
        services.AddSingleton<TokenOptions>();
        return services;
    }
}