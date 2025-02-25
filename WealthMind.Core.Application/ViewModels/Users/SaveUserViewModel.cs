using System.ComponentModel.DataAnnotations;

namespace WealthMind.Core.Application.ViewModels.User
{
    public class SaveUserViewModel
    {
        public string? Id { get; set; }

        [Required(ErrorMessage = "You must enter a name.")]
        [DataType(DataType.Text)]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "You must enter a lastname.")]
        [DataType(DataType.Text)]
        public string LastName { get; set; }

        [Required(ErrorMessage = "You mus enter a Username.")]
        [DataType(DataType.Text)]
        public string UserName { get; set; }

        [Required(ErrorMessage = "You must enter an email.")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [DataType(DataType.Password)]
        public string? Password { get; set; }

        [DataType(DataType.Text)]
        public string? ProfilePicture { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "User must have a role.")]
        public int Role { get; set; }

        public bool HasError { get; set; }
        public string? Error { get; set; }
    }
}
