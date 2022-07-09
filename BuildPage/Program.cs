using BuildPage.Interfaces;
using BuildPage.Models;
using BuildPage.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddHttpContextAccessor();

builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(15);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});
builder.Services.AddMemoryCache();

builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<DbContextModel>(options =>
{
    options.UseMySQL(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddTransient<IUser, UserMock>();
builder.Services.AddTransient<IImages, ImageMock>();
builder.Services.AddTransient<ICalendar, CalendarMock>();
builder.Services.AddRazorPages();
//builder.Services.AddDistributedMemoryCache();
//builder.Services.AddSession();
var app = builder.Build();

// Configure the HTTP request pipeline.
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
app.UseSession();
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}"
        );
});

//app.MapControllerRoute(
//    name: "default",
//    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
