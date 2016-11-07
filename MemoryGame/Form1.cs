using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MemoryGame
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        PictureBox prev;
        byte flag = 0;
        int remain = 8;
        byte hint = 3;
        byte timeLeft = 60;
        private void Form1_Load(object sender, EventArgs e)
        {
            newgame();

        }

        void resetImages()
        {
            foreach (Control x in this.Controls) if (x is PictureBox) (x as PictureBox).Image = Properties.Resources._0;

            // Initialize all pictureBoxes' images to question mark (?). 
        }

        void resetTags()
        {
            foreach (Control x in this.Controls) if (x is PictureBox) (x as PictureBox).Tag = "0";

            // Initialize all pictureBoxes' tags to 0
        }

        void showImage(PictureBox box)
        {
            switch(Convert.ToInt32(box.Tag))
            {
                case 1:
                    box.Image = Properties.Resources._1;
                    break;
                case 2:
                    box.Image = Properties.Resources._2;
                    break;
                case 3:
                    box.Image = Properties.Resources._3;
                    break;
                case 4:
                    box.Image = Properties.Resources._4;
                    break;
                case 5:
                    box.Image = Properties.Resources._5;
                    break;
                case 6:
                    box.Image = Properties.Resources._6;
                    break;
                case 7:
                    box.Image = Properties.Resources._7;
                    break;
                case 8:
                    box.Image = Properties.Resources._8;
                    break;
                default:
                    box.Image = Properties.Resources._0;
                    break;
            }
            // This function converts picureBox tag to image;

        }
 

        void setTagRandom()
        {
            int[] arr = new int[16];
            int index = 0;
            Random rand = new Random();
            int r;
            while (index < 16)
            {
                r = rand.Next(1, 17);
                if(Array.IndexOf(arr,r)==-1)
                {
                    arr[index] = r;
                    index++;
                }  
            }
            for(index =0; index < 16; index++)
            {
                if (arr[index] > 8) arr[index] -= 8;
            }
            index = 0;
            foreach(Control x in this.Controls)
            {
                if(x is PictureBox)
                {
                    (x as PictureBox).Tag = arr[index].ToString();
                    index++;
                }
            }


          /* This function has 3 steps.
           * 1) It creates an array and fill inside of the array with random numbers between 1 and 16.
           * 2) It does subtraction process for numbers greater than 8. (because we have 8 different images for this game.
           * 3) It sets array's numbers to pictureBoxes' tags.
           */
        }
        void compare(PictureBox previous, PictureBox current)
        {
            if(previous.Tag.ToString()==current.Tag.ToString())
            {
                Application.DoEvents();
                System.Threading.Thread.Sleep(500);
                previous.Visible = false;
                current.Visible = false;
                if(--remain==0)
                {
                    timer1.Enabled = false;
                    remaining.Text = "Congratualations.";
                    MessageBox.Show("Congratulations. You have fnished the game.", "End of the game");
                    Hint.Enabled = false;
                }
               else remaining.Text = "Remaining Items: " + remain;
            }
            else
            {
                Application.DoEvents();
                System.Threading.Thread.Sleep(500);
                previous.Image = Properties.Resources._0;
                current.Image = Properties.Resources._0;
            }
        }

        void allvisibleTrue()
        {
            foreach (Control x in this.Controls) if (x is PictureBox) (x as PictureBox).Visible = true;
        }
        void activeAll()
        {
            foreach (Control x in this.Controls) if (x is PictureBox) (x as PictureBox).Enabled = true;
        }
        void deActiveAll()
        {
            foreach (Control x in this.Controls) if (x is PictureBox) (x as PictureBox).Enabled = false;
        }
        void newgame()
        {
            remain = 8;
            hint = 3;
            setTagRandom();
            allvisibleTrue();
            resetImages();
            Hint.Enabled = true;
            remaining.Text = "Remaining Items: " + remain;
            Hint.Text = "Hint (" + hint + ")";
            flag = 0;
            timeLeft = 60;
            time.Text = "Time Left: " + timeLeft;
            timer1.Enabled = true;
            activeAll();

        }
        private void pictureBox1_Click(object sender, EventArgs e)
        {
            PictureBox current = (sender as PictureBox);
            showImage((sender as PictureBox));
            if (flag == 0)
            {
                prev = current;
                flag = 1;
            }
            else if(prev!=current)
            {
                compare(prev, current);
                flag = 0;
            }
           
        }

        private void Hint_Click(object sender, EventArgs e)
        {
            foreach(Control x in this.Controls) if(x is PictureBox) showImage((x as PictureBox));
            Application.DoEvents();
            System.Threading.Thread.Sleep(1500);
            resetImages();
            if (--hint == 0) Hint.Enabled = false;

            Hint.Text = "Hint (" + hint + ")";
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            
            if (--timeLeft == 0)
            {
                timer1.Enabled = !timer1.Enabled;
                time.Text = "Time's out.";
                MessageBox.Show("TIME IS OVER", "End of the game");
                deActiveAll();
                Hint.Enabled = false;
                
            }
            else
            time.Text = "Time Left: " + timeLeft;

        }

        private void newGameButton_Click(object sender, EventArgs e)
        {
            newgame();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("http://" +(sender as LinkLabel).Text);
        }
    }
}
