using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System;

namespace SkyRim.Data
{
    public class DbInitializer
    {
        public static async Task Initialize(
            ApplicationDbContext context,
            UserManager<IdentityUser> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            context.Database.Migrate(); // Ensures migrations are applied

            // 1. Create Roles if they don't exist
            string[] roleNames = { "Admin", "Tutor", "Student" };
            foreach (var roleName in roleNames)
            {
                if (!await roleManager.RoleExistsAsync(roleName))
                {
                    await roleManager.CreateAsync(new IdentityRole(roleName));
                }
            }

            // 2. Create a default Admin user if one doesn't exist
            string adminEmail = "admin@example.com"; // Choose a strong email
            string adminPassword = "AdminPassword123!"; // Choose a strong password for initial setup

            if (await userManager.FindByEmailAsync(adminEmail) == null)
            {
                var adminUser = new IdentityUser
                {
                    UserName = adminEmail,
                    Email = adminEmail,
                    EmailConfirmed = true // Confirm email for easier testing
                };

                var result = await userManager.CreateAsync(adminUser, adminPassword);
                if (result.Succeeded)
                {
                    // Assign Admin role to the newly created user
                    await userManager.AddToRoleAsync(adminUser, "Admin");
                    // Optionally, add basic UserDetails for the admin
                    var adminUserDetails = new Models.UserDetails
                    {
                        Id = adminUser.Id,
                        FirstName = "Super",
                        LastName = "Admin",
                        Gender = "Male",
                        Nationality = "Nigeria",
                        Race = "Black or African American",
                        LinkedInUrl = null,
                        GitHubUrl = null
                    };
                    context.UserDetails.Add(adminUserDetails);
                    await context.SaveChangesAsync();
                }
            }

            //string tutorEmail = "tutor@example.com";
            //string tutorPassword = "TutorPassword123!";
            //if (await userManager.FindByEmailAsync(tutorEmail) == null)
            //{
            //    var tutorUser = new IdentityUser { UserName = tutorEmail, Email = tutorEmail, EmailConfirmed = true };
            //    var result = await userManager.CreateAsync(tutorUser, tutorPassword);
            //    if (result.Succeeded)
            //    {
            //        await userManager.AddToRoleAsync(tutorUser, "Tutor");
            //    }
            //}

        }
    }
}
