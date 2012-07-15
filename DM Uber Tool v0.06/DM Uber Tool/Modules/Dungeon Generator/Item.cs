using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace Generator
{
  /// <summary>
  /// Item class - loot!
  /// </summary>
  public abstract class Item
  {
    public  int       value = 0;
    public  int       x     = 0;
    public  int       y     = 0;
    public  ItemType  type     ;

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="t"></param>
    public Item( ItemType t )
    {
      // set item type
      type = t;
    }

    /// <summary>
    /// Draw this item
    /// </summary>
    /// <param name="g"></param>
    public void DrawSelf( Graphics g )
    {
      g.DrawImage( Core.ItemPics[(int)type], x*Core.tileSize, y*Core.tileSize, Core.tileSize, Core.tileSize );
    }


    /// <summary>
    /// Short name of item for display
    /// </summary>
    /// <returns></returns>
    public override string ToString()
    {
      return type.ToString();
    }
  }

  /// <summary>
  /// Item Factory - create random or specified Item objects
  /// </summary>
  public static class ItemFactory
  {
    /// <summary>
    /// Creates a ranodm Item object
    /// </summary>
    /// <returns></returns>
    public static Item CreateItem()
    {
      // Radomly pick any ItemType and create it
      return CreateItem( (ItemType)Core.Random( Enum.GetValues( typeof( ItemType ) ).Length ) );
    }

    /// <summary>
    /// Creates an instance of the specified type of Item
    /// </summary>
    /// <param name="type"></param>
    /// <returns></returns>
    public static Item CreateItem( ItemType type )
    {
      switch( type )
      {
        //
        // ARMORS
        //
        case ItemType.Belt:
          return new Belt();

        case ItemType.Boots:
          return new Boots();

        case ItemType.Chest:
          return new Chest();

        case ItemType.Cloak:
          return new Cloak();

        case ItemType.Gloves:
          return new Gloves();

        case ItemType.Helm:
          return new Helm();

        case ItemType.Legs:
          return new Legs();

        case ItemType.Shoulders:
          return new Shoulders();

        //
        // TRINKETS
        //
        case ItemType.Neck:
          return new Neck();

        case ItemType.Ring:
          return new Ring();

        //
        // WEAPONS
        //
        case ItemType.Dagger:
          return new Dagger();

        case ItemType.Mace:
          return new Mace();

        case ItemType.Sword:
          return new Sword();

        case ItemType.Staff:
          return new Staff();

        //
        // POTIONS
        //
        case ItemType.HealthPotion:
          return new HealthPotion();

        case ItemType.ManaPotion:
          return new ManaPotion();


        //
        // LOOT
        //
        case ItemType.TreasureChest:
          return new TreasureChest();

        //
        // MONEY
        //
        default:
          return new Money();
      }
    }
  }



  /// <summary>
  /// Base class for Weapons
  /// </summary>
  public abstract class Weapon : Item
  {
    public int toHit  = 0;  // bonus vs target DEX
    public int toDmg  = 0;  // bonus damage, added to normal damage roll
    public int minDmg = 0;  // normal damage roll - min
    public int maxDmg = 0;  // normal damage roll - max

    public Weapon( ItemType t )
      : base( t )
    {

    }
  }

  /// <summary>
  /// Base class for Armor
  /// </summary>
  public abstract class Armor : Item
  {
    // Damage absorbtion
    public int armor = 0;

    // Stat modifiers
    public int toSTR = 0;
    public int toDEX = 0;
    public int toINT = 0;
    public int toWIZ = 0;
    public int toCON = 0;
    public int toCHR = 0;

    // Raw combat modifiers (use for gloves
    public int toHit = 0;
    public int toDmg = 0;


    public Armor( ItemType t )
      : base( t )
    {

    }
  }



  /// <summary>
  /// Money item
  /// </summary>
  public class Money : Item
  {
    public Money()
      : base( ItemType.Money )
    {
      value = Core.Random( 15 ) + 5;
    }
  }

  /// <summary>
  /// Health Potion item
  /// </summary>
  public class HealthPotion : Item
  {
    public int hp = 20; //health restored

    public HealthPotion()
      : base( ItemType.HealthPotion )
    {

    }
  }

  /// <summary>
  ///  Mana Potion Item
  /// </summary>
  public class ManaPotion : Item
  {
    public int mp = 20; // mana restored

    public ManaPotion()
      : base( ItemType.ManaPotion )
    {

    }
  }

  /// <summary>
  /// Treasure Chest Item
  /// 
  /// This is the only Item treated as a special case.
  /// Treasure chests have other Item() objects in them.
  /// These are generated automgiacally; every chest will have 
  ///   a Money item at a minimum, and possibly additional items as loot.
  /// </summary>
  public class TreasureChest : Item
  {
    public ArrayList contents = new ArrayList();

    /// <summary>
    /// Constructor with no items specified
    ///   Will generate a chest that either only has money, or has money and up to 4 items.
    /// </summary>
    public TreasureChest()
      : base( ItemType.TreasureChest )
    {
      contents.Add( ItemFactory.CreateItem( ItemType.Money ) );

      if( Core.Random( 100 ) < 50 ) // 50% chance to create items
      {
        int numItems = Core.Random( 4 );
        for( int i=0; i<numItems; i++ )
          CreateItem();
      }
    }

    /// <summary>
    /// Constructor with a specific number of items required
    /// </summary>
    /// <param name="numItems"></param>
    public TreasureChest( int numItems )
      : base( ItemType.TreasureChest )
    {
      contents.Add( ItemFactory.CreateItem( ItemType.Money ) );

      for( int i=0; i<numItems; i++ )
        CreateItem();
    }

    /// <summary>
    /// Constructor with a min/max number of items required
    /// </summary>
    /// <param name="minItems"></param>
    /// <param name="maxItems"></param>
    public TreasureChest( int minItems, int maxItems )
      : base( ItemType.TreasureChest )
    {
      contents.Add( ItemFactory.CreateItem( ItemType.Money ) );

      // nim/max number of items specified - add a random number of item in that range
      int num = Core.Random( minItems, maxItems );
      for( int i=0; i<num; i++ )
        CreateItem();
    }

    /// <summary>
    /// Private method to create an item at random and add it to the Chest's contents.  Used by the constructors only.
    /// </summary>
    private void CreateItem()
    {
      contents.Add( ItemFactory.CreateItem() );
    }
  }



  /// <summary>
  /// Chest Armor
  /// </summary>
  public class Chest : Armor
  {
    public Chest()
      : base( ItemType.Chest )
    {
      
    }
  }

  /// <summary>
  /// Belth Armor
  /// </summary>
  public class Belt : Armor
  {
    public Belt()
      : base( ItemType.Belt )
    {
      
    }
  }

  /// <summary>
  /// Boots Armor
  /// </summary>
  public class Boots : Armor
  {
    public Boots()
      : base( ItemType.Boots )
    {

    }
  }

  /// <summary>
  /// Cloak Armor
  /// </summary>
  public class Cloak : Armor
  {
    public Cloak()
      : base( ItemType.Cloak )
    {

    }
  }

  /// <summary>
  /// Gloves Armor
  /// </summary>
  public class Gloves : Armor
  {
    public Gloves()
      : base( ItemType.Gloves )
    {

    }
  }

  /// <summary>
  /// Helm Armor
  /// </summary>
  public class Helm : Armor
  {
    public Helm()
      : base( ItemType.Helm )
    {

    }
  }

  /// <summary>
  /// Legs Armor
  /// </summary>
  public class Legs : Armor
  {
    public Legs()
      : base( ItemType.Legs )
    {

    }
  }

  /// <summary>
  /// Shoulders Armor
  /// </summary>
  public class Shoulders : Armor
  {
    public Shoulders()
      : base( ItemType.Shoulders )
    {

    }
  }



  /// <summary>
  /// Neck Armor (necklace / trinket)
  /// </summary>
  public class Neck : Armor
  {
    public Neck()
      : base( ItemType.Neck )
    {

    }
  }

  /// <summary>
  /// Ring Armor (finger)
  /// </summary>
  public class Ring : Armor
  {
    public Ring()
      : base( ItemType.Ring )
    {

    }
  }



  /// <summary>
  /// Dagger Weapon
  /// </summary>
  public class Dagger : Weapon
  {
    public Dagger()
      : base( ItemType.Dagger )
    {
      toHit  = 1;
      minDmg = 1;
      maxDmg = 3;
    }
  }

  /// <summary>
  /// Staff Weapon
  /// </summary>
  public class Staff : Weapon
  {
    public Staff()
      : base( ItemType.Staff )
    {
      minDmg = 1;
      maxDmg = 4;
    }
  }

  /// <summary>
  /// Sword Weapon
  /// </summary>
  public class Sword : Weapon
  {
    public Sword()
      : base( ItemType.Sword )
    {
      minDmg = 2;
      maxDmg = 8;
    }
  }

  /// <summary>
  /// Mace Weapon
  /// </summary>
  public class Mace : Weapon
  {
    public Mace()
      : base( ItemType.Mace )
    {
      minDmg = 5;
      maxDmg = 5;
    }
  }

}
