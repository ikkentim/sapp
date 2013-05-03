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
        }

        private void SettingsForm_Load(object sender, EventArgs e)
        {
            if (!Settings.FileExists())
                MessageBox.Show("Welcome to Sapp!\n" +
                    "Since this is the first time you start this application, this settings window has opened.\n" +
                    "From now on, you will be able to access this menu trough the LCD application.", "Welcome!", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);

            Settings.Load();

            quickSwitchCheckBox.Checked = Settings.QuickSwitch;
            chatScrollSpeedListBox.SelectedIndex = Settings.ChatScrollSpeed;
            libraryListBox.SelectedIndex = Settings.Library == "G15" ? 0 : 1;

            foreach(MessageSet m in Settings.MessageSets)
                messageSetsListbox.Items.Add(m.Name);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            MessageSet ms = new MessageSet("New", "", "", "", "");
            Settings.MessageSets.Add(ms);
            messageSetsListbox.Items.Add(ms.Name);

            messageSetsListbox.SelectedIndex = messageSetsListbox.Items.Count - 1;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (messageSetsListbox.SelectedIndex >= 0 && messageSetsListbox.SelectedIndex < messageSetsListbox.Items.Count)
            {
                messageSetPropertyGrid.SelectedObject = null;
                Settings.MessageSets.RemoveAt(messageSetsListbox.SelectedIndex);
                messageSetsListbox.Items.RemoveAt(messageSetsListbox.SelectedIndex);
            }
        }

        private void messageSetsListbox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (messageSetsListbox.SelectedIndex >= 0 && messageSetsListbox.SelectedIndex < messageSetsListbox.Items.Count)
            {
                messageSetPropertyGrid.SelectedObject = Settings.MessageSets[messageSetsListbox.SelectedIndex];
            }
        }

        private void SettingsForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Settings.Save();
        }

        private void quickSwitchCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            Settings.QuickSwitch = quickSwitchCheckBox.Checked;
        }

        private void libraryListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            Settings.Library = libraryListBox.SelectedIndex == 0 ? "G15" : "G510";
        }

        private void messageSetPropertyGrid_PropertyValueChanged(object s, PropertyValueChangedEventArgs e)
        {
            if (e.ChangedItem.Label == "Name")
            {
                messageSetsListbox.Items[messageSetsListbox.SelectedIndex] = e.ChangedItem.Value;
            }
        }

        private void chatScrollSpeedListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (chatScrollSpeedListBox.SelectedIndex >= 0 && chatScrollSpeedListBox.SelectedIndex < chatScrollSpeedListBox.Items.Count)
            {
                Settings.ChatScrollSpeed = chatScrollSpeedListBox.SelectedIndex;
            }
        }
    }
}
