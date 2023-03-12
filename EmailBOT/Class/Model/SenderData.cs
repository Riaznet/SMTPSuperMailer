using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmailBOT.Class.Model
{
    public class SenderData
    {
        public int Id { get; set; }
        public string SenderId { get; set; }
        public string Name { get; set; }
        public string Content { get; set; }
        public string Subject { get; set; }
        public string Host { get; set; }
        public int Port { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Date { get; set; }
        public int UserId { get; set; }
        public string Type { get; set; }
    }
}
