using Domain.Models.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presistence
{
    public static class IdentityDataSeeder
    {
        public static async Task SeedPermanentAdminOnceAsync(IServiceProvider serviceProvider)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<AppRole>>();
            var userManager = serviceProvider.GetRequiredService<UserManager<AppUser>>();

            
            var existingAdmins = await userManager.GetUsersInRoleAsync("Admin");

            if (existingAdmins.Any())
            {
                return;
            }

            string[] systemRoles = { "Admin", "Librarian", "Staff", "Member" };
            foreach (var roleName in systemRoles)
            {
                if (!await roleManager.RoleExistsAsync(roleName))
                {
                    await roleManager.CreateAsync(new AppRole { Name = roleName });
                }
            }

            var adminEmail = "admin@library.com";

            var permanentAdmin = new AppUser
            {
                UserName = adminEmail,
                Email = adminEmail,
                FirstName = "Primary",
                LastName = "Administrator",
                EmailConfirmed = true,
                IsActive = true
            };

            var result = await userManager.CreateAsync(permanentAdmin, "PermanentAdminSecret2026!");

            if (result.Succeeded)
            {
                await userManager.AddToRoleAsync(permanentAdmin, "Admin");
            }
        }
    }
}

