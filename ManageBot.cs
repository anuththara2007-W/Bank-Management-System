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
        private async void InitializeAsync() //async is a new feature of the .net framework which doesnt freeze ui when loading 
        {
          
            await webView21.EnsureCoreWebView2Async(null);  //await is used to wait for the webview to load
            webView21.CoreWebView2.Navigate("https://app.fastbots.ai/bots/cmfgy9k0900w5qp1krjn4p7ex/history?page=1&limit=30"); //link to the chatbot
        }

     

        private void btnRefreshs_Click(object sender, EventArgs e)
        {
            ManageBot chatbot = new ManageBot();
            chatbot.Show();
            this.Hide();
        }

        private void goback_Click(object sender, EventArgs e)
        {
            Main admin = new Main();
            admin.Show();
            this.Hide();
        }
    }

    
}
