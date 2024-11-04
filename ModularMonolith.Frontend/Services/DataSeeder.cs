using CustomerManagement.Domain.Identity;
using Microsoft.AspNetCore.Identity; 

namespace ModularMonolith.Frontend.Services
{
    public class DataSeeder
    {
        public static async Task SeedAsync(IServiceProvider serviceProvider)
        {
            var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
             
            string roleName = "Admin";
            if (!await roleManager.RoleExistsAsync(roleName))
            {
                await roleManager.CreateAsync(new IdentityRole(roleName));
            }
             
            string defaultEmail = "admin@example.com";
            string defaultPassword = "Admin@123";

            if (await userManager.FindByEmailAsync(defaultEmail) == null)
            {
                var defaultUser = new ApplicationUser { UserName = defaultEmail, Email = defaultEmail };
                var result = await userManager.CreateAsync(defaultUser, defaultPassword);

                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(defaultUser, roleName);
                }
            }
        }
    }
}
