using Mentoring.Models;
using Mentoring.BL;
using Microsoft.EntityFrameworkCore;
    var builder = WebApplication.CreateBuilder(args);
    builder.Logging.AddLog4Net();
    builder.Services.AddDbContext<NorthwindContext>(options => options.UseSqlServer(
        builder.Configuration.GetSection("ConnectionStrings").Value));
    builder.Services.AddControllersWithViews();
builder.Services.AddTransient<IBusinessLogic, BusinessLogic>();

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

    app.Run();