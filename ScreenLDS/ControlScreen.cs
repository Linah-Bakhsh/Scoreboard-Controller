using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Globalization;
using System.Drawing.Drawing2D;

namespace ScreenLDS
{
    public partial class ControlScreen : Form
    {
        /////////////////// for new window ////////////////////
        private ShowScreen_Logo childToShowScreen_Logo;
        ///////////////////SET & GET///////////////////////////
        public string ClockShape { get; set; }
        public Image Background { get; set; }


        /// /////////////////////ALL values for Clock/////////////////////////////
        Timer counter = new Timer();// this used for counter match timer
        int sec_counter, min_counter;

        Timer t = new Timer();// this used for clock timer
        private new int Width = 201, Height = 201; // Most be Same size pictireBox to fit backgruond clock picture
        private Bitmap bmp;

        private Graphics g;

        private float rad;
        private PointF cen;
        private Color handColor = Color.Black;// hand Clock of hour and min
        private Color innerColor = Color.Transparent;// backgruond inner
        private Color outerColor = Color.Transparent;// backgruond outter
        private Color tickColor = Color.Transparent;// second dash
        private Color secondhandColor = Color.Red; // hand Clock of hour and min
        private float size1;
        private float size2;

        private DateTime cuirrentTime = DateTime.Now;
        private Calendar calender = new GregorianCalendar();
        private Color hourColor = Color.DarkGoldenrod;

        int counter_Home = 0;
        int counter_away = 0;

        public ControlScreen()
        {
            InitializeComponent();
        }

        //////////////FOR PASSING DATA TO FORM3////////////////////
        private void frmParent_Shown(object sender, EventArgs e)
        {
            childToShowScreen_Logo = new ShowScreen_Logo();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            /// /////////////////////ALL values for Clock/////////////////////////////
            size2 = (Width * .78f) / (2);
            size1 = (Height * .9f) / (2);
            rad = Width / 2;
            cen = new PointF(Width / 2, Height / 2);
            /// /////////////////////Transparent Background///////////////////////////
            panel1.BackColor = Color.FromArgb(175, 175, 175, 175);
            Clock_pictureBox.BackColor = Color.FromArgb(50, 100, 100, 100);
            Home_pictureBox.BackColor = Color.FromArgb(175, 175, 175, 175);
            Away_pictureBox.BackColor = Color.FromArgb(175, 175, 175, 175);

            this.BackgroundImage = Background;
            ///////////////Timer Match//////////////////////
            counter.Interval = 1000;
            counter.Tick += new EventHandler(this.OnTimeEvent);
            ////////////////////////////////////////////////

            bmp = new Bitmap(Width + 1, Height + 1);

            ///////////////Timer Clock//////////////////////
            t.Interval = 1000;
            t.Tick += new EventHandler(this.t_Tick);
            t.Start();

            //////////////Change sharpe Clock//////////////
            /*switch (ClockShape)
            {
                case "1":
                    Clock_pictureBox.BackgroundImage = Properties.Resources.Clock1;
                    break;
                case "2":
                    Clock_pictureBox.BackgroundImage = Properties.Resources.Clock2;
                    break;
                case "3":
                    Clock_pictureBox.BackgroundImage = Properties.Resources.Clock3;
                    break;
                case "4":
                    Clock_pictureBox.BackgroundImage = Properties.Resources.Clock4;
                    break;
                case "5":
                    Clock_pictureBox.BackgroundImage = Properties.Resources.Clock5;
                    break;
                case "6":
                    Clock_pictureBox.BackgroundImage = Properties.Resources.Clock6;
                    break;
                case "7":
                    Clock_pictureBox.BackgroundImage = Properties.Resources.Clock7;
                    break;
                case "8":
                    Clock_pictureBox.BackgroundImage = Properties.Resources.Clock8;
                    break;
                default:
                    Clock_pictureBox.BackgroundImage = Properties.Resources.Clock1;
                    break;
            }*/
        }

