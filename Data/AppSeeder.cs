using BookNest.Entities;
using First.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;

namespace BookNest.Data
{
    public class AppSeeder
    {
        public static void Seed(IServiceProvider serviceProvider)
        {
            using (var scope = serviceProvider.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                var userManager = scope.ServiceProvider.GetRequiredService<UserManager<User>>();
                var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

                // Create roles if they don't exist
                CreateRoles(roleManager);

                // Create the Admin user
                var adminUser = new User
                {
                    Name = "Admin User",
                    Email = "admin@booknest.com",
                    PasswordHash = "adminpassword123", // This will be overridden by CreateAsync
                    Role = "Admin",
                    MembershipID = Guid.NewGuid().ToString(),
                    RegistrationDate = DateTime.Now,
                    OrderCount = 0,
                    IsActive = true
                };

                var result = userManager.CreateAsync(adminUser, "adminpassword123").Result;

                if (result.Succeeded)
                {
                    userManager.AddToRoleAsync(adminUser, "Admin").Wait();
                }
            }
        }

        private static void CreateRoles(RoleManager<IdentityRole> roleManager)
        {
            // Create Admin role
            if (!roleManager.RoleExistsAsync("Admin").Result)
            {
                var role = new IdentityRole("Admin");
                roleManager.CreateAsync(role).Wait();
            }

            // Create User role
            if (!roleManager.RoleExistsAsync("User").Result)
            {
                var role = new IdentityRole("User");
                roleManager.CreateAsync(role).Wait();
            }

            // Create Staff role
            if (!roleManager.RoleExistsAsync("Staff").Result)
            {
                var role = new IdentityRole("Staff");
                roleManager.CreateAsync(role).Wait();
            }
        }
    }
}
