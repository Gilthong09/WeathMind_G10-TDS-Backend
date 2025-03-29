using Microsoft.AspNetCore.Identity;
using WealthMind.Core.Application.Enums;
using WealthMind.Infrastructure.Identity.Entities;

namespace WealthMind.Infrastructure.Identity.Seeds
{
    public class OthersDefaultUsers
    {
        public static async Task SeedAsync(UserManager<ApplicationUser> userManager)
        {

            List<ApplicationUser> defaultUsers = new()
            {
                new() { Id = "1a234b56-789c-4d12-e3f4-5678ghijk901", UserName = "user1", Email = "user1@email.com", FirstName = "John", LastName = "Smith", EmailConfirmed = true, PhoneNumberConfirmed = true, ProfilePicture = string.Empty },
                new() { Id = "2b345c67-890d-4e23-f5g6-6789hijk012", UserName = "user2", Email = "user2@email.com", FirstName = "Emma", LastName = "Brown", EmailConfirmed = true, PhoneNumberConfirmed = true, ProfilePicture = string.Empty },
                new() { Id = "3c456d78-901e-4f34-g5h6-7890ijkl123", UserName = "user3", Email = "user3@email.com", FirstName = "Michael", LastName = "Johnson", EmailConfirmed = true, PhoneNumberConfirmed = true, ProfilePicture = string.Empty },
                new() { Id = "4d567e89-012f-4g45-h6i7-8901jklm234", UserName = "user4", Email = "user4@email.com", FirstName = "Sophia", LastName = "Williams", EmailConfirmed = true, PhoneNumberConfirmed = true, ProfilePicture = string.Empty },
                new() { Id = "5e678f90-123g-4h56-i7j8-9012klmn345", UserName = "user5", Email = "user5@email.com", FirstName = "Daniel", LastName = "Davis", EmailConfirmed = true, PhoneNumberConfirmed = true, ProfilePicture = string.Empty }
            };

            foreach (var defaultUser in defaultUsers)
            {
                if (userManager.Users.All(u => u.Id != defaultUser.Id))
                {
                    var user = await userManager.FindByEmailAsync(defaultUser.Email);

                    if (user == null)
                    {
                        await userManager.CreateAsync(defaultUser, "!Pa$$word01");
                        await userManager.AddToRoleAsync(defaultUser, Roles.User.ToString());
                    }
                }
            }
        }
    }
}