        private void t_Tick(object sender, EventArgs e)
        {
            g = Graphics.FromImage(bmp);

            int sec = DateTime.Now.Second;
            int min = DateTime.Now.Minute;
            int hour = DateTime.Now.Hour;

            g.Clear(Color.Transparent);

            GraphicsPath gp = new GraphicsPath();
            gp.AddEllipse(0, 0, Width, Height);
            PathGradientBrush pgb = new PathGradientBrush(gp);
            pgb.CenterColor = innerColor;
            pgb.SurroundColors = new Color[] { outerColor };
            g.FillEllipse(pgb, 0, 0, Width, Height);
            g.DrawEllipse(new Pen(Color.Transparent, 2f), 0, 0, Width, Height);

            //g.DrawString(hour + ":" + min + ":" + sec, new Font("Arial", 25), Brushes.Black, new PointF(130, 280)); // for show digital clock time
            /*
            g.DrawString("55", new Font("Impact", 10), Brushes.DarkRed, new PointF(98, 26));
            g.DrawString("60", new Font("Impact", 10), Brushes.DarkRed, new PointF(190, 1));
            g.DrawString("5", new Font("Impact", 10), Brushes.DarkRed, new PointF(290, 25));
            g.DrawString("10", new Font("Impact", 10), Brushes.DarkRed, new PointF(358, 97));
            g.DrawString("15", new Font("Impact", 10), Brushes.DarkRed, new PointF(384, 190));
            g.DrawString("20", new Font("Impact", 10), Brushes.DarkRed, new PointF(355, 286));
            g.DrawString("25", new Font("Impact", 10), Brushes.DarkRed, new PointF(285, 359));
            g.DrawString("30", new Font("Impact", 10), Brushes.DarkRed, new PointF(192, 383));
            g.DrawString("35", new Font("Impact", 10), Brushes.DarkRed, new PointF(95, 357));
            g.DrawString("40", new Font("Impact", 10), Brushes.DarkRed, new PointF(27, 286));
            g.DrawString("45", new Font("Impact", 10), Brushes.DarkRed, new PointF(1, 193));
            g.DrawString("50", new Font("Impact", 10), Brushes.DarkRed, new PointF(26, 95));
            */
            for (int i = 0; i < 60; i++)
                if (i % 5 != 0)
                    g.DrawLine(new Pen(tickColor, rad * .015f),
                        (float)Math.Cos(i * 6 * Math.PI / 180) * rad * .95f + cen.X,
                        (float)Math.Sin(i * 6 * Math.PI / 180) * rad * .95f + cen.Y,
                        (float)Math.Cos(i * 6 * Math.PI / 180) * rad + cen.X,
                        (float)Math.Sin(i * 6 * Math.PI / 180) * rad + cen.Y);
            /*
            for(int i = 0; i<60; i++)
                if(i*5 == 0)
                    g.DrawLine(new Pen (handColor, size1 * .04f),
                        (float)Math.Cos(i * 6 * Math.PI / 180) * size1 * .95f + cen.X,
                        (float)Math.Sin(i * 6 * Math.PI / 180) * size1 * .95f + cen.Y,
                        (float)Math.Cos(i * 6 * Math.PI / 180) * size1 + cen.X,
                        (float)Math.Sin(i * 6 * Math.PI / 180) * size1 + cen.Y);
            */
            Color c = Color.FromArgb((innerColor.R + outerColor.R) / 2,
                (innerColor.G + outerColor.G) / 2,
                (innerColor.B + outerColor.B) / 2);
            /////////////////////////////those for show date and day/////////////////////////////////////////
            /*
            StringFormat sf = new StringFormat();
            sf.Alignment = StringAlignment.Center;

            g.FillRectangle(Brushes.White,
                cen.X + size2 * .36f, cen.Y - size2 * .06f,
                size2 * .24f, size2 * .12f);
            g.DrawRectangle(new Pen(Color.Black, size2 * .015f),
                cen.X + size2 * .36f, cen.Y - size2 * .06f,
                size2 * .24f, size2 * .12f);
            g.DrawString(DateTime.Now.DayOfWeek.ToString().Substring(0, 3).ToUpper(),
                new Font("Lucida Console", size2 * .09f, FontStyle.Bold), Brushes.Black,
                cen.X + size2 * .485f, cen.Y - size2 * .05f, sf);

            g.FillRectangle(Brushes.White,
                cen.X + size2 * .6f, cen.Y - size2 * .06f,
                size2 * .16f, size2 * .12f);
            g.DrawRectangle(new Pen(Color.Black, size2 * .015f),
                cen.X + size2 * .6f, cen.Y - size2 * .06f,
                size2 * .16f, size2 * .12f);
            g.DrawString(calender.GetDayOfMonth(DateTime.Now).ToString(),
                new Font("Lucida Console", size2 * .08f, FontStyle.Bold), Brushes.Black,
                cen.X + size2 * .685f, cen.Y - size2 * .04f, sf);
             * /
             //////////////////////////////////////////////////////////////////////////////////////
            /*
            #region Chữ trang tri
            FontFamily ff = new FontFamily("Arial");
            System.Drawing.Font font_1 = new System.Drawing.Font(ff, 20f);
            g.DrawString("Clock", font_1, Brushes.LightBlue, new PointF(155, 75));

            Font font_2 = new Font(ff, 7f);
            g.DrawString("Thanks a stranger on github for giving me the idae of this watch", font_2, Brushes.LightBlue, new PointF(67, 120));
            #endregion
            */
            //Set for Hand hour
            g.FillPolygon(new SolidBrush(handColor),
                new PointF[]{
                    new PointF(
                        cen.X - (float)(Math.Sin((hour * 30 + min / 12 * 6) * Math.PI / 180)) * rad * .1f,
                        cen.Y - (float)(-Math.Cos((hour * 30 + min / 12 * 6) * Math.PI / 180)) * rad * .1f),
                    new PointF(
                        cen.X - (float)(Math.Sin((hour * 30 + min / 12 * 6 + 90) * Math.PI / 180)) * rad * .05f,
                        cen.Y - (float)(-Math.Cos((hour * 30 + min / 12 * 6 + 90) * Math.PI / 180)) * rad * .05f),
                    new PointF(
                        (float)(Math.Sin((hour * 30 + min / 12 * 6) * Math.PI / 180)) * rad * .5f + cen.X,
                        (float)(-Math.Cos((hour * 30 + min / 12 * 6) * Math.PI / 180)) * rad * .5f + cen.Y),
                    new PointF(
                        cen.X - (float)(Math.Sin((hour * 30 + min / 12 * 6 - 90) * Math.PI / 180)) * rad * .05f,
                        cen.Y - (float)(-Math.Cos((hour * 30 + min / 12 * 6 - 90) * Math.PI / 180)) * rad * .05f)
                            });
            //Set for Hand min
            g.FillPolygon(new SolidBrush(handColor),
                new PointF[]{
                    new PointF(
                        cen.X - (float)(Math.Sin((min * 6) * Math.PI / 180)) * rad * .1f,
                        cen.Y - (float)(-Math.Cos((min * 6) * Math.PI / 180)) * rad * .1f),
                    new PointF(
                        cen.X - (float)(Math.Sin((min * 6 + 90) * Math.PI / 180)) * rad * .05f,
                        cen.Y - (float)(-Math.Cos((min * 6 + 90) * Math.PI / 180)) * rad * .05f),
                    new PointF(
                        (float)(Math.Sin((min * 6) * Math.PI / 180)) * rad * .7f + cen.X,
                        (float)(-Math.Cos((min * 6) * Math.PI / 180)) * rad * .7f + cen.Y),
                    new PointF(
                        cen.X - (float)(Math.Sin((min * 6 - 90) * Math.PI / 180)) * rad * .05f,
                        cen.Y - (float)(-Math.Cos((min * 6 - 90) * Math.PI / 180)) * rad * .05f)
                            });
            //Set for Hand second
            g.DrawLine(new Pen(secondhandColor, size1 * .01f),
                        cen.X - (float)(Math.Sin((sec * 6) * Math.PI / 180)) * rad * .2f,
                        cen.Y - (float)(-Math.Cos((sec * 6) * Math.PI / 180)) * rad * .2f,
                        (float)(Math.Sin((sec * 6) * Math.PI / 180)) * rad * .9f + cen.X,
                        (float)(-Math.Cos((sec * 6) * Math.PI / 180)) * rad * .9f + cen.Y);

            g.FillEllipse(new SolidBrush(secondhandColor), cen.X - rad * .03f, cen.Y - rad * .03f, rad * .06f, rad * .06f);

            Clock_pictureBox.Image = bmp;
            childToShowScreen_Logo.Data_Clock = Clock_pictureBox.Image;

            g.Dispose();
        }

