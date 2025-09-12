using System;
using System.Windows.Forms;
using Microsoft.Web.WebView2.WinForms;
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
            await webView21.EnsureCoreWebView2Async(null);
            webView21.CoreWebView2.Navigate("https://app.fastbots.ai/embed/cmfgy9k0900w5qp1krjn4p7ex");
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            // Simply reload the current page
            webView21.Reload();
        }
    }
}
