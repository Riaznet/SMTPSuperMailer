
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;

namespace EmailBOT.Class.Model
{
    public class Emails
    {
        public string Host { get; set; }
        public int Port { get; set; }
        public string From { get; set; }
        public string DisplayName { get; set; }
        public bool EnableSSL { get; set; }
        public string To { get; set; }
        public string CC { get; set; }
        public string BCC { get; set; }
        public string Body { get; set; }
        public string Subject { get; set; }
        public string Password { get; set; }
        public string AttachFilePath { get; set; }
        public bool IsHtml { get; set; }
        public bool IsAttachment { get; set; }
        public string RandomAttachmentName { get; internal set; }
    }
  
    public class SenderProcess
    {
        public static bool SendMailPage(Emails email)
        {
            bool ret = false;
            try
            {
                ContentType mimeType = null;
                if (email.IsHtml)
                    mimeType = new System.Net.Mime.ContentType("text/html");
                else
                    mimeType = new System.Net.Mime.ContentType("text/plain");
                MailMessage message = new MailMessage();
                SmtpClient smtp = new SmtpClient();
                AlternateView alternate = AlternateView.CreateAlternateViewFromString(email.Body, mimeType); 
                message.AlternateViews.Add(alternate); 

                message.From = new MailAddress(email.From, email.DisplayName);
                message.To.Add(new MailAddress(email.To));
                if (email.BCC != "")
                    message.Bcc.Add(email.BCC);
                if (email.CC != "")
                    message.CC.Add(email.CC);
                message.Subject = email.Subject;
                message.IsBodyHtml = true; //to make message body as html    
                //if (filename != "")
                //    message.Attachments.Add(new Attachment(filename));
                if (email.IsAttachment)
                {
                    //ChangeFileName(attachment);
                    string[] filepathhs = Directory.GetFiles(email.AttachFilePath, "*");
                    foreach (var filepath in filepathhs)
                    {
                        var attachment_ = new Attachment(filepath);
                        message.Attachments.Add(attachment_); 
                    }
                }
                smtp.Port = email.Port;
                smtp.Host = email.Host;
                //message.Headers.Add();
                //smtp.Timeout = 30; 
                if (email.EnableSSL)
                    smtp.EnableSsl = true;
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = new NetworkCredential(email.From, email.Password);
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtp.Send(message);
                ret = true;
                return ret;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public static void SaveSendHistory(Emails email)
        { 
            try
            {
                 
            }
            catch (Exception ex)
            {
                
            }
        }
    }
}
