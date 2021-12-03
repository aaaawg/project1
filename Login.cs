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
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent(); 
        }
        //aasdads
        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text.Trim() == "")
            {
                label5.ForeColor = Color.White;
                label5.Text = "아이디를 입력해주세요.";

            }
            else if (textBox2.Text.Trim() == "")
            {
                label5.ForeColor = Color.White;
                label5.Text = "비밀번호를 입력해주세요.";
            }
            else
            {
                if (DataManager.Users.Exists((x) => x.Id == textBox1.Text && x.Passwd == textBox2.Text))
                {
                    User.LoginId = textBox1.Text;
                    new Main().ShowDialog();
                }
                else
                {
                    label5.ForeColor = Color.White;
                    label5.Text = "아이디 또는 비밀번호가 틀렸습니다.";
                }
            }
        }

        private void label4_Click(object sender, EventArgs e)
        {
            new Join().ShowDialog();
        }
    }
}
