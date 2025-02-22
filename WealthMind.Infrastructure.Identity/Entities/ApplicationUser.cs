using Microsoft.AspNetCore.Identity;

namespace WealthMind.Infrastructure.Identity.Entities
{
    public class ApplicationUser: IdentityUser 
    {
<<<<<<< HEAD
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string ProfilePicture { get; set; }
=======
        [Key]
        public int UserId { get; set; }

        [Required]
        public string Name { get; set; }

        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        public byte[] ProfilePicture { get; set; }
>>>>>>> main

    }
}
