using System;
using System.Threading.Tasks;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Configuration;
using MimeKit;

namespace CourseWork.BusinessLogicLayer.Services.MessageSenders.Implementations
{
    public class AuthMessageSender : IEmailSender
    {
        private readonly IConfigurationRoot _configuration;

        public AuthMessageSender(IConfigurationRoot configuration)
        {
            _configuration = configuration;
        }

        public Task SendEmailAsync(string email, string subject, string message)
        {
            SendMessage(CreateMessage(email, subject, message));
            return Task.FromResult(0);
        }

        private MimeMessage CreateMessage(string email, string subject, string message)
        {
            var emailMessage = new MimeMessage();
            emailMessage.From.Add(new MailboxAddress("Email confirmation", _configuration["Mail:Email"]));
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
                client.Connect(_configuration["Mail:Server"], Int32.Parse(_configuration["Mail:Port"]), SecureSocketOptions.SslOnConnect);
                client.Authenticate(_configuration["Mail:Email"], _configuration["Mail:Password"]);
                client.Send(emailMessage);
                client.Disconnect(true);
            }
        }
    }
}
