using System.Reflection;

namespace Hangfire;

/// <summary>
/// Defines methods for retrieving job method information by job name.
/// </summary>
public interface IJobProvider
{
    /// <summary>
    /// Attempts to retrieve the method information for the specified job name.
    /// </summary>
    /// <param name="jobName">The name of the job.</param>
    /// <returns>The associated method information, or null if not found.</returns>
    MethodInfo? GetJobMethod(string jobName);

    /// <summary>
    /// Retrieves the method information for the specified job name.
    /// </summary>
    /// <param name="jobName">The name of the job.</param>
    /// <returns>The associated method information.</returns>
    MethodInfo GetRequiredJobMethod(string jobName);
}
