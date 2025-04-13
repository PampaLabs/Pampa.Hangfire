namespace Hangfire;

/// <summary>
/// Provides a default instance of <see cref="IJobProvider"/> for job resolution.
/// </summary>
public static class JobProviderDefaults
{
    /// <summary>
    /// Gets or sets the current job provider instance used for resolving jobs.
    /// </summary>
    public static IJobProvider Current { get; set; }

    static JobProviderDefaults()
    {
        Current = new JobCollection().BuildJobProvider();
    }
}
