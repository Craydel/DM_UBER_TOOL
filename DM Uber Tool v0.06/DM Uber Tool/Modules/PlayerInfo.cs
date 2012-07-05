﻿using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DM_Uber_Tool
{
  public partial class PlayerInfo : Form
  {
    private Player currentPlayer;
    private string lastSelectedPlayerName = "";

    /// <summary>
    /// code to convert inches to feet for playr height
    /// </summary>
    /// <param name="inches"></param>
    /// <returns></returns>
    static KeyValuePair<int, double> ToFeetInches(double inches)
    {
      return new KeyValuePair<int, double>((int)inches / 12, inches % 12);
    }

    /// <summary>
    /// PlayerInfo form constructor
    /// Pulls a list of files from character directory and lists them without the extention to create a selectable character list.
    /// </summary>
    public PlayerInfo()
    {
      InitializeComponent();

      currentPlayer = null;

      LoadCharacterList();

      splitContainer1.Panel2Collapsed = true;
      this.Width = 365;
      lblACDexMod.Text = "0";


      cboRace.Items.Clear();
      cboRace.Items.Add("Human");
      cboRace.Items.Add("Dwarf");
      cboRace.Items.Add("Elf");
      cboRace.Items.Add("Gnome");
      cboRace.Items.Add("Half-Elf");
      cboRace.Items.Add("Half-Orc");
      cboRace.Items.Add("Halfling");
    }

    #region form value accessors
    //Strings below are declared to more easily write textbox contents to a text file.  
    public string charname
    {
      get { return cboName.Text; }
    }
    private string race
    {
      get { return cboRace.Text; }
    }
    private string charsize
    {
      get { return cboSize.Text; }
    }
    private string plyrclass
    {
      get { return cboClass.Text; }
    }
    private string level
    {
      get { return cboLevel.Text; }
    }
    private string gender
    {
      get { return cboGender.Text; }
    }
    private string age
    {
      get { return cboAge.Text; }
    }
    private string height
    {
      get { return cboHeight.Text; }
    }
    private string weight
    {
      get { return cboWeight.Text; }
    }
    private string diety
    {
      get { return cboDiety.Text; }
    }
    private string alignmnt
    {
      get { return cboAlignment.Text; }
    }
    private string armor
    {
      get { return txtArmor.Text; }
    }
    private string weapons
    {
      get { return txtWeapon.Text; }
    }
    private string notableItems
    {
      get { return txtNotable.Text; }
    }
    private string strength
    {
      get { return txtSTR.Text; }
    }
    private string dexterity
    {
      get { return txtDEX.Text; }
    }
    private string constitution
    {
      get { return txtCON.Text; }
    }
    private string intelligence
    {
      get { return txtINT.Text; }
    }
    private string wisdom
    {
      get { return txtWIS.Text; }
    }
    private string charisma
    {
      get { return txtCHA.Text; }
    }
    private string speed
    {
      get { return txtSpeed.Text; }
    }
    private string overhead
    {
      get { return txtOverHead.Text; }
    }
    private string offground
    {
      get { return txtOffGround.Text; }
    }
    private string pushdrag
    {
      get { return txtPushDrag.Text; }
    }
    private string fortitude
    {
      get { return txtFortitude.Text; }
    }
    private string reflex
    {
      get { return txtReflex.Text; }
    }
    private string will
    {
      get { return txtWillPower.Text; }
    }
    private string savemod
    {
      get { return txtSavingMod.Text; }
    }
    private string armorclass
    {
      get { return txtArmorClass.Text; }
    }
    private string flatfoot
    {
      get { return txtAC_FlatFoot.Text; }
    }
    private string touch
    {
      get { return txtAC_VsTouch.Text; }
    }
    private string acmod
    {
      get { return txtACMods.Text; }
    }
    private string damage
    {
      get { return txtCurrent_HP.Text; }
    }
    private string hitpoints
    {
      get { return txtTotal_HP.Text; }
    }
    private string subdual
    {
      get { return txtSub_Dual.Text; }
    }
    #endregion

    /// <summary>
    /// Populates Character List Dropdown
    /// </summary>
    private void LoadCharacterList()
    {
      if(!Directory.Exists("characters"))
      {
        Directory.CreateDirectory("characters");
      }

      string currentSelection = cboName.SelectedItem == null ? "" : cboName.SelectedItem.ToString();
      cboName.Items.Clear(); // prevent duplicates - always start with a blank list

      foreach(string file in Directory.GetFiles(@"characters\"))
      {
        var value = Path.GetFileNameWithoutExtension(file);
        if(!cboName.Items.Contains(value))
          cboName.Items.Add(value);  // kind of a hack... won't re-alphebetize them until next form load.
      }
    }

    /// <summary>
    /// Selectable Dropdown for previously created characters.  
    /// If sheet is empty will load character values into text and combo boxes.  
    /// If not empty will display a conformation box before loading new character.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
    {
      if(lastSelectedPlayerName.Trim() != "")
      {
        if(DialogResult.Yes == MessageBox.Show("Save current player sheet?",
                                                "Save File",
                                                MessageBoxButtons.YesNo,
                                                MessageBoxIcon.Hand,
                                                MessageBoxDefaultButton.Button1
                                               )
          )
        {
          // user clicked "Yes" to save current file first... so, save it.
          SaveCharacterSheet();
        }
      }

      // then proceed to load the selected character
      lastSelectedPlayerName = cboName.Text;

      currentPlayer = new Player(cboName.SelectedItem.ToString());
      txtSTR.Text = currentPlayer.Strength;
      cboName.Text = currentPlayer.CharName;
      cboRace.Text = currentPlayer.Race;
      cboSize.Text = currentPlayer.CharSize;
      cboClass.Text = currentPlayer.PlayerClass;
      cboLevel.Text = currentPlayer.Level;
      cboGender.Text = currentPlayer.Gender;
      cboAge.Text = currentPlayer.Age;
      cboHeight.Text = currentPlayer.Height;
      cboWeight.Text = currentPlayer.Weight;
      cboDiety.Text = currentPlayer.Diety;
      cboAlignment.Text = currentPlayer.Alignmnt;
      txtArmor.Text = currentPlayer.Armor;
      txtWeapon.Text = currentPlayer.Weapons;
      txtNotable.Text = currentPlayer.NotableItems;
      txtSTR.Text = currentPlayer.Strength;
      txtDEX.Text = currentPlayer.Dexterity;
      txtCON.Text = currentPlayer.Constitution;
      txtINT.Text = currentPlayer.Intelligence;
      txtWIS.Text = currentPlayer.Wisdom;
      txtCHA.Text = currentPlayer.Charisma;
      txtSpeed.Text = currentPlayer.Speed;
      txtOverHead.Text = currentPlayer.Overhead;
      txtOffGround.Text = currentPlayer.Offground;
      txtPushDrag.Text = currentPlayer.Pushdrag;
      txtFortitude.Text = currentPlayer.Fortitude;
      txtReflex.Text = currentPlayer.Reflex;
      txtWillPower.Text = currentPlayer.Will;
      txtSavingMod.Text = currentPlayer.Savemod;
      txtArmorClass.Text = currentPlayer.Armorclass;
      txtAC_FlatFoot.Text = currentPlayer.FlatFoot;
      txtAC_VsTouch.Text = currentPlayer.Touch;
      txtACMods.Text = currentPlayer.Acmod;
      txtCurrent_HP.Text = currentPlayer.Damage;
      txtTotal_HP.Text = currentPlayer.Hitpoints;
      txtSub_Dual.Text = currentPlayer.Subdual;

    }

    /// <summary>
    /// Calculate Abilitiy Modifiers
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void stat_TextChanged(object sender, EventArgs e)
    {
      string stat = ((MaskedTextBox)sender).Text;
      string modifier = "";

      if(stat != "")
        modifier = ((int.Parse(stat) - 10) / 2).ToString();

      if(sender == txtSTR)
        lblStrMod.Text = modifier;
      else if(sender == txtDEX)
      {
        lblDexMod.Text = modifier;
        lblACDexMod.Text = modifier;
      }
      else if(sender == txtCON)
        lblConMod.Text = modifier;
      else if(sender == txtINT)
        lblIntMod.Text = modifier;
      else if(sender == txtWIS)
        lblWisMod.Text = modifier;
      else if(sender == txtCHA)
        lblChaMod.Text = modifier;
    }

    /// <summary>
    /// Upon entering a Masked Text Box highlight all contents
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void txtMaskBox_Enter(object sender, System.EventArgs e)
    {
      ((MaskedTextBox)sender).SelectAll();
      //if (!String.IsNullOrEmpty(txtBox.))
      //{
      //  textBox1.SelectionStart = 0;
      //  textBox1.SelectionLength = textBox1.Text.Length;
      //}
    }

    /// <summary>
    /// Upon entering textbox highlight all contents
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void txtBox_Enter(object sender, System.EventArgs e)
    {
      ((MaskedTextBox)sender).SelectAll();
      //if (!String.IsNullOrEmpty(txtBox.))
      //{
      //  textBox1.SelectionStart = 0;
      //  textBox1.SelectionLength = textBox1.Text.Length;
      //}
    }


    /// <summary>
    /// Event: button1_Click: Save character info to text or csv file.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void button1_Click(object sender, EventArgs e)
    {
      lastSelectedPlayerName = cboName.Text.Trim();
      SaveCharacterSheet();
    }

    /// <summary>
    /// Method: save Info from "Player Info" Form to character text document
    /// </summary>
    private void SaveCharacterSheet()
    {
      // cannot assume the currentPlayer has any values - might be a new character.
      currentPlayer = new Player();

      // if this is the first character AND it's a new one being saved....
      if(lastSelectedPlayerName == "")
        lastSelectedPlayerName = cboName.Text;


      if(lastSelectedPlayerName.Trim() == "")
      {
        MessageBox.Show("For new characters, a name must be provided!");
        return; // exit this method without doing anything else...prevents saving a file named ".txt"
      }

      // set all values to be saved.
      currentPlayer.CharName = charname;
      currentPlayer.Race = race;
      currentPlayer.CharSize = charsize;
      currentPlayer.PlayerClass = plyrclass;
      currentPlayer.Level = level;
      currentPlayer.Gender = gender;
      currentPlayer.Age = age;
      currentPlayer.Height = height;
      currentPlayer.Weight = weight;
      currentPlayer.Diety = diety;
      currentPlayer.Alignmnt = alignmnt;
      currentPlayer.Armor = armor;
      currentPlayer.Weapons = weapons;
      currentPlayer.NotableItems = notableItems;
      currentPlayer.Strength = strength;
      currentPlayer.Dexterity = dexterity;
      currentPlayer.Constitution = constitution;
      currentPlayer.Intelligence = intelligence;
      currentPlayer.Wisdom = wisdom;
      currentPlayer.Charisma = charisma;
      currentPlayer.Speed = speed;
      currentPlayer.Overhead = overhead;
      currentPlayer.Offground = offground;
      currentPlayer.Pushdrag = pushdrag;
      currentPlayer.Fortitude = fortitude;
      currentPlayer.Reflex = reflex;
      currentPlayer.Will = will;
      currentPlayer.Savemod = savemod;
      currentPlayer.Armorclass = armorclass;
      currentPlayer.FlatFoot = flatfoot;
      currentPlayer.Touch = touch;
      currentPlayer.Acmod = acmod;
      currentPlayer.Damage = damage;
      currentPlayer.Hitpoints = hitpoints;
      currentPlayer.Subdual = subdual;


      // Moved the file save to the Player class - since other forms might want 
      //  to create player sheets (like, say, the Monster Generator), the Player 
      //  object created should be able to save itself.
      // Just give it a name to save as.
      currentPlayer.Save();

      // since we may have just created a new character, refresh the dropdown list real quick
      LoadCharacterList();
    }

    /// <summary>
    /// Event: maintains previous character name in case of character change from combobox
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void SaveCurrentCharacterName(object sender, EventArgs e)
    {
      lastSelectedPlayerName = cboName.Text.Trim();
    }

    /// <summary>
    /// Event:  Prevents Character Change from arrow press in combobox
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void SaveCurrentCharacterName(object sender, KeyEventArgs e)
    {
      if(e.KeyCode == Keys.Up || e.KeyCode == Keys.Down)
      {
        e.SuppressKeyPress = true;
      }
      else
      {
        lastSelectedPlayerName = cboName.Text.Trim();
      }
    }

    /// <summary>
    /// FLy Out window fir all calculatory feilds
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void btnMODS_Click(object sender, EventArgs e)
    {
      if(splitContainer1.Panel2Collapsed == false)
      {
        splitContainer1.Panel2Collapsed = true;
        this.Width = 365;
      }
      else
      {
        splitContainer1.Panel2Collapsed = false;
        this.Width = 1133;
      }
    }

    /// <summary>
    /// change height range, weight range, age range and size based on race and gender
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void Race_Gender_Changed(object sender, EventArgs e)
    {
      string gender = cboGender.Text == "F" ? "M" : "F";
      string size = "";

      int beginWeight = 0;
      int endWeight = 0;

      int beginAge = 0;
      int endAge = 0;

      int beginHeight = 0;
      int endHeight = 0;

      if(cboRace.Text == "Human")
      {
        //Race, Gender      height  HMod    weight       ??
        //Human, male	      4' 10"	+2d10	  120 lb.	x (2d4) lb.
        //Human, female	    4' 5"	  +2d10	   85 lb.	x (2d4) lb.

        beginHeight = gender == "M" ? (4*12+10     ) : (4*12+ 5     );
        endHeight   = gender == "M" ? (4*12+10 + 20) : (4*12+10 + 20);
        beginWeight =  85; // minimum weight for male/female 
        endWeight   = 300; // = max base weight + (max HeightMod x max WeightMod)
        beginAge    =  19; // gender neutral
        endAge      = 110; // gender neutral
        size        = "M";
      }
      else if(cboRace.Text == "Dwarf")
      {
        //Race, Gender      height  HMod    weight       ??
        //Dwarf, male	      3' 9"	  +2d4	  130 lb.	x (2d6) lb.
        //Dwarf, female	    3' 7"	  +2d4	  100 lb.	x (2d6) lb.

        beginHeight = gender == "M" ? (4*12+10) : (4*12+ 5);
        endHeight   = gender == "M" ? (4*12+10 + 20) : (4*12+10 + 20);
        beginWeight =  85; // minimum weight for male/female 
        endWeight   = 300; // = max base weight + (max HeightMod x max WeightMod)
        beginAge    =  19; // gender neutral
        endAge      = 110; // gender neutral
        size        = "M";
      }
      else if(cboRace.Text == "Elf")
      {
        //Race, Gender      height  HMod    weight       ??
        //Elf, male	        4' 5"	  +2d6	   85 lb.	x (1d6) lb.
        //Elf, female	      4' 5"	  +2d6	   80 lb.	x (1d6) lb.

        beginHeight = gender == "M" ? (4*12+10) : (4*12+ 5);
        endHeight   = gender == "M" ? (4*12+10 + 20) : (4*12+10 + 20);
        beginWeight =  85; // minimum weight for male/female 
        endWeight   = 300; // = max base weight + (max HeightMod x max WeightMod)
        beginAge    =  19; // gender neutral
        endAge      = 110; // gender neutral
        size        = "M";
      }
      else if(cboRace.Text == "Gnome")
      {
        //Race, Gender      height  HMod    weight       ??
        //Gnome, male	      3' 0"	  +2d4	   40 lb.	x 1 lb.
        //Gnome, female	    2' 10"	+2d4	   35 lb.	x 1 lb.

        beginHeight = gender == "M" ? (4*12+10) : (4*12+ 5);
        endHeight   = gender == "M" ? (4*12+10 + 20) : (4*12+10 + 20);
        beginWeight =  85; // minimum weight for male/female 
        endWeight   = 300; // = max base weight + (max HeightMod x max WeightMod)
        beginAge    =  19; // gender neutral
        endAge      = 110; // gender neutral
        size        = "M";
      }
      else if(cboRace.Text == "Half-Elf")
      {
        //Race, Gender      height  HMod    weight       ??
        //Half-elf, male	  4' 7"	  +2d8	  100 lb.	x (2d4) lb.
        //Half-elf, female	4' 5"	  +2d8	   80 lb.	x (2d4) lb.

        beginHeight = gender == "M" ? (4*12+10) : (4*12+ 5);
        endHeight   = gender == "M" ? (4*12+10 + 20) : (4*12+10 + 20);
        beginWeight =  85; // minimum weight for male/female 
        endWeight   = 300; // = max base weight + (max HeightMod x max WeightMod)
        beginAge    =  19; // gender neutral
        endAge      = 110; // gender neutral
        size        = "M";
      }
      else if(cboRace.Text == "Half-Orc")
      {
        //Race, Gender      height  HMod    weight       ??
        //Half-orc, male	  4' 10"	+2d12	  150 lb.	x (2d6) lb.
        //Half-orc, female	4' 5"	  +2d12	  110 lb.	x (2d6) lb.

        beginHeight = gender == "M" ? (4*12+10) : (4*12+ 5);
        endHeight   = gender == "M" ? (4*12+10 + 20) : (4*12+10 + 20);
        beginWeight =  85; // minimum weight for male/female 
        endWeight   = 300; // = max base weight + (max HeightMod x max WeightMod)
        beginAge    =  19; // gender neutral
        endAge      = 110; // gender neutral
        size        = "M";
      }
      else if(cboRace.Text == "Halfling")
      {
        //Race, Gender      height  HMod    weight       ??
        //Halfling, male	  2' 8"	  +2d4	   30 lb.	x 1 lb.
        //Halfling, female	2' 6"	  +2d4	   25 lb.	x 1 lb.

        beginHeight = gender == "M" ? (4*12+10) : (4*12+ 5);
        endHeight   = gender == "M" ? (4*12+10 + 20) : (4*12+10 + 20);
        beginWeight =  85; // minimum weight for male/female 
        endWeight   = 300; // = max base weight + (max HeightMod x max WeightMod)
        beginAge    =  19; // gender neutral
        endAge      = 110; // gender neutral
        size        = "M";
      }


      //
      // all sart/end values set - populate lists
      //
      cboSize.Text = size;

      cboWeight.Items.Clear();
      for(int i = beginWeight; i <= endWeight; i++)
        cboWeight.Items.Add(i.ToString());

      cboAge.Items.Clear();
      for(int i = beginAge; i <= endAge; i++)
        cboAge.Items.Add(i.ToString());

      cboHeight.Items.Clear();
      for(int i = beginHeight; i <= endHeight; i++)
        cboHeight.Items.Add(ToFeetInches(i).ToString());
    }

  }
}


