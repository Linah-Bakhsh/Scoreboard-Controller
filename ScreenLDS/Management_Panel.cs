using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ScreenLDS
{
    public partial class Management_Panel : Form
    {
        /////////////////// for new window ///////////////////
        private Main_Menu childMain_Menu;
        public Management_Panel()
        {
            InitializeComponent();
        }

        private void ManagementParent_Shown(object sender, EventArgs e)
        {
            childMain_Menu = new Main_Menu();
        }

        private void Management_Panel_Load(object sender, EventArgs e)
        {
            populate();

            foreach (FontFamily font in FontFamily.Families)
            {
                FontTimer_comboBox.Items.Add(font.Name.ToString());
                FontTitle_comboBox.Items.Add(font.Name.ToString());
                FontTeams_comboBox.Items.Add(font.Name.ToString());
            }
        }

        private void populate()
        {
            listViewClock.Items.Add("1", 0);
            listViewClock.Items.Add("2", 1);
            listViewClock.Items.Add("3", 2);
            listViewClock.Items.Add("4", 3);
            listViewClock.Items.Add("5", 4);
            listViewClock.Items.Add("6", 5);
            listViewClock.Items.Add("7", 6);
            listViewClock.Items.Add("8", 7);
        }

        private void AcceptClock_button_Click(object sender, EventArgs e)
        {
            childMain_Menu = new Main_Menu();
            
            if (childMain_Menu != null)
            {
                try
                {
                    //childMain_Menu.Data_ClockShape = listViewClock.SelectedItems[0].SubItems[0].Text;
                    //Main_Menu.self.Data_ClockShape = listViewClock.SelectedItems[0].SubItems[0].Text;
                   // MessageBox.Show(Main_Menu.self.Data_ClockShape + " " + childMain_Menu.Data_ClockShape);
                }
                catch
                {
                    MessageBox.Show("Your account Banned, contact to supervisor.", "Alert", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void ChangeBackground_button_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "(*.jpg; *.jpeg; *.gif; *.bmp; *.png)|*.jpg; *.jpeg; *.gif; *.bmp; *.png";

            if (ofd.ShowDialog() == DialogResult.OK && ofd.FileName.Length > 0)
            {
                Background_pictureBox.SizeMode = PictureBoxSizeMode.Zoom;
                Background_pictureBox.Image = Image.FromFile(ofd.FileName);
            }
        }

        private void FontTimer_comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                TestFontTimer_label.Font = new Font(FontTimer_comboBox.Text, TestFontTimer_label.Font.Size);
            }
            catch { }
        }

        private void SizeTimer_comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            TestFontTimer_label.Font = new Font(FontTimer_comboBox.Font.FontFamily, float.Parse(SizeTimer_comboBox.SelectedItem.ToString()));
        }

        private void FontTitle_comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                TestFontTitle_label.Font = new Font(FontTitle_comboBox.Text, TestFontTitle_label.Font.Size);
            }
            catch { }
        }

        private void TitleSize_comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            TestFontTitle_label.Font = new Font(FontTitle_comboBox.Font.FontFamily, float.Parse(TitleSize_comboBox.SelectedItem.ToString()));
        }

        private void FontTeams_comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                TestFontTeams_label.Font = new Font(FontTeams_comboBox.Text, TestFontTeams_label.Font.Size);
            }
            catch { }
        }

        private void TeamSize_comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            TestFontTeams_label.Font = new Font(TeamSize_comboBox.Font.FontFamily, float.Parse(TeamSize_comboBox.SelectedItem.ToString()));
        }

        private void AcceptBackground_button_Click(object sender, EventArgs e)
        {
            //childShowScreen_Logo.Data_Image_Background = Background_pictureBox.Image;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //childMain_Menu.label2.Text = "qewdrfgthyuijkouyjthrgefs";
            childMain_Menu = new Main_Menu();
            childMain_Menu.Show();
        }


    }
}
