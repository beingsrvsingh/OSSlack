namespace Shared.Domain.Mail
{
    public class Email
    {
        public string From { get; set; } = null!;
        public string To { get; set; } = null!;
        public string Cc { get; set; } = null!;
        public string Bcc { get; set; } = null!;
        public string Subject { get; set; } = null!;
        public string Body { get; set; } = null!;
        public List<MailAttachment> Attachments { get; set; } = new();
    }
}