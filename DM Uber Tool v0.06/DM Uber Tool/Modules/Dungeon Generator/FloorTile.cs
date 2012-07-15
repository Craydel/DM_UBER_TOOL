using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DM_Uber_Tool
{
  /// <summary>
  /// FloorTile - base onject type for each tile in a maze
  /// </summary>
  public class FloorTile : IComparable
  {
    public FloorType type;

    public int x = 0;
    public int y = 0;

    public int F = 0;
    public int G = 0;
    public int H = 0;

    public FloorTile cameFrom = null;

    /// <summary>
    /// Constructor, default to Wall
    /// </summary>
    /// <param name="xV"></param>
    /// <param name="yV"></param>
    public FloorTile(int xV, int yV)
    {
      type=FloorType.Wall;
      x = xV;
      y = yV;
    }

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="t"></param>
    /// <param name="xV"></param>
    /// <param name="yV"></param>
    public FloorTile(int xV, int yV, FloorType t)
    {
      type = t;
      x = xV;
      y = yV;
    }

    /// <summary>
    /// IComparable = specifies how to sort this object in a list of other objects of this type.
    /// In this case, we're using this to impliment the A* Pathfinding algorithm, which checks 
    ///    the F-Score of each tile to pick the lowest one.  Thus, we will sort on the F value of
    ///    this object by comparing it to the reference 'obj' F value
    ///    
    /// This tries to look at 'obj' as a FloorTile object...
    ///    if 'obj' is not actually a FloorTile, this will throw an error and your computer 
    ///    will catch on fire.  Maybe.
    /// </summary>
    /// <param name="obj"></param>
    /// <returns></returns>
    public int CompareTo(object obj)
    {
      // CompareTo is a built in function
      // It will return the value of 
      //  -1 indicating the left-hand 'thing' is less than    the thing compared to
      //   0 indicating the left-hand 'thing' is the same as  the thing compared to
      //   1 indicating the left-hand 'thing' is greater than the thing compared to

      return this.F.CompareTo(((FloorTile)obj).F);
    }
  }
}
