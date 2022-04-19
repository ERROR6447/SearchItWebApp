
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace SearchIt.Utility
{
    public class EmailSender : IEmailSender
    {
        public string SendGridSecret { get; set; }
    
     
        public EmailSender(IConfiguration config)
        {
            
            SendGridSecret = config.GetValue<string>("SendGrid:SecretKey");
        }

        public async Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
          
            var client = new SendGridClient(SendGridSecret);
            var from = new EmailAddress("pokemonwh7@gmail.com", "Pokemon");
            var to = new EmailAddress(email);
            var msg = MailHelper.CreateSingleEmail(from, to, subject, "", htmlMessage);
            msg.SetClickTracking(false, false);

            var response = await client.SendEmailAsync(msg);
            if (response.StatusCode != HttpStatusCode.OK
                && response.StatusCode != HttpStatusCode.Accepted)
            {
                var errorMessage = response.Body.ReadAsStringAsync().Result;
                throw new Exception($"Failed to send mail to {to}, status code {response.StatusCode}, {errorMessage}");
            }


        }
    }
}