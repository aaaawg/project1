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
    public partial class AddMovie : Form
    {
        Movie mo;
        public AddMovie(Movie movie)
        {
            InitializeComponent();
            mo = movie;
            comboBox1.SelectedIndex = 0;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string date;
            if (comboBox1.Text == "기대")
                date = dateTimePicker1.Value.ToString("yyyy-MM");
            else
                date = dateTimePicker1.Value.ToString("yyyy-MM-dd");

            if (checkBox1.Checked)
                date = "미정";
            if (checkBox2.Checked)
                textBox3.Text = "미정";

            if (textBox1.Text.Trim() == "" || textBox2.Text.Trim() == "" || textBox3.Text.Trim() == ""
                || textBox4.Text.Trim() == "")
            {
                label8.ForeColor = Color.White;
                label8.Text = "필수 정보를 입력해주세요.";
            }
            else
            {
                try
                {
                    if (DataManager.Videos.Exists((x) => x.Title == textBox2.Text && x.Actor == textBox3.Text && x.Date == date && x.UserId == User.LoginId))
                    {
                        label8.ForeColor = Color.White;
                        label8.Text = "이미 존재하는 작품입니다.";
                    }
                    else
                    {
                        Video video = new Video()
                        {
                            Category = "영화",
                            TypeA = comboBox1.Text,
                            Nation = textBox1.Text,
                            Title = textBox2.Text,
                            Date = date,
                            Actor = textBox3.Text,
                            OTT = textBox4.Text,
                            Rating = (double)numericUpDown1.Value,
                            Comment = textBox5.Text,
                            Delete = false,
                            UserId = User.LoginId
                        };
                        DataManager.Videos.Add(video);
                        DataManager.Save();
                        mo.refresh();
                        this.Close();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void comboBox1_TextChanged(object sender, EventArgs e)
        {
            if (comboBox1.Text == "기대")
            {
                label4.Text = "개봉";
                textBox4.ReadOnly = true;
                textBox5.ReadOnly = true;
                textBox4.BackColor = Color.Silver;
                textBox5.BackColor = Color.Silver;
                checkBox1.Enabled = true;
                checkBox2.Enabled = true;
                textBox4.Text = "-";
                dateTimePicker1.CustomFormat = "yyyy-MM";
                numericUpDown1.Enabled = false;
            }
            else
            {
                label4.Text = "시청";
                textBox4.ReadOnly = false;
                textBox5.ReadOnly = false;
                textBox4.BackColor = Color.White;
                textBox5.BackColor = Color.White;
                checkBox1.Checked = false;
                checkBox2.Checked = false;
                checkBox1.Enabled = false;
                checkBox2.Enabled = false;
                textBox4.Text = "";
                numericUpDown1.Enabled = true;
                dateTimePicker1.CustomFormat = "yyyy-MM-dd";
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
                dateTimePicker1.Enabled = false;
            else
            {
                dateTimePicker1.BackColor = Color.White;
                dateTimePicker1.Enabled = true;
            }
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox2.Checked)
            {
                textBox3.BackColor = Color.Silver;
                textBox3.Enabled = false;
            }
            else
            {
                textBox3.BackColor = Color.White;
                textBox3.Enabled = true;
            }
        }
    }
}
