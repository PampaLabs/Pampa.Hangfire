using System.Reflection;

using Hangfire;

using SchedulerWorker;

var builder = Host.CreateApplicationBuilder(args);
// builder.Services.AddHostedService<Worker>();

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
