# Hangfire Deferred Job

This package enables deferred jobs for Hangfire. Neither the dashboard nor the scheduler needs to know how a job is implemented. Instead, the job's name and parameters are passed to a remote executor, enabling flexible and dynamic job definitions.

## Installation

To use deferred jobs with `Hangfire`, you will first need to install the package.

```
dotnet add package Pampa.Hangfire.DeferredJob
```

## Usage

### Configure the Job Provider

Registers the job provider using the `UseJobProvider(...)` method so that Hangfire can discover and execute deferred jobs.

```csharp
using System.Reflection;

using Hangfire;

var builder = Host.CreateApplicationBuilder(args);

builder.Services.AddHangfire((sp, options) =>
{
    var configuration = sp.GetRequiredService<IConfiguration>();

    var connectionString = configuration.GetConnectionString("HangfireConnection");
    options.UseSqlServerStorage(connectionString);
 
    options.UseJobProvider(Assembly.GetExecutingAssembly());
});

builder.Services.AddHangfireServer();

var host = builder.Build();
host.Run();
```

### Define a Job

Creates a class with a method marked as a deferred job using the `[Job]` attribute.

```csharp
public class DemoJob
{
    [Job("Demo")]
    public async Task Run(string arg)
    {
        // ...
    }
}
```

### Enqueue a Job

Adds a deferred job to the Hangfire queue by specifying its name and parameters.

```csharp
var arg = Guid.NewGuid().ToString();

var client = serviceProvider.GetRequiredService<IBackgroundJobClient>();
client.Enqueue<DeferredJob>(job => job.Run("Demo", arg));
```

## Contributing

Contributions are welcome! Please open an issue or submit a pull request on GitHub.
