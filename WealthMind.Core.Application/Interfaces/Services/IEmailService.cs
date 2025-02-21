using WealthMind.Core.Application.DTOs.Email;
using WealthMind.Core.Domain.Settings;

namespace WealthMind.Core.Application.Interfaces.Services
{
    public interface IEmailService
    {
        public MailSettings MailSettings { get; }
        Task SendAsync(EmailRequest request);
    }
}
