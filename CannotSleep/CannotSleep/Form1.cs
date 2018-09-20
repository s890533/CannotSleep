using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CannotSleep
{
    public partial class Form1 : Form
    {

        private int curr_x;
        private int curr_y;
        private int mouse_x;
        private int mouse_y;
        private bool isWinMove;
        bool IsCannotSleep;
        int mousecount;

        [DllImport("user32.dll")]
        public static extern void keybd_event(byte bVK, byte bScan,uint dwFlags,int dw);
        public Form1()
        {
            InitializeComponent();
            timer1.Enabled = true;
            timer1.Interval = 1000;
            timer1.Start();
            timer1.Tick += new EventHandler(CannotSleep);
            IsCannotSleep = false;
        }

        private void CannotSleep(object sender,EventArgs e)
        {
            Point position = MousePosition;
            if(position.X == this.mouse_x && position.Y == this.mouse_y)
            {
                mousecount++;
            }
            else
            {
                mousecount = 0;
                this.mouse_x = position.X;
                this.mouse_y = position.Y;

            }
            if(mousecount > 120)
            {
                if (IsCannotSleep)
                {
                    keybd_event(0x12, 0, 0, 0);
                    keybd_event(0x12, 0, 0x0002, 0);
                    mousecount = 0;
                }
                
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (button1.Text == "Can Sleep")
            {
                button1.Text = "Cannot Sleep";
                IsCannotSleep = true;
                timer1.Enabled = true;
                button1.BackColor = Color.Salmon;

            }
            else
            {
                button1.Text = "Can Sleep";
                IsCannotSleep = false;
                timer1.Enabled = false;
                button1.BackColor = Color.CornflowerBlue;
            }
        }

        

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
            this.TopMost = true;
        }

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                this.curr_x = e.X;
                this.curr_y = e.Y;
                this.isWinMove = true;
            }
        }

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            if (this.isWinMove == true)
            {
                this.Location = new Point(this.Left + e.X - this.curr_x, this.Top + e.Y - this.curr_y);
            }
        }

        private void Form1_MouseUp(object sender, MouseEventArgs e)
        {
            this.isWinMove = false;
        }
    }
}
