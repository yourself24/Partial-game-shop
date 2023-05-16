using Proj.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Proj.BLL.Services
{
    public class EmailAccountCreationObserver : IAccountCreationObserver
    {
        private readonly string _smtpServer;
        private readonly int _smtpPort;
        private readonly string _smtpUsername;
        private readonly string _smtpPassword;
        private readonly string _fromEmail;
        private readonly string _toEmail;

        public EmailAccountCreationObserver(string smtpServer, int smtpPort, string smtpUsername, string smtpPassword, string fromEmail, string toEmail)
        {
            _smtpServer = smtpServer;
            _smtpPort = smtpPort;
            _smtpUsername = smtpUsername;
            _smtpPassword = smtpPassword;
            _fromEmail = fromEmail;
            _toEmail = toEmail;
        }

        public void HandleAccountCreation(User user)
        {
            // Configure SMTP client
            SmtpClient client = new SmtpClient(_smtpServer, _smtpPort);
            client.UseDefaultCredentials = false;
            client.Credentials = new NetworkCredential(_smtpUsername, _smtpPassword);
            client.EnableSsl = true;

            // Compose email message
            MailMessage message = new MailMessage();
            message.From = new MailAddress(_fromEmail);
            message.To.Add(new MailAddress(_toEmail));
            message.Subject = "New User Account Created";
            message.Body = $"User account created: {user.Username}, {DateTime.Now}";

            // Send email message
            client.Send(message);
        }
    }

}
