using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using E_Commerce.Application.Common;

namespace E_Commerce.Application.Authentication
{
    public class EmailSender : IEmailSender
    {

        public async Task SendEmailAsync(string email, string subject, string message)
        {
            #region information
            var mail = "mohamed141462013@gmail.com"; // Sender's email address
            var pw = "bxnm nqqk kwgt uspq"; // Sender's email password
            var host = "smtp.gmail.com"; // SMTP server host
            var port = 587; // SMTP server port (e.g., 587 for TLS/STARTTLS)
            #endregion

            // Create the email message
            var mailMessage = new MailMessage(mail, email, subject, message);

            // Configure the SMTP client
            var smtpClient = new SmtpClient(host)
            {
                Port = port,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(mail, pw),
                EnableSsl = true, // Set to true if the SMTP server requires SSL/TLS
            };

            // Send the email asynchronously
            try
            {
                await smtpClient.SendMailAsync(mailMessage);
                Console.WriteLine("Email sent successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to send email: {ex.Message}");
                throw;
            }
            finally
            {
                // Dispose of the resources
                mailMessage.Dispose();
                smtpClient.Dispose();
            }
        }
    }
}
