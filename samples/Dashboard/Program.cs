using Hangfire;
using Hangfire.Console;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHangfire(configuration =>
{
    configuration
        .UseConsole()
        .UseSqlServerStorage(builder.Configuration.GetConnectionString("HangfireConnection"));
});

var app = builder.Build();

app.UseHangfireDashboard(pathMatch: "");

app.Run();
