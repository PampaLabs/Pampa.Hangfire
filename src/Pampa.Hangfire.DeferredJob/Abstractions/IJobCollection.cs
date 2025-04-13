namespace Hangfire;

/// <summary>
/// Represents a collection of job descriptors, supporting list operations for managing registered jobs.
/// </summary>
public interface IJobCollection : IList<JobDescriptor>
{
}
