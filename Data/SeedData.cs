
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ToyStore.Models;

namespace ToyStore.Data
{
    public class SeedData
    {
        // Seed categories and products synchronously
        public static void Initialize(AppDbContext context)
        {
            if (!context.Categories.Any())
            {
                var cat1 = new Category { Name = "Stuffed Toys" };
                var cat2 = new Category { Name = "Vehicles" };

                context.Categories.AddRange(cat1, cat2);
                context.SaveChanges();

                context.Products.AddRange(
                    new Product { Name = "Teddy Bear", Price = 499, ImageUrl = "/images/teddy.png", Category = cat1 },
                    new Product { Name = "Toy Helicopter", Price = 349, ImageUrl = "/images/helicopter.webp", Category = cat2 },
                    new Product { Name = "Doll", Price = 299, ImageUrl = "/images/barbie.png", Category = cat1 }
                );
                context.SaveChanges();
            }

            if (!context.Products.Any(p => p.Name == "Bunny"))
            {
                var cat1 = context.Categories.FirstOrDefault(c => c.Name == "Stuffed Toys");
                var cat2 = context.Categories.FirstOrDefault(c => c.Name == "Vehicles");

                context.Products.AddRange(
                    new Product { Name = "Bunny", Price = 499, ImageUrl = "/images/bunny.png", Category = cat1 },
                    new Product { Name = "Lego Toy", Price = 299, ImageUrl = "/images/lego.png", Category = cat1 },
                    new Product { Name = "Remote Control Car", Price = 299, ImageUrl = "/images/remote controlcar.webp", Category = cat2 },
                    new Product { Name = "Monster Truck", Price = 699, ImageUrl = "/images/monster truck.png", Category = cat2 },
                    new Product { Name = "Elephant", Price = 399, ImageUrl = "/images/elephant.png", Category = cat1 },
                    new Product { Name = "Teddy with bow", Price = 599, ImageUrl = "/images/bow teddybear.png", Category = cat1 }
                );
                context.SaveChanges();
            }
        }

        // Async method to seed Identity roles and default admin user
        public static async Task InitializeAsync(IServiceProvider services)
        {
            var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
            var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();

    string[] roleNames = { "Admin", "User" };
    foreach (var roleName in roleNames)
    {
        if (!await roleManager.RoleExistsAsync(roleName))
            await roleManager.CreateAsync(new IdentityRole(roleName));
    }

            // Seed default admin user
            var adminEmail = "admin@toystore.com";
            var adminUser = await userManager.FindByEmailAsync(adminEmail);

            if (adminUser == null)
            {
                adminUser = new ApplicationUser
                {
                    UserName = adminEmail,
                    Email = adminEmail,
                    EmailConfirmed = true,
                    PhoneNumber = "9826961234",  // Add phone number
                    Address = "Admin Address",    // Add address (make sure this property exists)
                    FullName= "Admin User"
                };

                var result = await userManager.CreateAsync(adminUser, "Admin@1234"); // Strong password recommended

                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(adminUser, "Admin");
                }
                else
                {
                    throw new Exception("Failed to create admin user: " +
                        string.Join(", ", result.Errors.Select(e => e.Description)));
                }
            }
        }
    }
}
