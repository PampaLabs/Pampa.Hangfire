namespace Hangfire;

/// <summary>
/// Default implementation of <see cref="IJobCollection"/> that provides a list-based storage for job descriptors.
/// </summary>
public class JobCollection : List<JobDescriptor>, IJobCollection
{
}
