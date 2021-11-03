using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp4
{
    public partial class Subscription : Form
    {
        public Subscription()
        {
            InitializeComponent();
            refresh();
        }
        private void refresh()
        {
            dataGridView1.DataSource = null;
            dataGridView1.DataSource = DataManager.Subs.Where((x) =>
            {
                return x.UserId == User.LoginId;
            }).ToList();
            label2.Text = DataManager.Subs.Where((x) =>
            {
                return x.UserId == User.LoginId;
            }).Count().ToString() + "개 플랫폼 구독 중";
            label3.Text = "총 " + DataManager.Subs.Where(x => x.UserId == User.LoginId).Sum(x => x.Money).ToString()
                + "원 지출 중";
        }

        private void dataGridView1_CurrentCellChanged(object sender, EventArgs e)
        {
            try
            {
                Sub sub = dataGridView1.CurrentRow.DataBoundItem as Sub;
                textBox1.Text = sub.Platform;
                dateTimePicker1.Value = sub.Sdate;
                textBox2.Text = sub.Money.ToString();
                textBox3.Text = sub.Memo;
                textBox4.Text = sub.Link;

                TimeSpan day = DateTime.Now - sub.Sdate;
                int dday = (int)day.TotalDays + 1;
                label10.Text = sub.Platform + " 구독한지 " + dday.ToString() + "일 째";
            }
            catch (Exception)
            {

            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text.Trim() == "") 
            {
                label9.ForeColor = Color.White;
                label9.Text = "플랫폼을 입력해주세요.";
            }
            else if (textBox2.Text.Trim() == "")
            {
                label9.ForeColor = Color.White;
                label9.Text = "구독료를 입력해주세요.";
            }
            else
            {
                try
                {
                    if (DataManager.Subs.Exists(x => x.Platform == textBox1.Text && x.UserId == User.LoginId))
                    {
                        label9.ForeColor = Color.White;
                        label9.Text = "중복된 플랫폼입니다.";
                    }
                    else
                    {
                        string date= dateTimePicker1.Value.ToString("yyyy-MM-dd");
                        Sub subscription = new Sub()
                        {
                            Platform = textBox1.Text,
                            Sdate = Convert.ToDateTime(date),
                            Money = int.Parse(textBox2.Text),
                            Memo = textBox3.Text,
                            Link = textBox4.Text,
                            UserId = User.LoginId
                        };
                        DataManager.Subs.Add(subscription);
                        refresh();
                        DataManager.Save();
                        MessageBox.Show(subscription.Platform + " 플랫폼이 추가되었습니다.");
                    }         
                }
                catch (Exception)
                {
                    label9.ForeColor = Color.White;
                    label9.Text = "구독료에 숫자만 입력해주세요.";
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox1.Text.Trim() == "")
            {
                label9.ForeColor = Color.White;
                label9.Text = "플랫폼을 입력해주세요.";
            }
            else if (textBox2.Text.Trim() == "")
            {
                label9.ForeColor = Color.White;
                label9.Text = "구독료를 입력해주세요.";
            }
            else
            {
                try
                {
                    string date = dateTimePicker1.Value.ToString("yyyy-MM-dd");
                    Sub s = dataGridView1.CurrentRow.DataBoundItem as Sub;
                    if (s.Platform != textBox1.Text)
                    {
                        label9.ForeColor = Color.White;
                        label9.Text = "플랫폼은 수정할 수 없습니다.";
                    }
                    else
                    {
                        Sub sub = DataManager.Subs.Single(x => x.UserId == User.LoginId && x.Platform == textBox1.Text);
                        sub.Sdate = Convert.ToDateTime(date);
                        sub.Money = int.Parse(textBox2.Text);
                        sub.Memo = textBox3.Text;
                        sub.Link = textBox4.Text;

                        refresh();
                        DataManager.Save();
                        MessageBox.Show(sub.Platform + " 정보가 수정되었습니다.");
                    }
                }
                catch (Exception)
                {
                    MessageBox.Show("존재하지 않는 플랫폼입니다.");
                } 
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                Sub sub = DataManager.Subs.Single(x => x.UserId == User.LoginId && x.Platform == textBox1.Text);
                DataManager.Subs.Remove(sub);
                refresh();
                DataManager.Save();

                textBox1.Clear();
                textBox2.Clear();
                textBox3.Clear();
                textBox4.Clear();
                dateTimePicker1.Value = DateTime.Now;
                label10.Text = "";
                MessageBox.Show(sub.Platform + " 정보가 삭제되었습니다.");
                dataGridView1.ClearSelection();
            }
            catch (Exception)
            {
                MessageBox.Show("삭제할 플랫폼을 선택해 주세요.");
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1 || e.ColumnIndex == -1) return;
            label9.Text = "";
            label10.ForeColor = Color.White;
            
            Sub sub = dataGridView1.CurrentRow.DataBoundItem as Sub;
            string link = sub.Link.ToString();
            
            if (dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex] is DataGridViewLinkCell)
            {
                if (link.Trim() == "")
                {
                    label9.ForeColor = Color.White;
                    label9.Text = "입력된 주소가 없습니다.";
                }
                else if (link.Contains("http://") && link.Contains(".com"))
                    Process.Start(sub.Link.ToString());
                else if (link.Contains("www.") && link.Contains(".com"))
                    Process.Start(sub.Link.ToString());
                else
                {
                    label9.ForeColor = Color.White;
                    label9.Text = "이동할 수 없는 주소입니다.";
                }
            }
        }

        private void Subscription_Shown(object sender, EventArgs e)
        {
            dataGridView1.ClearSelection();
        }
    }
}
