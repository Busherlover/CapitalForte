using Finances.Models;

namespace Finances.Services.EmailService
{
    public interface IEmailService
    {
        void SendEmail(EmailDto request);
        void SendEmail(string to, string subject, string body);
    }
}
