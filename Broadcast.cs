using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp4
{
    public partial class Broadcast : Form
    {
        public Broadcast()
        {
            InitializeComponent();
            brRefresh();
        }

        public void brRefresh()
        {
            dataGridView1.DataSource = null;
            dataGridView1.DataSource = DataManager.Videos.Where((x) =>
            {
                return x.Category == "방송" && x.TypeA == "시청" && x.UserId == User.LoginId;
            }).ToList();
            dataGridView2.DataSource = null;
            dataGridView2.DataSource = DataManager.Videos.Where((x) =>
            {
                return x.Category == "방송" && x.TypeA == "기대" && x.UserId == User.LoginId;
            }).ToList();
            panel1.Visible = false;
            dataGridView1.ClearSelection();
            comboBox1.SelectedIndex = 0;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            AddBroadcast addBroadcast = new AddBroadcast(this);
            addBroadcast.Show();
        }

        private void dataGridView1_CurrentCellChanged(object sender, EventArgs e)
        {
            try
            {   
                panel1.Visible = true;
                Video video = dataGridView1.CurrentRow.DataBoundItem as Video;
                label2.Text = video.Title;
                if (video.TypeB == "애니메이션")
                    label3.Text = video.TypeB + "\n" + video.Nation + " | " + video.Actor;
                else
                    label3.Text = video.TypeB + " | " + video.Nation + " | " + video.Actor;
                label4.Text = video.Date;
                textBox1.Text = video.Comment;
                numericUpDown1.Value = (decimal)video.Rating;
            }
            catch (Exception)
            {
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                DataManager.Videos.RemoveAll((x) => x.Delete == true && x.UserId == User.LoginId);
                brRefresh();
                DataManager.Save();
            }
            catch (Exception)
            {
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                Video video = DataManager.Videos.Single((x) => x.UserId == User.LoginId && x.Title == label2.Text
                && x.Date == label4.Text && x.Category == "방송");
                video.Rating = (double)numericUpDown1.Value;
                video.Comment = textBox1.Text;
                DataManager.Save();
                brRefresh();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            double rating = (double)numericUpDown1.Value;
            PictureBox[] pictureBox = new PictureBox[5];
            pictureBox[0] = pictureBox1;
            pictureBox[1] = pictureBox2;
            pictureBox[2] = pictureBox3;
            pictureBox[3] = pictureBox4;
            pictureBox[4] = pictureBox5;
            for (int i = 0; i < 5; i++)
            {
                if (i < rating - 0.5)
                {
                    pictureBox[i].Image = Image.FromFile(@"./image/star1.png");
                }
                else if (rating % 0.5 == 0 && rating - 0.5 == i)
                {
                    pictureBox[i].Image = Image.FromFile(@"./image/star2.png");
                }
                else
                    pictureBox[i].Image = Image.FromFile(@"./image/star3.png");
            }
        }

        private void Broadcast_Shown(object sender, EventArgs e)
        {
            panel1.Visible = false; 
            dataGridView1.ClearSelection();
            dataGridView2.ClearSelection();
        }

        private void comboBox1_TextChanged(object sender, EventArgs e)
        {
            dataGridView1.DataSource = null;
            if (comboBox1.Text == "드라마")
            {               
                dataGridView1.DataSource = DataManager.Videos.Where((x) =>
                {
                    return x.Category == "방송" && x.TypeA == "시청" && x.TypeB == "드라마" && x.UserId == User.LoginId;
                }).ToList();
            }
            else if (comboBox1.Text == "애니메이션")
            {
                dataGridView1.DataSource = DataManager.Videos.Where((x) =>
                {
                    return x.Category == "방송" && x.TypeA == "시청" && x.TypeB == "애니메이션" && x.UserId == User.LoginId;
                }).ToList();
            }
            else if (comboBox1.Text == "기타")
            {
                dataGridView1.DataSource = DataManager.Videos.Where((x) =>
                {
                    return x.Category == "방송" && x.TypeA == "시청" && x.TypeB == "기타" && x.UserId == User.LoginId;
                }).ToList();
            }
            else
                brRefresh();
            panel1.Visible = false;
            dataGridView1.ClearSelection();
        }
    }
}
