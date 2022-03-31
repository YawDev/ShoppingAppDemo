using System.Collections.Generic;
using System.Linq;
using MimeKit;

namespace ShoppingDemo.App.Services
{
    public class EmailTemplate
    {
        public List<MailboxAddress> To { get; set; }

        public string Content { get; set; }

        public string Subject { get; set; }

        public EmailTemplate(IEnumerable<string> to, string subject, string content)
        {
            To = new List<MailboxAddress>();

            To.AddRange(to.Select(x => new MailboxAddress(x)));

            Content = content;

            Subject = subject;
        }
    }
}