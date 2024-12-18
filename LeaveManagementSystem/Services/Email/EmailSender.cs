﻿using Microsoft.AspNetCore.Identity.UI.Services;
using System.Net.Mail;

namespace LeaveManagementSystem.Services.Email
{
    public class EmailSender(IConfiguration _configuration) : IEmailSender
    {
        public async Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            string? fromAddress = _configuration["EmailSettings:DefaultEmailAddress"];
            string? smtpServer = _configuration["EmailSettings:Server"];
            Int32 smtpPort = Convert.ToInt32(_configuration["EmailSettings:Port"]);

            var message = new MailMessage
            {
                From = new MailAddress(fromAddress),
                Subject = subject,
                Body = htmlMessage,
                IsBodyHtml = true

            };

            message.To.Add(new MailAddress(email));

            using var client = new SmtpClient(smtpServer, smtpPort);
            await client.SendMailAsync(message);
        }
    }
}
