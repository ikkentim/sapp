namespace Sapp
{
    partial class SettingsForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SettingsForm));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.libraryListBox = new System.Windows.Forms.ListBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.quickSwitchCheckBox = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.messageSetPropertyGrid = new System.Windows.Forms.PropertyGrid();
            this.panel1 = new System.Windows.Forms.Panel();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.messageSetsListbox = new System.Windows.Forms.ListBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.chatScrollSpeedListBox = new System.Windows.Forms.ListBox();
            this.label4 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.panel2.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.chatScrollSpeedListBox);
            this.groupBox1.Controls.Add(this.libraryListBox);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.quickSwitchCheckBox);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(289, 201);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Settings";
            // 
            // libraryListBox
            // 
            this.libraryListBox.FormattingEnabled = true;
            this.libraryListBox.Items.AddRange(new object[] {
            "G15",
            "G510"});
            this.libraryListBox.Location = new System.Drawing.Point(109, 132);
            this.libraryListBox.Name = "libraryListBox";
            this.libraryListBox.ScrollAlwaysVisible = true;
            this.libraryListBox.Size = new System.Drawing.Size(128, 30);
            this.libraryListBox.TabIndex = 4;
            this.libraryListBox.SelectedIndexChanged += new System.EventHandler(this.libraryListBox_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 165);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(248, 26);
            this.label3.TabIndex = 3;
            this.label3.Text = "(Changing the Display Library requires \r\nthe application to restart for changes t" +
    "o take effect)";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 132);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Library:";
            // 
            // quickSwitchCheckBox
            // 
            this.quickSwitchCheckBox.AutoSize = true;
            this.quickSwitchCheckBox.Location = new System.Drawing.Point(109, 21);
            this.quickSwitchCheckBox.Name = "quickSwitchCheckBox";
            this.quickSwitchCheckBox.Size = new System.Drawing.Size(65, 17);
            this.quickSwitchCheckBox.TabIndex = 1;
            this.quickSwitchCheckBox.Text = "Enabled";
            this.quickSwitchCheckBox.UseVisualStyleBackColor = true;
            this.quickSwitchCheckBox.CheckedChanged += new System.EventHandler(this.quickSwitchCheckBox_CheckedChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(74, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Quick switch: ";
            // 
            // messageSetPropertyGrid
            // 
            this.messageSetPropertyGrid.CommandsVisibleIfAvailable = false;
            this.messageSetPropertyGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.messageSetPropertyGrid.HelpVisible = false;
            this.messageSetPropertyGrid.Location = new System.Drawing.Point(3, 16);
            this.messageSetPropertyGrid.Name = "messageSetPropertyGrid";
            this.messageSetPropertyGrid.PropertySort = System.Windows.Forms.PropertySort.Alphabetical;
            this.messageSetPropertyGrid.Size = new System.Drawing.Size(489, 455);
            this.messageSetPropertyGrid.TabIndex = 1;
            this.messageSetPropertyGrid.ToolbarVisible = false;
            this.messageSetPropertyGrid.PropertyValueChanged += new System.Windows.Forms.PropertyValueChangedEventHandler(this.messageSetPropertyGrid_PropertyValueChanged);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.groupBox3);
            this.panel1.Controls.Add(this.groupBox1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(289, 474);
            this.panel1.TabIndex = 3;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.messageSetsListbox);
            this.groupBox3.Controls.Add(this.panel2);
            this.groupBox3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox3.Location = new System.Drawing.Point(0, 201);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(289, 273);
            this.groupBox3.TabIndex = 1;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Quick Message Sets";
            // 
            // messageSetsListbox
            // 
            this.messageSetsListbox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.messageSetsListbox.FormattingEnabled = true;
            this.messageSetsListbox.Location = new System.Drawing.Point(3, 16);
            this.messageSetsListbox.Name = "messageSetsListbox";
            this.messageSetsListbox.ScrollAlwaysVisible = true;
            this.messageSetsListbox.Size = new System.Drawing.Size(283, 207);
            this.messageSetsListbox.TabIndex = 9;
            this.messageSetsListbox.SelectedIndexChanged += new System.EventHandler(this.messageSetsListbox_SelectedIndexChanged);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.button1);
            this.panel2.Controls.Add(this.button2);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(3, 223);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(283, 47);
            this.panel2.TabIndex = 8;
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.Location = new System.Drawing.Point(239, 3);
            this.button1.Margin = new System.Windows.Forms.Padding(0);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(22, 22);
            this.button1.TabIndex = 2;
            this.button1.Text = "-";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button2.Location = new System.Drawing.Point(261, 3);
            this.button2.Margin = new System.Windows.Forms.Padding(0);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(22, 22);
            this.button2.TabIndex = 1;
            this.button2.Text = "+";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.messageSetPropertyGrid);
            this.groupBox4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox4.Location = new System.Drawing.Point(289, 0);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(495, 474);
            this.groupBox4.TabIndex = 6;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Quick Message Set";
            // 
            // chatScrollSpeedListBox
            // 
            this.chatScrollSpeedListBox.FormattingEnabled = true;
            this.chatScrollSpeedListBox.Items.AddRange(new object[] {
            "No scroll",
            "Slow",
            "Normal",
            "Fast",
            "Very Fast",
            "NITRO SPEED"});
            this.chatScrollSpeedListBox.Location = new System.Drawing.Point(109, 44);
            this.chatScrollSpeedListBox.Name = "chatScrollSpeedListBox";
            this.chatScrollSpeedListBox.ScrollAlwaysVisible = true;
            this.chatScrollSpeedListBox.Size = new System.Drawing.Size(128, 82);
            this.chatScrollSpeedListBox.TabIndex = 5;
            this.chatScrollSpeedListBox.SelectedIndexChanged += new System.EventHandler(this.chatScrollSpeedListBox_SelectedIndexChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 44);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(91, 13);
            this.label4.TabIndex = 6;
            this.label4.Text = "Chat scroll speed:";
            // 
            // SettingsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 474);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.panel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "SettingsForm";
            this.Text = "Sapp Settings";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.SettingsForm_FormClosing);
            this.Load += new System.EventHandler(this.SettingsForm_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox quickSwitchCheckBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.PropertyGrid messageSetPropertyGrid;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.ListBox messageSetsListbox;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ListBox libraryListBox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ListBox chatScrollSpeedListBox;
    }
}