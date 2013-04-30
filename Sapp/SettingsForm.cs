using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace Sapp
{
    public partial class SettingsForm : Form
    {
        [DllImport("User32.dll")]
        public static extern Int32 SetForegroundWindow(int hWnd);   

        public SettingsForm()
        {
            InitializeComponent();

            SetForegroundWindow(Handle.ToInt32());

            LogiFrame.MessageSet set = new LogiFrame.MessageSet();

            messageSetPropertyGrid.SelectedObject = set;
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }
    }
}
