using System;
using System.Windows.Forms;
using Microsoft.Web.WebView2.WinForms;
using Microsoft.Web.WebView2.Core;

namespace Bank__Management_System
{
    public partial class ManageBot : Form
    {
        private WebView2 webView21;

        public ManageBot()
        {
            InitializeComponent();
            InitializeWebView();
        }

        private async void InitializeWebView()
        {
            // Create WebView2 and fill the form
            webView21 = new WebView2
            {
                Dock = DockStyle.Fill
            };
            this.Controls.Add(webView21);

            // Initialize and navigate to chatbot
            await webView21.EnsureCoreWebView2Async(null);
            webView21.CoreWebView2.Navigate("https://app.fastbots.ai/embed/cmfgy9k0900w5qp1krjn4p7ex");
        }

        private void webView21_Click(object sender, EventArgs e)
        {

        }
    }
}
