using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmailBOT.Tasks
{
    internal class EmailList
    {
        public string Email { get; set; }
        public string Name { get; set; }
        public string Content { get; set; }
        public string Subject { get; set; }
        public string Credential { get; set; }
        public int Limit { get; set; } 
        public int PerSenderLimit { get; set; }
        public int Port { get; set; }
        public string Host { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
