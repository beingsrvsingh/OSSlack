using Shared.Domain.Mail;

namespace Shared.Application.Interfaces.Communication
{
    public interface IEmailService
    {
        void SendEmail(Email email);
    }
}