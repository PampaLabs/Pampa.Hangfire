using Hangfire;

using SchedulerMaster;

var builder = Host.CreateApplicationBuilder(args);
builder.Services.AddHostedService<Worker>();

builder.Services.AddHangfire((sp, options) =>
{
    var configuration = sp.GetRequiredService<IConfiguration>();

    var connectionString = configuration.GetConnectionString("HangfireConnection");
    options.UseSqlServerStorage(connectionString);
});

var host = builder.Build();
host.Run();
