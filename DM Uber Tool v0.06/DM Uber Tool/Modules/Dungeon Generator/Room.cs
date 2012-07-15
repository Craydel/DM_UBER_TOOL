using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DM_Uber_Tool
{
  /// <summary>
  /// Room object - collection of FloorTile objects to be referenced as a group
  /// </summary>
  public class Room
  {
    public CastleRoomType type;
    public FloorTile[,] cells = null;

    public int x = 0;
    public int y = 0;

    public List<FloorTile> doors = new List<FloorTile>();

    /// <summary>
    /// default blank constructor
    /// </summary>
    public Room( CastleRoomType roomType)
    {
      this.type = roomType;
    }

    /// <summary>
    /// Set the offset for the room (top/left corner)
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    public void SetRoomPosition(int x, int y)
    {
      this.x = x;
      this.y = y;

      for(int i = 0; i < cells.GetLength(0); i++)
        for(int j = 0; j < cells.GetLength(1); j++)
        {
          cells[i, j].x = x+i;
          cells[i, j].y = y+j;
        }
    }
  }
}
