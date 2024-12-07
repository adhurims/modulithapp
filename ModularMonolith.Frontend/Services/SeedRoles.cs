using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace ModularMonolith.Frontend.Services
{
    public class SeedRoles
    {
        private readonly RoleManager<IdentityRole> _roleManager;

        public SeedRoles(RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
        }

        public async Task SeedAsync()
        {
            var roles = new[] { "Admin", "User", "Business" };
            foreach (var role in roles)
            {
                if (!await _roleManager.RoleExistsAsync(role))
                {
                    await _roleManager.CreateAsync(new IdentityRole(role));
                }
            }
        }
    }
}
