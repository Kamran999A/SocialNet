using System.Net;
using System;
using System.Net.Mail;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows;


namespace Network
{
    static class Mail
    {
        private static SmtpClient SmtpClient { get; set; }
        private static string SenderAddress { get; } = "johnjohnlu00@gmail.com";
        private static string SenderPassword{ get; } = "john.1234";
        static Mail()
        {
            SmtpClient = new SmtpClient("smtp.gmail.com")
            {
                Port = 587,
                Credentials = new NetworkCredential(SenderAddress, SenderPassword),
                EnableSsl = true
            };
        }

        public static void SendMail(in string recipient, in string subject, in string body)
        {
            string html = $@"<h1 style='background-color:#00bfff;padding:25px;border-radius:15px;border:5px solid red'>" +body+ "</h1>";
            using (MailMessage mail = new MailMessage())
            {
                mail.From = new MailAddress(SenderAddress);
                mail.To.Add(recipient);
                mail.Subject = subject;
                mail.IsBodyHtml = true;
                mail.Body = html;

            SmtpClient.Send(mail);
            }

               
        }
    }

}
