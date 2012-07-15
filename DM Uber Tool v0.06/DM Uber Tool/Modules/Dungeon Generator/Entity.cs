using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DM_Uber_Tool
{
  public abstract class Entity
  {
    public int        x               =  0;
    public int        y               =  0;

    // Base stats
    public int        STR             = 10;
    public int        DEX             = 10;
    public int        INT             = 10;
    public int        WIZ             = 10;
    public int        CHR             = 10;
    public int        CON             = 10;

    public int        level           =  1;

    // Combat
    internal int      minDmg          =  1;
    internal int      maxDmg          =  3;
    internal int      toHit           =  0;
    internal int      toDmg           =  0;

    // applicable to Mobs only - Hero's AC comes entirely from armor
    // TODO : change minDmg, maxDmg, toHit and AC based on mob level
    // TODO : create mobs at a certain level based on the dungeon level
    internal int      ac              =  1;

    // Health and Mana
    public int        hp              =  0;
    public int        hpMax           =  0;

    public int        mp              =  0;
    public int        mpMax           =  0;

    public int        turnsPerHpRegen = 20;
    public int        turnsPerMpRegen = 10;

    internal int      turnsLeftHp     = 20;
    internal int      turnsLeftMp     = 10;

    public bool       isDead          =  false;

    public EntityType type;

    public int        sightRange      = 0;
    public bool       xRayVision      = false;

    public int        speed           = 10;
    public int        moveLeft        =  0;

    public ArrayList  inventory       = new ArrayList();

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="t"></param>
    public Entity( EntityType t )
    { 
      // set item type
      type = t;
    }

    /// <summary>
    /// Line of slight / fog of war from hero's position
    /// </summary>
    /// <param name="gridX"></param>
    /// <param name="gridY"></param>
    /// <returns></returns>
    public bool TileVisible( int gridX, int gridY )
    {
      // simulating X-Ray vision!!1!
      return true;

      int xS=0;
      int yS=0;

      double cx = x;
      double cy = y;

      if( Math.Sqrt( Math.Pow( gridX-x, 2 ) + Math.Pow( gridY-y, 2 ) ) > sightRange )
        return false;

      if( xRayVision )
        return (Core.visibility[gridX, gridY] = true);

      double a = Math.Atan2( gridY-y, gridX-x );

      while( Math.Abs( cx-gridX )>0.5 || Math.Abs( cy-gridY )>0.5 )
      {
        xS = (int)Math.Round( cx );
        yS = (int)Math.Round( cy );

        if( xS < 0 || xS > Core.grid.GetLength(0)-1 || yS < 0 || yS > Core.grid.GetLength(1)-1 )
          return false;

        if( Core.grid[xS, yS].type == FloorType.Wall
          ||Core.grid[xS, yS].type == FloorType.DoorClosed
          ||Core.grid[xS, yS].type == FloorType.DoorSecret
          )
        {
          if( x==gridX && y==gridY )
            return (Core.visibility[gridX, gridY] = true);
          else
            return false;
        }

        cx += 1.0 * Math.Cos( a );
        cy += 1.0 * Math.Sin( a );
      }

      Core.visibility[gridX, gridY] = true;
      return true;
    }

    /// <summary>
    /// Health and mana regen per turn
    /// </summary>
    public virtual void HealthManaRegen()
    {
      // Regen health / mana
      if( --turnsLeftHp <=0 )           // countdown till regen 
      {
        turnsLeftHp = turnsPerHpRegen;  // reset count
        if( ++hp > hpMax )              // increment HP (capped at hpMAx)
          hp = hpMax;
      }

      if( --turnsLeftMp <= 0 )          // countdown till regen 
      {
        turnsLeftMp = turnsPerMpRegen;  // reset count
        if( ++mp > mpMax )              // increment MP (capped at mpMax)
          mp = mpMax;
      }
    }

    /// <summary>
    /// Attack target mob
    /// 
    /// Attack rolls are as follows :
    ///  d20Roll for attack
    ///  
    ///  if d20Roll is 20, 
    ///    defender takes (1.5x attacker's normal damage roll) damage ... no damage reduction for defender AC.  (sucks to be them.)
    ///  
    ///  If not a critical, then 
    ///    Compare attacker's_(d20Roll + toHit + DexModifier) vs target's_(10+DexModiier)
    ///      If attacker's number >= defender's number, then
    ///        defender takes (attacker's normal roll)-(defender's AC reduction) damage
    ///  
    ///  If defender's hp <= 0, defender has been slain
    ///    defender's 'isDead' flag gets set to true so Core will remove that mob from the list next update
    ///    
    ///  NOTE : Nothing happens when the player dies... their flag gets set, but Core does not 
    ///         monitor the Hero mob the same as other mobs.
    ///         Result - the game continues with the player as a walking corpse.
    /// </summary>
    /// <param name="mob"></param>
    internal void Attack( Entity mob )
    {
      int d20Roll =  Core.Random( 20 )+1;
      int damage = 0;

      string evt = string.Empty;

      if( d20Roll==20 )
      {
        // critical attack - skip armor, 1.5x dmg roll
        damage = (int)(Core.Random( MinDmg(), MaxDmg()+1 ) * 1.5);

        evt += "<< " + d20Roll.ToString() + " vs " + mob.EvadeScore().ToString() + " --> " + damage.ToString() + " - 0ac>>   ";
        evt += this.ToString() + " crits " + mob.ToString() + " for " + damage.ToString() + " pts damage";
      }
      else if( d20Roll+ToHit() >= mob.EvadeScore() )
      {
        // attack landed - roll damage'
        damage = Core.Random( MinDmg(), MaxDmg()+1 );
        evt += "<< " + (d20Roll+ToHit()).ToString() + " vs " + mob.EvadeScore().ToString() + "  --> " + damage.ToString() + " - " + mob.AC().ToString() + "ac>>   ";

        damage -= mob.AC();
        if( damage<0 )
          damage = 0;

        evt += this.ToString() + " attacked " + mob.ToString() + " for " + damage.ToString() + " pts damage";
      }
      else
      {
        // missed - no damage.  Report feeble attempt anyway.
        evt += this.ToString() + " missed " + mob.ToString();
      }


      // after attack roll, update target mob's status
      mob.hp -= damage;
      if( mob.hp <= 0 )
        mob.isDead = true;

      // append final bits of the event string (either they were killed, ot just put a period at the end
      if( mob.isDead )
        evt += ", killing them";
      
      evt += ".";

      Core.UpdateHistory( evt );
    }

    /// <summary>
    /// Combat numbers
    /// </summary>
    /// <returns></returns>
    public virtual int AC()
    {
      return ac;
    }
    public virtual int MinDmg()
    {
      return minDmg + toDmg;
    }
    public virtual int MaxDmg()
    {
      return maxDmg + toDmg;
    }
    public virtual int ToHit()
    {
      return (DEX-10)/2 + toHit;
    }
    public virtual int EvadeScore()
    {
      return 10 + (DEX-10)/2;
    }

    /// <summary>
    /// draws self on main map image at self position, then draws health/mana bars
    /// </summary>
    /// <param name="g"></param>
    public virtual void DrawSelf( Graphics g )
    {
      // icon
      g.DrawImage( Core.EntityPics[(int)type], x*Core.tileSize, y*Core.tileSize, Core.tileSize, Core.tileSize );

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
    /// Short name of the entity
    /// </summary>
    /// <returns></returns>
    public override string ToString()
    {
      return type.ToString();
    }

  }
}
