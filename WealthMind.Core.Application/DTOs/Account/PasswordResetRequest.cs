using System.ComponentModel.DataAnnotations;

namespace WealthMind.Core.Application.DTOs.Account
{
    public class PasswordResetRequest
    {
        [Required(ErrorMessage = "El correo electrónico es requerido")]
        [EmailAddress(ErrorMessage = "El formato del correo electrónico no es válido")]
        public string Email { get; set; }
    }
} 