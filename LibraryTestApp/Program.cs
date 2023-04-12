using LibraryTestApp.ConfigurationHelper;
using Serilog;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

var configure = builder.Configuration;

builder.Services.AddSingleton<IConfiguration>(builder.Configuration);

Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo.File($"Logs/{Assembly.GetExecutingAssembly().GetName().Name}.log")
                .WriteTo.Console()
                .CreateLogger();
builder.Logging.ClearProviders();
builder.Logging.AddSerilog();

ConfigurationHelper.ConfigurationServices(builder.Services, configure);

var app = builder.Build();

ConfigurationHelper.Configure(app);

app.Run();
