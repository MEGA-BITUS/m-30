using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using System.Collections.Specialized;

namespace M_30
{
    public partial class Form1 : Form
    {
        DataTable mTable = new DataTable();
        CheckBox[] checkBox = new CheckBox[1];
        string mConnString = "server=sql5.freemysqlhosting.net;uid=sql579515;" + "pwd=gE9%aF5!;database=sql579515;";
        public Form1()
        {
            InitializeComponent();
            loadDatabase();
            int y = 30;
            if (checkBox.Length < mTable.Rows.Count) Array.Resize(ref checkBox, mTable.Rows.Count);
            for (int i = 0; i < mTable.Rows.Count; i++)
            {
                create_checks(i, nameGetter(i), y);
                y += 20;
            }
        }

        private void loadDatabase()
        {
            try
            {
                string Query = "select * from Programs;";
                MySqlConnection MyConn2 = new MySqlConnection(mConnString);
                MySqlCommand MyCommand2 = new MySqlCommand(Query, MyConn2);
                MySqlDataAdapter MyAdapter = new MySqlDataAdapter();
                MyAdapter.SelectCommand = MyCommand2;
                MyAdapter.Fill(mTable);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void addToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form2 frm = new Form2();
            frm.Show();
        }

        private string nameGetter(int x)
        {
            return mTable.Rows[x]["Name"].ToString();
        }

        private string typeGetter(int x)
        {
            return mTable.Rows[x]["Type"].ToString();
        }

        private string LinkGetter(int x)
        {
            return mTable.Rows[x]["Link"].ToString();
        }

        private void create_checks(int nr, string name, int top)
        {
            checkBox[nr] = new CheckBox();
            checkBox[nr].AutoSize = true;
            checkBox[nr].Name = name;
            checkBox[nr].Text = name;
            checkBox[nr].Location = new Point(12, top);
            checkBox[nr].CheckedChanged += new System.EventHandler(this.checkBox_ChangeEvent);
            checkBox[nr].Tag = "false";
            this.Controls.Add(checkBox[nr]);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
        }

        private void refresToolStripMenuItem_Click(object sender, EventArgs e)
        {
            mTable.Reset();
            for (int i = 0; i < mTable.Rows.Count; i++) checkBox[i].Dispose();
            loadDatabase();
            int y = 30;
            if (checkBox.Length < mTable.Rows.Count) Array.Resize(ref checkBox, mTable.Rows.Count);
            for (int i = 0; i < mTable.Rows.Count; i++)
            {
                create_checks(i, nameGetter(i), y);
                y += 20;
            }
        }

        private void checkBox_ChangeEvent(object sender, EventArgs e)
        {
            for (int i = 0; i < mTable.Rows.Count; i++)
            {
                if (checkBox[i].Checked == true && checkBox[i].Tag.ToString() == "false")
                {
                    Download down = new Download(LinkGetter(i), nameGetter(i));
                    down.Show();
                    checkBox[i].Tag = "true";
                }
            }
        }
    }
}