        private void OnTimeEvent(Object sender, EventArgs e)
        {
            Invoke(new Action(() =>
            {
                sec_counter++;
                if (min_counter < 45 && sec_counter > 0)
                {
                    Round_label.Text = "1 st";
                }
                else
                {
                    Round_label.Text = "2 nd";
                }
                if (sec_counter >= 60)
                {
                    sec_counter = 0;
                    min_counter++;
                }
                if (min_counter == 45 && sec_counter == 0 || min_counter >= 90)
                {
                    if (min_counter >= 90 && sec_counter >= 0)
                    {
                        counter.Stop();
                        min_counter = 90;
                        sec_counter = 0;
                        MessageBox.Show("Error , Can't counter more then 90");
                    }
                    counter.Stop();
                }

                Time_Label.Text = String.Format("{0}:{1}", min_counter.ToString().PadLeft(2, '0'), sec_counter.ToString().PadLeft(2, '0'));

                ////////////////////FOR GET & SET FORM3////////////////////
                if (childToShowScreen_Logo != null)
                {
                    childToShowScreen_Logo.Data__Name_counter = Round_label.Text;
                }
                if (childToShowScreen_Logo != null)
                {
                    childToShowScreen_Logo.Data_counter_Time = Time_Label.Text;
                }
                //////////////////////////////////////////////////////////
            }));
        }

