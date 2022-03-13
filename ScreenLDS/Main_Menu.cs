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
    public partial class Main_Menu : Form
    {
        public static Main_Menu self;
        /////////////////// for new window ///////////////////
        private Setting childToSetting = new Setting();
        private ShowScreen_Logo childToShowScreen_Logo;
        private ShowScreen_NoLogo childToShowScreen_NoLogo;
        ///////////////////Back button make dynamic-variables(array) with counter///////////////////////////////////
        private int PreviousPageCounter { get; set; }
        Form[] s = new Form[10000000];
        ///////////////////////SET&GET////////////////////////////
        public string Data_ClockShape { get; set; }
        public string Data_LogoBorderColor { get; set; }
        public Image BackgroundClock { get; set; }
        public bool flag = true;

        /// /////////////////////ALL values for Clock/////////////////////////////
        Timer counter = new Timer();// this used for counter match timer
        int sec_counter, min_counter;

        Timer t = new Timer();// this used for clock timer
        private new int Width = 201, Height = 201; // Most be Same size pictireBox to fit backgruond clock picture
        private Bitmap bmp;

        private Graphics g;

        private float rad;
        private PointF cen;
        private Color handColor;
        private Color handColorWhite = Color.White;// hand Clock of hour and min
        private Color handColorBlack = Color.Black;// hand Clock of hour and min
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
        int Plenty_Home = 0;
        int Plenty_Away = 0;

        public Main_Menu()
        {
            InitializeComponent();

            self = this;
        }
    

        public static class FormState
        {
            public static Form PreviousPage;
        }

        private void DataSendToMain()
        {
            
        }


        private void frmParent_Shown(object sender, EventArgs e)
        {
           // childToSetting = new Setting();
            childToShowScreen_Logo = new ShowScreen_Logo();
            childToShowScreen_NoLogo = new ShowScreen_NoLogo();
        }

        private void Main_Menu_Load(object sender, EventArgs e)
        {
            //childToSetting.DataSent += Datasent;
            /// /////////////////////Transparent Background///////////////////////////
            Upper_panel.BackColor = Color.FromArgb(175, 175, 175, 175);
            Side_panel.BackColor = Color.FromArgb(175, 175, 175, 175);
            Main_panel.BackColor = Color.FromArgb(0, 175, 175, 175); //V2.3 change vlaues from Transparent to not Transparent
            /// /////////////////////Transparent Title and teams names///////////////////////////
            BackgroundTitle_label.BackColor = Color.FromArgb(175, 100, 100, 100);
            BackgroundTeams_label.BackColor = Color.FromArgb(175, 100, 100, 100);
            /// /////////////////////Transparent Timer and Scores///////////////////////////
            BackgroundTimer_label.BackColor = Color.FromArgb(175, 100, 100, 100);
            BackgroundScores_label.BackColor = Color.FromArgb(175, 100, 100, 100);
            /// /////////////////////Transparent Background///////////////////////////
            Clock_pictureBox.BackColor = Color.FromArgb(100, 100, 100, 100);
            Home_pictureBox.BackColor = Color.FromArgb(0, 175, 175, 175); //V2.3 change vlaues from Transparent to not Transparent
            Away_pictureBox.BackColor = Color.FromArgb(0, 175, 175, 175); //V2.3 change vlaues from Transparent to not Transparent
            ExtraTime_checkBox.BackColor = Color.FromArgb(100, 175, 175, 175);
            /// /////////////////////ALL values for Clock/////////////////////////////
            size2 = (Width * .78f) / (2);
            size1 = (Height * .9f) / (2);
            rad = Width / 2;
            cen = new PointF(Width / 2, Height / 2);
            ///////////////Timer Match//////////////////////
            counter.Interval = 994;
            counter.Tick += new EventHandler(this.OnTimeEvent);
            ////////////////////////////////////////////////

            bmp = new Bitmap(Width + 1, Height + 1);

            ///////////////Timer Clock//////////////////////
            t.Interval = 1000;
            t.Tick += new EventHandler(this.t_Tick);
            t.Start();

            //label2.Text = Data_ClockShape;

            //////////////Change sharpe Clock//////////////
            DataSendToMain();
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
            if (BackgroundClock == Properties.Resources.Clock1w || BackgroundClock == Properties.Resources.Clock1w || BackgroundClock == Properties.Resources.Clock1w || Clock_pictureBox.BackgroundImage == Properties.Resources.Clock1w)
            {
                handColor = handColorBlack;
            }
            else
            {
                handColor = handColorWhite;
            }

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
            if (childToShowScreen_Logo != null)
            {
               childToShowScreen_Logo.Data_Clock = Clock_pictureBox.Image;
            }
            if (childToShowScreen_NoLogo != null)
            {
                childToShowScreen_NoLogo.Data_Clock = Clock_pictureBox.Image;
            }

            g.Dispose();
        }

        private void OnTimeEvent(Object sender, EventArgs e)
        {
            
                sec_counter++;

                if (ExtraTime_checkBox.CheckState != CheckState.Unchecked)
                {
                    if (min_counter < 10 && sec_counter > 0)
                    {
                        Round_label.Text = "E.FH";
                    }
                    else
                    {
                        Round_label.Text = "E.SH";
                    }
                    if (sec_counter >= 60)
                    {
                        sec_counter = 0;
                        min_counter++;
                    }
                    if (min_counter == 10 && sec_counter == 0 || min_counter >= 20)
                    {
                        if (min_counter > 20 && sec_counter >= 0)
                        {
                            counter.Stop();
                            min_counter = 20;
                            sec_counter = 0;
                            MessageBox.Show("Error , Can't counter more then 30");
                        }
                        counter.Stop();
                    }
                }
                else
                {
                    if (min_counter < 35 && sec_counter > 0)
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
                    if (min_counter == 35 && sec_counter == 0 || min_counter >= 70)
                    {
                        if (min_counter > 70 && sec_counter >= 0)
                        {
                            counter.Stop();
                            min_counter = 70;
                            sec_counter = 0;
                            MessageBox.Show("Error , Can't counter more then 90");
                        }
                        counter.Stop();
                    }
                }

                Time_Label.Text = String.Format("{0}:{1}", min_counter.ToString().PadLeft(2, '0'), sec_counter.ToString().PadLeft(2, '0'));

                ////////////////////FOR GET & SET FORM3////////////////////
                if (childToShowScreen_Logo != null)
                {
                    childToShowScreen_Logo.Data__Name_counter = Round_label.Text;
                    childToShowScreen_Logo.Data_counter_Time = Time_Label.Text;
                }
                if (childToShowScreen_NoLogo != null)
                {
                    childToShowScreen_NoLogo.Data__Name_counter = Round_label.Text;
                    childToShowScreen_NoLogo.Data_counter_Time = Time_Label.Text;
                }
                //////////////////////////////////////////////////////////
            
        }

        private void Home_pictureBox_Paint(object sender, PaintEventArgs e)
        {

            switch (Data_LogoBorderColor)
            {
                case "No Border":
                    e.Graphics.DrawRectangle(new Pen(Color.Transparent, 0f), 0, 0, Home_pictureBox.Size.Width - 0, Home_pictureBox.Size.Height - 0);
                    break;
                case "Black":
                    e.Graphics.DrawRectangle(new Pen(Color.Black, 12f), 0, 0, Home_pictureBox.Size.Width - 2, Home_pictureBox.Size.Height - 2);
                    break;
                case "Blue":
                    e.Graphics.DrawRectangle(new Pen(Color.Blue, 12f), 0, 0, Home_pictureBox.Size.Width - 2, Home_pictureBox.Size.Height - 2);
                    break;
                case "Brown":
                    e.Graphics.DrawRectangle(new Pen(Color.Brown, 12f), 0, 0, Home_pictureBox.Size.Width - 2, Home_pictureBox.Size.Height - 2);
                    break;
                case "Gold":
                    e.Graphics.DrawRectangle(new Pen(Color.Gold, 12f), 0, 0, Home_pictureBox.Size.Width - 2, Home_pictureBox.Size.Height - 2);
                    break;
                case "Gray":
                    e.Graphics.DrawRectangle(new Pen(Color.Gray, 12f), 0, 0, Home_pictureBox.Size.Width - 2, Home_pictureBox.Size.Height - 2);
                    break;
                case "Green":
                    e.Graphics.DrawRectangle(new Pen(Color.Green, 12f), 0, 0, Home_pictureBox.Size.Width - 2, Home_pictureBox.Size.Height - 2);
                    break;
                case "Orange":
                    e.Graphics.DrawRectangle(new Pen(Color.Orange, 12f), 0, 0, Home_pictureBox.Size.Width - 2, Home_pictureBox.Size.Height - 2);
                    break;
                case "Pink":
                    e.Graphics.DrawRectangle(new Pen(Color.Pink, 12f), 0, 0, Home_pictureBox.Size.Width - 2, Home_pictureBox.Size.Height - 2);
                    break;
                case "Red":
                    e.Graphics.DrawRectangle(new Pen(Color.Red, 12f), 0, 0, Home_pictureBox.Size.Width - 2, Home_pictureBox.Size.Height - 2);
                    break;
                case "Silver":
                    e.Graphics.DrawRectangle(new Pen(Color.Silver, 12f), 0, 0, Home_pictureBox.Size.Width - 2, Home_pictureBox.Size.Height - 2);
                    break;
                case "White":
                    e.Graphics.DrawRectangle(new Pen(Color.White, 12f), 0, 0, Home_pictureBox.Size.Width - 2, Home_pictureBox.Size.Height - 2);
                    break;
                case "Yellow":
                    e.Graphics.DrawRectangle(new Pen(Color.Yellow, 12f), 0, 0, Home_pictureBox.Size.Width - 2, Home_pictureBox.Size.Height - 2);
                    break;
                default:
                    break;
            }           
        }

        private void PictureHome_button_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "(*.jpg; *.jpeg; *.gif; *.bmp; *.png)|*.jpg; *.jpeg; *.gif; *.bmp; *.png";

            if (ofd.ShowDialog() == DialogResult.OK && ofd.FileName.Length > 0)
            {
                Home_pictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
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
            if (childToShowScreen_Logo != null)
            {
                childToShowScreen_Logo.Data_Image_Home = Home_pictureBox.Image;
            }
        }

        private void Away_pictureBox_Paint(object sender, PaintEventArgs e)
        {
            switch (Data_LogoBorderColor)
            {
                case "No Border":
                    e.Graphics.DrawRectangle(new Pen(Color.Transparent, 0f), 0, 0, Away_pictureBox.Size.Width - 0, Away_pictureBox.Size.Height - 0);
                    break;
                case "Black":
                    e.Graphics.DrawRectangle(new Pen(Color.Black, 12f), 0, 0, Away_pictureBox.Size.Width - 2, Away_pictureBox.Size.Height - 2);
                    break;
                case "Blue":
                    e.Graphics.DrawRectangle(new Pen(Color.Blue, 12f), 0, 0, Away_pictureBox.Size.Width - 2, Away_pictureBox.Size.Height - 2);
                    break;
                case "Brown":
                    e.Graphics.DrawRectangle(new Pen(Color.Brown, 12f), 0, 0, Away_pictureBox.Size.Width - 2, Away_pictureBox.Size.Height - 2);
                    break;
                case "Gold":
                    e.Graphics.DrawRectangle(new Pen(Color.Gold, 12f), 0, 0, Away_pictureBox.Size.Width - 2, Away_pictureBox.Size.Height - 2);
                    break;
                case "Gray":
                    e.Graphics.DrawRectangle(new Pen(Color.Gray, 12f), 0, 0, Away_pictureBox.Size.Width - 2, Away_pictureBox.Size.Height - 2);
                    break;
                case "Green":
                    e.Graphics.DrawRectangle(new Pen(Color.Green, 12f), 0, 0, Away_pictureBox.Size.Width - 2, Away_pictureBox.Size.Height - 2);
                    break;
                case "Orange":
                    e.Graphics.DrawRectangle(new Pen(Color.Orange, 12f), 0, 0, Away_pictureBox.Size.Width - 2, Away_pictureBox.Size.Height - 2);
                    break;
                case "Pink":
                    e.Graphics.DrawRectangle(new Pen(Color.Pink, 12f), 0, 0, Away_pictureBox.Size.Width - 2, Away_pictureBox.Size.Height - 2);
                    break;
                case "Red":
                    e.Graphics.DrawRectangle(new Pen(Color.Red, 12f), 0, 0, Away_pictureBox.Size.Width - 2, Away_pictureBox.Size.Height - 2);
                    break;
                case "Silver":
                    e.Graphics.DrawRectangle(new Pen(Color.Silver, 12f), 0, 0, Away_pictureBox.Size.Width - 2, Away_pictureBox.Size.Height - 2);
                    break;
                case "White":
                    e.Graphics.DrawRectangle(new Pen(Color.White, 12f), 0, 0, Away_pictureBox.Size.Width - 2, Away_pictureBox.Size.Height - 2);
                    break;
                case "Yellow":
                    e.Graphics.DrawRectangle(new Pen(Color.Yellow, 12f), 0, 0, Away_pictureBox.Size.Width - 2, Away_pictureBox.Size.Height - 2);
                    break;
                default:
                    break;
            }
        }

        private void PictureAway_button_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "(*.jpg; *.jpeg; *.gif; *.bmp; *.png)|*.jpg; *.jpeg; *.gif; *.bmp; *.png";

            if (ofd.ShowDialog() == DialogResult.OK && ofd.FileName.Length > 0)
            {
                Away_pictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
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
            childToShowScreen_Logo.Data_Image_Away = Away_pictureBox.Image;
        }

        private void PictureCenter_button_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "(*.jpg; *.jpeg; *.gif; *.bmp; *.png)|*.jpg; *.jpeg; *.gif; *.bmp; *.png";

            if (ofd.ShowDialog() == DialogResult.OK && ofd.FileName.Length > 0)
            {
                Center_pictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
                Center_pictureBox.Image = Image.FromFile(ofd.FileName);

                if (childToShowScreen_Logo != null)
                {
                    childToShowScreen_Logo.Data_Image_Center = Center_pictureBox.Image;
                }
                if (childToShowScreen_NoLogo != null)
                {
                    childToShowScreen_NoLogo.Data_Image_Center = Center_pictureBox.Image;
                }
            }
        }

        private void RemovePictureCenter_button_Click(object sender, EventArgs e)
        {
            Center_pictureBox.Image = null;
            childToShowScreen_Logo.Data_Image_Center = Center_pictureBox.Image;
        }

        private void PictureUpper_button_Click(object sender, EventArgs e)
        {
            
        }

        private void RemovePictureUpper_button_Click(object sender, EventArgs e)
        {
            
        }

        private void GoalHomePlus_button_Click(object sender, EventArgs e)
        {
            if (Penalty_checkBox.CheckState != CheckState.Unchecked)
            {
                counter_Home++;
                Home_NOScore.Text = counter_Home.ToString();
            }
            else
            {
                Plenty_Home++;
                Home_NOScore.Text = Plenty_Home.ToString();
            }

            if (childToShowScreen_Logo != null)
            {
                childToShowScreen_Logo.Data_goal_Home = Home_NOScore.Text;
            }
            if (childToShowScreen_NoLogo != null)
            {
                childToShowScreen_NoLogo.Data_goal_Home = Home_NOScore.Text;
            }
        }

        private void GoalHomeNeg_button_Click(object sender, EventArgs e)
        {
            if (Penalty_checkBox.CheckState != CheckState.Unchecked)
            {
                counter_Home--;
                Home_NOScore.Text = counter_Home.ToString();
            }
            else
            {
                Plenty_Home--;
                Home_NOScore.Text = Plenty_Home.ToString();
            }

            if (childToShowScreen_Logo != null)
            {
                childToShowScreen_Logo.Data_goal_Home = Home_NOScore.Text;
            }
            if (childToShowScreen_NoLogo != null)
            {
                childToShowScreen_NoLogo.Data_goal_Home = Home_NOScore.Text;
            }
        }

        private void GoalAwayPlus_button_Click(object sender, EventArgs e)
        {
            
            if (Penalty_checkBox.CheckState != CheckState.Unchecked)
            {
                counter_away++;
                Away_NOScore.Text = counter_away.ToString();
            }
            else
            {
                Plenty_Away++;
                Away_NOScore.Text = Plenty_Away.ToString();
            }

            if (childToShowScreen_Logo != null)
            {
                childToShowScreen_Logo.Data_goal_Away = Away_NOScore.Text;
            }
            if (childToShowScreen_NoLogo != null)
            {
                childToShowScreen_NoLogo.Data_goal_Away = Away_NOScore.Text;
            }
        }

        private void GoalAwayNeg_button_Click(object sender, EventArgs e)
        {
            if (Penalty_checkBox.CheckState != CheckState.Unchecked)
            {
                counter_away--;
                Away_NOScore.Text = counter_away.ToString();
            }
            else
            {
                Plenty_Away--;
                Away_NOScore.Text = Plenty_Away.ToString();
            }

            if (childToShowScreen_Logo != null)
            {
                childToShowScreen_Logo.Data_goal_Away = Away_NOScore.Text;
            }
            if (childToShowScreen_NoLogo != null)
            {
                childToShowScreen_NoLogo.Data_goal_Away = Away_NOScore.Text;
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
                if (sec_counter == 0 && min_counter == 0)
                {
                    childToShowScreen_NoLogo.Time_Label.Text = childToShowScreen_Logo.Time_Label.Text = Time_Label.Text = "00:00";
                    childToShowScreen_NoLogo.Round_label.Text = childToShowScreen_Logo.Round_label.Text = Round_label.Text = "1 st";
                }
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
            if (childToShowScreen_Logo != null)
            {
                childToShowScreen_Logo.Data_Name_Event = NameEvent_textBox.Text;
                childToShowScreen_Logo.Data_RoundNumber = RoundNumber_textBox.Text;
            }
            if (childToShowScreen_NoLogo != null)
            {
                childToShowScreen_NoLogo.Data_Name_Event = NameEvent_textBox.Text;
            }
        }

        private void NameHome_button_Click(object sender, EventArgs e)
        {
            if (childToShowScreen_Logo != null)
            {
                childToShowScreen_Logo.Data_Name_Home = Home_textBox.Text;
            }
            if (childToShowScreen_NoLogo != null)
            {
                childToShowScreen_NoLogo.Data_Name_Home = Home_textBox.Text;
            }
        }

        private void NameAway_button_Click(object sender, EventArgs e)
        {
            if (childToShowScreen_Logo != null)
            {
                childToShowScreen_Logo.Data_Name_Away = Away_textBox.Text;
            }
            if (childToShowScreen_NoLogo != null)
            {
                childToShowScreen_NoLogo.Data_Name_Away = Away_textBox.Text;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Clock_pictureBox.BackgroundImage = Properties.Resources.Clock1;
            MessageBox.Show(Data_ClockShape);
            Data_ClockShape = "1";

        }

        private void Refresh_button_Click(object sender, EventArgs e)
        {
            if (flag == true && Data_LogoBorderColor != "No Border")
            {
                Home_pictureBox.Refresh();
                Away_pictureBox.Refresh();

                flag = false;
            }
            flag = true;
        }

        private void HomePlayerName_button_Click(object sender, EventArgs e)
        {
            if (childToShowScreen_Logo != null)
            {
                if (HomePlayer1_textBox.Text != "Home Player 1" && GoalHomePlayer1textBox.Text != string.Empty)
                {
                    childToShowScreen_Logo.HomePlayer1_label.Text = HomePlayer1_textBox.Text;
                    childToShowScreen_Logo.GoalHomePlayer1_label.Text = GoalHomePlayer1textBox.Text;
                    childToShowScreen_Logo.HomePlayer1_label.Visible = true;
                    childToShowScreen_Logo.GoalHomePlayer1_label.Visible = true;
                }
                if (HomePlayer2_textBox.Text != "Home Player 2" && GoalHomePlayer2textBox.Text != string.Empty)
                {
                    childToShowScreen_Logo.HomePlayer2_label.Text = HomePlayer2_textBox.Text;
                    childToShowScreen_Logo.GoalHomePlayer2_label.Text = GoalHomePlayer2textBox.Text;
                    childToShowScreen_Logo.HomePlayer2_label.Visible = true;
                    childToShowScreen_Logo.GoalHomePlayer2_label.Visible = true;
                }
                if (HomePlayer3_textBox.Text != "Home Player 3" && GoalHomePlayer3textBox.Text != string.Empty)
                {
                    childToShowScreen_Logo.HomePlayer3_label.Text = HomePlayer3_textBox.Text;
                    childToShowScreen_Logo.GoalHomePlayer3_label.Text = GoalHomePlayer3textBox.Text;
                    childToShowScreen_Logo.HomePlayer3_label.Visible = true;
                    childToShowScreen_Logo.GoalHomePlayer3_label.Visible = true;
                }
                if (HomePlayer4_textBox.Text != "Home Player 4" && GoalHomePlayer4textBox.Text != string.Empty)
                {
                    childToShowScreen_Logo.HomePlayer4_label.Text = HomePlayer4_textBox.Text;
                    childToShowScreen_Logo.GoalHomePlayer4_label.Text = GoalHomePlayer4textBox.Text;
                    childToShowScreen_Logo.HomePlayer4_label.Visible = true;
                    childToShowScreen_Logo.GoalHomePlayer4_label.Visible = true;
                }
                if (HomePlayer5_textBox.Text != "Home Player 5" && GoalHomePlayer5textBox.Text != string.Empty)
                {
                    childToShowScreen_Logo.HomePlayer5_label.Text = HomePlayer5_textBox.Text;
                    childToShowScreen_Logo.GoalHomePlayer5_label.Text = GoalHomePlayer5textBox.Text;
                    childToShowScreen_Logo.HomePlayer5_label.Visible = true;
                    childToShowScreen_Logo.GoalHomePlayer5_label.Visible = true;
                }
            }

            if (childToShowScreen_NoLogo != null)
            {
                if (HomePlayer1_textBox.Text != "Home Player 1" && GoalHomePlayer1textBox.Text != string.Empty)
                {
                    childToShowScreen_NoLogo.HomePlayer1_label.Text = HomePlayer1_textBox.Text;
                    childToShowScreen_NoLogo.GoalHomePlayer1_label.Text = GoalHomePlayer1textBox.Text;
                    childToShowScreen_NoLogo.HomePlayer1_label.Visible = true;
                    childToShowScreen_NoLogo.GoalHomePlayer1_label.Visible = true;
                }
                if (HomePlayer2_textBox.Text != "Home Player 2" && GoalHomePlayer2textBox.Text != string.Empty)
                {
                    childToShowScreen_NoLogo.HomePlayer2_label.Text = HomePlayer2_textBox.Text;
                    childToShowScreen_NoLogo.GoalHomePlayer2_label.Text = GoalHomePlayer2textBox.Text;
                    childToShowScreen_NoLogo.HomePlayer2_label.Visible = true;
                    childToShowScreen_NoLogo.GoalHomePlayer2_label.Visible = true;
                }
                if (HomePlayer3_textBox.Text != "Home Player 3" && GoalHomePlayer3textBox.Text != string.Empty)
                {
                    childToShowScreen_NoLogo.HomePlayer3_label.Text = HomePlayer3_textBox.Text;
                    childToShowScreen_NoLogo.GoalHomePlayer3_label.Text = GoalHomePlayer3textBox.Text;
                    childToShowScreen_NoLogo.HomePlayer3_label.Visible = true;
                    childToShowScreen_NoLogo.GoalHomePlayer3_label.Visible = true;
                }
                if (HomePlayer4_textBox.Text != "Home Player 4" && GoalHomePlayer4textBox.Text != string.Empty)
                {
                    childToShowScreen_NoLogo.HomePlayer4_label.Text = HomePlayer4_textBox.Text;
                    childToShowScreen_NoLogo.GoalHomePlayer4_label.Text = GoalHomePlayer4textBox.Text;
                    childToShowScreen_NoLogo.HomePlayer4_label.Visible = true;
                    childToShowScreen_NoLogo.GoalHomePlayer4_label.Visible = true;
                }
                if (HomePlayer5_textBox.Text != "Home Player 5" && GoalHomePlayer5textBox.Text != string.Empty)
                {
                    childToShowScreen_NoLogo.HomePlayer5_label.Text = HomePlayer5_textBox.Text;
                    childToShowScreen_NoLogo.GoalHomePlayer5_label.Text = GoalHomePlayer5textBox.Text;
                    childToShowScreen_NoLogo.HomePlayer5_label.Visible = true;
                    childToShowScreen_NoLogo.GoalHomePlayer5_label.Visible = true;
                }
            }
        }

        private void RemoveHomePlayerName_button_Click(object sender, EventArgs e)
        {
            /////////////////////////////Home Player 1//////////////////////////////
            HomePlayer1_textBox.Text = "Home Player 1";
            GoalHomePlayer1textBox.Text = string.Empty;
            /////////////////////////////Home Player 2//////////////////////////////
            HomePlayer2_textBox.Text = "Home Player 2";
            GoalHomePlayer2textBox.Text = string.Empty;
            /////////////////////////////Home Player 3//////////////////////////////
            HomePlayer3_textBox.Text = "Home Player 3";
            GoalHomePlayer3textBox.Text = string.Empty;
            /////////////////////////////Home Player 4//////////////////////////////
            HomePlayer4_textBox.Text = "Home Player 4";
            GoalHomePlayer4textBox.Text = string.Empty;
            /////////////////////////////Home Player 5//////////////////////////////
            HomePlayer5_textBox.Text = "Home Player 5";
            GoalHomePlayer5textBox.Text = string.Empty;

            if (childToShowScreen_Logo != null)
            {
                /////////////////////////////Home Player 1//////////////////////////////
                childToShowScreen_Logo.GoalHomePlayer1_label.Visible = false;
                childToShowScreen_Logo.HomePlayer1_label.Visible = false;
                /////////////////////////////Home Player 2//////////////////////////////
                childToShowScreen_Logo.GoalHomePlayer2_label.Visible = false;
                childToShowScreen_Logo.HomePlayer2_label.Visible = false;
                /////////////////////////////Home Player 3//////////////////////////////
                childToShowScreen_Logo.GoalHomePlayer3_label.Visible = false;
                childToShowScreen_Logo.HomePlayer3_label.Visible = false;
                /////////////////////////////Home Player 4//////////////////////////////
                childToShowScreen_Logo.GoalHomePlayer4_label.Visible = false;
                childToShowScreen_Logo.HomePlayer4_label.Visible = false;
                /////////////////////////////Home Player 5//////////////////////////////
                childToShowScreen_Logo.GoalHomePlayer5_label.Visible = false;
                childToShowScreen_Logo.HomePlayer5_label.Visible = false;
            }

            if (childToShowScreen_NoLogo != null)
            {
                /////////////////////////////Home Player 1//////////////////////////////
                childToShowScreen_NoLogo.GoalHomePlayer1_label.Visible = false;
                childToShowScreen_NoLogo.HomePlayer1_label.Visible = false;
                /////////////////////////////Home Player 2//////////////////////////////
                childToShowScreen_NoLogo.GoalHomePlayer2_label.Visible = false;
                childToShowScreen_NoLogo.HomePlayer2_label.Visible = false;
                /////////////////////////////Home Player 3//////////////////////////////
                childToShowScreen_NoLogo.GoalHomePlayer3_label.Visible = false;
                childToShowScreen_NoLogo.HomePlayer3_label.Visible = false;
                /////////////////////////////Home Player 4//////////////////////////////
                childToShowScreen_NoLogo.GoalHomePlayer4_label.Visible = false;
                childToShowScreen_NoLogo.HomePlayer4_label.Visible = false;
                /////////////////////////////Home Player 5//////////////////////////////
                childToShowScreen_NoLogo.GoalHomePlayer5_label.Visible = false;
                childToShowScreen_NoLogo.HomePlayer5_label.Visible = false;
            }
        }

        private void AwayPlayerName_button_Click(object sender, EventArgs e)
        {
            if (childToShowScreen_Logo != null)
            {
                if (AwayPlayer1_textBox.Text != "Away Player 1" && GoalAwayPlayer1_textBox.Text != string.Empty)
                {
                    childToShowScreen_Logo.AwayPlayer1_label.Text = AwayPlayer1_textBox.Text;
                    childToShowScreen_Logo.GoalAwayPlayer1_label.Text = GoalAwayPlayer1_textBox.Text;
                    childToShowScreen_Logo.AwayPlayer1_label.Visible = true;
                    childToShowScreen_Logo.GoalAwayPlayer1_label.Visible = true;
                }
                if (AwayPlayer2_textBox.Text != "Away Player 2" && GoalAwayPlayer2_textBox.Text != string.Empty)
                {
                    childToShowScreen_Logo.AwayPlayer2_label.Text = AwayPlayer2_textBox.Text;
                    childToShowScreen_Logo.GoalAwayPlayer2_label.Text = GoalAwayPlayer2_textBox.Text;
                    childToShowScreen_Logo.AwayPlayer2_label.Visible = true;
                    childToShowScreen_Logo.GoalAwayPlayer2_label.Visible = true;
                }
                if (AwayPlayer3_textBox.Text != "Away Player 3" && GoalAwayPlayer3_textBox.Text != string.Empty)
                {
                    childToShowScreen_Logo.AwayPlayer3_label.Text = AwayPlayer3_textBox.Text;
                    childToShowScreen_Logo.GoalAwayPlayer3_label.Text = GoalAwayPlayer3_textBox.Text;
                    childToShowScreen_Logo.AwayPlayer3_label.Visible = true;
                    childToShowScreen_Logo.GoalAwayPlayer3_label.Visible = true;
                }
                if (AwayPlayer4_textBox.Text != "Away Player 4" && GoalAwayPlayer4_textBox.Text != string.Empty)
                {
                    childToShowScreen_Logo.AwayPlayer4_label.Text = AwayPlayer4_textBox.Text;
                    childToShowScreen_Logo.GoalAwayPlayer4_label.Text = GoalAwayPlayer4_textBox.Text;
                    childToShowScreen_Logo.AwayPlayer4_label.Visible = true;
                    childToShowScreen_Logo.GoalAwayPlayer4_label.Visible = true;
                }
                if (AwayPlayer5_textBox.Text != "Away Player 5" && GoalAwayPlayer5_textBox.Text != string.Empty)
                {
                    childToShowScreen_Logo.AwayPlayer5_label.Text = AwayPlayer5_textBox.Text;
                    childToShowScreen_Logo.GoalAwayPlayer5_label.Text = GoalAwayPlayer5_textBox.Text;
                    childToShowScreen_Logo.AwayPlayer5_label.Visible = true;
                    childToShowScreen_Logo.GoalAwayPlayer5_label.Visible = true;
                }
            }

            if (childToShowScreen_NoLogo != null)
            {
                if (AwayPlayer1_textBox.Text != "Away Player 1" && GoalAwayPlayer1_textBox.Text != string.Empty)
                {
                    childToShowScreen_NoLogo.AwayPlayer1_label.Text = AwayPlayer1_textBox.Text;
                    childToShowScreen_NoLogo.GoalAwayPlayer1_label.Text = GoalAwayPlayer1_textBox.Text;
                    childToShowScreen_NoLogo.AwayPlayer1_label.Visible = true;
                    childToShowScreen_NoLogo.GoalAwayPlayer1_label.Visible = true;
                }
                if (AwayPlayer2_textBox.Text != "Away Player 2" && GoalAwayPlayer2_textBox.Text != string.Empty)
                {
                    childToShowScreen_NoLogo.AwayPlayer2_label.Text = AwayPlayer2_textBox.Text;
                    childToShowScreen_NoLogo.GoalAwayPlayer2_label.Text = GoalAwayPlayer2_textBox.Text;
                    childToShowScreen_NoLogo.AwayPlayer2_label.Visible = true;
                    childToShowScreen_NoLogo.GoalAwayPlayer2_label.Visible = true;
                }
                if (AwayPlayer3_textBox.Text != "Away Player 3" && GoalAwayPlayer3_textBox.Text != string.Empty)
                {
                    childToShowScreen_NoLogo.AwayPlayer3_label.Text = AwayPlayer3_textBox.Text;
                    childToShowScreen_NoLogo.GoalAwayPlayer3_label.Text = GoalAwayPlayer3_textBox.Text;
                    childToShowScreen_NoLogo.AwayPlayer3_label.Visible = true;
                    childToShowScreen_NoLogo.GoalAwayPlayer3_label.Visible = true;
                }
                if (AwayPlayer4_textBox.Text != "Away Player 4" && GoalAwayPlayer4_textBox.Text != string.Empty)
                {
                    childToShowScreen_NoLogo.AwayPlayer4_label.Text = AwayPlayer4_textBox.Text;
                    childToShowScreen_NoLogo.GoalAwayPlayer4_label.Text = GoalAwayPlayer4_textBox.Text;
                    childToShowScreen_NoLogo.AwayPlayer4_label.Visible = true;
                    childToShowScreen_NoLogo.GoalAwayPlayer4_label.Visible = true;
                }
                if (AwayPlayer5_textBox.Text != "Away Player 5" && GoalAwayPlayer5_textBox.Text != string.Empty)
                {
                    childToShowScreen_NoLogo.AwayPlayer5_label.Text = AwayPlayer5_textBox.Text;
                    childToShowScreen_NoLogo.GoalAwayPlayer5_label.Text = GoalAwayPlayer5_textBox.Text;
                    childToShowScreen_NoLogo.AwayPlayer5_label.Visible = true;
                    childToShowScreen_NoLogo.GoalAwayPlayer5_label.Visible = true;
                }
            }
        }

        private void RemoveAwayPlayerName_button_Click(object sender, EventArgs e)
        {
            /////////////////////////////Away Player 1//////////////////////////////
            AwayPlayer1_textBox.Text = "Away Player 1";
            GoalAwayPlayer1_textBox.Text = string.Empty;
            /////////////////////////////Away Player 2//////////////////////////////
            AwayPlayer2_textBox.Text = "Away Player 2";
            GoalAwayPlayer2_textBox.Text = string.Empty;
            /////////////////////////////Away Player 3//////////////////////////////
            AwayPlayer3_textBox.Text = "Away Player 3";
            GoalAwayPlayer3_textBox.Text = string.Empty;
            /////////////////////////////Away Player 4//////////////////////////////
            AwayPlayer4_textBox.Text = "Away Player 4";
            GoalAwayPlayer4_textBox.Text = string.Empty;
            /////////////////////////////Away Player 5//////////////////////////////
            AwayPlayer5_textBox.Text = "Away Player 5";
            GoalAwayPlayer5_textBox.Text = string.Empty;

            if (childToShowScreen_Logo != null)
            {
                /////////////////////////////Away Player 1//////////////////////////////
                childToShowScreen_Logo.GoalAwayPlayer1_label.Visible = false;
                childToShowScreen_Logo.AwayPlayer1_label.Visible = false;
                /////////////////////////////Away Player 2//////////////////////////////
                childToShowScreen_Logo.GoalAwayPlayer2_label.Visible = false;
                childToShowScreen_Logo.AwayPlayer2_label.Visible = false;
                /////////////////////////////Away Player 3//////////////////////////////
                childToShowScreen_Logo.GoalAwayPlayer3_label.Visible = false;
                childToShowScreen_Logo.AwayPlayer3_label.Visible = false;
                /////////////////////////////Away Player 4//////////////////////////////
                childToShowScreen_Logo.GoalAwayPlayer4_label.Visible = false;
                childToShowScreen_Logo.AwayPlayer4_label.Visible = false;
                /////////////////////////////Away Player 5//////////////////////////////
                childToShowScreen_Logo.GoalAwayPlayer5_label.Visible = false;
                childToShowScreen_Logo.AwayPlayer5_label.Visible = false;
            }

            if (childToShowScreen_NoLogo != null)
            {
                /////////////////////////////Away Player 1//////////////////////////////
                childToShowScreen_NoLogo.GoalAwayPlayer1_label.Visible = false;
                childToShowScreen_NoLogo.AwayPlayer1_label.Visible = false;
                /////////////////////////////Away Player 2//////////////////////////////
                childToShowScreen_NoLogo.GoalAwayPlayer2_label.Visible = false;
                childToShowScreen_NoLogo.AwayPlayer2_label.Visible = false;
                /////////////////////////////Away Player 3//////////////////////////////
                childToShowScreen_NoLogo.GoalAwayPlayer3_label.Visible = false;
                childToShowScreen_NoLogo.AwayPlayer3_label.Visible = false;
                /////////////////////////////Away Player 4//////////////////////////////
                childToShowScreen_NoLogo.GoalAwayPlayer4_label.Visible = false;
                childToShowScreen_NoLogo.AwayPlayer4_label.Visible = false;
                /////////////////////////////Away Player 5//////////////////////////////
                childToShowScreen_NoLogo.GoalAwayPlayer5_label.Visible = false;
                childToShowScreen_NoLogo.AwayPlayer5_label.Visible = false;
            }
        }

        private void FullScreen_button_Click(object sender, EventArgs e)
        {
            if ((Application.OpenForms["ShowScreen_Logo"]) != null)
            {
                childToShowScreen_Logo.FormBorderStyle = FormBorderStyle.None;
                childToShowScreen_Logo.WindowState = FormWindowState.Maximized;
            }
            else 
            {
                childToShowScreen_Logo.FormBorderStyle = FormBorderStyle.Sizable;
                childToShowScreen_Logo.WindowState = FormWindowState.Normal;
            }
            if ((Application.OpenForms["ShowScreen_NoLogo"]) != null)
            {
                childToShowScreen_NoLogo.FormBorderStyle = FormBorderStyle.None;
                childToShowScreen_NoLogo.WindowState = FormWindowState.Maximized;
            }
            else 
            {
                childToShowScreen_NoLogo.FormBorderStyle = FormBorderStyle.Sizable;
                childToShowScreen_NoLogo.WindowState = FormWindowState.Normal;
            }
        }

        private void Management_button_Click(object sender, EventArgs e)
        {
            if ((Application.OpenForms["Setting"]) == null)
            {
                childToSetting.ShowDialog();
            }
            else
                Application.OpenForms["Setting"].Focus();
        }

        private void Clock_pictureBox_Click(object sender, EventArgs e)
        {
            
        }

        private void ExtraTime_checkBox_CheckedChanged(object sender, EventArgs e)
        {
            min_counter = 0;
            sec_counter = 0;
            counter.Stop();

            Time_Label.Text = String.Format("{0}:{1}", min_counter.ToString().PadLeft(2, '0'), sec_counter.ToString().PadLeft(2, '0'));

            childToShowScreen_NoLogo.Time_Label.Text = childToShowScreen_Logo.Time_Label.Text = Time_Label.Text = "00:00";
            childToShowScreen_NoLogo.Round_label.Text = childToShowScreen_Logo.Round_label.Text = Round_label.Text = "888";
        }

        private void Plenty_checkBox_CheckedChanged(object sender, EventArgs e)
        {
            counter.Stop();

            if (Penalty_checkBox.CheckState != CheckState.Unchecked)
            {
                Home_NOScore.Text = counter_Home.ToString();
                Away_NOScore.Text = counter_away.ToString();
            }
            else
            {
                Home_NOScore.Text = Plenty_Home.ToString();
                Away_NOScore.Text = Plenty_Away.ToString();
            }
        }

        private void Round_label_Click(object sender, EventArgs e)
        {

        }

        private void Time_Label_Click(object sender, EventArgs e)
        {

        }

        private void Home_NOScore_Click(object sender, EventArgs e)
        {

        }

        private void PenaltyHome3_label_Click(object sender, EventArgs e)
        {

        }

        private void PenaltyHome5_label_Click(object sender, EventArgs e)
        {

        }

        private void PenaltyHome1_label_Click(object sender, EventArgs e)
        {

        }

        private void Home_pictureBox_Click(object sender, EventArgs e)
        {

        }

        private void ShowScreenLogo_button_Click(object sender, EventArgs e)
        {
            childToShowScreen_Logo.BackgroundImage = BackgroundImage;

            try
            {
                childToShowScreen_Logo = new ShowScreen_Logo();
                childToShowScreen_Logo.Location = Screen.AllScreens[1].WorkingArea.Location;
                if ((Application.OpenForms["Setting"]) == null)
                {
                    childToShowScreen_Logo.Show();
                }
            }
            catch (Exception error)
            {
                childToShowScreen_Logo = new ShowScreen_Logo();
                childToShowScreen_Logo.Location = Screen.AllScreens[0].WorkingArea.Location;
                if ((Application.OpenForms["Setting"]) == null)
                {
                    childToShowScreen_Logo.Show();
                }
            }
        }

        private void ShowScreenNoLogo_bottun_Click(object sender, EventArgs e)
        {
            childToShowScreen_NoLogo.BackgroundImage = BackgroundImage;

            try
            {
                childToShowScreen_NoLogo = new ShowScreen_NoLogo();
                childToShowScreen_NoLogo.Location = Screen.AllScreens[1].WorkingArea.Location;
                if ((Application.OpenForms["Setting"]) == null)
                {
                    childToShowScreen_NoLogo.Show();
                }
            }
            catch (Exception error)
            {
                childToShowScreen_NoLogo = new ShowScreen_NoLogo();
                childToShowScreen_NoLogo.Location = Screen.AllScreens[0].WorkingArea.Location;
                if ((Application.OpenForms["Setting"]) == null)
                {
                    childToShowScreen_NoLogo.Show();
                }
            }
        }


    }
}
