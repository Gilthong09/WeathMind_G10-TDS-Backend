using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace WealthMind.Infrastructure.Identity.Entities
{
    public class ApplicationUser: IdentityUser
    {
        [Key]
        public int UserId { get; set; }

        [Required]
        public string Name { get; set; }

        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        public byte[] ProfilePicture { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        //public string Role { get; set; } = "user";
    }
}
