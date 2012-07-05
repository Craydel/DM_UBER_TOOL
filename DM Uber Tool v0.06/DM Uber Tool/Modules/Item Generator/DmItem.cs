using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DM_Uber_Tool
{
  /// <summary>
  /// The base class defining an Item.
  /// All Items generated inherit from this class, which provides basic values for cost, weight, quality, and an enchantment.
  /// 
  /// More specific information for a give item should be added to that item type's class 
  ///   for example, weapon types include damage and crit ratings that money types wouldn't use.  Both have a moneytary value, though.
  /// </summary>
  public abstract class DmItemAbstract
  {
    public DmItemType    baseItemType;         // Money, Weapon, Container, Food, Armor, etc
    public object        itemSubType  = null;  // DmItemTypeWeaponType, DmItemTypeArmorType, or null
    public object        itemClass    = null;  // matrial, simple, exotic
    public object        itemName     = null;  // Bucket, TrailRation, Copper, LongSword, etc.

    public  double          Weight      = 0;  // lbs
    public  int             Cost        = 0;  // in coppers - desc string should format appropriately
    public  int             Quantity    = 1;  // number of this item (useful for, say, paper or matches)

    public DmItemQuality    Quality     = DmItemQuality.Average;
    public object           Enchantment = null;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="oType"></param>
    public DmItemAbstract( object iName )
    {
      itemName = iName;
    }

    /// <summary>
    /// Simple, generig description of an Item available to all items inheriting this class.
    ///   More specific descriptions should override this method  (i.e.: weapons for damage information)
    /// </summary>
    /// <param name="includeQuality">Flag indicating whether the description should include the quality</param>
    /// <returns></returns>
    public virtual string GetDescription( bool includeQuality )
    {
      if( includeQuality || Quality==DmItemQuality.Masterwork )
      {
        if( Enchantment != null )
          return string.Format( "{0} quality {1} **Enchanted : +{2}**",
            /* */               DmItemFactory.GetDescription( Quality ).ToLower(),
            /* */               DmItemFactory.GetDescription( itemName ).ToLower(),
            /* */               DmItemFactory.GetDescription( Enchantment )
            /* */             );
        else
          return string.Format( "{0} quality {1}",
            /* */               DmItemFactory.GetDescription( Quality ).ToLower(),
            /* */               DmItemFactory.GetDescription( itemName ).ToLower()
            /* */             );
      }
      else
        return string.Format( "{0}",
          /* */               DmItemFactory.GetDescription( itemName ).ToLower()
          /* */             );
    }

    /// <summary>
    /// Simple, generig description of an Item available to all items inheriting this class.
    ///   More specific descriptions should override this method  (i.e.: weapons for damage information)
    /// </summary>
    /// <returns></returns>
    public virtual string GetDescription()
    {
      return GetDescription( false );
    }

    /// <summary>
    /// Generic treeview node branch.
    /// Override if the item contains other otems (i.e.: DmItemContainer objects)
    /// </summary>
    /// <returns></returns>
    public virtual System.Windows.Forms.TreeNode GetDescriptionNode()
    {
      System.Windows.Forms.TreeNode node = new System.Windows.Forms.TreeNode();

      node.Text = GetDescription();

      return node;
    }
  }

  /* ------------------------------------------------------------------------------------
   *          Money
   * --------------------------------------------------------------------------------- */
  #region Money

  /// <summary>
  /// 
  /// </summary>
  public class DmItemMoney 
    : DmItemAbstract
  {
    public DmItemTypeMoney Type;

    public DmItemMoney( DmItemTypeMoney iType )
      : base ( iType )
    {
      itemName = iType;
      Type = iType;
      baseItemType = DmItemType.Money;
    }
  }

  #endregion


  /* ------------------------------------------------------------------------------------
   *          Containers and Bags
   * --------------------------------------------------------------------------------- */
  #region Containers

  /// <summary>
  /// 
  /// </summary>
  public class DmItemContainer 
    : DmItemAbstract
  {
    public DmItemTypeContainer Type;

    public List<DmItemAbstract> Contents = new List<DmItemAbstract>();

    public DmItemContainer( DmItemTypeContainer iType )
      : base( iType )
    {
      itemName = iType;
      Type = iType;
      baseItemType = DmItemType.Container;
    }

    public override string GetDescription()
    {
      return GetDescription( false );
    }

    public override string GetDescription( bool includeQualty )
    {
      // self description;
      string desc = base.GetDescription( includeQualty );

      for( int i=0; i< Contents.Count; i++ )
        desc += "\r\n  +-- " + Contents[i].GetDescription();

      return desc;
    }

    /// <summary>
    /// Similar to the base classes node creator, 
    ///   this also adds child nodes for each item in the Contents list
    /// </summary>
    /// <returns></returns>
    public override System.Windows.Forms.TreeNode GetDescriptionNode()
    {
      System.Windows.Forms.TreeNode node = new System.Windows.Forms.TreeNode( DmItemFactory.GetDescription( itemName ) );;

      for( int i=0; i<Contents.Count; i++ )
        node.Nodes.Add( Contents[i].GetDescriptionNode() );

      return node;
    }
  }

  #endregion


  /* ------------------------------------------------------------------------------------
   *          Food Items
   * --------------------------------------------------------------------------------- */
  #region Food

  /// <summary>
  /// 
  /// </summary>
  public class DmItemFood 
    : DmItemAbstract
  {
    public DmItemTypeFood Type;

    public DmItemFood( DmItemTypeFood iType )
      : base( iType )
    {
      itemName = iType;
      Type = iType;
      baseItemType = DmItemType.Food;
    }
  }

  #endregion


  /* ------------------------------------------------------------------------------------
   *          General Equipment and Adventuring Gear
   * --------------------------------------------------------------------------------- */
  #region Equipment

  /// <summary>
  /// 
  /// </summary>
  public class DmItemEquipment 
    : DmItemAbstract
  {
    public DmItemTypeEquipment Type;

    public DmItemEquipment( DmItemTypeEquipment iType )
      : base( iType )
    {
      itemName = iType;
      Type = iType;
      baseItemType = DmItemType.Equipment;
    }
  }

  #endregion


  /* ------------------------------------------------------------------------------------
   *          Cothing (non-armor)
   * --------------------------------------------------------------------------------- */
  #region Clothing

  /// <summary>
  /// 
  /// </summary>
  public class DmItemClothing 
    : DmItemAbstract
  {
    public DmItemTypeClothing Type;

    public DmItemClothing( DmItemTypeClothing iType )
      : base( iType )
    {
      itemName = iType;
      Type = iType;
      baseItemType = DmItemType.Clothing;
    }

    /// <summary>
    /// Clothing shall include the quality of the item in the description by default
    /// </summary>
    /// <returns></returns>
    public override string GetDescription()
    {
      return base.GetDescription( true );
    }
  }

  #endregion


  /* ------------------------------------------------------------------------------------
   *          Weapons - melee and ranged
   * --------------------------------------------------------------------------------- */
  #region Weapons

  /// <summary>
  /// Weapons have their own abstract class inheriting from DmItemAbstract to provide a single
  ///   place to add the descriptions required - damage, range, speed, crit, etc.
  /// </summary>
  public abstract class DmItemWeaponAbstract
    : DmItemAbstract
  {
    public DmItemTypeWeaponClass WeaponClass;
    public DmItemTypeWeaponType WeaponType;

    public DmItemWeaponAbstract( object iType, object iClass, object iName )
      : base( iName )
    {
      itemName = iName;
      itemClass = iClass;
      itemSubType = iType;
      WeaponType = (DmItemTypeWeaponType)iType;
      WeaponClass = (DmItemTypeWeaponClass)iClass;
      
      baseItemType = DmItemType.Weapon;
    }

    /// <summary>
    /// TODO : Weapon descriptions should include weight, damage, speed, range, crit, etc
    /// </summary>
    /// <returns></returns>
    public override string GetDescription()
    {
      return string.Format( "{0} quality {1}   ({2} {3}{4})",
        /* */               DmItemFactory.GetDescription( Quality ).ToLower(),
        /* */               DmItemFactory.GetDescription( itemName ).ToLower(),
        /* */               DmItemFactory.GetDescription( itemClass ).ToLower(),
        /* */               DmItemFactory.GetDescription( itemSubType ).ToLower(),
        /* */               ( Enchantment != null ? " ** Enchanted : +" + DmItemFactory.GetDescription( Enchantment ) + " ** " : "" )
        /* */             );
    }
  }

  /// <summary>
  /// 
  /// </summary>
  public class DmItemWeaponMeleeSimple
    : DmItemWeaponAbstract
  {
    public DmItemTypeWeaponMeleeSimple Type;

    public DmItemWeaponMeleeSimple( DmItemTypeWeaponMeleeSimple iName )
      : base( DmItemTypeWeaponType.Melee, DmItemTypeWeaponClass.Simple, iName )
    {
      Type = iName;
    }
  }


  /// <summary>
  /// 
  /// </summary>
  public class DmItemWeaponMeleeMartial
    : DmItemWeaponAbstract
  {
    public DmItemTypeWeaponMeleeMartial Type;

    public DmItemWeaponMeleeMartial( DmItemTypeWeaponMeleeMartial iName )
      : base( DmItemTypeWeaponType.Melee, DmItemTypeWeaponClass.Martial, iName )
    {
      Type = iName;
    }
  }


  /// <summary>
  /// 
  /// </summary>
  public class DmItemWeaponMeleeExotic
    : DmItemWeaponAbstract
  {
    public DmItemTypeWeaponMeleeExotic Type;

    public DmItemWeaponMeleeExotic( DmItemTypeWeaponMeleeExotic iName )
      : base( DmItemTypeWeaponType.Melee, DmItemTypeWeaponClass.Exotic, iName )
    {
      Type = iName;
    }
  }


  /// <summary>
  /// 
  /// </summary>
  public class DmItemWeaponRangedSimple
    : DmItemWeaponAbstract
  {
    public DmItemTypeWeaponRangedSimple Type;

    public DmItemWeaponRangedSimple( DmItemTypeWeaponRangedSimple iName )
      : base( DmItemTypeWeaponType.Ranged, DmItemTypeWeaponClass.Simple, iName )
    {
      Type = iName;
    }
  }


  /// <summary>
  /// 
  /// </summary>
  public class DmItemWeaponRangedMartial
    : DmItemWeaponAbstract
  {
    public DmItemTypeWeaponRangedMartial Type;

    public DmItemWeaponRangedMartial( DmItemTypeWeaponRangedMartial iName )
      : base( DmItemTypeWeaponType.Ranged, DmItemTypeWeaponClass.Martial, iName )
    {
      Type = iName;
    }
  }


  /// <summary>
  /// 
  /// </summary>
  public class DmItemWeaponRangedExotic
    : DmItemWeaponAbstract
  {
    public DmItemTypeWeaponRangedExotic Type;

    public DmItemWeaponRangedExotic( DmItemTypeWeaponRangedExotic iName )
      : base( DmItemTypeWeaponType.Ranged, DmItemTypeWeaponClass.Exotic, iName )
    {
      Type = iName;
    }
  }


  /// <summary>
  /// 
  /// </summary>
  public class DmItemWeaponAmmoSimple
    : DmItemWeaponAbstract
  {
    public DmItemTypeWeaponAmmoSimple Type;

    public DmItemWeaponAmmoSimple( DmItemTypeWeaponAmmoSimple iName )
      : base( DmItemTypeWeaponType.Ammo, DmItemTypeWeaponClass.Simple, iName )
    {
      Type = iName;
    }
  }


  /// <summary>
  /// 
  /// </summary>
  public class DmItemWeaponAmmoMartial
    : DmItemWeaponAbstract
  {
    public DmItemTypeWeaponAmmoMartial Type;

    public DmItemWeaponAmmoMartial( DmItemTypeWeaponAmmoMartial iName )
      : base( DmItemTypeWeaponType.Ammo, DmItemTypeWeaponClass.Martial, iName )
    {
      Type = iName;
    }
  }


  /// <summary>
  /// 
  /// </summary>
  public class DmItemWeaponAmmoExotic
    : DmItemWeaponAbstract
  {
    public DmItemTypeWeaponAmmoExotic Type;

    public DmItemWeaponAmmoExotic( DmItemTypeWeaponAmmoExotic iName )
      : base( DmItemTypeWeaponType.Ammo, DmItemTypeWeaponClass.Exotic, iName )
    {
      Type = iName;
    }
  }

  #endregion


  /* ------------------------------------------------------------------------------------
   *          Armor
   * --------------------------------------------------------------------------------- */
  #region Armor

  /// <summary>
  /// Armors have their own abstract class inheriting from DmItemAbstract to provide a single
  ///   place to add the descriptions required - AC, modifiers, penalties, etc.
  /// </summary>
  public abstract class DmItemArmorAbstract
    : DmItemAbstract
  {
    public DmItemTypeArmorType ArmorType;
    public int dexMod = 0;

    public DmItemArmorAbstract( object iType, object iName )
      : base( iName )
    {
      itemName = iName;
      itemSubType = iType;
      ArmorType = (DmItemTypeArmorType)iType;
      baseItemType = DmItemType.Armor;
    }

    /// <summary>
    /// TODO : Armor description should include AC, weight, penalties, etc.
    /// </summary>
    /// <returns></returns>
    public override string GetDescription()
    {
      return string.Format( "{0} quality {1}   ({2} {3}{4})",
        /* */               DmItemFactory.GetDescription( Quality ),
        /* */               DmItemFactory.GetDescription( itemName ),
        /* */               DmItemFactory.GetDescription( ArmorType ),
        /* */               DmItemFactory.GetDescription( baseItemType ),
        /* */               (Enchantment != null ? " ** Enchanted : +" + DmItemFactory.GetDescription(Enchantment)+" ** " : "" )
        /* */             );
    }
  }

  /// <summary>
  /// 
  /// </summary>
  public class DmItemArmorLight
    : DmItemArmorAbstract
  {
    public DmItemTypeArmorLight Type;

    public DmItemArmorLight( DmItemTypeArmorLight iName )
      : base( DmItemTypeArmorType.Light, iName )
    {
      Type = iName;
    }
  }


  /// <summary>
  /// 
  /// </summary>
  public class DmItemArmorMedium
    : DmItemArmorAbstract
  {
    public DmItemTypeArmorMedium Type;

    public DmItemArmorMedium( DmItemTypeArmorMedium iName )
      : base( DmItemTypeArmorType.Medium, iName )
    {
      Type = iName;
    }
  }


  /// <summary>
  /// 
  /// </summary>
  public class DmItemArmorHeavy
    : DmItemArmorAbstract
  {
    public DmItemTypeArmorHeavy Type;

    public DmItemArmorHeavy( DmItemTypeArmorHeavy iName )
      : base( DmItemTypeArmorType.Heavy, iName )
    {
      Type = iName;
    }
  }


  /// <summary>
  /// 
  /// </summary>
  public class DmItemArmorShield
    : DmItemArmorAbstract
  {
    public DmItemTypeArmorShield Type;

    public DmItemArmorShield( DmItemTypeArmorShield iName )
      : base( DmItemTypeArmorType.Shield, iName )
    {
      Type = iName;
    }
  }


  /// <summary>
  /// 
  /// </summary>
  public class DmItemArmorExtra
    : DmItemArmorAbstract
  {
    public DmItemTypeArmorExtra Type;

    public DmItemArmorExtra( DmItemTypeArmorExtra iName )
      : base( DmItemTypeArmorType.Extra, iName )
    {
      Type = iName;
    }
  }
  #endregion

}
