using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Options;
//using SendGrid;
//using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;
namespace Autofocus.Utility
{
    public class EmailSender : IEmailSender
    {
        private readonly IWebHostEnvironment _hostingEnvironment;
        public EmailSender(IWebHostEnvironment hostingEnvironment)
        {
           
            _hostingEnvironment = hostingEnvironment;
        }

//        wikoindia @onlineerp.org
//Llyl66$2
        public async Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            StringBuilder strBul = new StringBuilder("<div>");
            // strBul = strBul.Append("<div>You can use the following link to reset your password: " + htmlMessage + ",</div>");






            string fromemail = "wikoindia@onlineerp.org";
            string password = "Llyl66$2";

            bool send = false;
            MailMessage mail = new MailMessage();
            mail.To.Add(email);

            mail.From = new MailAddress(fromemail, subject);
            mail.Subject = subject;

          
            //----------------------------
            mail.Body = htmlMessage.ToString();
            mail.IsBodyHtml = true;
            SmtpClient smtp = new SmtpClient();
            smtp.Host = "103.250.184.62";
            smtp.Port = 25;
            smtp.UseDefaultCredentials = false;
            smtp.Credentials = new System.Net.NetworkCredential(fromemail, password);
            try
            {
                smtp.Send(mail);
                send = true;
            }
            catch (Exception ex)
            {
                send = false;
                //  ErrHandler.writeError(ex.Message, ex.StackTrace);
            }


            //  throw new NotImplementedException();
        }


    }
}
    