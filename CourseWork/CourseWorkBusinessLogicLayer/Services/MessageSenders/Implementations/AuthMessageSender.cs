using System.Threading.Tasks;
using CourseWork.BusinessLogicLayer.Options;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;

namespace CourseWork.BusinessLogicLayer.Services.MessageSenders.Implementations
{
    public class AuthMessageSender : IEmailSender
    {
        private readonly MailOptions _options;

        public AuthMessageSender(IOptions<MailOptions> options)
        {
            _options = options.Value;
        }

        public Task SendEmailAsync(string email, string subject, string message)
        {
            return Task.Factory.StartNew(() => SendMessage(CreateMessage(email, subject, message)));
        }

        private MimeMessage CreateMessage(string email, string subject, string message)
        {
            var emailMessage = new MimeMessage();
            emailMessage.From.Add(new MailboxAddress("Course work", _options.Email));
            emailMessage.To.Add(new MailboxAddress("", email));
            emailMessage.Subject = subject;
            emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html)
            {
                Text = message
            };
            return emailMessage;
        }

        private void SendMessage(MimeMessage emailMessage)
        {
            using (var client = new SmtpClient())
            {
                client.ServerCertificateValidationCallback = (s, c, h, e) => true;
                client.Connect(_options.Server, _options.Port, SecureSocketOptions.SslOnConnect);
                client.Authenticate(_options.Email, _options.Password);
                client.Send(emailMessage);
                client.Disconnect(true);
            }
        }
    }
}