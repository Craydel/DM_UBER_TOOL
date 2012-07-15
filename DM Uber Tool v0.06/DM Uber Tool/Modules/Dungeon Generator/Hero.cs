using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DM_Uber_Tool
{
  class Hero : Entity
  {
    public Weapon     weapon;

    public Helm       helm;
    public Shoulders  shoulders;
    public Chest      chest;
    public Belt       belt;
    public Legs       legs;
    public Boots      boots;
    public Gloves     gloves;
    public Cloak      cloak;

    public ArrayList  gear = new ArrayList();

    public int        healthPots  = 0;
    public int        manaPots    = 0;

    public int        money       = 0;

    public Bitmap[]   gearPics;

    /// <summary>
    /// Constructor
    /// </summary>
    public Hero()
      : base( EntityType.Hero )
    {
      sightRange = 20;
      hpMax = hp = 20;
      mpMax = mp = 20;
      weapon = (Weapon)ItemFactory.CreateItem( ItemType.Dagger );

      gear.Add( helm );
      gear.Add( shoulders );
      gear.Add( chest );
      gear.Add( belt );
      gear.Add( legs );
      gear.Add( boots );
      gear.Add( gloves );
      gear.Add( cloak );

      LoadEquippedGearImages();
    }


    /// <summary>
    /// Detailed character sheet info for display
    /// </summary>
    /// <returns></returns>
    public string GetStatus()
    {
      string status = string.Empty;

      string eol = Environment.NewLine;

      status = "Stats : " + eol
             + string.Format( "  STR {0,3}   +Dmg {1,2}{2}", STR, (STR-10)/2, eol )
             + string.Format( "  DEX {0,3}   +Hit {1,2}{2}", DEX, (DEX-10)/2, eol )
             + string.Format( "  INT {0,3}             {2}", INT, 0, eol )
             + string.Format( "  WIZ {0,3}             {2}", WIZ, 0, eol )
             + string.Format( "  CON {0,3}   +AC  {1,2}{2}", CON, (CON-10)/2, eol )
             + string.Format( "  CHR {0,3}             {2}", CHR, 0, eol )
             + eol
             + "Equipment : " + eol
             + string.Format( "  Helm     {0}{1}", helm      ==null ? " -- " : helm.armor.ToString(), eol )
             + string.Format( "  Shoulder {0}{1}", shoulders ==null ? " -- " : shoulders.armor.ToString(), eol )
             + string.Format( "  Chest    {0}{1}", chest     ==null ? " -- " : chest.armor.ToString(), eol )
             + string.Format( "  Belt     {0}{1}", belt      ==null ? " -- " : belt.armor.ToString(), eol )
             + string.Format( "  Legs     {0}{1}", legs      ==null ? " -- " : legs.armor.ToString(), eol )
             + string.Format( "  Gloves   {0}{1}", gloves    ==null ? " -- " : gloves.armor.ToString(), eol )
             + string.Format( "  Boots    {0}{1}", boots     ==null ? " -- " : boots.armor.ToString(), eol )
             + string.Format( "  Cloak    {0}{1}", cloak     ==null ? " -- " : cloak.armor.ToString(), eol )
             + eol
             + "Weapon : " + eol
             + string.Format( "  Dmg      {0}{1}", MinDmg() + "-" + MaxDmg(), eol )
             + string.Format( "  +toHit   {0}{1}", ToHit(), eol )
             + eol
             + "Health Potions : " + healthPots.ToString() + eol
             + "Mana   Potions : " + manaPots.ToString() + eol
             + eol
             + "Money          : " + money.ToString() + "g";

      return status;
    }

    /// <summary>
    /// Adds up current AC and returns the value
    /// </summary>
    /// <returns></returns>
    public override int AC()
    {
      int ac = 0;

      if( helm != null )
        ac += helm.armor;

      if( shoulders != null )
        ac += shoulders.armor;

      if( chest != null )
        ac += chest.armor;

      if( belt != null )
        ac += belt.armor;

      if( legs != null )
        ac += legs.armor;

      if( boots != null )
        ac += boots.armor;

      if( gloves != null )
        ac += gloves.armor;

      if( cloak != null )
        ac += cloak.armor;

      return ac + (CON-10)/2;
    }

    /// <summary>
    /// Adds up current Minimum Damage and returns the value
    /// </summary>
    /// <returns></returns>
    public override int MinDmg()
    {
      if( weapon != null )
        return weapon.minDmg + (STR+GearStrBonus()-10)/2 + GearDamageBonus();
      else
        return Math.Max(0, (STR+GearStrBonus()-10)/2 + GearDamageBonus() );
    }

    /// <summary>
    /// Adds up current Maximum Damage and returns the value
    /// </summary>
    /// <returns></returns>
    public override int MaxDmg()
    {
      if( weapon != null )
        return weapon.maxDmg + (STR+GearStrBonus()-10)/2 + GearDamageBonus();
      else
        return Math.Max(1, (STR+GearStrBonus()-10)/2 + GearDamageBonus() );
    }

    /// <summary>
    /// Adds up current +ToHot bonuses and returns the value
    /// </summary>
    /// <returns></returns>
    public override int ToHit()
    {
      return (DEX+GearDexBonus()-10)/2 + GearHitBonus();
    }

    /// <summary>
    /// Adds up current Evade chance and returns the value
    /// </summary>
    /// <returns></returns>
    public override int EvadeScore()
    {
      return 10 + (DEX+GearDexBonus()-10)/2;
    }

    /// <summary>
    /// Attack modifiers from all gear
    /// </summary>
    /// <returns></returns>
    private int GearDamageBonus()
    {
      int bonus = 0;

      if( weapon != null )
        bonus += weapon.toDmg;

      foreach( Armor armor in gear )
        if( armor != null )
          bonus += armor.toDmg;

      return bonus;
    }

    /// <summary>
    /// +ToHit modifiers from all gear
    /// </summary>
    /// <returns></returns>
    private int GearHitBonus()
    {
      int bonus = 0;

      if( weapon != null )
        bonus += weapon.toHit;

      foreach( Armor armor in gear )
        if( armor != null )
          bonus += armor.toHit;

      return bonus;
    }

    /// <summary>
    /// Stat modifiers from all gear
    /// </summary>
    /// <returns></returns>
    private int GearStrBonus()
    {
      int bonus = 0;

      foreach( Armor armor in gear )
        if( armor != null )
          bonus += armor.toSTR;

      return bonus;
    }

    /// <summary>
    /// Stat modifiers from all gear
    /// </summary>
    /// <returns></returns>
    private int GearDexBonus()
    {
      int bonus = 0;

      foreach( Armor armor in gear )
        if( armor != null )
          bonus += armor.toDEX;

      return bonus;
    }

    /// <summary>
    /// Stat modifiers from all gear
    /// </summary>
    /// <returns></returns>
    private int GearIntBonus()
    {
      int bonus = 0;

      foreach( Armor armor in gear )
        if( armor != null )
          bonus += armor.toINT;

      return bonus;
    }

    /// <summary>
    /// Stat modifiers from all gear
    /// </summary>
    /// <returns></returns>
    private int GearWizBonus()
    {
      int bonus = 0;

      foreach( Armor armor in gear )
        if( armor != null )
          bonus += armor.toWIZ;

      return bonus;
    }

    /// <summary>
    /// Stat modifiers from all gear
    /// </summary>
    /// <returns></returns>
    private int GearConBonus()
    {
      int bonus = 0;

      foreach( Armor armor in gear )
        if( armor != null )
          bonus += armor.toCON;

      return bonus;
    }

    /// <summary>
    /// Stat modifiers from all gear
    /// </summary>
    /// <returns></returns>
    private int GearChrBonus()
    {
      int bonus = 0;

      foreach( Armor armor in gear )
        if( armor != null )
          bonus += armor.toCHR;

      return bonus;
    }

    /// <summary>
    /// Try to move/attack in a given direction based on user keypress
    /// </summary>
    /// <param name="e"></param>
    public void Move( System.Windows.Forms.KeyEventArgs e )
    {
      HealthManaRegen();

      int prevX = x;
      int prevY = y;

      // Perform move based on user input
      MoveDirection( e );

      // clear last turn pathing
      for( int i=-1; i<=1; i++ )
        for( int j=-1; j<=1; j++ )
          Core.grid[Core.hero.x+i, Core.hero.y+j].cameFrom = null;

      // check to see if we moved onto something that eaither needs to be picked up or beat up.
      Item item = null;
      Mob  mob  = null;
      if( (mob=Core.MobAt( x, y )) != null )
      {
        x = prevX;
        y = prevY;

        Attack( mob );
      }
      else if( (item=Core.ItemAt( x, y )) != null )
      {
        PickUpItem( item );
        Core.items.Remove( item );
      }
    }

    /// <summary>
    /// Fer teh cheatxorz!1!!
    /// </summary>
    /// <param name="item"></param>
    public void GiveItem( Item item )
    {
      PickUpItem( item );
    }

    /// <summary>
    /// Handles picking up inventory, adjusting item stats, money, postion counts and all that jazz
    /// </summary>
    /// <param name="item"></param>
    private void PickUpItem( Item item )
    {
      Core.UpdateHistory( "Recieved " + item.ToString() );

      if( item is TreasureChest )
      {
        foreach( Item itm in ((TreasureChest)item).contents )
          PickUpItem( itm );
      }
      else if( item is Weapon )
      {
        if( weapon == null )
        {
          weapon = (Weapon)item;
        }
        else
        {
          if( Core.Random(2)==0 )
            weapon.maxDmg++;
          else
            weapon.minDmg++;

          if( weapon.minDmg>weapon.maxDmg )
          {
            weapon.maxDmg = weapon.minDmg;
            weapon.minDmg--;
          }
        }
      }
      else if( item is HealthPotion )
      {
        healthPots++;
      }
      else if( item is ManaPotion )
      {
        manaPots++;
      }
      else if( item is Money )
      {
        money += ((Money)item).value;
      }
      else if( item is Armor )
      {
        if( item is Helm )
        {
          if( helm == null )
          {
            helm = (Helm)item;
          }
          else
          {
            helm.armor++;
          }
        }
        else if( item is Shoulders )
        {
          if( shoulders == null )
          {
            shoulders = (Shoulders)item;
          }
          else
          {
            shoulders.armor++;
          }
        }
        else if( item is Chest )
        {
          if( chest == null )
          {
            chest = (Chest)item;
          }
          else
          {
            chest.armor++;
          }
        }
        else if( item is Belt )
        {
          if( belt == null )
          {
            belt = (Belt)item;
          }
          else
          {
            belt.armor++;
          }
        }
        else if( item is Legs )
        {
          if( legs == null )
          {
            legs = (Legs)item;
          }
          else
          {
            legs.armor++;
          }
        }
        else if( item is Boots )
        {
          if( boots == null )
          {
            boots = (Boots)item;
          }
          else
          {
            boots.armor++;
          }
        }
        else if( item is Gloves )
        {
          if( gloves == null )
          {
            gloves = (Gloves)item;
          }
          else
          {
            gloves.armor++;
          }
        }
        else if( item is Cloak )
        {
          if( cloak == null )
          {
            cloak = (Cloak)item;
          }
          else
          {
            cloak.armor++;
          }
        }
      }
    }

    /// <summary>
    /// Move direction based on keypress
    /// </summary>
    /// <param name="e"></param>
    private void MoveDirection( KeyEventArgs e )
    {
      switch( e.KeyCode )
      {
        case Keys.Up:
        case Keys.I:
        case Keys.NumPad8: // N
          if(Core.grid[x, y-1].type == FloorType.DoorSecret)
          {
            Core.grid[x, y-1].type = FloorType.DoorClosed;
          }
          else if(Core.grid[x, y-1].type == FloorType.DoorClosed)
          {
            Core.grid[x, y-1].type = FloorType.DoorOpen;
          }
          else if( Core.grid[x, y-1].type != FloorType.Wall )
          {
            y--;
          }
          break;

        case Keys.O:
        case Keys.NumPad9: // NE
          if(Core.grid[x+1, y-1].type == FloorType.DoorSecret)
          {
            Core.grid[x+1, y-1].type = FloorType.DoorClosed;
          }
          else if(Core.grid[x+1, y-1].type == FloorType.DoorClosed)
          {
           Core.grid[x+1, y-1].type = FloorType.DoorOpen;
          }
          else if( Core.grid[x+1, y-1].type != FloorType.Wall )
          {
            x++;
            y--;
          }
          else if( Core.grid[x, y-1].type != FloorType.Wall ) // trying to go diagonally, but against a wall... bais to try going north/south first, then try east/west if that fails.
          {
            y--;
          }
          else if( Core.grid[x+1, y].type != FloorType.Wall )
          {
            x++;
          }
          break;

        case Keys.Right:
        case Keys.L:
        case Keys.NumPad6: // E
          if(Core.grid[x+1, y].type == FloorType.DoorSecret)
          {
            Core.grid[x+1, y].type = FloorType.DoorClosed;
          }
          else if(Core.grid[x+1, y].type == FloorType.DoorClosed)
          {
            Core.grid[x+1, y].type = FloorType.DoorOpen;
          }
          else if( Core.grid[x+1, y].type != FloorType.Wall )
          {
            x++;
          }
          break;

        case Keys.OemPeriod:
        case Keys.NumPad3: // SE
          if(Core.grid[x+1, y+1].type == FloorType.DoorSecret)
          {
            Core.grid[x+1, y+1].type = FloorType.DoorClosed;
          }
          else if(Core.grid[x+1, y+1].type == FloorType.DoorClosed)
          {
            Core.grid[x+1, y+1].type = FloorType.DoorOpen;
          }
          else if(Core.grid[x+1, y+1].type != FloorType.Wall)
          {
            x++;
            y++;
          }
          else if( Core.grid[x, y+1].type != FloorType.Wall ) // trying to go diagonally, but against a wall... bais to try going north/south first, then try east/west if that fails.
          {
            y++;
          }
          else if( Core.grid[x+1, y].type != FloorType.Wall )
          {
            x++;
          }
          break;
        
        case Keys.Down:
        case Keys.Oemcomma:
        case Keys.NumPad2: // S
          if(Core.grid[x, y+1].type == FloorType.DoorSecret)
          {
            Core.grid[x, y+1].type = FloorType.DoorClosed;
          }
          else if(Core.grid[x, y+1].type == FloorType.DoorClosed)
          {
            Core.grid[x, y+1].type = FloorType.DoorOpen;
          }
          else if( Core.grid[x, y+1].type != FloorType.Wall )
          {
            y++;
          }
          break;

        case Keys.M:
        case Keys.NumPad1: // SW
          if(Core.grid[x-1, y+1].type == FloorType.DoorSecret)
          {
            Core.grid[x-1, y+1].type = FloorType.DoorClosed;
          }
          else if(Core.grid[x-1, y+1].type == FloorType.DoorClosed)
          {
            Core.grid[x-1, y+1].type = FloorType.DoorOpen;
          }
          else if(Core.grid[x-1, y+1].type != FloorType.Wall)
          {
            x--;
            y++;
          }
          else if( Core.grid[x, y+1].type != FloorType.Wall ) // trying to go diagonally, but against a wall... bais to try going north/south first, then try east/west if that fails.
          {
            y++;
          }
          else if( Core.grid[x-1, y].type != FloorType.Wall )
          {
            x--;
          }
          break;

        case Keys.Left:
        case Keys.J:
        case Keys.NumPad4: // W
          if(Core.grid[x-1, y].type == FloorType.DoorSecret)
          {
            Core.grid[x-1, y].type = FloorType.DoorClosed;
          }
          else if(Core.grid[x-1, y].type == FloorType.DoorClosed)
          {
            Core.grid[x-1, y].type = FloorType.DoorOpen;
          }
          else if(Core.grid[x-1, y].type!= FloorType.Wall)
          {
            x--;
          }
          break;

        case Keys.U:
        case Keys.NumPad7: // NW
          if(Core.grid[x-1, y-1].type == FloorType.DoorSecret)
          {
            Core.grid[x-1, y-1].type = FloorType.DoorClosed;
          }
          else if(Core.grid[x-1, y-1].type == FloorType.DoorClosed)
          {
            Core.grid[x-1, y-1].type = FloorType.DoorOpen;
          }
          else if(Core.grid[x-1, y-1].type != FloorType.Wall)
          {
            x--;
            y--;
          }
          else if( Core.grid[x, y-1].type != FloorType.Wall ) // trying to go diagonally, but against a wall... bais to try going north/south first, then try east/west if that fails.
          {
            y--;
          }
          else if( Core.grid[x-1, y].type != FloorType.Wall )
          {
            x--;
          }
          break;

        case Keys.K:
        case Keys.NumPad5:
          if(Core.grid[x, y].type == FloorType.StairsUp || Core.grid[x, y].type == FloorType.StairsDown)
            Core.GenerateMaze();
          else
            Rest();
          break;

        default: // No movement command - rest
          Rest();
          break;
      }
    }

    /// <summary>
    /// Player not moving - resting should heal the player up some.
    /// </summary>
    private void Rest()
    {
      // for the purposes of this silly game, "resting" will yield an additional 3x normal regen.
      HealthManaRegen();
      HealthManaRegen();
      HealthManaRegen();
    }

    /// <summary>
    /// draws self on main map image at self position, then draws health/mana bars
    /// </summary>
    /// <param name="g"></param>
    public override void DrawSelf( Graphics g )
    {
      // for the purposes of maze editing, do not draw the hero
      return;

      // icon
      g.DrawImage( Core.EntityPics[(int)type], x*Core.tileSize, y*Core.tileSize, Core.tileSize, Core.tileSize );

      // equipped gear
      if( helm != null )
        g.DrawImage( gearPics[(int)ItemType.Helm], x*Core.tileSize, y*Core.tileSize, Core.tileSize, Core.tileSize );

      if( shoulders != null )
        g.DrawImage( gearPics[(int)ItemType.Shoulders], x*Core.tileSize, y*Core.tileSize, Core.tileSize, Core.tileSize );
      
      if( chest != null )
        g.DrawImage( gearPics[(int)ItemType.Chest], x*Core.tileSize, y*Core.tileSize, Core.tileSize, Core.tileSize );
      
      if( gloves != null )
        g.DrawImage( gearPics[(int)ItemType.Gloves], x*Core.tileSize, y*Core.tileSize, Core.tileSize, Core.tileSize );
      
      if( legs != null )
        g.DrawImage( gearPics[(int)ItemType.Legs], x*Core.tileSize, y*Core.tileSize, Core.tileSize, Core.tileSize );
      
      if( boots != null )
        g.DrawImage( gearPics[(int)ItemType.Boots], x*Core.tileSize, y*Core.tileSize, Core.tileSize, Core.tileSize );


      // mana bar
      if( mpMax > 0 )
        g.FillRectangle( Brushes.DarkBlue,
                          x*Core.tileSize + 0.1f*Core.tileSize,
                          y*Core.tileSize + 0.8f*Core.tileSize,
                          0.8f*Core.tileSize * (float)mp/mpMax,
                          0.1f*Core.tileSize );

      // health bar
      if( hpMax > 0 )
        g.FillRectangle( Brushes.Red,
                          x*Core.tileSize + 0.1f*Core.tileSize,
                          y*Core.tileSize + 0.9f*Core.tileSize,
                          0.8f*Core.tileSize * (float)hp/hpMax,
                          0.1f*Core.tileSize );
    }

    /// <summary>
    /// Loads images for displaying equipped
    /// </summary>
    public void LoadEquippedGearImages()
    {
      // array will have one netry per possible item, but not all will be used.
      if( gearPics == null )
        gearPics = new Bitmap[Enum.GetValues( typeof( ItemType ) ).Length];
      else
        for( int i=0; i<gearPics.Length; i++ )
          if( gearPics[i] != null )
            gearPics[i].Dispose();

      Bitmap tmp;

      tmp = new Bitmap(Core.resourcesPath + "\\" + "gear_hero_helm.bmp");
      gearPics[(int)ItemType.Helm] = (Bitmap)tmp.GetThumbnailImage( Core.tileSize, Core.tileSize, new Image.GetThumbnailImageAbort( Core.Callback ), IntPtr.Zero );
      tmp.Dispose();

      tmp = new Bitmap(Core.resourcesPath + "\\" + "gear_hero_shoulders.bmp");
      gearPics[(int)ItemType.Shoulders] = (Bitmap)tmp.GetThumbnailImage( Core.tileSize, Core.tileSize, new Image.GetThumbnailImageAbort( Core.Callback ), IntPtr.Zero );
      tmp.Dispose();

      tmp = new Bitmap(Core.resourcesPath + "\\" + "gear_hero_chest.bmp");
      gearPics[(int)ItemType.Chest] = (Bitmap)tmp.GetThumbnailImage( Core.tileSize, Core.tileSize, new Image.GetThumbnailImageAbort( Core.Callback ), IntPtr.Zero );
      tmp.Dispose();

      tmp = new Bitmap(Core.resourcesPath + "\\" + "gear_hero_legs.bmp");
      gearPics[(int)ItemType.Legs] = (Bitmap)tmp.GetThumbnailImage( Core.tileSize, Core.tileSize, new Image.GetThumbnailImageAbort( Core.Callback ), IntPtr.Zero );
      tmp.Dispose();

      tmp = new Bitmap(Core.resourcesPath + "\\" + "gear_hero_gloves.bmp");
      gearPics[(int)ItemType.Gloves] = (Bitmap)tmp.GetThumbnailImage( Core.tileSize, Core.tileSize, new Image.GetThumbnailImageAbort( Core.Callback ), IntPtr.Zero );
      tmp.Dispose();

      tmp = new Bitmap(Core.resourcesPath + "\\" + "gear_hero_boots.bmp");
      gearPics[(int)ItemType.Boots] = (Bitmap)tmp.GetThumbnailImage( Core.tileSize, Core.tileSize, new Image.GetThumbnailImageAbort( Core.Callback ), IntPtr.Zero );
      tmp.Dispose();

      for( int i=0; i<gearPics.Length; i++ )
        if( gearPics[i] != null )
          gearPics[i].MakeTransparent( Color.White );
    }

  }
}
