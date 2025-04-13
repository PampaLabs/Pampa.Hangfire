using System.Reflection;

namespace Hangfire;

internal static class JobCollectionRegisterExtensions
{
    public static IJobCollection RegisterJobsFromAssembly(this IJobCollection jobs, params Assembly[] assemblies)
    {
        foreach (var assembly in assemblies)
        {
            var types = assembly.GetTypes();
            jobs.RegisterJobsFromType(types);
        }

        return jobs;
    }

    public static IJobCollection RegisterJobsFromType(this IJobCollection jobs, params Type[] types)
    {
        foreach (var type in types)
        {
            var methods = type.GetMethods();

            foreach (var method in methods)
            {
                var attributes = method.GetCustomAttributes(typeof(JobAttribute), false).OfType<JobAttribute>();

                foreach (var attribute in attributes)
                {
                    jobs.AddJob(attribute.Name, method);
                }
            }
        }

        return jobs;
    }
}
