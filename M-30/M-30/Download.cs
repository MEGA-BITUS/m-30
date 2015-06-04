using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace M_30
{
    public partial class Download : Form
    {
        public Download(string link)
        {
            InitializeComponent();
            label2.Text = "Initializing ...";
        }
        private void createFolder()
        {
            string folder = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            string specificFolder = Path.Combine(folder, Application.ProductName);
            if (!Directory.Exists(specificFolder))
                Directory.CreateDirectory(specificFolder);
        }
    }
}
