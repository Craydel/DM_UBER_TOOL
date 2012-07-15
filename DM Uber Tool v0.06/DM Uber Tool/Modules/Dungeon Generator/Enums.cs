using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DM_Uber_Tool
{
  /// <summary>
  /// Maze Type enum
  /// </summary>
  public enum MazeType
  {
    Tunneler=0,
    CircularTunneler,
    WallAdder,
    HexTunneler,
    SubDivision,
    Dungeon,
    Castle,
    Fracture,
  }

  /// <summary>
  /// Enumerator for creatures types.
  /// IMPORTANT : Should be in the same order as the list of file paths below
  ///             If you add something, you need to 
  ///               - add it to the list of filespaths below
  ///               - create the file that path refereces.
  /// </summary>
  public enum EntityType
  {
    Hero=0,

    Gargoyle,
    GreenSludge,
    Mage,
    Rogue,
    Warrior,
    Trog,
    BloodSlime,
  }

  /// <summary>
  /// Enumerator for item types.
  /// IMPORTANT : Should be in the same order as the list of file paths below
  ///             If you add something, you need to 
  ///               - add it to the list of filespaths below
  ///               - create the file that path refereces.
  /// </summary>
  public enum ItemType
  {
    Money=0,

    Sword,
    Mace,
    Staff,
    Dagger,

    Helm,
    Shoulders,
    Chest,
    Legs,
    Belt,
    Gloves,
    Boots,
    Cloak,

    Ring,
    Neck,

    HealthPotion,
    ManaPotion,

    TreasureChest,
  }

  /// <summary>
  /// Cell type for each cell in the maze
  /// </summary>
  public enum FloorType
  {
    Floor = 0,
    StairsDown,
    StairsUp,
    Wall,
    DoorClosed,
    DoorOpen,
    DoorSecret,
    Trap,
  }

  /// <summary>
  /// Define room types to build castle layouts from
  /// </summary>
  public enum CastleRoomType
  {
    GreatRoomSquare,      // 20x20 to 30x30, square, pillars optional
    GreatRoomRectangle,   // 20x20 to 30x30, rectangle, pillars optional
    GreatroomCircle,      // 10 to 15 radius, pillars optional
    BedroomMaster,        // 5x5 to 8x8
    Bedroom,              // 4x4 to 6x6
    Kitchen,              // 3x3 to 10x10
    Storage,              // 2x2 to 4x4
    Closet,               // 1x2 to 2x2
    LongHall,             // 3xN to Nx3
  }

  /// <summary>
  /// Used in room connections
  /// </summary>
  public enum Directions
  {
    North = 0,
    NorthEast,
    East,
    SouthEast,
    South,
    SouthWest,
    West,
    Northwest,
  }
}
