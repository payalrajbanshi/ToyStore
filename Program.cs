// using Microsoft.EntityFrameworkCore;
// using ToyStore.Data;
// using ToyStore.Services;


// var builder = WebApplication.CreateBuilder(args);
// builder.Services.AddControllersWithViews();
// // 🔗 Replace with your own connection string
// var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
// builder.Services.AddDbContext<AppDbContext>(options =>
//     options.UseMySql(connectionString, new MySqlServerVersion(new Version(8, 0, 32))));
//     builder.Services.AddHttpContextAccessor();
// builder.Services.AddSession();             
// builder.Services.AddScoped<CartService>();
// var app = builder.Build();
// app.UseStaticFiles();
// app.UseRouting();
// app.UseSession();
// //app.MapControllerRoute(name: "default", pattern: "{controller=Product}/{action=Index}/{id?}");
// app.MapControllerRoute(
//     name: "default",
//     pattern: "{controller=Product}/{action=Index}/{id?}");

// using (var scope = app.Services.CreateScope())
// {
//     var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
//     SeedData.Initialize(context);
// }

// app.Run();
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ToyStore.Data;
using ToyStore.Models;
using ToyStore.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

// Connection string from appsettings.json
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// Add DbContext with MySQL
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseMySql(connectionString, new MySqlServerVersion(new Version(8, 0, 32))));

// Add Identity services
builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
{
    // You can customize password options here if needed
    options.Password.RequireDigit = false;
    options.Password.RequiredLength = 6;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
    options.Password.RequireLowercase = false;
})
    .AddEntityFrameworkStores<AppDbContext>()
    .AddDefaultTokenProviders();

builder.Services.AddHttpContextAccessor();
builder.Services.AddSession();
builder.Services.AddScoped<CartService>();

var app = builder.Build();

// Middleware pipeline
app.UseStaticFiles();

app.UseRouting();

app.UseSession();

app.UseAuthentication();  // <-- Add Authentication middleware
app.UseAuthorization();   // <-- Add Authorization middleware

// Seed roles and admin user asynchronously
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<AppDbContext>();
    SeedData.Initialize(context);
    await SeedData.InitializeAsync(services);  // Make sure SeedData has async method

}
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Account}/{action=Register}/{id?}");
    app.MapGet("/", context =>
{
    context.Response.Redirect("/Account/Register");
    return Task.CompletedTask;
});



app.Run();
