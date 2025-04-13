using System.Reflection;

namespace Hangfire;

/// <summary>
/// Provides extension methods for <see cref="IJobCollection"/>.
/// </summary>
public static class JobCollectionExtensions
{
    /// <summary>
    /// Adds a new job descriptor to the job collection with the specified name and method information.
    /// </summary>
    /// <param name="jobs">The job collection to which the job descriptor will be added.</param>
    /// <param name="name">The name of the job.</param>
    /// <param name="methodInfo">The method information associated with the job.</param>
    /// <returns>The updated job collection with the new job descriptor added.</returns>
    public static IJobCollection AddJob(this IJobCollection jobs, string name, MethodInfo methodInfo)
    {
        jobs.Add(new JobDescriptor(name, methodInfo));
        return jobs;
    }

    /// <summary>
    /// Builds a <see cref="JobProvider"/> from the specified job collection.
    /// </summary>
    /// <param name="jobs">The job collection to use for building the job provider.</param>
    /// <returns>A <see cref="JobProvider"/> instance configured with the provided job collection.</returns>
    public static JobProvider BuildJobProvider(this IJobCollection jobs)
    {
        return new JobProvider(jobs);
    }
}
