using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DM_Uber_Tool;

namespace DM_Uber_Tool
{
  public partial class DmModuleItemGenerator : AbstractModule, IDmModule
  {
    List<DmItemAbstract> itemList = new List<DmItemAbstract>();

    public DmModuleItemGenerator()
    {
      InitializeComponent();
    }

    #region IDmModule Members

    public void SaveState( System.Xml.XmlWriter writer )
    {
      throw new NotImplementedException();
    }

    public void LoadState( System.Xml.XmlReader reader )
    {
      throw new NotImplementedException();
    }

    public string GetModuleName()
    {
      throw new NotImplementedException();
    }

    public DmModuleType GetModuleType()
    {
      throw new NotImplementedException();
    }

    #endregion

    /// <summary>
    /// Generate an item, calling DmItemFactory's methods based on which button triggered this call
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void GenerateButton_Click( object sender, EventArgs e )
    {
      if( sender == btnGenerateRandom )
        itemList.Add( DmItemFactory.CreateItem() );

      else if( sender == btnGenerateMoney )
        itemList.Add( DmItemFactory.CreateItem( DmItemType.Money ) );

      else if( sender == btnGeneratePack )
        itemList.Add( DmItemFactory.CreateItem( DmItemType.Container ) );

      else if( sender == btnGenerateFood )
        itemList.Add( DmItemFactory.CreateItem( DmItemType.Food ) );

      else if( sender == btnGenerateEquipment )
        itemList.Add( DmItemFactory.CreateItem( DmItemType.Equipment ) );

      else if( sender == btnGenerateWeapon )
        itemList.Add( DmItemFactory.CreateItem( DmItemType.Weapon ) );

      else if( sender == btnGenerateArmor )
        itemList.Add( DmItemFactory.CreateItem( DmItemType.Armor ) );

      else if( sender == btnGenerateClothing )
        itemList.Add( DmItemFactory.CreateItem( DmItemType.Clothing) );

      DisplayList();
    }

    /// <summary>
    /// Display the items in the order generated
    /// </summary>
    private void DisplayList()
    {
      txtItemList.Text = "";

      for( int i=0; i<itemList.Count; i++ )
        txtItemList.Text += itemList[i].GetDescription() + "\r\n";

    }

    /// <summary>
    /// Clear the entire list of generated items
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void btnClearList_Click( object sender, EventArgs e )
    {
      itemList.Clear();

      DisplayList();
    }

