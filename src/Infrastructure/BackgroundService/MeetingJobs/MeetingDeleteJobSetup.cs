using Microsoft.Extensions.Options;
using Quartz;

namespace BackgroundService.MeetingJobs;

public class MeetingDeleteJobSetup : IConfigureOptions<QuartzOptions>
{
    public void Configure(QuartzOptions options)
    {
        var jobKey = JobKey.Create("mettingDeleteJob", "meetingDelete-group");

        options.AddJob<MeetingDeleteJob>(jobBuilder => jobBuilder.WithIdentity(jobKey)).AddTrigger(trigger =>
            trigger.ForJob(jobKey).WithSimpleSchedule(schedule => schedule.WithIntervalInMinutes(30).RepeatForever()));


    }
}