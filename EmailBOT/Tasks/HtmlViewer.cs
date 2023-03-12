using Microsoft.Web.WebView2.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EmailBOT.Tasks
{
    public partial class HtmlViewer : Form
    {
        public HtmlViewer(string htmlSource)
        {
            InitializeComponent();
            //if (File.Exists("htmlsource.html"))
            //    File.WriteAllText("htmlsource.html", htmlSource);
            //else
            //{
            //    //File.CreateText("htmlsource.html");
            //    using (StreamWriter sw = File.CreateText("htmlsource.html"))
            //        File.WriteAllText("htmlsource.html", htmlSource);
            //}
            LoadHtml(htmlSource);
        }
        private async void LoadHtml(string htmlContent)
        {
            if (webView21 != null)
            {
                await webView21.EnsureCoreWebView2Async();
                webView21.CoreWebView2.NavigateToString(htmlContent);
            }
        } 
    }
}
