using MailKit.Net.Smtp;
using Microsoft.Extensions.Configuration;
using MimeKit;

namespace ShoppingDemo.App.Services
{
    public interface IEmailService
    {
        MimeMessage CreateEmailMessage(EmailTemplate message);
        void SendMessage(MimeMessage message);
    }


    public class EmailService : IEmailService
    {
        EmailConfiguration _emailConfiguration;

        public EmailService(IConfiguration configuration)
        {
            _emailConfiguration = new EmailConfiguration(configuration);
        }

        public MimeMessage CreateEmailMessage(EmailTemplate message)
        {
            var emailMessage = new MimeMessage();
            emailMessage.From.Add(new MailboxAddress(_emailConfiguration.From));
            emailMessage.To.AddRange(message.To);
            emailMessage.Subject = message.Subject;
            emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Text) { Text = message.Content };
            return emailMessage;
        }

        public void SendMessage(MimeMessage message)
        {
            using (var client = new SmtpClient())
            {
                try
                {
                    client.Connect(_emailConfiguration.SmtpServer, _emailConfiguration.Port, true);
                    client.AuthenticationMechanisms.Remove("XOAUTH2");
                    client.Authenticate(_emailConfiguration.UserName, _emailConfiguration.Password);
                    client.Send(message);
            }
            catch
            {
                //log an error message or throw an exception or both.
                throw;
            }
            finally
            {
                client.Disconnect(true);
                client.Dispose();
            }
        }
    }
    }
}