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
  public partial class Form1 : Form
  {
    public Form1()
    {
      InitializeComponent();
    }

   /// <summary>
    /// button click Event for opening Reader form (Adobe reader in webBrowser control)
   /// </summary>
   /// <param name="sender"></param>
   /// <param name="e"></param>
    private void btnBooks_Click(object sender, EventArgs e)
    {
      Form tmpReader = new Reader();
      tmpReader.Show();
    }

    /// <summary>
    /// button click Event for opening dice roller
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void btnDice_Click(object sender, EventArgs e)
    {
      Form tmpDice = new Dice();
      tmpDice.Show();
    }

    /// <summary>
    /// button click Event for opening Dungeon Generator
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void btnDungeon_Click(object sender, EventArgs e)
    {
      Form tmpDungeonGen = new DungeonGen();
      tmpDungeonGen.Show();
    }
    /// <summary>
    /// button click Event for opening PlayerInfo form (Player Quick Reference)
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void btnPC_Click(object sender, EventArgs e)
    {
      Form tmpPlayerInfo = new PlayerInfo();
      tmpPlayerInfo.Show();
    }
    
    /// <summary>
    /// button click Event for opening NPC Generator
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void btnNPC_Click(object sender, EventArgs e)
    {
      Form tmpNPCGen = new NPCGen();
      tmpNPCGen.Show();
    }

   /// <summary>
   /// button click Event for opening the item generator
   /// </summary>
   /// <param name="sender"></param>
   /// <param name="e"></param>
    private void btnLoot_Click(object sender, EventArgs e)
    {
      Form tmpLootGen = new LootGen();
      tmpLootGen.Show();
    }

    /// <summary>
    /// Code to load a new dragon generator form
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void btnDragon_Click(object sender, EventArgs e)
    {
      Form tmpDragon = new DragonGen();
      tmpDragon.Show();
    }

  }
}
