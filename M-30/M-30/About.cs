﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace M_30
{
    public partial class About : Form
    {
        public About()
        {
            InitializeComponent();
            this.Name = "About " + this.ProductName;
            label1.Text = label1.Text + this.ProductVersion;
            label2.Text = label2.Text + this.CompanyName;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
