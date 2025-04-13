namespace Hangfire;

/// <summary>
/// Executes deferred jobs by resolving and invoking registered job methods using a job activator and provider.
/// </summary>
public class DeferredJob
{
    private readonly JobActivator _jobActivator;

    private readonly IJobProvider _jobProvider = JobProviderDefaults.Current;

    /// <summary>
    /// Initializes a new instance of the <see cref="DeferredJob"/> class with the specified job activator.
    /// </summary>
    public DeferredJob(JobActivator jobActivator)
    {
        _jobActivator = jobActivator;
    }

    /// <summary>
    /// Executes the job associated with the given name, passing any provided parameters.
    /// </summary>
    /// <param name="jobName">The name of the job to execute.</param>
    /// <param name="parameters">Optional parameters to pass to the job method.</param>
    public async Task Run(string jobName, params object?[]? parameters)
    {
        var methodInfo = _jobProvider.GetRequiredJobMethod(jobName);

        using var scope = _jobActivator.BeginScope(null as JobActivatorContext);

        var target = methodInfo.IsStatic ? null : scope.Resolve(methodInfo.DeclaringType);

        var result = methodInfo.Invoke(target, parameters);

        if (result is Task asyncResult)
        {
            await asyncResult;
        }
    }
}
