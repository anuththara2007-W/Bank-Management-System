using System;
using System.Windows.Forms;
using Microsoft.Web.WebView2.Core;

namespace Bank__Management_System
{
    public partial class ChatBot : Form
    {
        public ChatBot()
        {
            InitializeComponent();
            InitializeAsync();
        }

        private async void InitializeAsync()
        {
            // Make sure webView21 exists (added via Designer)
            await webView21.EnsureCoreWebView2Async(null);
            webView21.CoreWebView2.Navigate("https://your-chatbot-website.com");
        }
    }
}
