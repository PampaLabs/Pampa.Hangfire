using System.Reflection;

namespace Hangfire;

/// <summary>
/// Provides job method resolution based on a predefined job collection.
/// </summary>
public class JobProvider : IJobProvider
{
    private readonly IJobCollection _jobs;

    /// <summary>
    /// Initializes a new instance of the <see cref="JobProvider"/> class with the specified job collection.
    /// </summary>
    /// <param name="jobs">The collection of registered job descriptors.</param>
    public JobProvider(IJobCollection jobs)
    {
        _jobs = jobs;
    }

    /// <inheritdoc/>
    public MethodInfo? GetJobMethod(string jobName)
    {
        var descriptor = _jobs.FirstOrDefault(x => x.Name == jobName);
        return descriptor?.JobMethod;
    }

    /// <inheritdoc/>
    public MethodInfo GetRequiredJobMethod(string jobName)
    {
        var descriptor = _jobs.First(x => x.Name == jobName);
        return descriptor.JobMethod;
    }
}
