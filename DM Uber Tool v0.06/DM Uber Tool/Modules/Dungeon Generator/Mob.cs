using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace DM_Uber_Tool
{
  /// <summary>
  /// Mob class - enemies
  /// </summary>
  public class Mob : Entity, IComparable
  {
    public bool isTrackingHero = false;
    private int distanceToHero = 0;

    private int dx = 0;
    private int dy = 0;

    private ArrayList pathToHero = new ArrayList();

    /// <summary>
    /// COnstructor
    /// </summary>
    /// <param name="t"></param>
    public Mob( EntityType t )
      : base( t )
    {

    }


    /// <summary>
    /// Simulated level up for mobs.  Used when generating new mobs for a specific dungeon level
    /// </summary>
    /// <param name="lvl"></param>
    public void LevelUp( int lvl )
    {
      level = lvl;

      hp    *= level;
      hpMax *= level;

      mp    *= level;
      mpMax *= level;

      minDmg +=   level;
      maxDmg += 2*level;

      toHit = level % 4;
      toDmg = level % 4;
    }

    /// <summary>
    /// Update this mob's actions, movement, position, target, etc.
    /// </summary>
    public void UpdateSelf()
    {
      HealthManaRegen();
      moveLeft += speed;

      // If not tracking hero AND the hero is close enough to see/sense, then start traking hero.
      // For simplicity's sake, one a mob starts tracking the hero, they won't give up until they're dead or the hero is dead.
      if( !isTrackingHero && TileVisible( Core.hero.x, Core.hero.y)  )
        isTrackingHero = true;

      for( int i=-1; i<=1; i++ )
        for( int j=-1; j<=1; j++ )
          Core.grid[x+i, y+j].cameFrom = null;

      // If we're not tracking the hero. then wander aimlessly.
      if( isTrackingHero )
        SeekHero();
      else
        Wander();

      // distance is after move
      distanceToHero = (int)Math.Sqrt( Math.Pow( x-Core.hero.x, 2 ) + Math.Pow( y-Core.hero.y, 2 ) );
    }

    /// <summary>
    /// Random wandering if not targeting the hero
    /// </summary>
    private void Wander()
    {
      if( dx==0 && dy==0 )
      {
        dx = -1 + Core.Random( 3 );
        dy = -1 + Core.Random( 3 );
      }

      // mobs will not travel over stairs like player do.  Arbitary... allows a possible means of escape for our Hero, if otherwise surrounded.
      if(  (Core.grid[x+dx, y+dy].type == FloorType.Floor   ||
            Core.grid[x+dx, y+dy].type == FloorType.DoorOpen
           ) 
        && Core.MobAt( x+dx, y+dy )==null 
        )
      {
        // weight for move - kick back out if not enough moveLeft
        if( (dx==0 && dy!=0) || (dx!=0 && dy==0) )
        {
          if( moveLeft >= 10 )
            moveLeft -= 10;
          else
            return;
        }
        else
        {
          if( moveLeft >= 14 )
            moveLeft-= 14;
          else
            return;
        }
        
        // had enough for the move
        x += dx;
        y += dy;
      }
      else
        dx = dy = 0;
    }

    /// <summary>
    /// Simple A* Pathfinding implimented.
    /// Goal state for all mobs is to reach the hero (once they've detected them)
    /// 
    /// http://www.policyalmanac.org/games/aStarTutorial.htm
    /// http://en.wikipedia.org/wiki/A*_search_algorithm
    /// </summary>
    private void SeekHero()
    {
      ArrayList closedSet = new ArrayList();
      ArrayList openSet   = new ArrayList();

      Core.grid[x,y].G = 0;                                                         // G-Score = distance from self to this cell.  Starting cell distance = 0;
      Core.grid[x,y].H = 10*(Math.Abs(Core.hero.x-x) + Math.Abs(Core.hero.y-y));    // Guess at distance from start cell to hero - calculated with no diagonals, and a tile weight of 10 (arbitrary)
      Core.grid[x,y].F = 0;                                                         // F-Score = total distance traveled from start to this cell.  Start cell traveled dist = 0

      openSet.Add( Core.grid[x,y] );                                                // Open set is to be evaluated.  Add starting cell to this set.

      // evaluate cells in openSet until the hero cell is found
      while( openSet.Count>0 )
      {
        openSet.Sort();                               // To always pick the next cell with the lowest actual traveled distance.

        FloorTile current = openSet[0] as FloorTile;  // grab the next path square with the lowest F-Score value

        if( current == Core.grid[Core.hero.x, Core.hero.y] )
        {
          // path to hero found.  Move along it.
          MoveAlongPath();
          break;
        }

        openSet.Remove( current );    // cell being evaluated is removed from the openSet
        closedSet.Add( current );     //  ... and added to the closedSet


        // add all valid neighbors-of-current cells to the openSet
        for( int i=-1; i<=1; i++ )
          for( int j=-1; j<=1; j++ )
          {
            FloorTile neighbor = Core.grid[current.x+i, current.y+j];

            // ignore invalid cells - can't move through walls.
            if( neighbor.type == FloorType.Wall )
              continue;

            // if the neighbor has already been looked at, don't revisit it.
            if( closedSet.Contains( neighbor ) )
              continue;

            // determine tenative G-Score for neighbor cell
            int tenativeGScore = current.G; // base value = current cell G-Score

            // Add a weight to move to the neighbor cell
            if( (i==0 && j!=0) || (i!=0 && j==0) )
              tenativeGScore += 10; // straight-line weight of 10
            else
              tenativeGScore += 14; // diagonal weight of 14

            // Add additional weight if the cell is a stairwell
            //  (purely cosmetic, really... mobs can move across stairs in my program)
            if( Core.grid[neighbor.x, neighbor.y].type == FloorType.StairsDown
              ||Core.grid[neighbor.x, neighbor.y].type == FloorType.StairsUp
              )
              tenativeGScore += 10;

            // Add a HUGE bias to try to avoid cells that are currently occupied.
            //  This will allow paths around a horde surrounding the Hero if possible, but
            //  will still path through a horde if the horde is blocking the entire pathway.
            // NOTE : Preventing moving into an occupied cell is handled in the MoveAlongPath()
            //        method - this just finds a path to the hero.
            if( Core.MobAt( neighbor.x, neighbor.y ) != null )
              tenativeGScore += 80;

            bool tenativeGScoreIsBetter = false;  // pessimisitc default

            if( !openSet.Contains( neighbor ) )
            {
              openSet.Add( neighbor );
              tenativeGScoreIsBetter = true;  
            }
            else if( tenativeGScore < current.G )
            {
              tenativeGScoreIsBetter = true;
            }

            // set values and cameFrom 'pointers'
            if( tenativeGScoreIsBetter )
            {
              neighbor.cameFrom = current;
              neighbor.G = tenativeGScore;
              neighbor.H = 10*(Math.Abs(Core.hero.x-x) + Math.Abs(Core.hero.y-y));
              neighbor.F = neighbor.G + neighbor.H;
            }
          }
      }

      // Clean up after myself
      openSet.Clear();
      closedSet.Clear();
    }

    /// <summary>
    /// Called from SeekHero() once the hero cell has been pathed to.
    /// This will trace backwards along the path until the nearest neighbor tile is found
    /// If the tile is the hero, the mob will attack
    /// If the tile is empty, the mob will move to that tile
    /// If the tile is occupied, the mob does an astonishing amount of nothing until next turn.
    /// </summary>
    private void MoveAlongPath()
    {
      int moveWeight = 10;

      pathToHero.Clear();
      FloorTile target = Core.grid[ Core.hero.x, Core.hero.y ];


      while( target.cameFrom != Core.grid[x,y] )
      {
        pathToHero.Add( new PointF( (target.x+0.5f)*Core.tileSize, (target.y+0.5f)*Core.tileSize ) );
        target = target.cameFrom;
      }
      pathToHero.Add( new PointF( (target.x+0.5f)*Core.tileSize, (target.y+0.5f)*Core.tileSize ) );

      if( target.x==Core.hero.x && target.y==Core.hero.y )
      {
        Attack( Core.hero );  // The target cell is the Hero - Wail on 'em!!1!
      }
      else if( Core.MobAt( target.x, target.y ) == null )  // Path not blocked by other mobs - move to the next cell in the path
      {
        Core.grid[x, y].cameFrom = null;
        int dx = x-target.x;
        int dy = y-target.y;
        
        if( (dx==0 && dy!=0) || (dx!=0 && dy==0) )
          moveWeight = 10;  // N|S or E|W --> 10
        else
          moveWeight = 14;  // diagonal   --> 14

        if( moveLeft >= moveWeight )
        {
          x = target.x;
          y = target.y;
        }
        else
        {
          // couldn't move - add current cell to the target line list
          pathToHero.Add( new PointF( (x+0.5f)*Core.tileSize, (y+0.5f)*Core.tileSize ) );
        }

        Core.grid[x, y].cameFrom = null;
      }
      else
      {
        // Can't move along path - impatiently wait until next turn.  Maybe the mob in my way will move.
      }

      // whether we moved or not, we cannot save up more than 1 move's worth of speed for next turn.
      if( moveLeft > 10 )
        moveLeft = 0;
    }

    /// <summary>
    /// Draw self, including pathing line when tracking the Hero
    /// </summary>
    /// <param name="g"></param>
    public override void DrawSelf( Graphics g )
    {
      if( pathToHero.Count >= 2 )
        g.DrawLines( Pens.Green, (PointF[])(pathToHero.ToArray( typeof( PointF ) )) );

      base.DrawSelf( g );
    }

    /// <summary>
    /// Implimethation of CompareTo for the A* pating algorithm
    /// </summary>
    /// <param name="obj"></param>
    /// <returns></returns>
    public int CompareTo( object obj )
    {
      // Both tracking?  Closest if first
      if( isTrackingHero && ((Mob)obj).isTrackingHero )
      {
        int retVal = pathToHero.Count.CompareTo( ((Mob)obj).pathToHero.Count );

        if( retVal == 0 )
          return distanceToHero.CompareTo( ((Mob)obj).distanceToHero );

        return retVal;
      }

      // I'm not tracking, but they are?  I'm last
      if( !isTrackingHero && ((Mob)obj).isTrackingHero )
        return 1;
      
      // I'm tracking and they aren't?  I'm first
      return -1;
    }
  }

  /// <summary>
  /// Factory to create random mobs during dungeon init, or on command
  /// </summary>
  public static class MobFactory
  {
    /// <summary>
    /// Creates a random mob Entity
    /// </summary>
    /// <returns></returns>
    public static Mob CreateMob()
    {
      // Randomly pick an EntityType that's not the Hero (value 0), and create it.
      return CreateMob( (EntityType)Core.Random( 1, Enum.GetValues( typeof( EntityType ) ).Length ) );
    }

    /// <summary>
    /// Returns a Mob of the specified type
    /// </summary>
    /// <param name="t"></param>
    /// <returns></returns>
    public static Mob CreateMob( EntityType t )
    {
      Mob mob = new Mob( t );

      switch( t )
      {
        case EntityType.BloodSlime:
          mob.hpMax = mob.hp = 10;
          mob.mpMax = mob.mp =  0;
          mob.sightRange = 4;
          break;

        case EntityType.Gargoyle:
          mob.hpMax = mob.hp = 10;
          mob.mpMax = mob.mp =  0;
          mob.sightRange = 8;
          break;

        case EntityType.GreenSludge:
          mob.hpMax = mob.hp = 10;
          mob.mpMax = mob.mp =  0;
          mob.sightRange = 4;
          break;

        case EntityType.Mage:
          mob.hpMax = mob.hp = 10;
          mob.mpMax = mob.mp = 15;
          mob.sightRange = 10;
          break;

        case EntityType.Rogue:
          mob.hpMax = mob.hp = 10;
          mob.mpMax = mob.mp =  0;
          mob.sightRange = 10;
          break;

        case EntityType.Trog:
          mob.hpMax = mob.hp = 10;
          mob.mpMax = mob.mp =  0;
          mob.sightRange = 10;
          break;

        case EntityType.Warrior:
          mob.hpMax = mob.hp = 10;
          mob.mpMax = mob.mp =  0;
          mob.sightRange = 8;
          break;
      }

      // 30% chance for mobs to have loots
      if( Core.Random(100)<=30 )
        mob.inventory.Add( ItemFactory.CreateItem() );

      return mob;
    }
  }
}
