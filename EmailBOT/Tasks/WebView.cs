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

namespace Tracker
{
    public partial class WebView : Form
    {
        public  WebView(string htmlSource)
        {
            InitializeComponent();
            File.WriteAllText("htmlsource.html", htmlSource);
            
           
        }
        private async Task InitializeAsync()
        {
            Debug.WriteLine("InitializeAsync");
            await webView21.EnsureCoreWebView2Async(null);
            Debug.WriteLine("WebView2 Runtime version: " + webView21.CoreWebView2.Environment.BrowserVersionString);
        }

        private async void WebView_Load(object sender, EventArgs e)
        {
            //webView21.ExecuteScriptAsync("document.documentElement.outerHTML"); 
            //webView21.NavigateToString(System.IO.File.ReadAllText(Application.StartupPath + "/htmlsource.html"));
            try
            {
                await InitializeAsync(); 
                webView21.NavigateToString(System.IO.File.ReadAllText("htmlsource.html")); 
            }
            catch (Exception ex)
            {

            }
        }
    }
}
