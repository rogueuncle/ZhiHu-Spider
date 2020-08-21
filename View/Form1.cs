using CefSharp;
using CefSharp.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.EntityFrameworkCore;


namespace View
{
    public partial class Form1 : Form
    {
        public ChromiumWebBrowser chromeBrowser;
        public Form1()
        {
            InitializeComponent();
            InitializeChromium();
        }
        public void InitializeChromium()
        {
            CefSettings settings = new CefSettings();
            // Initialize cef with the provided settings
            Cef.Initialize(settings);
            // Create a browser component
            chromeBrowser = new ChromiumWebBrowser("https://www.baidu.com");
            // Add it to the form and fill it to the form window.
            
            this.tabPage2.Controls.Add(chromeBrowser);
            //this.Controls.Add(chromeBrowser);
            chromeBrowser.Dock = DockStyle.Fill;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            using (var conn = new AnswerContext())
            {
                var list = conn.answers.ToList();
                Console.WriteLine(list.Count);
            }
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            Cef.Shutdown();
        }

        private void listView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            MessageBox.Show("1");
        }

        private void listView1_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            ListView obj = (ListView)sender;
            if(obj.Columns[e.Column].Text == "点赞数")
            {
                obj.BeginUpdate();

                List<ListViewItem> list = new List<ListViewItem>();
                System.Collections.IEnumerator enumerator = obj.Items.GetEnumerator();

                while (enumerator.MoveNext())
                {
                    ListViewItem item = (ListViewItem)enumerator.Current;
                    list.Add(item);
                }

                if (e.Column == 0)
                {
                    list.Sort((p1,p2) => p2.Text.CompareTo(p1.Text));
                }
                else
                {
                    int _column = e.Column - 1;
                    list.Sort((p1, p2) => p2.SubItems[_column].Text.CompareTo(p1.SubItems[_column].Text));
                }

                obj.Items.Clear();
                for (int i = 0; i < list.Count; i++)
                {
                    obj.Items.Add(list[i]);
                }
                obj.EndUpdate();
            }
        }
    }

    
}
