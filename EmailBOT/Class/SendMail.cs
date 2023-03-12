
using EmailBOT.Class.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;

namespace EmailBOT.Class
{
    internal class SendMail
    {
        public class SenderProcess
        {
            public static bool SendMailPage(Emails email)
            {
                bool ret = false;
                try
                {
                    ContentType mimeType = null;
                    //if (email.IsHtml)
                      
                    //else
                    //    mimeType = new System.Net.Mime.ContentType("text/plain");
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
        }
    }
}
