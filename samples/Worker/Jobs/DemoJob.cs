using Hangfire;

namespace SchedulerWorker;

public class DemoJob
{
    private readonly ILogger<DemoJob> _logger;

    public DemoJob(ILogger<DemoJob> logger)
    {
        _logger = logger;
    }

    [Job("Demo")]
    public Task Run(string arg)
    {
        if (_logger.IsEnabled(LogLevel.Information))
        {
            _logger.LogInformation("Job executed: {arg}", arg);
        }

        return Task.CompletedTask;
    }
}
