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
            await webView21.EnsureCoreWebView2Async(null);
            webView21.CoreWebView2.Navigate("https://app.fastbots.ai/embed/cmfgy9k0900w5qp1krjn4p7ex");
        }
    }
}
