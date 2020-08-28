using CefSharp;
using CefSharp.WinForms;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics.Eventing;
using System.Linq;
using System.Windows.Forms;

namespace View
{
    public partial class Form1 : Form
    {
        public ChromiumWebBrowser chromeBrowser;
        private const string ConnectionString = "Server=127.0.0.1;Database=ZhiHu;User Id=sa;Password=sa123456789;";
        public Form1()
        {
            InitializeComponent();
            InitializeChromium();
        }
        public void InitializeChromium()
        {
            CefSettings settings = new CefSettings();
            Cef.Initialize(settings);
            chromeBrowser = new ChromiumWebBrowser("https://www.baidu.com");
            
            this.tabPage2.Controls.Add(chromeBrowser);
            //this.Controls.Add(chromeBrowser);
            chromeBrowser.Dock = DockStyle.Fill;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            Cef.Shutdown();
        }

        private void listView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            string questionid = listView1.SelectedItems[0].SubItems[4].Text;
            string answerid = listView1.SelectedItems[0].SubItems[5].Text;
            Console.WriteLine(questionid);
            Console.WriteLine(answerid);
            chromeBrowser.Load($"https://www.zhihu.com/question/{questionid}/answer/{answerid}");
            this.tabPage2.Focus();
            this.tabControl1.SelectedIndex = 1;
            //
        }

        private void listView1_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            sort((ListView)sender, e.Column, true);
        }

        private void 刷新ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection(ConnectionString);
            conn.Open();
            SqlCommand cur = conn.CreateCommand();
            string sql = "select count(*) from Answer";
            cur.CommandText = sql;
            int count = (int)cur.ExecuteScalar();

            Random random = new Random(System.Environment.TickCount);
            int[] arr = new int[10];
            for (int i = 0; i < 10; i++)
            {
                arr[i] = random.Next(0, count);
            }

            sql = "SELECT top 100 Question.Title,Answer.Excerpt,Answer.[Voteup_Count],Answer.Question_Id,Answer.Id " +
                $"FROM[Answer], [Question] where Answer.Question_Id = Question.Id and Answer.[Index] in {Arr2Str(arr)}";

            

            cur.CommandText = sql;


            SqlDataReader readdata = cur.ExecuteReader();

            int _index = 0;
            listView1.BeginUpdate();
            listView1.Items.Clear();
            Console.WriteLine(readdata.HasRows);
            while (readdata.Read())
            {
                ListViewItem listViewItem = new ListViewItem(_index.ToString());
                listViewItem.SubItems.Add(readdata.GetString(0));
                listViewItem.SubItems.Add(readdata.GetString(1));
                listViewItem.SubItems.Add(readdata.GetInt32(2).ToString());
                listViewItem.SubItems.Add(readdata.GetInt32(3).ToString());
                listViewItem.SubItems.Add(readdata.GetInt32(4).ToString());

                listView1.Items.Add(listViewItem);
                _index++;
            }
            listView1.EndUpdate();

            cur.Dispose();
            conn.Close();
        }

        public static string Arr2Str(int[] arr)
        {
            string data = "(";
            for (int i = 0; i < arr.Length-1; i++)
            {
                data += arr[i].ToString() + ",";
            }
            return data + arr[arr.Length - 1]+")";
        }

        public static void sort(ListView obj, int Column, bool desc)
        {
            if (obj.Columns[Column].Text == "点赞数")
            {
                obj.BeginUpdate();

                List<ListViewItem> list = new List<ListViewItem>();
                System.Collections.IEnumerator enumerator = obj.Items.GetEnumerator();

                while (enumerator.MoveNext())
                {
                    ListViewItem item = (ListViewItem)enumerator.Current;
                    list.Add(item);
                }

                if (Column == 0)
                {
                    if (desc)
                    {
                        list.Sort((p1, p2) => p1.Text.CompareTo(p2.Text));
                    }
                    else
                    {
                        list.Sort((p1, p2) => p2.Text.CompareTo(p1.Text));
                    }
                }
                else
                {
                    int _column = Column;
                    if (desc)
                    {
                        list.Sort((p1, p2) => Convert.ToInt32(p2.SubItems[_column].Text).CompareTo(Convert.ToInt32(p1.SubItems[_column].Text)));
                    }
                    else
                    {
                        list.Sort((p1, p2) => Convert.ToInt32(p1.SubItems[_column].Text).CompareTo(Convert.ToInt32(p2.SubItems[_column].Text)));
                    }
                    
                    
                }

                obj.Items.Clear();
                for (int i = 0; i < list.Count; i++)
                {
                    obj.Items.Add(list[i]);
                }
                obj.EndUpdate();
            }
        }

        private void 正序ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            sort(listView1, 3, false);
        }

        private void 倒序ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            sort(listView1, 3, true);
        }
    }


}
