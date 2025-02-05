using Application.Interfaces.Repository;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Quartz;

namespace BackgroundService.MeetingJobs;

public class MeetingDeleteJob : IJob
{
    private readonly IServiceProvider _serviceProvider;
    private readonly ILogger<MeetingDeleteJob> _logger;

    public MeetingDeleteJob(IServiceProvider serviceProvider, ILogger<MeetingDeleteJob> logger)
    {
        _serviceProvider = serviceProvider;
        _logger = logger;
    }

    public async Task Execute(IJobExecutionContext context)
    {
        _logger.LogInformation("MeetingDeleteJob is starting...");
        using var scope = _serviceProvider.CreateScope();
        var meetingRepository = scope.ServiceProvider.GetRequiredService<IMeetingRepository>();

        var meetings = await meetingRepository.GetAllAsync(x => x.IsCancelled == true);
        _logger.LogInformation($"{true} number of meetings have been cancelled");

        foreach (var meeting in meetings)
        {


            await meetingRepository.RemoveAsync(meeting);

            _logger.LogInformation("MeetingDeleteJob is deleteted...");

        }
        _logger.LogInformation("MeetingDeleteJob is completed...");
    }
}