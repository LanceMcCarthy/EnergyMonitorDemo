using EnergyMonitor.Client.Pages;
using EnergyMonitor.Client.Services;
using EnergyMonitor.Components;
using EnergyMonitor.Client.Models;
using Microsoft.EntityFrameworkCore;
using Blazored.LocalStorage;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: "_myAllowAllOrigins", policy  => { policy.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin(); });
});


// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents()
    .AddInteractiveWebAssemblyComponents();

builder.Services.AddTelerikBlazor();

if(Environment.GetEnvironmentVariable("DOTNET_RUNNING_IN_CONTAINER") == "true")
{
    builder.Services.AddDbContext<MeasurementsDbContext>(o => { o.UseSqlite("Data Source=/home/app/Measurements.db", b => b.MigrationsAssembly("EnergyMonitor")); });
}
else
{
    builder.Services.AddDbContext<MeasurementsDbContext>(o => { o.UseSqlite("Data Source=Measurements.db", b => b.MigrationsAssembly("EnergyMonitor")); });
}

builder.Services.AddBlazoredLocalStorage();

builder.Services.AddScoped<MessagesDbService>();

builder.Services.AddSingleton<MqttService>();
builder.Services.AddHostedService(sp => sp.GetRequiredService<MqttService>());

var app = builder.Build();

using (var serviceScope = app.Services.CreateScope())
{
    var dbContext = serviceScope.ServiceProvider.GetRequiredService<MeasurementsDbContext>();

    await dbContext.Database.EnsureCreatedAsync();

    if (dbContext.Database.GetPendingMigrations().Any())
    {
        await dbContext.Database.MigrateAsync();
    }
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.UseCors("_myAllowAllOrigins");

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode()
    .AddInteractiveWebAssemblyRenderMode()
    .AddAdditionalAssemblies(typeof(Home).Assembly);

app.Run();
