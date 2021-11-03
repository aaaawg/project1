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
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
            open(new Broadcast());
            Text = "방송";
        }

        private Form form1 = null;
        private void open(Form form)
        {
            if (form1 != null)
                form1.Close();
            form1 = form;
            form.TopLevel = false;
            form.FormBorderStyle = FormBorderStyle.None;
            form.Dock = DockStyle.Fill;
            panel2.Controls.Add(form);
            form.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            open(new Broadcast());
            Text = "방송";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            open(new Movie());
            Text = "영화";
        }

        private void button3_Click(object sender, EventArgs e)
        {
            open(new Subscription());
            Text = "구독";
        }

        private void label1_Click(object sender, EventArgs e)
        {
            this.Close(); 
            Application.Exit();
        }
    }
}
