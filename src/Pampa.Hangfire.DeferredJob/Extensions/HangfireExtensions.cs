using System.Reflection;

namespace Hangfire;

/// <summary>
/// Provides extension methods for <see cref="IGlobalConfiguration"/>.
/// </summary>
public static class HangfireExtensions
{
    /// <summary>
    /// Configures the job provider to be used globally in the given configuration.
    /// </summary>
    /// <typeparam name="TJobProvider">The type of the job provider to use.</typeparam>
    /// <param name="configuration">The global configuration instance.</param>
    /// <param name="provider">The job provider to set.</param>
    /// <returns>The updated global configuration with the job provider.</returns>
    /// <exception cref="ArgumentNullException">Thrown when the configuration or provider is null.</exception>
    public static IGlobalConfiguration<TJobProvider> UseJobProvider<TJobProvider>(this IGlobalConfiguration configuration, TJobProvider provider)
        where TJobProvider : IJobProvider
    {
        if (configuration is null) throw new ArgumentNullException(nameof(configuration));
        if (provider is null) throw new ArgumentNullException(nameof(provider));

        return configuration.Use(provider, delegate (TJobProvider jp)
        {
            JobProviderDefaults.Current = jp;
        });
    }

    /// <summary>
    /// Configures the job provider using a custom action to modify a job collection.
    /// </summary>
    /// <param name="configuration">The global configuration instance.</param>
    /// <param name="action">The action to modify the job collection.</param>
    /// <returns>The updated global configuration with the job provider.</returns>
    /// <exception cref="ArgumentNullException">Thrown when the configuration is null.</exception>
    public static IGlobalConfiguration<IJobProvider> UseJobProvider(this IGlobalConfiguration configuration, Action<IJobCollection> action)
    {
        if (configuration is null) throw new ArgumentNullException(nameof(configuration));

        var jobs = new JobCollection();
        action.Invoke(jobs);

        var provider = jobs.BuildJobProvider();

        return configuration.UseJobProvider(provider);
    }

    /// <summary>
    /// Configures the job provider by registering jobs from the specified assemblies.
    /// </summary>
    /// <param name="configuration">The global configuration instance.</param>
    /// <param name="assemblies">The assemblies from which to register job types.</param>
    /// <returns>The updated global configuration with the job provider.</returns>
    /// <exception cref="ArgumentNullException">Thrown when the configuration is null.</exception>
    public static IGlobalConfiguration<IJobProvider> UseJobProvider(this IGlobalConfiguration configuration, params Assembly[] assemblies)
    {
        if (configuration is null) throw new ArgumentNullException(nameof(configuration));

        var jobs = new JobCollection();
        jobs.RegisterJobsFromAssembly(assemblies);

        var provider = jobs.BuildJobProvider();

        return configuration.UseJobProvider(provider);
    }

    /// <summary>
    /// Configures the job provider by registering jobs from the specified types.
    /// </summary>
    /// <param name="configuration">The global configuration instance.</param>
    /// <param name="types">The types containing job methods to register.</param>
    /// <returns>The updated global configuration with the job provider.</returns>
    /// <exception cref="ArgumentNullException">Thrown when the configuration is null.</exception>
    public static IGlobalConfiguration<IJobProvider> UseJobProvider(this IGlobalConfiguration configuration, params Type[] types)
    {
        if (configuration is null) throw new ArgumentNullException(nameof(configuration));

        var jobs = new JobCollection();
        jobs.RegisterJobsFromType(types);

        var provider = jobs.BuildJobProvider();

        return configuration.UseJobProvider(provider);
    }
}