    /// <summary>
    /// DEBUG : Code generator to create the descItemXXX list arrays so I wouldn't have to type them out.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void button1_Click( object sender, EventArgs e )
    {
      string tmp = "";

      tmp += "// Weapon enchants\r\n";
      for( int i=0; i<Enum.GetValues( typeof( DmItemEnchantmentWeapon ) ).Length; i++ )
        tmp += string.Format( "{0,-92}{1}", "descItemEnchantmentWeapon[(int)DmItemEnchantmentWeapon." + ((DmItemEnchantmentWeapon)i).ToString() + "]", " = \"" + ((DmItemEnchantmentWeapon)i).ToString() + "\";\r\n" );
      tmp += "\r\n";

      tmp += "// Armor enchants\r\n";
      for( int i=0; i<Enum.GetValues( typeof( DmItemEnchantmentArmor ) ).Length; i++ )
        tmp += string.Format( "{0,-92}{1}", "descItemEnchantmentArmor[(int)DmItemEnchantmentArmor." + ((DmItemEnchantmentArmor)i).ToString() + "]", " = \"" + ((DmItemEnchantmentArmor)i).ToString() + "\";\r\n" );
      tmp += "\r\n";

      tmp += "// Clothing enchants\r\n";
      for( int i=0; i<Enum.GetValues( typeof( DmItemEnchantmentClothing ) ).Length; i++ )
        tmp += string.Format( "{0,-92}{1}", "descItemEnchantmentClothing[(int)DmItemEnchantmentClothing." + ((DmItemEnchantmentClothing)i).ToString() + "]", " = \"" + ((DmItemEnchantmentClothing)i).ToString() + "\";\r\n" );
      tmp += "\r\n";

      tmp += "// Consumable enchants (spells, scrolls, wands, and the like)\r\n";
      for( int i=0; i<Enum.GetValues( typeof( DmItemEnchantmentConsumable ) ).Length; i++ )
        tmp += string.Format( "{0,-92}{1}", "descItemEnchantmentConsumable[(int)DmItemEnchantmentConsumable." + ((DmItemEnchantmentConsumable)i).ToString() + "]", " = \"" + ((DmItemEnchantmentConsumable)i).ToString() + "\";\r\n" );
      tmp += "\r\n";

      tmp += "// Item Quality Description\r\n";
      for( int i=0; i<Enum.GetValues( typeof( DmItemQuality ) ).Length; i++ )
        tmp += string.Format( "{0,-92}{1}", "descItemQuality[(int)DmItemQuality." + ((DmItemQuality)i).ToString() + "]", " = \"" + ((DmItemQuality)i).ToString() + "\";\r\n" );
      tmp += "\r\n";

      tmp += "// Basic item types\r\n";
      for( int i=0; i<Enum.GetValues( typeof( DmItemType ) ).Length; i++ )
        tmp += string.Format( "{0,-92}{1}", "descItemType[(int)DmItemType." + ((DmItemType)i).ToString() + "]", " = \"" + ((DmItemType)i).ToString() + "\";\r\n" );
      tmp += "\r\n";

      tmp += "// Money / Coin types\r\n";
      for( int i=0; i<Enum.GetValues( typeof( DmItemTypeMoney ) ).Length; i++ )
        tmp += string.Format( "{0,-92}{1}", "descItemTypeMoney[(int)DmItemTypeMoney." + ((DmItemTypeMoney)i).ToString() + "]", " = \"" + ((DmItemTypeMoney)i).ToString() + "\";\r\n" );
      tmp += "\r\n";

      tmp += "// Container types\r\n";
      for( int i=0; i<Enum.GetValues( typeof( DmItemTypeContainer ) ).Length; i++ )
        tmp += string.Format( "{0,-92}{1}", "descItemTypeContainer[(int)DmItemTypeContainer." + ((DmItemTypeContainer)i).ToString() + "]", " = \"" + ((DmItemTypeContainer)i).ToString() + "\";\r\n" );
      tmp += "\r\n";

      tmp += "// Food types\r\n";
      for( int i=0; i<Enum.GetValues( typeof( DmItemTypeFood ) ).Length; i++ )
        tmp += string.Format( "{0,-92}{1}", "descItemTypeFood[(int)DmItemTypeFood." + ((DmItemTypeFood)i).ToString() + "]", " = \"" + ((DmItemTypeFood)i).ToString() + "\";\r\n" );
      tmp += "\r\n";

      tmp += "// General Equipment\r\n";
      for( int i=0; i<Enum.GetValues( typeof( DmItemTypeEquipment ) ).Length; i++ )
        tmp += string.Format( "{0,-92}{1}", "descItemTypeEquipment[(int)DmItemTypeEquipment." + ((DmItemTypeEquipment)i).ToString() + "]", " = \"" + ((DmItemTypeEquipment)i).ToString() + "\";\r\n" );
      tmp += "\r\n";

      tmp += "// Weapon types\r\n";
      for( int i=0; i<Enum.GetValues( typeof( DmItemTypeWeaponType ) ).Length; i++ )
        tmp += string.Format( "{0,-92}{1}", "descItemTypeWeaponType[(int)DmItemTypeWeaponType." + ((DmItemTypeWeaponType)i).ToString() + "]", " = \"" + ((DmItemTypeWeaponType)i).ToString() + "\";\r\n" );
      tmp += "\r\n";

      tmp += "// Simple melee Weapons\r\n";
      for( int i=0; i<Enum.GetValues( typeof( DmItemTypeWeaponMeleeSimple ) ).Length; i++ )
        tmp += string.Format( "{0,-92}{1}", "descItemTypeWeaponMeleeSimple[(int)DmItemTypeWeaponMeleeSimple." + ((DmItemTypeWeaponMeleeSimple)i).ToString() + "]", " = \"" + ((DmItemTypeWeaponMeleeSimple)i).ToString() + "\";\r\n" );
      tmp += "\r\n";

      tmp += "// Martial melee Weapons\r\n";
      for( int i=0; i<Enum.GetValues( typeof( DmItemTypeWeaponMeleeMartial ) ).Length; i++ )
        tmp += string.Format( "{0,-92}{1}", "descItemTypeWeaponMeleeMartial[(int)DmItemTypeWeaponMeleeMartial." + ((DmItemTypeWeaponMeleeMartial)i).ToString() + "]", " = \"" + ((DmItemTypeWeaponMeleeMartial)i).ToString() + "\";\r\n" );
      tmp += "\r\n";

      tmp += "// Exotic melee weapons\r\n";
      for( int i=0; i<Enum.GetValues( typeof( DmItemTypeWeaponMeleeExotic ) ).Length; i++ )
        tmp += string.Format( "{0,-92}{1}", "descItemTypeWeaponMeleeExotic[(int)DmItemTypeWeaponMeleeExotic." + ((DmItemTypeWeaponMeleeExotic)i).ToString() + "]", " = \"" + ((DmItemTypeWeaponMeleeExotic)i).ToString() + "\";\r\n" );
      tmp += "\r\n";

      tmp += "// Simple ranged weapons\r\n";
      for( int i=0; i<Enum.GetValues( typeof( DmItemTypeWeaponRangedSimple ) ).Length; i++ )
        tmp += string.Format( "{0,-92}{1}", "descItemTypeWeaponRangedSimple[(int)DmItemTypeWeaponRangedSimple." + ((DmItemTypeWeaponRangedSimple)i).ToString() + "]", " = \"" + ((DmItemTypeWeaponRangedSimple)i).ToString() + "\";\r\n" );
      tmp += "\r\n";

      tmp += "// Martial ranged weapons\r\n";
      for( int i=0; i<Enum.GetValues( typeof( DmItemTypeWeaponRangedMartial ) ).Length; i++ )
        tmp += string.Format( "{0,-92}{1}", "descItemTypeWeaponRangedMartial[(int)DmItemTypeWeaponRangedMartial." + ((DmItemTypeWeaponRangedMartial)i).ToString() + "]", " = \"" + ((DmItemTypeWeaponRangedMartial)i).ToString() + "\";\r\n" );
      tmp += "\r\n";

      tmp += "// Exotic ranged wepaons\r\n";
      for( int i=0; i<Enum.GetValues( typeof( DmItemTypeWeaponRangedExotic ) ).Length; i++ )
        tmp += string.Format( "{0,-92}{1}", "descItemTypeWeaponRangedExotic[(int)DmItemTypeWeaponRangedExotic." + ((DmItemTypeWeaponRangedExotic)i).ToString() + "]", " = \"" + ((DmItemTypeWeaponRangedExotic)i).ToString() + "\";\r\n" );
      tmp += "\r\n";

      tmp += "// Clothing types\r\n";
      for( int i=0; i<Enum.GetValues( typeof( DmItemTypeClothing ) ).Length; i++ )
        tmp += string.Format( "{0,-92}{1}", "descItemTypeClothing[(int)DmItemTypeClothing." + ((DmItemTypeClothing)i).ToString() + "]", " = \"" + ((DmItemTypeClothing)i).ToString() + "\";\r\n" );
      tmp += "\r\n";

      tmp += "// Armor Types\r\n";
      for( int i=0; i<Enum.GetValues( typeof( DmItemTypeArmorType ) ).Length; i++ )
        tmp += string.Format( "{0,-92}{1}", "descItemTypeArmorType[(int)DmItemTypeArmorType." + ((DmItemTypeArmorType)i).ToString() + "]", " = \"" + ((DmItemTypeArmorType)i).ToString() + "\";\r\n" );
      tmp += "\r\n";

      tmp += "// Light armors\r\n";
      for( int i=0; i<Enum.GetValues( typeof( DmItemTypeArmorLight ) ).Length; i++ )
        tmp += string.Format( "{0,-92}{1}", "descItemTypeArmorLight[(int)DmItemTypeArmorLight." + ((DmItemTypeArmorLight)i).ToString() + "]", " = \"" + ((DmItemTypeArmorLight)i).ToString() + "\";\r\n" );
      tmp += "\r\n";

      tmp += "// Medium armors\r\n";
      for( int i=0; i<Enum.GetValues( typeof( DmItemTypeArmorMedium ) ).Length; i++ )
        tmp += string.Format( "{0,-92}{1}", "descItemTypeArmorMedium[(int)DmItemTypeArmorMedium." + ((DmItemTypeArmorMedium)i).ToString() + "]", " = \"" + ((DmItemTypeArmorMedium)i).ToString() + "\";\r\n" );
      tmp += "\r\n";

      tmp += "// Heavy armors\r\n";
      for( int i=0; i<Enum.GetValues( typeof( DmItemTypeArmorHeavy ) ).Length; i++ )
        tmp += string.Format( "{0,-92}{1}", "descItemTypeArmorHeavy[(int)DmItemTypeArmorHeavy." + ((DmItemTypeArmorHeavy)i).ToString() + "]", " = \"" + ((DmItemTypeArmorHeavy)i).ToString() + "\";\r\n" );
      tmp += "\r\n";

      tmp += "// Sheild (armors)\r\n";
      for( int i=0; i<Enum.GetValues( typeof( DmItemTypeArmorShield ) ).Length; i++ )
        tmp += string.Format( "{0,-92}{1}", "descItemTypeArmorShield[(int)DmItemTypeArmorShield." + ((DmItemTypeArmorShield)i).ToString() + "]", " = \"" + ((DmItemTypeArmorShield)i).ToString() + "\";\r\n" );
      tmp += "\r\n";

      tmp += "// Armor enhancements\r\n";
      for( int i=0; i<Enum.GetValues( typeof( DmItemTypeArmorExtra ) ).Length; i++ )
        tmp += string.Format( "{0,-92}{1}", "descItemTypeArmorExtra[(int)DmItemTypeArmorExtra." + ((DmItemTypeArmorExtra)i).ToString() + "]", " = \"" + ((DmItemTypeArmorExtra)i).ToString() + "\";\r\n" );
      tmp += "\r\n";

      txtItemList.Text = tmp;
    }

  }
}
