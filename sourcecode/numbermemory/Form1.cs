using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WindowsFormsApplication3
{
    public partial class Form1 : Form
    {
        public static int level = 1;
        public static int retried = 0;
        public static int getkelipatan = 1;
        public static int maximumcountdown = 30;
        public static int maxcountdown = 5;
        public static int countdown = maxcountdown;
        public static string number = "";

        public Form1()
        {
            InitializeComponent();
            labelnumber.Text = "SELECT DIFFICULTY";
            //CalculateDiff(1); 
            //GetNewNumber();
        }

        private bool mouseDown;
        private Point lastLocation;

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            mouseDown = true;
            lastLocation = e.Location;
        }

        private void Form1_MouseUp(object sender, MouseEventArgs e)
        {
            mouseDown = false;
        }

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            if (mouseDown)
            {
                this.Location = new Point(
                    (this.Location.X - lastLocation.X) + e.X, (this.Location.Y - lastLocation.Y) + e.Y);

                this.Update();
            }
        }

        private void CalculateDiff(byte levelget)
        {
            level = 1;
            retried = 0;
            getkelipatan = 1 * levelget;
            maximumcountdown = 30;
            maxcountdown = 5;
            countdown = maxcountdown;
            number = "";
            timer1.Enabled = false;
            timer2.Enabled = false;
            labelnumber.Text = "ENTER THE NUMBER";
            labelcountdown.Text = "-- : --";
        }

        private void CalculateAccuracy()
        {
            double getper1 = (double)(level - retried) / (double)level * 100.0;
            double getper = Math.Round(getper1);
            labelscore.Text = getper.ToString()+"%";
            labelretried.Text = retried.ToString();
            if (getper == 100)
                labelrank.Text = "SS";
            else if (getper1 >= 95.50)
                labelrank.Text = "S";
            else if (getper1 >= 87.50)
                labelrank.Text = "A";
            else if (getper1 >= 75.00)
                labelrank.Text = "B";
            else if (getper1 >= 57.50)
                labelrank.Text = "C";
            else if (getper1 < 57.50)
                labelrank.Text = "F";
            // 100% SS
            // 95.50% S
            // 95.50%-87.50% A
            // 87.50-75% B
            // 75%-57.50% C
            // 57.50% F
        }

        private void GetNewNumber()
        {
            CalculateAccuracy();
            countdown = maxcountdown;
            textboxnumber.Text = "";
            textboxnumber.Enabled = false; 
            labelnumber.Text = "";
            timer1.Interval = (1000);
            timer1.Enabled = true;
            timer1.Start();
            labellevel.Text = "LEVEL " + level.ToString();

            Random rnd = new Random();
            /*if (level % 5 == 0)
            {
                getkelipatan += 1;
                maxcountdown += 2;
                if (maxcountdown >= maximumcountdown)
                    maxcountdown = maximumcountdown;
            }*/
            if (getkelipatan >= 4)
                getkelipatan = 4;
            for (int i = 0; i < getkelipatan*2; i += 1)
            {
                int getnum = rnd.Next(0, 9);
                labelnumber.Text += getnum.ToString();
            }
        }

        private void timer1_Tick_1(object sender, EventArgs e)
        {
            countdown -= 1;
            labelcountdown.Text = "-- : 0" + countdown.ToString();
            if (countdown <= 0)
            {
                countdown = 0;
                timer1.Enabled = false;
                number = labelnumber.Text;
                textboxnumber.Enabled = true;
                labelnumber.Text = "ENTER THE NUMBER";
                labelcountdown.Text = "-- : --";
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (textboxnumber.Text == number)
            {
                labelnumber.Text = "CORRECT";
                countdown = 5;
                level += 1;
                textboxnumber.Text = "";
                textboxnumber.Enabled = false;
                timer2.Interval = (1000);
                timer2.Enabled = true;
                timer2.Start();
                CalculateAccuracy();
            }
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            countdown -= 1;
            labelcountdown.Text = "-- : 0" + countdown.ToString();
            if (countdown <= 0)
            {
                countdown = 0;
                timer2.Enabled = false;
                GetNewNumber();
            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (labelnumber.Text == "ENTER THE NUMBER")
            {
                GetNewNumber();
                retried += 1;
                CalculateAccuracy();
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            CalculateDiff(1);
            GetNewNumber();
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            CalculateDiff(2);
            GetNewNumber();
        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            CalculateDiff(3);
            GetNewNumber();
        }

        private void checkBox4_CheckedChanged(object sender, EventArgs e)
        {
            CalculateDiff(4);
            GetNewNumber();
        }

        private void checkBox1_CheckedChanged_1(object sender, EventArgs e)
        {
            this.ResumeLayout(false);
            this.TopMost = checkBox1.Checked;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
