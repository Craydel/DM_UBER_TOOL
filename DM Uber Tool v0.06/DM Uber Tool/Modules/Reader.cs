using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace DM_Uber_Tool
{
    public partial class Reader : Form
    {
      List<FileInfo> files = new List<FileInfo>();
        public Reader()
        {
            InitializeComponent();
            comboBox1.Items.Clear();
            foreach (FileInfo file in new DirectoryInfo(@"..\..\..\..\Resources\Reference\D&D Books\3.5\").GetFiles("*.pdf"))
            {
              comboBox1.Items.Add(file.Name.Substring(0, file.Name.Length -4));
              files.Add(file);
            }
        }
        
        private void xxx(object sender, EventArgs e)
        {
          ComboBox c = (ComboBox)sender;
          webBrowser1.Navigate(files[c.SelectedIndex].FullName);

        }
    }
}
