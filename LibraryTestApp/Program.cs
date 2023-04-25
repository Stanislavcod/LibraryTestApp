using Library.BusinessLogic.Services.Contracts;
using Library.Common.ModelsDto;
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

//var authService = app.Services.CreateScope().ServiceProvider.GetRequiredService<IAuthService>();
//var admin = new UserDto { Login = "admin", Password = "admin", RoleId = 2 };
//authService.RegisterAdmin(admin);

ConfigurationHelper.Configure(app);

app.Run();
