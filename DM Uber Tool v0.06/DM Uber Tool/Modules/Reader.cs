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
    private string prefsFileName = "Prefs - Arcane Repository.txt";
    string resourcesPath = "";

    List<FileInfo> files = new List<FileInfo>();
    public Reader()
    {
      InitializeComponent();
      
      cboDocuments.Items.Clear();

      if( resourcesPath == "" )
        GetResourcesPath();

      foreach( FileInfo file in new DirectoryInfo( resourcesPath + "\\Reference\\D&D Books\\3.5\\").GetFiles("*.pdf") )
      {
        cboDocuments.Items.Add(file.Name.Substring(0, file.Name.Length -4));
        files.Add(file);
      }
    }

    private void Documents_OnChange( object sender, EventArgs e )
    {
      if( resourcesPath == "" )
        GetResourcesPath();

      ComboBox c = (ComboBox)sender;
      webBrowser1.Navigate(files[c.SelectedIndex].FullName);
    }

    public void GetResourcesPath()
    {
      // read resourcesPath
      if(!File.Exists(Application.StartupPath + "\\" + prefsFileName))
      {
        File.Create(prefsFileName).Close(); // create the file, and immediately release it
      }

      StreamReader reader = new StreamReader(Application.StartupPath + "\\" + prefsFileName);
      if((resourcesPath = reader.ReadLine()) == null || !(new DirectoryInfo(resourcesPath).Exists))
      {
        FolderBrowserDialog dlg = new FolderBrowserDialog();
        dlg.SelectedPath = Application.StartupPath;
        dlg.Description = "Please locate the 'Resources' directory (about 3 levels up from the debug directory) :";

        if(dlg.ShowDialog() == DialogResult.Cancel)
          Application.Exit();

        resourcesPath = dlg.SelectedPath;
        reader.Close();

        StreamWriter output = new StreamWriter(Application.StartupPath + "\\" + prefsFileName);
        output.WriteLine(resourcesPath);
        output.Close();
      }
      else
      {
        reader.Close();
      }
    }
  }
}
