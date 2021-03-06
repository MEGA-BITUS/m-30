﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Threading;
using System.Net;

namespace M_30
{
    public partial class Download : Form
    {
        string appFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), Application.ProductName);
        public Download(string link, string name)
        {
            InitializeComponent();
            label2.Text = "Initializing ...";
            createFolder();
            startDownload(link, name);
        }
        private void createFolder()
        {
            label2.Text = "Creating folder ...";
            if (!Directory.Exists(appFolder))
                Directory.CreateDirectory(appFolder);
        }
        private void startDownload(string link, string name)
        {
            label2.Text = "Starting Download ...";
            Thread thread = new Thread(() =>
            {  
                try
                {
                    WebClient client = new WebClient();
                    client.DownloadProgressChanged += new DownloadProgressChangedEventHandler(client_DownloadProgressChanged);
                    client.DownloadFileCompleted += new AsyncCompletedEventHandler(client_DownloadFileCompleted);
                    client.DownloadFileAsync(new Uri(link), appFolder + @"\" + name + getExtension(link));
                }
                catch (WebException w)
                {
                    MessageBox.Show(w.ToString());
                    throw;
                }
            });
            thread.Start();
        }
        void client_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            this.BeginInvoke((MethodInvoker)delegate
            {
                double bytesIn = double.Parse(e.BytesReceived.ToString());
                double totalBytes = double.Parse(e.TotalBytesToReceive.ToString());
                double percentage = bytesIn / totalBytes * 100;
                label2.Text = "Downloaded " + e.BytesReceived + " of " + e.TotalBytesToReceive;
                progressBar1.Value = int.Parse(Math.Truncate(percentage).ToString());
            });
        }
        private string getExtension(string link)
        {
            Uri myUri = new Uri(link);
            string path = String.Format("{0}{1}{2}{3}", myUri.Scheme, Uri.SchemeDelimiter, myUri.Authority, myUri.AbsolutePath);
            string extension = Path.GetExtension(path);
            return extension;
        }
        void client_DownloadFileCompleted(object sender, AsyncCompletedEventArgs e)
        {
            this.BeginInvoke((MethodInvoker)delegate
            {
                this.Close();
            });
        }
    }
}
