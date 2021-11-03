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
    public partial class Join : Form
    {
        public Join()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text.Trim() == "")
            {
                label6.ForeColor = Color.White;
                label6.Text = "이름을 입력해주세요.";
            }
            else if (textBox2.Text.Trim() == "")
            {
                label6.ForeColor = Color.White;
                label6.Text = "아이디를 입력해주세요.";
            }
            else if (textBox3.Text.Trim() == "")
            {
                label6.ForeColor = Color.White;
                label6.Text = "비밀번호를 입력해주세요.";
            }
            else
            {
                try
                {
                    if (DataManager.Users.Exists((x) => x.Id == textBox2.Text))
                    {
                        label6.ForeColor = Color.White;
                        label6.Text = "사용할 수 없는 아이디입니다.";
                    }
                    else
                    {
                        User user = new User()
                        {
                            Name = textBox1.Text,
                            Id = textBox2.Text,
                            Passwd = textBox3.Text,
                            Email = textBox4.Text
                        };
                        DataManager.Users.Add(user);
                        DataManager.Save();
                        this.Close();
                    }
                }
                catch (Exception)
                {

                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
