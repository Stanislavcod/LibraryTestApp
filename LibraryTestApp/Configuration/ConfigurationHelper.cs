using Library.BusinessLogic.Services.Contracts;
using Library.BusinessLogic.Services.Implementations;
using Library.Model.DatabaseContext;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;

namespace LibraryTestApp.ConfigurationHelper
{
    public static class ConfigurationHelper
    {
        public static void ConfigurationServices(IServiceCollection services, IConfiguration configuration)
        {
            string connection = configuration.GetConnectionString("DefaultConnection");

            services.AddDbContext<ApplicationDatabaseContext>(options =>
            options.UseSqlServer(connection ?? throw new InvalidOperationException("Connection string 'LibraryTestAppContext' not found.")));

            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
     .AddCookie(options => options.LoginPath = "/login");

            services.AddHttpContextAccessor();
            services.AddAuthorization();

            services
                .AddTransient<IBookService, BookService>()
                .AddTransient<IUserService, UserService>()
                .AddTransient<IAuthService, AuthService>();
        }
        public static void Configure(WebApplication app)
        {
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Book}/{action=Index}/{id?}");
        }
    }
}
