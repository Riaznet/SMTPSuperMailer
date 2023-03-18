using EmailBOT.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EmailBOT.Class
{
    public  class PublicProperty
    { 

        internal static string uniqueId="";
        internal static bool isDemo = false;
        internal static string license="";
        internal static bool isActive = true;
        internal static Form Uploader;
        internal static bool datacall = true;
        internal static int rowIndex = 0;
        internal static int lastSelected = 0;
        internal const string version = "1.0";
        internal static string name = "";
        internal static string UserId = "";
        internal static string OnlineConnection = "";

        public static string SettingsName { get; internal set; }
        public static int MailChangingValue { get; internal set; }
        public static string[] MultiData { get; internal set; }
        public static TextBox Message { get; internal set; }
        public static TextBox MessageHtml { get; internal set; }
        public static SendersList SenderForm { get; internal set; }
        public static string DisplayName { get; internal set; }
        public static string SubjectName { get; internal set; }
        public static List<string> linesList { get;  set; }
        public static Label LabelStatus1 { get; internal set; }
        //public static List<string> SenderList { get; internal set; }
        //public static List<EmailList> SenderList { get;  set; }
        
        public static string SenderId { get; internal set; }
    }
}
