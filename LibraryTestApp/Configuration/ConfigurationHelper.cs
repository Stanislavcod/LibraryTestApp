using Microsoft.EntityFrameworkCore;
using Library.Model.DatabaseContext;
using Library.BusinessLogic.Services.Contracts;
using Library.BusinessLogic.Services.Implementations;

namespace LibraryTestApp.Configuration
{
    public static class ConfigurationHelper
    {
        public static void ConfigurationServices(IServiceCollection services, IConfiguration configuration)
        {
            string connection = configuration.GetConnectionString("DefaultConnection");

            services.AddDbContext<ApplicationDatabaseContext>(options => options.UseSqlServer(connection,
                opt => opt.MigrationsAssembly("StudentAccounting")));

            services
                .AddTransient<IUserService, UserService>()
                .AddTransient<IBookService, BookService>();
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

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");
        }
    }
}
