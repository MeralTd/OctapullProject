using BackgroundService.MeetingJobs;
using Microsoft.Extensions.DependencyInjection;
using Quartz;
using Quartz.AspNetCore;

namespace BackgroundService;

public static class ServiceRegistration
{
    public static void AddBackgroundServiceServices(this IServiceCollection services)
    {
        services.AddQuartz();

        services.AddQuartzServer(options =>
        {
            options.WaitForJobsToComplete = true;
        });

        services.AddQuartzHostedService(options =>
        {
            options.WaitForJobsToComplete = true;
        });

        services.ConfigureOptions<MeetingDeleteJobSetup>();
    }
}