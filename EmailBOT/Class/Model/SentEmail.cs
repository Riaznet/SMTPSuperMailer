
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
    public class SentEmail
    {
        public string SenderId { get; set; }
        public int TotalMail { get; set; }
        public int TotalSent { get; set; }
        public int TotalFailed { get; set; }
        public string Date { get; set; } 
    } 
}
