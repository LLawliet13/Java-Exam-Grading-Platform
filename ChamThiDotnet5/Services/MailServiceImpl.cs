using System;
using System.Net.Mail;

namespace ChamThiDotnet5.Services
{
    public class MailServiceImpl 
    {
        public void SendMail(string _from, string _to, string _subject, string _body)
        {
            MailMessage message = new MailMessage();
            message.BodyEncoding = System.Text.Encoding.UTF8;
            message.SubjectEncoding = System.Text.Encoding.UTF8;
            message.IsBodyHtml = true;

            // them dia chi reply cho mail
            message.ReplyToList.Add(new MailAddress(_from));
            message.Sender = new MailAddress(_from);

            using var smtpClient = new SmtpClient("localhost");
            try
            {
                smtpClient.Send(message);
            }catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }


        }
    }
}
