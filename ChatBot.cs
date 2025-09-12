using System;
using System.Windows.Forms;
using Microsoft.Web.WebView2.WinForms;
using Microsoft.Web.WebView2.Core;

namespace Bank__Management_System
{
    public partial class ChatBot : Form
    {
        private WebView2 webView21;

        public ChatBot()
        {
            InitializeComponent();
            InitializeWebView();
            InitializeAsync();
        }

        private void InitializeWebView()
        {
            webView21 = new WebView2
            {
                Dock = DockStyle.Fill
            };
            this.Controls.Add(webView21);
        }

        private async void InitializeAsync()
        {
            await webView21.EnsureCoreWebView2Async(null);
            webView21.CoreWebView2.Navigate("https://your-chatbot-website.com");
        }
    }
}
