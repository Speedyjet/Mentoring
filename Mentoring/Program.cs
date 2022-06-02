using Mentoring.Models;

// TODO: Minimum api (not related to any of tasks)
namespace Mentoring
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // TODO: t1 should be AddDbContext with appropriate options e.g. UseSqlServer
            builder.Services.AddSingleton<NorthwindContext>();
            // ex. builder.Services.AddDbContext<NorthwindContext>(
            // options => options.UseSqlServer(builder.Configuration.GetConnectionString("NorthWindConnectionExpress")));

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