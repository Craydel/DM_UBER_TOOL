using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DM_Uber_Tool
{
  public partial class Dice : Form
  {
    Random rnd = new Random();
    public Dice()
    {
      InitializeComponent();

      rdoD4Mod_Plus.Checked = true;
      rdoD6Mod_Plus.Checked = true;
      rdoD8Mod_Plus.Checked = true;
      rdoD10Mod_Plus.Checked = true;
      rdoD12Mod_Plus.Checked = true;
      rdoD20Mod_Plus.Checked = true;
      rdoD100Mod_Plus.Checked = true;

    }

    /// <summary>
    /// Random dice rolling fum!!
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void DiceRoller(object sender, EventArgs e)
    {
      //lbResults.Items.Clear();

      if(mtbD4Quantity.Text == "")
        mtbD4Quantity.Text = "0";
      if(mtbD6Quantity.Text == "")
        mtbD6Quantity.Text = "0";
      if(mtbD8Quantity.Text == "")
        mtbD8Quantity.Text = "0";
      if(mtbD10Quantity.Text == "")
        mtbD10Quantity.Text = "0";
      if(mtbD12Quantity.Text == "")
        mtbD12Quantity.Text = "0";
      if(mtbD20Quantity.Text == "")
        mtbD20Quantity.Text = "0";
      if(mtbD100Quantity.Text == "")
        mtbD100Quantity.Text = "0";

      if(mtbD4Mod.Text == "")
        mtbD4Mod.Text = "0";
      if(mtbD6Mod.Text == "")
        mtbD6Mod.Text = "0";
      if(mtbD8Mod.Text == "")
        mtbD8Mod.Text = "0";
      if(mtbD10Mod.Text == "")
        mtbD10Mod.Text = "0";
      if(mtbD12Mod.Text == "")
        mtbD12Mod.Text = "0";
      if(mtbD20Mod.Text == "")
        mtbD20Mod.Text = "0";
      if(mtbD100Mod.Text == "")
        mtbD100Mod.Text = "0";

      int d4Quantity   = int.Parse(mtbD4Quantity.Text);
      int d6Quantity   = int.Parse(mtbD6Quantity.Text);
      int d8Quantity   = int.Parse(mtbD8Quantity.Text);
      int d10Quantity  = int.Parse(mtbD10Quantity.Text);
      int d12Quantity  = int.Parse(mtbD12Quantity.Text);
      int d20Quantity  = int.Parse(mtbD20Quantity.Text);
      int d100Quantity = int.Parse(mtbD100Quantity.Text);

      int d4Modifier   = (rdoD4Mod_Plus.Checked   ? 1 : -1) * int.Parse(mtbD4Mod.Text);
      int d6Modifier   = (rdoD6Mod_Plus.Checked   ? 1 : -1) * int.Parse(mtbD6Mod.Text);
      int d8Modifier   = (rdoD8Mod_Plus.Checked   ? 1 : -1) * int.Parse(mtbD8Mod.Text);
      int d10Modifier  = (rdoD10Mod_Plus.Checked  ? 1 : -1) * int.Parse(mtbD10Mod.Text);
      int d12Modifier  = (rdoD12Mod_Plus.Checked  ? 1 : -1) * int.Parse(mtbD12Mod.Text);
      int d20Modifier  = (rdoD20Mod_Plus.Checked  ? 1 : -1) * int.Parse(mtbD20Mod.Text);
      int d100Modifier = (rdoD100Mod_Plus.Checked ? 1 : -1) * int.Parse(mtbD100Mod.Text);

      int quantity = 0;
      int modifier = 0;
      int sides    = 0;

      if(sender == btnD4_Roll)
      {
        quantity = d4Quantity;
        modifier = d4Modifier;
        sides    = 4;
      }
      else if(sender == btnD6_Roll)
      {
        quantity = d6Quantity;
        modifier = d6Modifier;
        sides    = 6;
      }
      else if(sender == btnD8_Roll)
      {
        quantity = d8Quantity;
        modifier = d8Modifier;
        sides    = 8;
      }
      else if(sender == btnD10_Roll)
      {
        quantity = d10Quantity;
        modifier = d10Modifier;
        sides    = 10;
      }
      else if(sender == btnD12_Roll)
      {
        quantity = d12Quantity;
        modifier = d12Modifier;
        sides    = 12;
      }
      else if(sender == btnD20_Roll)
      {
        quantity = d20Quantity;
        modifier = d20Modifier;
        sides    = 20;
      }
      else if(sender == btnD100_Roll)
      {
        quantity = d100Quantity;
        modifier = d100Modifier;
        sides    = 100;
      }

      int total = 0;
      int val = 0;

      lbResults.Items.Add( "Roll : " + quantity + "D" + sides + "+" + (quantity*modifier) );

      for(int i = 0; i < quantity; i++)
      {
        val = rnd.Next(1, sides) + 1;
        total += val + modifier;
        lbResults.Items.Add( val.ToString() + " + " + modifier.ToString() + " = " + (val+modifier).ToString() );
      }
      lbResults.Items.Add("Total : " + total.ToString());
      lbResults.Items.Add("");
    }
  }
}
