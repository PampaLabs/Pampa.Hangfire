using System.Reflection;

namespace Hangfire;

/// <summary>
/// Represents a job descriptor that associates a name with a specific method to be invoked as a job.
/// </summary>
public class JobDescriptor
{
    /// <summary>
    /// Gets the name of the job.
    /// </summary>
    public string Name { get; }

    /// <summary>
    /// Gets the method information associated with the job.
    /// </summary>
    public MethodInfo JobMethod { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="JobDescriptor"/> class with the specified name and method.
    /// </summary>
    /// <param name="name">The name of the job.</param>
    /// <param name="jobMethod">The method to be executed for the job.</param>
    public JobDescriptor(string name, MethodInfo jobMethod)
    {
        Name = name;
        JobMethod = jobMethod;
    }
}
