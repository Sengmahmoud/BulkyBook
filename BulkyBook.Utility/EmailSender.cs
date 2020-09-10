﻿using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Options;
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BulkyBook.Utility
{
    public class EmailSender : IEmailSender
    {
        private readonly EmailOptions emailOptions;

        public EmailSender(IOptions<EmailOptions> options)
        {
            this.emailOptions = options.Value;
        }
        public Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            return Execute(emailOptions.sendGridKey, subject, htmlMessage, email);
        }

        private  Task Execute(string sendGridKey, string email, string subject, string message)
        {
          
            var client = new SendGridClient(sendGridKey);
            var from = new EmailAddress("m.abdelaal@wesamsoft.com", "Mahmoud Hassan");
            var to = new EmailAddress(email, "End User");
                       var msg = MailHelper.CreateSingleEmail(from, to, subject, message,"");
           return client.SendEmailAsync(msg);
        }
    }
}