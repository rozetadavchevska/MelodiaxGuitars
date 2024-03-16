using Microsoft.AspNetCore.Identity;

namespace MelodiaxGuitarsAPI.Data
{
    public class SeedRoles
    {
        public async Task SeedTheRoles(RoleManager<IdentityRole> roleManager)
        {
            if(!await roleManager.RoleExistsAsync(Roles.Admin))
            {
                await roleManager.CreateAsync(new IdentityRole(Roles.Admin));
            }

            if(!await roleManager.RoleExistsAsync(Roles.User))
            {
                await roleManager.CreateAsync(new IdentityRole(Roles.User));
            }
        }
    }
}
