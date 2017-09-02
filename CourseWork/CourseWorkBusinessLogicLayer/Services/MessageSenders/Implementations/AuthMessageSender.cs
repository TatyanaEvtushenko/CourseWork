using System;
using System.Threading.Tasks;
using CourseWork.Services;
using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace CourseWork.BusinessLogicLayer.Services
{
    public class AuthMessageSender : IEmailSender, ISmsSender
    {
        private readonly IConfiguration _configuration;

        public AuthMessageSender(IConfiguration configuration)
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

        public Task SendSmsAsync(string number, string message)
        {
            return Task.FromResult(0);
        }
    }
}