        private void Home_pictureBox_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawRectangle(new Pen(Color.Black, 12f), 0, 0, Home_pictureBox.Size.Width - 2, Home_pictureBox.Size.Height - 2);
        }

        private void PictureHome_button_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "(*.jpg; *.jpeg; *.gif; *.bmp; *.png)|*.jpg; *.jpeg; *.gif; *.bmp; *.png";

            if (ofd.ShowDialog() == DialogResult.OK && ofd.FileName.Length > 0)
            {
                Home_pictureBox.SizeMode = PictureBoxSizeMode.Zoom;
                Home_pictureBox.Image = Image.FromFile(ofd.FileName);
                if (childToShowScreen_Logo != null)
                {
                    childToShowScreen_Logo.Data_Image_Home = Home_pictureBox.Image;
                }
            }
        }

        private void RemovePictureHome_button_Click(object sender, EventArgs e)
        {
            Home_pictureBox.Image = null;
        }

        private void Away_pictureBox_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawRectangle(new Pen(Color.Black, 12f), 0, 0, Away_pictureBox.Size.Width - 2, Away_pictureBox.Size.Height - 2);
        }

        private void PictureAway_button_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "(*.jpg; *.jpeg; *.gif; *.bmp; *.png)|*.jpg; *.jpeg; *.gif; *.bmp; *.png";

            if (ofd.ShowDialog() == DialogResult.OK && ofd.FileName.Length > 0)
            {
                Away_pictureBox.SizeMode = PictureBoxSizeMode.Zoom;
                Away_pictureBox.Image = Image.FromFile(ofd.FileName);
                if (childToShowScreen_Logo != null)
                {
                    childToShowScreen_Logo.Data_Image_Away = Away_pictureBox.Image;
                }
            }
        }

        private void RemovePictureAway_button_Click(object sender, EventArgs e)
        {
            Away_pictureBox.Image = null;
        }

        private void PictureCenter_button_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "jpg  (*.jpg)|*.jpg|bmp (*.bmp)|*.bmp|png (*.png)|*.png";

            if (ofd.ShowDialog() == DialogResult.OK && ofd.FileName.Length > 0)
            {
                Center_pictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
                Center_pictureBox.Image = Image.FromFile(ofd.FileName);
            }
        }

        private void RemovePictureCenter_button_Click(object sender, EventArgs e)
        {
            Center_pictureBox.Image = null;
        }

        private void PictureUpper_button_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "jpg  (*.jpg)|*.jpg|bmp (*.bmp)|*.bmp|png (*.png)|*.png";

            if (ofd.ShowDialog() == DialogResult.OK && ofd.FileName.Length > 0)
            {
                Upper_pictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
                Upper_pictureBox.Image = Image.FromFile(ofd.FileName);
            }
        }

        private void RemovePictureUpper_button_Click(object sender, EventArgs e)
        {
            Upper_pictureBox.Image = null;
        }

        private void GoalHomePlus_button_Click(object sender, EventArgs e)
        {
            counter_Home++;
            Home_NOScore.Text = counter_Home.ToString();

            if (childToShowScreen_Logo != null)
            {
                childToShowScreen_Logo.Data_goal_Home = Home_NOScore.Text;
            }
        }

        private void GoalHomeNeg_button_Click(object sender, EventArgs e)
        {
            counter_Home--;
            Home_NOScore.Text = counter_Home.ToString();
            if (childToShowScreen_Logo != null)
            {
                childToShowScreen_Logo.Data_goal_Home = Home_NOScore.Text;
            }
        }

        private void GoalAwayPlus_button_Click(object sender, EventArgs e)
        {
            counter_away++;
            Away_NOScore.Text = counter_away.ToString();
            if (childToShowScreen_Logo != null)
            {
                childToShowScreen_Logo.Data_goal_Away = Away_NOScore.Text;
            }
        }

        private void GoalAwayNeg_button_Click(object sender, EventArgs e)
        {
            counter_away--;
            Away_NOScore.Text = counter_away.ToString();
            if (childToShowScreen_Logo != null)
            {
                childToShowScreen_Logo.Data_goal_Away = Away_NOScore.Text;
            }
        }

        private void StartTime_button_Click(object sender, EventArgs e)
        {
            counter.Start();
        }

        private void StopTime_button_Click(object sender, EventArgs e)
        {
            counter.Stop();
        }

        private void ResetTime_button_Click(object sender, EventArgs e)
        {
            if (textBox4.Text == "")
            {
                textBox4.Text = "00";
            }
            if (textBox5.Text == "")
            {
                textBox5.Text = "00";
            }
            try
            {
                sec_counter = Convert.ToInt32(textBox4.Text);
                if (sec_counter > 60)
                {
                    sec_counter = 60;
                    textBox4.Text = "60";
                    counter.Stop();
                    MessageBox.Show("Error input,the max input of second is 60");
                }
                min_counter = Convert.ToInt32(textBox5.Text);
                if (min_counter > 90)
                {
                    min_counter = 90;
                    textBox5.Text = "90";
                    counter.Stop();
                    MessageBox.Show("Error input,the max input of minutes is 90");
                }

                Time_Label.Text = String.Format("{0}:{1}", min_counter.ToString().PadLeft(2, '0'), sec_counter.ToString().PadLeft(2, '0'));
            }
            catch (Exception error)
            {
                MessageBox.Show("Error input, please insure your input just INTEGER NUMBERS");

            }
        }

        private void NameEvent_button_Click(object sender, EventArgs e)
        {

        }

        private void NameHome_button_Click(object sender, EventArgs e)
        {

        }

        private void NameAway_button_Click(object sender, EventArgs e)
        {

        }

    }
}
