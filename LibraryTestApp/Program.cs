using LibraryTestApp.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

var configure = builder.Configuration;

builder.Services.AddSingleton<IConfiguration>(builder.Configuration);

ConfigurationHelper.ConfigurationServices(builder.Services, configure);

var app = builder.Build();

ConfigurationHelper.Configure(app);

app.Run();
