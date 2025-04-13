namespace Hangfire;

/// <summary>
/// Identifies a method as a job and assigns it a name for registration and lookup.
/// </summary>
[AttributeUsage(AttributeTargets.Method)]
public class JobAttribute : Attribute
{
    /// <summary>
    /// Initializes a new instance of the <see cref="JobAttribute"/> class with the specified job name.
    /// </summary>
    /// <param name="name">The name to associate with the job method.</param>
    public JobAttribute(string name)
    {
        Name = name;
    }

    /// <summary>
    /// Gets the name associated with the job method.
    /// </summary>
    public string Name { get; }
}
