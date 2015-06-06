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

namespace M_30
{
    public partial class Form2 : Form
    {
        private MySqlConnection mConn;
        string mConnString = "server=sql5.freemysqlhosting.net;uid=sql579515;" + "pwd=gE9%aF5!;database=sql579515;";
        private void add(string name, string link, int type)
        {
            try
            {
                mConn = new MySql.Data.MySqlClient.MySqlConnection();
                mConn.ConnectionString = mConnString;
                mConn.Open();
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = mConn;
                cmd.CommandText = "INSERT INTO Programs(`Name`, `Link`, `Type`) VALUES(@Name, @Link, @Type)";
                cmd.Prepare();
                cmd.Parameters.AddWithValue("@Name", name);
                cmd.Parameters.AddWithValue("@Link", link);
                cmd.Parameters.AddWithValue("@Type", type);
                cmd.ExecuteNonQuery();
            }
            catch (MySqlException ex)
            {
                Console.WriteLine("Error: {0}", ex.ToString());
            }
            finally
            {
                if (mConn != null)
                {
                    mConn.Close();
                    MessageBox.Show("New program added!");
                    this.Close();
                }
            }
        }
        public Form2()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int x = 0;
            bool version = false, name = false, link = false;
            if (checkedListBox1.GetItemChecked(0))
            {
                x = 32;
                version = true;
            }
            else if (checkedListBox1.GetItemChecked(1))
            {
                x = 64;
                version = true;
            }
            if (textBox1.Text != null) name = true;
            if (textBox2.Text != null) link = true;
            if (version == true && name == true && link == true) add(textBox1.Text, textBox2.Text, x);
            else MessageBox.Show("Check the informations!");
        }

    }
}
