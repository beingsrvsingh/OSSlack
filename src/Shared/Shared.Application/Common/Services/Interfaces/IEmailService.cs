using Shared.Domain.Mail;

namespace Identity.Application.Common.Services.Interfaces
{
    public interface IEmailService
    {
        void SendEmail(Email email);
    }
}