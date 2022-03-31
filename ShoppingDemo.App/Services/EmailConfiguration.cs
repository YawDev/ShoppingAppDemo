using Microsoft.Extensions.Configuration;

namespace ShoppingDemo.App.Services
{
    public class EmailConfiguration
    {
        public string From { get; set; }
        public string SmtpServer { get; set; }
        public int Port { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }

        IConfiguration _configuration;

        public EmailConfiguration(IConfiguration configuration)
        {
            _configuration = configuration;
            From = _configuration["EmailConfiguration:From"];
            SmtpServer = _configuration["EmailConfiguration:SmtpServer"];
            Port = _configuration.GetValue<int>("EmailConfiguration:Port");
            UserName = _configuration["EmailConfiguration:Username"];
            Password = _configuration["EmailConfiguration:Password"];
        }


    }
}