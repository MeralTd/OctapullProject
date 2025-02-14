using Application.Pipelines.Transaction;
using Mailing;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Application;

public static class ServiceRegistration
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        var assembly = Assembly.GetExecutingAssembly();
        services.AddAutoMapper(assembly);
        services.AddMediatR(configuration =>
        {
            configuration.RegisterServicesFromAssemblies(assembly);
            configuration.AddOpenBehavior(typeof(TransactionScopeBehavior<,>));
        });

        services.AddSingleton<IMailService, MailKitMailService>();


        return services;
    }
}