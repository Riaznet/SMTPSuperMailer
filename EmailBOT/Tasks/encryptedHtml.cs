using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EmailBOT.Tasks
{
    public partial class encryptedHtml : Form
    {
        public encryptedHtml()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Generate a random IV with the correct size for the encryption algorithm
            
            string htmlContent = "<html><body><h1>Hello, world!</h1></body></html>";
            byte[] key = Encoding.UTF8.GetBytes("mySecretKey12345");
           // byte[] iv = Encoding.UTF8.GetBytes("myIV1234567890");
            byte[] iv = Encoding.UTF8.GetBytes("1234567890123456"); // 16 bytes = 128 bits (AES block size)

            // Use the fixed IV for encryption/decryption
            string encryptedHtmlContent = Encrypt("<html><body>"+richTextBox1.Text+ "</body></html>", "mySecretKey12345", "1234567890123456");

            // Create a new MailMessage object
            MailMessage message = new MailMessage();
            message.From = new MailAddress("riazcpi@gmail.com");
            message.To.Add(new MailAddress("riazpciu@gmail.com"));
            message.Subject = "Encrypted HTML template";
            message.IsBodyHtml = true;
            //message.Body = htmlContent;
            message.AlternateViews.Add(AlternateView.CreateAlternateViewFromString(richTextBox1.Text, new ContentType("text/html"))); 
        

            // Send the email
            SmtpClient smtpClient = new SmtpClient("smtp.gmail.com", 587);
            smtpClient.UseDefaultCredentials = false;
            smtpClient.Credentials = new NetworkCredential("riazcpi@gmail.com", "vegqfezgcmegdfns");
            smtpClient.EnableSsl = true;
            smtpClient.Send(message);
        }
        public static string EncryptString(string plainText, byte[] key, byte[] iv)
        {
            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = key;
                aesAlg.IV = iv;

                ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

                using (MemoryStream msEncrypt = new MemoryStream())
                {
                    using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                        {
                            swEncrypt.Write(plainText);
                        }

                        byte[] encryptedBytes = msEncrypt.ToArray();
                        return Convert.ToBase64String(encryptedBytes);
                    }
                }
            }
        }
        public static string Encrypt(string plainText, string key, string iv)
        {
            byte[] encryptedBytes;
            using (Aes aes = Aes.Create())
            {
                aes.Key = Encoding.UTF8.GetBytes(key);
                aes.IV = Encoding.UTF8.GetBytes(iv);
                ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter sw = new StreamWriter(cs))
                        {
                            sw.Write(plainText);
                        }
                        encryptedBytes = ms.ToArray();
                    }
                }
            }
            return Convert.ToBase64String(encryptedBytes);
        }

    }
}
