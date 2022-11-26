using AxlBookCatalog.Domain.Constants;
using AxlBookCatalog.Domain.Users;
using Microsoft.AspNetCore.Identity;

namespace AxlBookCatalog.DataAccess
{
    public class ApplicationDbContextSeed
    {
        public static async Task SeedEssentialsAsync(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            await roleManager.CreateAsync(new IdentityRole(Authorization.Roles.Administrator.ToString()));
            await roleManager.CreateAsync(new IdentityRole(Authorization.Roles.User.ToString()));

            var defaultUser = new ApplicationUser { UserName = Authorization.DefaultUsername, Email = Authorization.DefaultEmail, EmailConfirmed = true, PhoneNumberConfirmed = true };
            if (userManager.Users.All(u => u.Id != defaultUser.Id))
            {
                await userManager.CreateAsync(defaultUser, Authorization.DefaultPassword);
                await userManager.AddToRoleAsync(defaultUser, Authorization.DefaultRole.ToString());
            }
        }
    }
}
