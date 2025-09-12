using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Bank__Management_System
{
    public partial class ManageBot : Form
    {
        public ManageBot()
        {
            InitializeComponent();
            InitializeAsync();
        }
        private async void InitializeAsync()
        {
            // Make sure webView21 exists (added via Designer)
            await webView21.EnsureCoreWebView2Async(null);
            webView21.CoreWebView2.Navigate("https://app.fastbots.ai/bots/cmfgy9k0900w5qp1krjn4p7ex/history?page=1&limit=30");
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            ChatBot chatbot = new ChatBot(); chatbot.Show(); this.Hide();
        }
    }
}
