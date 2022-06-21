using Mentoring.Models;
using Mentoring.BL;
using Microsoft.EntityFrameworkCore;
using Mentoring.Controllers;

var builder = WebApplication.CreateBuilder(args);
    builder.Logging.AddLog4Net();
    builder.Services.AddDbContext<NorthwindContext>(options => options.UseSqlServer(
    builder.Configuration.GetSection("ConnectionStrings").Value));
    builder.Services.AddControllersWithViews();
    builder.Services.AddTransient<IBusinessLogic, BusinessLogic>();
    builder.Services.AddTransient<IHddCache, HddCache>();

    var app = builder.Build();

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

    app.MapControllerRoute(
        name: "images",
        pattern: "images/{id}",
        defaults: new { controller = "Categories", action = "GetImageById", });

app.Run();