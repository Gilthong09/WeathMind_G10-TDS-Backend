using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel.DataAnnotations;

namespace WealthMind.Core.Application.DTOs.Account
{
    /// <summary>
    /// Parameters for developer and admin registration
    /// </summary> 
    public class RegisterDTO
    {
        [SwaggerParameter(Description = "First name of the user")]
        public string FirstName { get; set; }


        [SwaggerParameter(Description = "Last name of the user")]
        public string LastName { get; set; }


        [SwaggerParameter(Description = "The email of the user")]
        public string Email { get; set; }


        [SwaggerParameter(Description = "The username of the user")]
        public string UserName { get; set; }

        [SwaggerParameter(Description = "Profile picture of the user")]
        public string ProfilePicture { get; set; }


        [SwaggerParameter(Description = "The password of the user")]
        public string Password { get; set; }


        [Compare(nameof(Password), ErrorMessage = "The password must match")]
        [SwaggerParameter(Description = "The confirmation of the users password")]
        public string ConfirmPassword { get; set; }


    }
}
