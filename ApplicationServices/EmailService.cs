using System.Net.Mail;
using System.Net;
using WoofpakGamingSiteServerApp.Data;
using WoofpakGamingSiteServerApp.Data.Services;
using System.Threading.Tasks;
using Microsoft.AspNetCore.WebUtilities;
using System.Text;
using System.Security.Policy;
using System;
using Microsoft.Extensions.Configuration;

namespace WoofpakGamingSiteServerApp.ApplicationServices
{
    public static class EmailService
    {
        static MailAddress fromAddress;
        static string mailPassword;
        private static ApplicationUserManager _userManager;
        static SmtpClient smtp;
        static EmailService()
        {
            IConfigurationRoot Configuration = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile("appsettings.json")
                .Build();

            EmailAccount mailProperties = Configuration.GetSection("FromMailAddress").Get<EmailAccount>();

            fromAddress = new MailAddress(mailProperties.EmailAddress, mailProperties.DisplayName);

            string mailPassword = mailProperties.Password;

             smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                Timeout = 20000
            };

            smtp.UseDefaultCredentials= false;
            smtp.EnableSsl = true;
            smtp.Credentials = new NetworkCredential(fromAddress.Address, mailPassword);

        }

        public static Task SendConfirmationEmailAsync(string toEmailAddress, string htmlMessage)
        {
            MailAddress toAddress = new MailAddress(toEmailAddress);

            string usrToken = string.Empty;  
            string subject = "Confirm your Woofpak Gaming Account";
            string body = htmlMessage;

            using (var message = new MailMessage(fromAddress, toAddress)
            {
                Subject = subject,
                Body = body
            })
            {
                smtp.SendMailAsync(message).Wait();
            }

            return Task.CompletedTask;

        }

        public static Task SendResetPasswordEmailAsync(string toEmailAddress, string htmlMessage)
        {
            MailAddress toAddress = new MailAddress(toEmailAddress);

            string usrToken = string.Empty;
            string subject = "Reset Your Woofpak Gaming Account Password";
            string body = htmlMessage;

            using (var message = new MailMessage(fromAddress, toAddress)
            {
                Subject = subject,
                Body = body
            })
            {
                smtp.SendMailAsync(message).Wait();
                //smtp.SendAsync(message, usrToken);
                //smtp.Send(message);
            }

            return Task.CompletedTask;


        }

    }

    internal class EmailAccount
    {
        public string EmailAddress { get; set; }
        public string Password { get; set; }
        public string DisplayName { get; set; }
    }
}




