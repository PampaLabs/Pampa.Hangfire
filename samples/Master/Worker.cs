using Hangfire;

namespace SchedulerMaster;

public class Worker : BackgroundService
{
    private readonly IServiceScopeFactory _serviceScopeFactory;

    private readonly ILogger<Worker> _logger;

    public Worker(IServiceScopeFactory serviceScopeFactory, ILogger<Worker> logger)
    {
        _serviceScopeFactory = serviceScopeFactory;
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        await using var scope = _serviceScopeFactory.CreateAsyncScope();

        var client = scope.ServiceProvider.GetRequiredService<IBackgroundJobClient>();

        while (!stoppingToken.IsCancellationRequested)
        {
            var arg = Guid.NewGuid().ToString();

            if (_logger.IsEnabled(LogLevel.Information))
            {
                _logger.LogInformation("Job scheduled: {arg}", arg);
            }

            client.Enqueue<DeferredJob>(job => job.Run("Demo", arg));

            await Task.Delay(5_000, stoppingToken);
        }
    }
}
