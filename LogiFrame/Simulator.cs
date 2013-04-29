using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace LogiFrame
{
    internal partial class Simulator : Form
    {
        private long buttonState = 0;

        public Simulator(string name)
        {
            InitializeComponent();
            this.Text = name;
            this.Show();
        }


        public long ReadSoftButtons()
        {
            return buttonState;
        }

        public void SetUpdatePriority(UpdatePriority priority)
        {
            label1.Text = "Priority: " + priority.ToString();
        }

        public void UpdateScreen(System.Drawing.Bitmap bitmap)
        {
            pictureBox1.Image = bitmap;
        }

        private void button1_MouseDown(object sender, MouseEventArgs e)
        {
            buttonState = buttonState | (long)Button.Button1;
        }

        private void button2_MouseDown(object sender, MouseEventArgs e)
        {
            buttonState = buttonState | (long)Button.Button2;
        }

        private void button3_MouseDown(object sender, MouseEventArgs e)
        {
            buttonState = buttonState | (long)Button.Button3;
        }

        private void button4_MouseDown(object sender, MouseEventArgs e)
        {
            buttonState = buttonState | (long)Button.Button3;
        }


        private void button1_MouseUp(object sender, MouseEventArgs e)
        {
            buttonState = buttonState ^ (long)Button.Button1;
        }

        private void button2_MouseUp(object sender, MouseEventArgs e)
        {
            buttonState = buttonState ^ (long)Button.Button2;
        }

        private void button3_MouseUp(object sender, MouseEventArgs e)
        {
            buttonState = buttonState ^ (long)Button.Button3;
        }

        private void button4_MouseUp(object sender, MouseEventArgs e)
        {
            buttonState = buttonState ^ (long)Button.Button4;
        }
    }
}
