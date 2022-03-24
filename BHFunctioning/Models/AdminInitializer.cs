using Microsoft.AspNetCore.Identity;

namespace BHFunctioning.Models
{
    public class AdminInitializer
    {

        public static void SeedData
        (UserManager<IdentityUser> userManager,
        RoleManager<IdentityRole> roleManager)
        {
            if (!roleManager.RoleExistsAsync("Administrator").Result)
            {
                IdentityRole r = new();
                r.Name = "Administrator";
                IdentityResult roleResult = roleManager.
                CreateAsync(r).Result;
            }
            if (userManager.FindByNameAsync("admin@admin.com").Result == null)
            {
                IdentityUser user = new IdentityUser();
                user.UserName = "admin@admin.com";
                user.Email = "admin@admin.com";

                IdentityResult result = userManager.CreateAsync(user, "Abc12#").Result;

                if (result.Succeeded)
                {
                    userManager.AddToRoleAsync(user, "Administrator").Wait();
                }
            }

        }
    }
}
