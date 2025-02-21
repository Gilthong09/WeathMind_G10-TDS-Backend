using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using WealthMind.Infrastructure.Identity.Entities;
using WealthMind.Infrastructure.Identity.Seeds;

namespace WealthMind.Infrastructure.Identity
{
    public static class ServiceApplication
    {
        public static async Task AddIdentitySeeds(this IServiceProvider services)
        {
            #region "Identity Seeds"
            using (var scope = services.CreateScope())
            {
                var serviceScope = scope.ServiceProvider;

                try
                {
                    var userManager = serviceScope.GetRequiredService<UserManager<ApplicationUser>>();
                    var roleManager = serviceScope.GetRequiredService<RoleManager<IdentityRole>>();

                    await DefaultRoles.SeedAsync(roleManager);
                    await DefaulDeveloperUser.SeedAsync(userManager);
                    await DefaulAdminUser.SeedAsync(userManager);

                }
                catch (Exception ex)
                {
                    Console.BackgroundColor = ConsoleColor.Red;
                    Console.WriteLine(ex.Message.ToString());
                    Console.ResetColor();

                }
            }
            #endregion
        }
    }
}
