using Mentoring.Models;
using Microsoft.Extensions.Logging;

namespace Mentoring
{
    public class Program
    {
        
        private static readonly log4net.ILog _log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        public static void Main(string[] args)
        {
            _log.Info("Starting application");
            var builder = WebApplication.CreateBuilder(args);
            _log.Info("Builder created");

            builder.Logging.AddLog4Net();

            builder.Services.AddSingleton<NorthwindContext>();

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}