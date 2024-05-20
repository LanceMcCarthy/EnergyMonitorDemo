using EnergyMonitor.Client.Pages;
using EnergyMonitor.Client.Services;
using EnergyMonitor.Components;
using EnergyMonitor.Client.Models;
using kDg.FileBaseContext.Extensions;

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

//builder.Services.AddDbContext<MeasurementsDbContext>(o => { o.UseSqlite("Data Source = Measurements.db", b => b.MigrationsAssembly("EnergyMonitor")); });
builder.Services.AddDbContext<MeasurementsDbContext>(options => options.UseFileBaseContextDatabase("measurements"));

builder.Services.AddScoped<MqttUiService>();
builder.Services.AddScoped<MessagesDataService>();

var app = builder.Build();

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
