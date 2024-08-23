using AutoMapper;
using Azure.Core;
using Business.Interface;
using Microsoft.AspNetCore.Identity.UI.Services;
using MRC_API.Service.Interface;
using Repository.Entity;
using System.Net;
using System.Net.Mail;

namespace MRC_API.Service.Implement
{
    public class EmailSender : Interface.IEmailSender
    {

        public Task SendEmailAsync(string email, string subject, string message)
        {
            var mail = "mrc.web@outlook.com";
            var pwd = "mrcweb12345";
            var client = new SmtpClient("smtp-mail.outlook.com", 587)
            {
                EnableSsl = true,
                Credentials = new NetworkCredential(mail, pwd)
            };
            return client.SendMailAsync(
                new MailMessage(from: mail,
                                  to: email,
                                  subject,
                                  message)
                );
        }     
    }
    
}
