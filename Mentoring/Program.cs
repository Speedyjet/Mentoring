using Mentoring.Models;
using Mentoring.BL;
using Microsoft.EntityFrameworkCore;
using Mentoring.Controllers;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.Mvc.TagHelpers;

var builder = WebApplication.CreateBuilder(args);
    builder.Logging.AddLog4Net();
    builder.Services.AddDbContext<NorthwindContext>(options => options.UseSqlServer(
    builder.Configuration.GetSection("ConnectionStrings").Value));
    builder.Services.AddSwaggerGen();
    builder.Services.AddControllersWithViews().AddRazorOptions(option =>
    {
        option.ViewLocationFormats.Add("/{0}.cshtml");
    });
    builder.Services.AddTransient<ICategoryService, CategoryService>();
    builder.Services.AddTransient<IProductService, ProductService>();
    builder.Services.AddTransient<IHddCache, HddCache>();

var app = builder.Build();

    if (!app.Environment.IsDevelopment())
    {
        app.UseExceptionHandler("/Home/Error");
    }
    else
    {
    app.UseSwagger();
    app.UseSwaggerUI();
    }
    app.UseStaticFiles();

    app.UseRouting();

    app.UseAuthorization();

    app.MapControllerRoute(
        name: "images",
        pattern: "images/{id}",
        defaults: new { controller = "Categories", action = "GetImageById", });

    app.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();