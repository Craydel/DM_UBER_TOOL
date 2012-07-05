using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml;
using System.Windows.Forms;

namespace DM_Uber_Tool
{
  public partial class AbstractModule : UserControl
  {
    protected Random        rand = new Random();

    protected DmModuleType  moduleType;
    protected string        moduleDisplayName;

    // constructor
    public AbstractModule()
    {
      moduleDisplayName = "Abstract Module";

      InitializeComponent();
    }
  }
}
