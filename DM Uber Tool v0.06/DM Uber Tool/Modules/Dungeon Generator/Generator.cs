using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace Generator
{
  /// <summary>
  /// Generates a random maze of a requested or random style:
  ///  Tunneler
  ///    Given a full grid of walls, knocks them down to form the maze
  ///    
  ///  Wall Adder
  ///    Given an empty grid with no walls, adds walls to create the maze
  ///    
  ///  Subdivision
  ///    Given an empty grid
  ///      Divides it into 4 sub grids
  ///      Puts walls up around each sub grid
  ///      Kncoks down walls to connect those subgrids
  ///      Recursively calls itself on each subgrid until the subgrid is 1x1.
  ///      
  ///  Hex Tunneler
  ///    Given a hex grid of walls, knocks walls down to form the maze
  ///    
  ///  Dungeon
  ///    Given a full grid, hollows out rooms at random, then connects those rooms at random with paths.  
  ///    Definitely not a "True Maze", as there can be multiple paths from one cell to another
  ///    All rooms will get connected to each other
  ///    
  ///  Castle
  ///    Similar in nature to a Dungeon, but with specified sizes, number, and proximity rules applied to rooms and connections.
  ///    
  ///  Circular
  ///    Tunneler style maze, but the base grid is a series of concentric rings.
  /// </summary>
  static class MazeGenerator
  {
    private static Random rand = new Random();
    public static Color[] TileColor = null;
    public static FloorTile[,] grid;

    #region Generate Maze Overload Methods

    /// <summary>
    /// Generates a random maze style at 100% cells used
    /// </summary>
    /// <param name="width"></param>
    /// <param name="height"></param>
    /// <param name="pathWidth"></param>
    /// <returns></returns>
    public static FloorTile[,] GenerateMaze( int width, int height, int pathWidth )
    {
      return GenerateMaze(width, height, pathWidth, (MazeType)(rand.Next(Enum.GetValues(typeof(MazeType)).Length)), 1.0f);
    }

    /// <summary>
    /// Generates a specified maze style at 100% cells used
    /// </summary>
    /// <param name="width"></param>
    /// <param name="height"></param>
    /// <param name="pathWidth"></param>
    /// <param name="type"></param>
    /// <returns></returns>
    public static FloorTile[,] GenerateMaze( int width, int height, int pathWidth, MazeType type )
    {
      return GenerateMaze(width, height, pathWidth, type, 1.0f);
    }

    /// <summary>
    /// Generates a random style maze style at the specified percent cells used
    /// </summary>
    /// <param name="width"></param>
    /// <param name="height"></param>
    /// <param name="pathWidth"></param>
    /// <param name="percentFull"></param>
    /// <returns></returns>
    public static FloorTile[,] GenerateMaze( int width, int height, int pathWidth, float percentFull )
    {
      return GenerateMaze(width, height, pathWidth, (MazeType)(rand.Next(Enum.GetValues(typeof(MazeType)).Length)), percentFull);
    }

    /// <summary>
    /// Generates a specified maze style at the specified percent cells used (for applicable maze types).
    /// </summary>
    /// <param name="width"></param>
    /// <param name="height"></param>
    /// <param name="pathWidth"></param>
    /// <param name="type"></param>
    /// <param name="percentFull"></param>
    /// <returns></returns>
    public static FloorTile[,] GenerateMaze( int width, int height, int pathWidth, MazeType type, float percentFull )
    {
      // colors for bitmap - used to draw the minimap for each generated maze
      TileColor = new Color[Enum.GetValues(typeof(FloorType)).Length];
      TileColor[(int)FloorType.DoorClosed] = Color.Brown;
      TileColor[(int)FloorType.DoorOpen] = Color.Tan;

      TileColor[(int)FloorType.Wall] = Color.DarkGray;
      TileColor[(int)FloorType.DoorSecret] = Color.DarkGray;

      TileColor[(int)FloorType.Floor] = Color.BlanchedAlmond;
      TileColor[(int)FloorType.Trap] = Color.BlanchedAlmond;

      TileColor[(int)FloorType.StairsDown] = Color.Green;
      TileColor[(int)FloorType.StairsUp] = Color.DarkGreen;

      switch( type )
      {
        case MazeType.HexTunneler:
          return GenerateStyle_HexTunneler(width, height, pathWidth, percentFull);

        case MazeType.SubDivision:
          return GenerateStyle_Subdivision(width, height, pathWidth, percentFull);

        case MazeType.WallAdder:
          return GenerateStyle_WallAdder(width, height, pathWidth);

        case MazeType.Dungeon:
          return GenerateStyle_Dungeon(width, height, pathWidth);

        case MazeType.Castle:
          return GenerateStyle_Castle(width, height, pathWidth, percentFull);

        case MazeType.CircularTunneler:
          return GenerateStyle_CircularTunneler(width, height, pathWidth, percentFull);

        case MazeType.Fracture:
          return GenerateStyle_Fracture(width, height, pathWidth);

        default:
          return GenerateStyle_Tunneler(width, height, pathWidth, percentFull);
      }
    }

    #endregion

    #region Castle

    /// <summary>
    /// Proposed implimentation : assume a tile is about 2ft square 
    ///  Great Rooms/Halls (30-40ft by 60-100 ft, rectangular )
    ///  Living rooms (15-20 ft square)
    ///  Bedrooms (small - 10ft to 15ft square or rectangular)
    ///  Closet/Storage rooms (tiny - 4ft to 6ft square or rectangular)
    /// 
    /// Connections : 
    ///  Place largest room first
    ///  Spawn hallways from that room (based on room size - great halls should get several, living areas get 2, or soemthing similar to that logic)
    ///  Chance to spawn halls of arbitrary length from existing halls (make a more detailed castle layout?)
    ///  
    ///  Place remaining rooms/closets along halls/rooms with 1 cell wall between and open a path to the nearest room or rooms
    ///    Halls get as many as they have rooms neighboring,
    ///    Living areas get several, but not necessarily one for every room
    ///    Bedrooms get one
    ///    Closets get one
    ///    
    ///  After all rooms get placed, scan hallways for open wall connections, and attach closets sporatically to fill them out some
    ///  
    /// Possibilities : 
    ///  Percent total area covered by each room type
    ///    Generate Great Halls until that percent area is covered,
    ///    Generate Living Areas until that percent area is covered,
    ///    Generate Bedrooms until that percent area is covered,
    ///    Spawn closets and storage like crazy
    ///    Stop generation loop once closet count is reached, all bedrooms have at least one closet OR no valid spots left to add.
    ///    
    ///  Specific count of each room type (possibly easier) :
    ///    If Greatroom count > 0
    ///      Spawn greatroom(s)
    ///      Spawn hallway(s)
    ///        Connect Greatrooms if multiple
    ///      
    ///    If Living Areas count > 0
    ///      Spawn living areas
    ///      Spawn hallways
    ///        Connect to all rooms within a certain radius?  Well connected floorplan...?
    ///      Spawn secret passages (muahaha...)
    ///      
    ///    If Bedroom count > 0
    ///      Spawn bedrooms
    ///      Spawn closets
    ///      Spawn secret passages (even more muahaha...)
    ///      
    ///    Spawn closets along remaining hallways, minimum distance from existing rooms/doorways (avoid over populating with closets, or stop at set count?)
    ///      Spawn secret passages from closets (rediculous quantities of muahaha...)
    ///      
    ///    For each room type, auto spawn specific loot - i.e.: pre-populated (pseudo random) chests.
    /// </summary>
    /// <param name="width"></param>
    /// <param name="height"></param>
    /// <param name="pathWidth"></param>
    /// <param name="percentFull"></param>
    /// <returns></returns>
    private static FloorTile[,] GenerateStyle_Castle( int width, int height, int pathWidth, float percentFull )
    {
      // Progress dialog usually doesn't get to display for too long... might not be needed.
      DM_Uber_Tool.ProgressDialog dlg = new DM_Uber_Tool.ProgressDialog();

      // set overall maze size and initialize each cell
      int castleScale = 1;
      grid = new FloorTile[width * pathWidth * castleScale, height * pathWidth * castleScale];
      for( int i = 0; i < width * pathWidth * castleScale; i++ )
        for( int j = 0; j < pathWidth * height * castleScale; j++ )
          grid[i, j] = new FloorTile(i, j); // defaults to Wall

      // set progress dialog references
      dlg.ShowProgressDialog();
      dlg.flags = grid;
      dlg.status = "Placing rooms...";
      dlg.percentDone = 0;

      List<Room> rooms = new List<Room>();
      Room room = null;
      List<CastleRoomType> roomsToPlace = CreateCastleLayout();

      if( roomsToPlace == null )
      {
        #region If the layout is null, this will simply jam room after room into the availabel area until it can't fit any more
        int tries = 0;
        int maxTries = 5;

        while( tries++ < maxTries )
        {
          int doorCount = 0;
          foreach( Room r in rooms )
            foreach( FloorTile d in r.doors )
              if( d.type == FloorType.DoorClosed )
                doorCount++;

          dlg.status = string.Format("Placing Rooms (fails={0}, rooms={1}, doors={2})", tries, rooms.Count, doorCount);

          CastleRoomType roomType = (CastleRoomType)rand.Next(Enum.GetValues(typeof(CastleRoomType)).Length);

          if( (room = GenerateGenericRoom(rooms, roomType)) != null )
          {
            // to list of existing rooms for next room to refernce doors from
            rooms.Add(room);

            // update the grid[,] of placed tiles
            foreach( FloorTile cell in room.cells )
              grid[cell.x, cell.y].type = cell.type;

            tries = 0;  // reset once a room is placed
          }
        }
        #endregion
      }
      else
      {
        #region Specified layout provided - will try to use only those rooms, in the specified order when placing
        // For each room in the list of rooms to place, try to place that room
        for( int r = 0; r < roomsToPlace.Count; r++ )
        {
          if( (room = GenerateGenericRoom(rooms, roomsToPlace[r])) != null )
          {
            // to list of existing rooms for next room to refernce doors from
            rooms.Add(room);

            // update the grid[,] of placed tiles
            foreach( FloorTile cell in room.cells )
              grid[cell.x, cell.y].type = cell.type;
          }
        }
        #endregion
      }

      // Once all rooms are placed, find all doors and try to connect a closet to each one.  Fill it out a little.
      // If a door cannot have a closet attached, remove the door (set back to wall)
      List<FloorTile> doors = new List<FloorTile>();
      foreach( Room r in rooms )
        foreach( FloorTile d in r.doors )
          if( d.type == FloorType.DoorClosed )
            doors.Add( d );

      foreach( FloorTile targetDoor in doors )
      {
        if( (room = GenerateGenericRoom(rooms, CastleRoomType.Closet, targetDoor)) != null )
        {
          // to list of existing rooms for next room to refernce doors from
          rooms.Add(room);

          // update tje grid[,] of placed tiles
          foreach( FloorTile cell in room.cells )
          {
            if( cell.x==targetDoor.x && cell.y==targetDoor.y )
              grid[cell.x, cell.y].type = FloorType.DoorOpen;
            else
              grid[cell.x, cell.y].type = cell.type;
          }
        }
        else
        {
          // couldn't attach a closet - change from door to wall (cosmetic, really)

          //
          // TODO - need to remove doors from rooms when being attached, both the new placed room and the room that was attached to.
          //        Otherwise this removes all the currently connected doors... not sure how to tackle this yet.
          grid[ targetDoor.x, targetDoor.y].type = FloorType.Floor;
        }
      }

      //finally pass - close all doors that were opened as flags
      for( int i = 0; i < width * pathWidth * castleScale; i++ )
        for( int j = 0; j < pathWidth * height * castleScale; j++ )
          if( grid[i,j].type == FloorType.DoorOpen )
            grid[i,j].type = FloorType.DoorClosed;

      //dlg.CanClose = true; // generation done - let the user click to close after admiring the nifty black&white for a bit.
      dlg.CloseProgressDialog();

      return grid;
    }

    /// <summary>
    /// Randomly decide how many rooms of what type this castle will have
    /// </summary>
    /// <returns></returns>
    private static List<CastleRoomType> CreateCastleLayout()
    {
      List<CastleRoomType> rooms = new List<CastleRoomType>();
      
      switch( rand.Next(5) )
      {
        case 1:
          #region Sveral rooms, no greatroom.  Built from a LongHall
          rooms.Add(CastleRoomType.LongHall);
          rooms.Add(CastleRoomType.BedroomMaster);
          rooms.Add(CastleRoomType.BedroomMaster);
          rooms.Add(CastleRoomType.BedroomMaster);
          rooms.Add(CastleRoomType.BedroomMaster);
          rooms.Add(CastleRoomType.BedroomMaster);
          rooms.Add(CastleRoomType.BedroomMaster);
          rooms.Add(CastleRoomType.BedroomMaster);
          rooms.Add(CastleRoomType.BedroomMaster);
          rooms.Add(CastleRoomType.BedroomMaster);
          rooms.Add(CastleRoomType.BedroomMaster);
          rooms.Add(CastleRoomType.BedroomMaster);
          rooms.Add(CastleRoomType.BedroomMaster);
          rooms.Add(CastleRoomType.BedroomMaster);
          rooms.Add(CastleRoomType.BedroomMaster);
          rooms.Add(CastleRoomType.BedroomMaster);
          rooms.Add(CastleRoomType.Storage);
          rooms.Add(CastleRoomType.Storage);
          rooms.Add(CastleRoomType.Storage);
          rooms.Add(CastleRoomType.Storage);
          rooms.Add(CastleRoomType.Storage);
          rooms.Add(CastleRoomType.Storage);
          rooms.Add(CastleRoomType.Storage);
          rooms.Add(CastleRoomType.Storage);
          #endregion
          break;

        case 2:
          #region based on a Greatroom, not many rooms
          rooms.Add(CastleRoomType.GreatRoomSquare);
          rooms.Add(CastleRoomType.LongHall);
          rooms.Add(CastleRoomType.BedroomMaster);
          rooms.Add(CastleRoomType.BedroomMaster);
          rooms.Add(CastleRoomType.BedroomMaster);
          rooms.Add(CastleRoomType.BedroomMaster);
          rooms.Add(CastleRoomType.Storage);
          rooms.Add(CastleRoomType.Storage);
          rooms.Add(CastleRoomType.Storage);
          rooms.Add(CastleRoomType.Storage);
          rooms.Add(CastleRoomType.Storage);
          rooms.Add(CastleRoomType.Storage);
          #endregion
          break;

        case 3:
          #region Small structure, based on a Long Hall, few rooms, lots of closets
          rooms.Add(CastleRoomType.LongHall);
          rooms.Add(CastleRoomType.BedroomMaster);
          rooms.Add(CastleRoomType.BedroomMaster);
          rooms.Add(CastleRoomType.BedroomMaster);
          rooms.Add(CastleRoomType.BedroomMaster);
          #endregion
          break;

        case 4:
          #region Sprawling, poorly designed castle/keep - several greatrooms, many, many other rooms.
          rooms.Add(CastleRoomType.GreatroomCircle);
          rooms.Add(CastleRoomType.LongHall);
          rooms.Add(CastleRoomType.GreatroomCircle);
          rooms.Add(CastleRoomType.LongHall);
          rooms.Add(CastleRoomType.LongHall);
          rooms.Add(CastleRoomType.GreatRoomRectangle);
          rooms.Add(CastleRoomType.GreatRoomRectangle);
          rooms.Add(CastleRoomType.GreatRoomSquare);
          rooms.Add(CastleRoomType.GreatRoomSquare);
          rooms.Add(CastleRoomType.LongHall);
          rooms.Add(CastleRoomType.LongHall);
          rooms.Add(CastleRoomType.LongHall);
          rooms.Add(CastleRoomType.LongHall);
          rooms.Add(CastleRoomType.LongHall);
          rooms.Add(CastleRoomType.BedroomMaster);
          rooms.Add(CastleRoomType.BedroomMaster);
          rooms.Add(CastleRoomType.BedroomMaster);
          rooms.Add(CastleRoomType.BedroomMaster);
          rooms.Add(CastleRoomType.BedroomMaster);
          rooms.Add(CastleRoomType.Bedroom);
          rooms.Add(CastleRoomType.Bedroom);
          rooms.Add(CastleRoomType.Bedroom);
          rooms.Add(CastleRoomType.Bedroom);
          rooms.Add(CastleRoomType.Bedroom);
          rooms.Add(CastleRoomType.Bedroom);
          rooms.Add(CastleRoomType.Bedroom);
          rooms.Add(CastleRoomType.Bedroom);
          rooms.Add(CastleRoomType.Bedroom);
          rooms.Add(CastleRoomType.Bedroom);
          rooms.Add(CastleRoomType.LongHall);
          rooms.Add(CastleRoomType.LongHall);
          rooms.Add(CastleRoomType.Kitchen);
          rooms.Add(CastleRoomType.Kitchen);
          rooms.Add(CastleRoomType.Kitchen);
          rooms.Add(CastleRoomType.Kitchen);
          rooms.Add(CastleRoomType.Storage);
          rooms.Add(CastleRoomType.Storage);
          rooms.Add(CastleRoomType.Storage);
          rooms.Add(CastleRoomType.Storage);
          rooms.Add(CastleRoomType.Storage);
          rooms.Add(CastleRoomType.GreatroomCircle);
          rooms.Add(CastleRoomType.LongHall);
          rooms.Add(CastleRoomType.GreatroomCircle);
          rooms.Add(CastleRoomType.LongHall);
          rooms.Add(CastleRoomType.LongHall);
          rooms.Add(CastleRoomType.GreatRoomRectangle);
          rooms.Add(CastleRoomType.GreatRoomRectangle);
          rooms.Add(CastleRoomType.GreatRoomSquare);
          rooms.Add(CastleRoomType.GreatRoomSquare);
          rooms.Add(CastleRoomType.LongHall);
          rooms.Add(CastleRoomType.LongHall);
          rooms.Add(CastleRoomType.LongHall);
          rooms.Add(CastleRoomType.LongHall);
          rooms.Add(CastleRoomType.LongHall);
          rooms.Add(CastleRoomType.BedroomMaster);
          rooms.Add(CastleRoomType.BedroomMaster);
          rooms.Add(CastleRoomType.BedroomMaster);
          rooms.Add(CastleRoomType.BedroomMaster);
          rooms.Add(CastleRoomType.BedroomMaster);
          rooms.Add(CastleRoomType.Bedroom);
          rooms.Add(CastleRoomType.Bedroom);
          rooms.Add(CastleRoomType.Bedroom);
          rooms.Add(CastleRoomType.Bedroom);
          rooms.Add(CastleRoomType.Bedroom);
          rooms.Add(CastleRoomType.Bedroom);
          rooms.Add(CastleRoomType.Bedroom);
          rooms.Add(CastleRoomType.Bedroom);
          rooms.Add(CastleRoomType.Bedroom);
          rooms.Add(CastleRoomType.Bedroom);
          rooms.Add(CastleRoomType.LongHall);
          rooms.Add(CastleRoomType.LongHall);
          rooms.Add(CastleRoomType.Kitchen);
          rooms.Add(CastleRoomType.Kitchen);
          rooms.Add(CastleRoomType.Kitchen);
          rooms.Add(CastleRoomType.Kitchen);
          rooms.Add(CastleRoomType.Storage);
          rooms.Add(CastleRoomType.Storage);
          rooms.Add(CastleRoomType.Storage);
          rooms.Add(CastleRoomType.Storage);
          rooms.Add(CastleRoomType.Storage);
          #endregion
          break;

        default:
          // will simply fill the area with as many rooms of whatever type as it can (no specified layout)
          rooms = null;
          break;
      }

      return rooms;
    }
    
    /// <summary>
    /// Tries to place the specified room.
    /// True = successful, and the connecting door has been flagged.
    /// False = unable to find a connection.
    /// </summary>
    /// <param name="rooms"></param>
    /// <param name="room"></param>
    /// <param name="roomType"></param>
    /// <param name="allowConnectionBias"></param>
    /// <param name="targetDoor"></param>
    /// <returns></returns>
    private static bool PlaceRoom(List<Room> rooms, Room room, CastleRoomType roomType, bool allowConnectionBias, FloorTile targetDoor )
    {
      int w = room.cells.GetLength(0);
      int h = room.cells.GetLength(1);

      if( rooms.Count == 0 )
      {
        // place in middle
        room.SetRoomPosition(grid.GetLength(0)/2 - w/2, grid.GetLength(1)/2 - h/2);
      }
      else
      {
        // pick a door to attach to, build from there.
        List<FloorTile> doors = new List<FloorTile>();
        FloorTile connectionDoor = null;

        if( targetDoor != null )
        {
          doors.Add(targetDoor);
        }
        else if( allowConnectionBias )
        {
          switch( roomType )
          {
            case CastleRoomType.LongHall:
              // custom bias to connect long Halls to Great Rooms if at all possible.
              foreach( Room r in rooms )
                if( r.type == CastleRoomType.GreatroomCircle
                  ||r.type == CastleRoomType.GreatRoomSquare
                  ||r.type == CastleRoomType.GreatRoomRectangle
                  )
                  foreach( FloorTile door in r.doors )
                    if( door.type == FloorType.DoorClosed)
                      doors.Add(door);
              break;

            case CastleRoomType.GreatroomCircle:
            case CastleRoomType.GreatRoomRectangle:
            case CastleRoomType.GreatRoomSquare:
              // Greatrooms should try to attach to the end of hallways.
              // Since the LongHall are generated with the end doors first, we can 
              //   reference those tow doors directly without searching though the room's doors list
              foreach( Room r in rooms )
                if( r.type == CastleRoomType.LongHall )
                {
                  if( r.doors[0].type == FloorType.DoorClosed)
                    doors.Add(r.doors[0]);

                  if( r.doors[1].type == FloorType.DoorClosed)
                    doors.Add(r.doors[1]);
                }
              break;

            default:
              // no custom doors list - the default of "All of them" will be used below
              break;
          }
        }

        // Check for custom doors list - if there are none, default to use all doors in all rooms for tries.
        if( doors.Count == 0 )
        {
          foreach( Room r in rooms )
            foreach( FloorTile door in r.doors )
              if( door.type == FloorType.DoorClosed)
                doors.Add(door);
        }


        // In the event of a LongHall, we'd like to attach it so that it 
        //  traveles away from the room being attached to, rather than attaching along the side of the hall.
        Directions[] directions;
        if( roomType == CastleRoomType.LongHall )
        {
          if( w>h )
          {
            // E/W hall - try to attach on East or West side only
            directions = new Directions[] { Directions.East, Directions.West };
          }
          else
          {
            // N/S hall - attach north or south only.
            directions = new Directions[] { Directions.North, Directions.South };
          }
        }
        else
        {
          // all other rooms may try to attach from any direction
          directions = new Directions[] { Directions.North, Directions.East, Directions.South, Directions.West };
        }


        while( doors.Count>0 && connectionDoor==null )
        {
          // pick a door
          // try to attach to it to the N/E/W/S
          //   update room position accordingly
          //   check room.cells against all other room.cells
          // break loop when successful

          FloorTile door = doors[rand.Next(doors.Count)];
          doors.Remove(door);

          if( CheckRoomAgainstGrid(room, door, directions) )
          {
            connectionDoor = door;
            break;
          }
        }


        // failed to place room
        if( connectionDoor == null )
          return false;

        //set the connection door
        foreach( FloorTile cell in room.cells )
          if( cell.x==connectionDoor.x && cell.y==connectionDoor.y )
          {
            cell.type = FloorType.DoorOpen;
            connectionDoor.type = FloorType.DoorOpen;
            break;
          }
      }
      return true;
    }

    /// <summary>
    /// True = valid placement found, room has been updated to the correct offset.
    /// False = no valid connections, room has been updated to last tried offset (but is probably useless).
    /// </summary>
    /// <param name="room"></param>
    /// <param name="door"></param>
    /// <returns></returns>
    private static bool CheckRoomAgainstGrid( Room room, FloorTile door )
    {
      return CheckRoomAgainstGrid(room, door, new Directions[] { Directions.North, Directions.East, Directions.South, Directions.West });
    }
    
    /// <summary>
    /// True = valid placement found, room has been updated to the correct offset.
    /// False = no valid connections, room has been updated to last tried offset (but is probably useless).
    /// </summary>
    /// <param name="room"></param>
    /// <param name="door"></param>
    /// <param name="directions"></param>
    /// <returns></returns>
    private static bool CheckRoomAgainstGrid( Room room, FloorTile door, Directions[] directions )
    {
      // kind of cumbersome, but allows random selection and deletion by reference.
      //   Arrays are more difficult to remove things from.
      List<Directions> dirs = new List<Directions>();
      for( int i=0; i<directions.Length; i++ )
        dirs.Add(directions[i]);

      // try attaching to the north
      int w = room.cells.GetLength(0);
      int h = room.cells.GetLength(1);

      Directions d;

      // Pick a direction specified, and test it.  
      // Avoids obvious bias against North, and allows specific tests for certain room types (halls, for example)
      while( dirs.Count > 0 )
      {
        d = dirs[rand.Next(dirs.Count)];
        dirs.Remove(d);

        switch( d )
        {
          case Directions.North:  // try attaching to the north
            room.SetRoomPosition(door.x - w/2, door.y-h+1);
            if( CheckRoomTilesAgainstGrid(room, door) )
              return true;
            break;

          case Directions.South: // try attaching to the south
            room.SetRoomPosition(door.x - w/2, door.y);
            if( CheckRoomTilesAgainstGrid(room, door) )
              return true;
            break;

          case Directions.East: // try attaching to the east
            room.SetRoomPosition(door.x-w+1, door.y-h/2);
            if( CheckRoomTilesAgainstGrid(room, door) )
              return true;
            break;

          case Directions.West: // try attaching to the west
            room.SetRoomPosition(door.x, door.y-h/2);
            if( CheckRoomTilesAgainstGrid(room, door) )
              return true;
            break;
        }
      }

      // no valid connection found.
      return false;
    }

    /// <summary>
    /// True = valid placement
    /// False = invalid (overlap found)
    /// </summary>
    /// <param name="room"></param>
    /// <param name="door"></param>
    /// <returns></returns>
    private static bool CheckRoomTilesAgainstGrid( Room room, FloorTile door )
    {
      FloorType gcType;
      FloorType rcType;

      foreach( FloorTile roomCell in room.cells )
      {
        // bounds checking - id it would fall off the scope of the base grid[,], then it's a no-go.
        if( roomCell.x < 1 || roomCell.x >= grid.GetLength(0)-1 || roomCell.y<1 || roomCell.y>=grid.GetLength(1)-1 )
          return false;

        gcType = grid[roomCell.x, roomCell.y].type;
        rcType = roomCell.type;

        if( (rcType != FloorType.Wall && gcType != FloorType.Wall)
            ||
            (rcType == FloorType.Wall && gcType != FloorType.Wall && !(roomCell.x==door.x && roomCell.y==door.y))
          )
        {
          return false;
        }
      }

      // checked everything and it was all OK.  Valid placement.
      return true;
    }

    /// <summary>
    /// Generates a rectabgular room (or hall)
    /// </summary>
    /// <param name="rooms"></param>
    /// <param name="roomType"></param>
    /// <returns></returns>
    private static Room GenerateGenericRoom( List<Room> rooms, CastleRoomType roomType )
    {
      return GenerateGenericRoom( rooms, roomType, null );
    }

    /// <summary>
    /// Generates a rectabgular room (or hall) and tries to attach it to the specified door (FloorTile)
    /// </summary>
    /// <param name="rooms"></param>
    /// <param name="roomType"></param>
    /// <param name="preferredDoor"></param>
    /// <returns></returns>
    private static Room GenerateGenericRoom( List<Room> rooms, CastleRoomType roomType, FloorTile targetDoor )
    {
      Room room = new Room(roomType);

      int tries = 0;
      int maxTries = 5;

      while( tries++ < maxTries )
      {
        int w = 0;
        int h = 0;
        int numDoors    = 0;
        int doorSpacing = 6;  // used for LongHall only
        int hallWidth   = 5;  // used for LongHall only.  Includes walls on either side : 3 should be the minimum, works best with odd numbers

        #region Specify sizes and number of doors, based on roomType
        switch( roomType )
        {
          case CastleRoomType.Bedroom:
            w = rand.Next(5, 9) + 2;
            h = rand.Next(5, 9) + 2;
            numDoors = 1;
            break;

          case CastleRoomType.BedroomMaster:
            w = rand.Next(6, 10) + 2;
            h = rand.Next(6, 10) + 2;
            numDoors = 2;
            break;

          case CastleRoomType.Closet:
            w = rand.Next(1, 3) + 2;
            h = rand.Next(1, 3) + 2;
            numDoors = 1;
            break;

          case CastleRoomType.GreatRoomRectangle:
            w = rand.Next(10, 25) + 2;
            h = rand.Next(15, 25) + 2;
            numDoors = 6;
            break;

          case CastleRoomType.GreatRoomSquare:
            w = rand.Next(10, 25) + 2;
            h = w;
            numDoors = 6;
            break;
            
          case CastleRoomType.GreatroomCircle:
            w = rand.Next(15,30) + 2;
            if( w%2==0 )
              w++;
            h = w;
            numDoors = 2*(rand.Next(6)+1);
            break;

          case CastleRoomType.Kitchen:
            w = rand.Next(3, 11) + 2;
            h = rand.Next(3, 11) + 2;
            numDoors = 3;
            break;

          case CastleRoomType.Storage:
            w = rand.Next(2, 5) + 2;
            h = rand.Next(2, 5) + 2;
            numDoors = 1;
            break;

          case CastleRoomType.LongHall:
            if( rand.Next(2) == 0 )
            {
              w = doorSpacing * (1+rand.Next(8));
              h = hallWidth;
              numDoors = w/doorSpacing;
            }
            else
            {
              w = hallWidth;
              h = doorSpacing * (1+rand.Next(8));
              numDoors = h/doorSpacing;
            }
            break;
        }
        #endregion

        room.cells = new FloorTile[w, h];
        room.doors.Clear();

        switch( roomType )
        {
          case CastleRoomType.GreatroomCircle:
            #region Circular Greatroom may have pillars (and generation is more complex than rectangular rooms)

            // fill room dimensions with wall
            for( int i = 0; i < w; i++ )
              for( int j = 0; j < h; j++ )
                room.cells[i, j] = new FloorTile(i, j, FloorType.Wall);

            // hollow out center, leaving a border of wall tiles
            for( int i = 1; i<w-1; i++ )
              for( int j = 1; j<h-1; j++ )
                if( (int)Math.Sqrt(Math.Pow(w/2 - i, 2.0) + Math.Pow(h/2 - j, 2.0)) < w/2.0-1 )
                  room.cells[i, j].type = FloorType.Floor;

            // Add pillars around the middle?
            if( rand.Next(2)==0 )
            {
              int pillars = (1+rand.Next(6))*2;
              double dist = rand.Next(3, w/2 - 2);
              for( int i=0; i<pillars; i++ )
              {
                room.cells[(int)Math.Round(w/2 - dist * Math.Cos(i*2.0*Math.PI/pillars), 0),
                            (int)Math.Round(w/2 - dist * Math.Sin(i*2.0*Math.PI/pillars), 0)
                          ].type = FloorType.Wall;
              }
            }

            #endregion
            break;

          case CastleRoomType.GreatRoomRectangle:
          case CastleRoomType.GreatRoomSquare:
            #region Greatrooms may have pillars

            // make solid wall outline, hollow center
            for( int i = 0; i < w; i++ )
              for( int j = 0; j < h; j++ )
              {
                room.cells[i, j] = new FloorTile(i, j);
                if( i == 0 || i == w - 1 || j == 0 || j == h - 1 )
                  room.cells[i, j].type = FloorType.Wall;
                else
                  room.cells[i, j].type = FloorType.Floor;
              }

            bool NSpillars = false;
            bool EWpillars = false;

            // Add pillars around the middle?
            switch( rand.Next(4) )
            {
              case 1:
                // all the way around the room
                NSpillars = EWpillars = true;
                break;

              case 2:
                // N/S walls only
                NSpillars = true;
                break;

              case 3:
                // E/W walls only
                EWpillars = true;
                break;

              default:
                // no pillars.
                break;
            }

            // spacing for pillars should be consistent within a room
            int offset  = rand.Next(2,6);
            int spacing = rand.Next(2,8);

            if( NSpillars )
              for( int i=offset; i<=w/2; i+=spacing )
              {
                room.cells[i, offset].type = FloorType.Wall;
                room.cells[i, h-1-offset].type = FloorType.Wall;
                room.cells[w-1-i, offset].type = FloorType.Wall;
                room.cells[w-1-i, h-1-offset].type = FloorType.Wall;
              }

            if( EWpillars )
              for( int i=offset; i<=h/2; i+=spacing )
              {
                room.cells[offset, i].type = FloorType.Wall;
                room.cells[w-1-offset, i].type = FloorType.Wall;
                room.cells[offset, h-1-i].type = FloorType.Wall;
                room.cells[w-1-offset, h-1-i].type = FloorType.Wall;
              }

            #endregion
            break;

          default:
            #region All other rectangualr rooms, including LongHall

            // make solid wall outline, hollow center
            for( int i = 0; i < w; i++ )
              for( int j = 0; j < h; j++ )
              {
                room.cells[i, j] = new FloorTile(i, j);
                if( i == 0 || i == w - 1 || j == 0 || j == h - 1 )
                  room.cells[i, j].type = FloorType.Wall;
                else
                  room.cells[i, j].type = FloorType.Floor;
              }

            #endregion
            break;
        }

        // So far, only applicable to LongHall and GreatRooms
        // If we haven't connected to a GreatRoom after half the max tried, stop limiting the attemps.
        // This setting has no effect on other room connections (so far).
        bool allowConnectionBias = tries < maxTries/2;

        if( !PlaceRoom( rooms, room, roomType, allowConnectionBias, targetDoor ) )
          continue;

        // placed a room via connection - remove that door from the number we're going to generate.
        // (closets, for example, should have that connection count as their only door)
        if( rooms.Count > 0 )
          numDoors--;

        // set doors
        if( roomType == CastleRoomType.GreatroomCircle )
        {
          #region Circular rooms will only place doors along the "flat" sections near N, E, S, W sides.

          int count = 0;
          int maxDoorTries = 30;
          int doorTries = 0;
          while( count < numDoors && doorTries < maxDoorTries )
          {
            double angle = rand.Next(360) * Math.PI / 180.0;

            int tX = (int)Math.Round((w/2) + (w-1)/2 * Math.Cos(angle), 0);
            int tY = (int)Math.Round((w/2) + (w-1)/2 * Math.Sin(angle), 0);

            bool EW =    tX>0 
                      && tX<w-1
                      && (room.cells[tX-1, tY].type == FloorType.Wall)  // || room.cells[tX-1, tY].type == FloorType.DoorClosed) 
                      && (room.cells[tX+1, tY].type == FloorType.Wall); // || room.cells[tX+1, tY].type == FloorType.DoorClosed);

            bool NS =    tY>0
                      && tY<h-1
                      && (room.cells[tX, tY-1].type == FloorType.Wall)  // || room.cells[tX, tY-1].type == FloorType.DoorClosed) 
                      && (room.cells[tX, tY+1].type == FloorType.Wall); // || room.cells[tX, tY+1].type == FloorType.DoorClosed);

            if( EW ^ NS ) // exclusive OR : must be one or the other, but not neither, and not both.
            {
              room.doors.Add(room.cells[tX, tY]);
              room.cells[tX, tY].type = FloorType.DoorClosed;
              count++;
            }
            else
            {
              // failed to place - up the count
              doorTries++;
            }
          }

          #endregion
        }
        else if( roomType == CastleRoomType.LongHall )
        {
          #region Special case : add doors one each end, and evenly spaced down both sides
          
          if( w > h )
          {
            // E/W hall
            room.doors.Add(room.cells[0, h/2]);
            room.doors.Add(room.cells[w-1, h/2]);
            for( int i = 0; i < w/doorSpacing; i++ )
            {
              room.doors.Add(room.cells[doorSpacing/2 + i*doorSpacing, 0+0]);
              room.doors.Add(room.cells[doorSpacing/2 + i*doorSpacing, h-1]);
            }
          }
          else
          {
            // N/S hall
            room.doors.Add(room.cells[w/2, 0]);
            room.doors.Add(room.cells[w/2, h-1]);
            for( int i = 0; i < h/doorSpacing; i++ )
            {
              room.doors.Add(room.cells[0+0, doorSpacing/2 + i*doorSpacing]);
              room.doors.Add(room.cells[w-1, doorSpacing/2 + i*doorSpacing]);
            }
          }

          foreach( FloorTile door in room.doors )
            door.type = FloorType.DoorClosed;

          #endregion
        }
        else
        {
          #region All other square rooms

          for( int i = 0; i < numDoors; i++ )
          {
            if( rand.Next(2) == 0 )
              // top/bottom... x= whatever, and y=1 (top edge) or y=h-2 (bottom edge)
              room.doors.Add(room.cells[rand.Next(1, w - 2), rand.Next(2) * (h - 1)]);
            else
              // left/right... x=1 (left side) or x=w-2 (right side) and y = whatever
              room.doors.Add(room.cells[rand.Next(2) * (w - 1), rand.Next(1, h - 2)]);

            room.doors[room.doors.Count-1].type = FloorType.DoorClosed;
          }

          #endregion
        }

        return room;
      }

      // could not find a valid connection for this room... gave up
      return null;
    }

    #endregion

    #region Subdivision - complete

    /// <summary>
    /// Divides a rectangular grid into quads, then knocks down a wallSection from 3 of the 4 walls.
    /// Calls SubDivide on each of those new sub-quads
    /// Recursion stops when the region is 1x1 unit.
    /// 
    /// percentFull is the chance that a subGrid will be divided (only applicable after the 1st division
    /// </summary>
    /// <param name="width"></param>
    /// <param name="height"></param>
    /// <param name="pathWidth"></param>
    /// <param name="percentFull"></param>
    /// <returns></returns>
    private static FloorTile[,] GenerateStyle_Subdivision( int width, int height, int pathWidth, float percentFull )
    {
      // define the image to draw the maze to
      Bitmap pic = new Bitmap(width*pathWidth+1, height*pathWidth+1);

      Graphics pG = Graphics.FromImage(pic);
      pG.Clear(TileColor[(int)FloorType.Floor]);
      pG.Dispose();

      // Subdivide the whole image
      Subdivide(pic, pathWidth, 0, 0, width-1, height-1, (int)(percentFull*100), true);

      // render maze from image blueprint
      return TransposePicToGrid(pic);
    }

    /// <summary>
    /// Subdivision recursive call
    /// </summary>
    /// <param name="pic"></param>
    /// <param name="pathWidth"></param>
    /// <param name="x1"></param>
    /// <param name="y1"></param>
    /// <param name="x2"></param>
    /// <param name="y2"></param>
    /// <param name="percentFull"></param>
    /// <param name="firstSplit"></param>
    private static void Subdivide( Bitmap pic, int pathWidth, int x1, int y1, int x2, int y2, int percentFull, bool firstSplit )
    {
      // If either width or height of the subsection is down to 0 (left/right are same X, or top/bottom same Y), 
      //  then no more divisions will make any difference, even if the other dimension is greater than 1.  Return.
      if( x2-x1<=0 || y2-y1<=0 )
        return;

      // Sub quad elligible to divide... but does the percentFull chance allow it?
      if( !firstSplit && percentFull < rand.Next(100) )
        return; // nope - didn't roll high enough

      // set sub division boundaries
      int xm = rand.Next(x1, x2);
      int ym = rand.Next(y1, y2);

      // put dividing walls up - horizontal
      for( int x=x1*pathWidth; x<(x2+1)*pathWidth; pic.SetPixel(x++, (ym+1)*pathWidth, TileColor[(int)FloorType.Wall]) )
        ;

      // put dividing walls up - vertical
      for( int y=y1*pathWidth; y<(y2+1)*pathWidth; pic.SetPixel((xm+1)*pathWidth, y++, TileColor[(int)FloorType.Wall]) )
        ;

      // pick one of the 4 new walls to _not_ punch a hole in
      int saveWall = rand.Next(4);
      int wallSection = 0;

      //          wallSection assigned at same time as loop init       safe wall condition + loop condition           loop indexer increment in pixel set call
      for( int x=(wallSection=rand.Next(x1+0, xm+0))*pathWidth+1; saveWall!=0 && x<(wallSection+1)*pathWidth; pic.SetPixel(x++, (ym+1)*pathWidth, TileColor[(int)FloorType.Floor]) )
        ;
      for( int x=(wallSection=rand.Next(xm+1, x2+1))*pathWidth+1; saveWall!=1 && x<(wallSection+1)*pathWidth; pic.SetPixel(x++, (ym+1)*pathWidth, TileColor[(int)FloorType.Floor]) )
        ;
      for( int y=(wallSection=rand.Next(y1+0, ym+0))*pathWidth+1; saveWall!=2 && y<(wallSection+1)*pathWidth; pic.SetPixel((xm+1)*pathWidth, y++, TileColor[(int)FloorType.Floor]) )
        ;
      for( int y=(wallSection=rand.Next(ym+1, y2+1))*pathWidth+1; saveWall!=3 && y<(wallSection+1)*pathWidth; pic.SetPixel((xm+1)*pathWidth, y++, TileColor[(int)FloorType.Floor]) )
        ;

      // with those walls as bounsaries, divide each new subdivision.
      Subdivide(pic, pathWidth, x1+0, y1+0, xm, ym, percentFull, false);
      Subdivide(pic, pathWidth, xm+1, y1+0, x2, ym, percentFull, false);
      Subdivide(pic, pathWidth, x1+0, ym+1, xm, y2, percentFull, false);
      Subdivide(pic, pathWidth, xm+1, ym+1, x2, y2, percentFull, false);
    }

    #endregion

    #region WallAdder - complete

    /// <summary>
    /// Generates a maze using a Wall Adding algorithm on a square grid
    /// 
    /// The corners of each cell are added to a list, and no walls are up to start
    /// 
    /// While there are still corners in the list to choose from :
    ///    Pick one at random
    ///    if there are no neighboring corners un-connected, then
    ///      remove this corner from the list.
    ///    Otherwise,
    ///      connect this corner to a random un-connected neighbor corner
    ///      add the connected corner to the list
    ///
    /// This is modified slightly - I assume the entire outside borader of the maze is connected to start
    /// </summary>
    /// <param name="width"></param>
    /// <param name="height"></param>
    /// <param name="pathWidth"></param>
    /// <returns></returns>
    private static FloorTile[,] GenerateStyle_WallAdder( int width, int height, int pathWidth )
    {
      // determine required withs of maze image
      Bitmap pic = new Bitmap(width*pathWidth+1, height*pathWidth+1);

      Graphics pG = Graphics.FromImage(pic);
      pG.Clear(TileColor[(int)FloorType.Floor]);
      pG.Dispose();

      ArrayList list = new ArrayList();

      bool[] m = new bool[(width+1)*(height+1)];

      int x=0, y=0;

      // Wall add algorithm : set all corners of 'cells' to walls
      //   Generation algorithm below connects the corners and puts walls up
      for( int i=0; i<pic.Width; i++ )
        for( int j=0; j<pic.Height; j++ )
        {
          if( i==0 
            || i==pic.Width-1
            || j==0 
            || j==pic.Height-1
            || (i%pathWidth==0 && j%pathWidth==0)
            )
          {
            // edge or corner post = wall up
            pic.SetPixel(i, j, TileColor[(int)FloorType.Wall]);

            // Corner post?  Add to list for generation
            if( i%pathWidth==0 && j%pathWidth==0  && (i==0 || i==width*pathWidth | j==0 || j==height*pathWidth) )
              m[(int)list[list.Add((j/pathWidth)*(width+1) + (i/pathWidth))]] = true;
          }
          else
            pic.SetPixel(i, j, TileColor[(int)FloorType.Floor]);
        }

      // Wall adding algorithm - connect corners (think of them as support poles) with walls until all corner poles are connected to another.
      while( list.Count > 0 )
      {
        int c = rand.Next(100)<50 ? (int)list[rand.Next(list.Count)] : (int)list[list.Count-1];
        x = c%(width+1);
        y = c/(width+1);
        int i=0;

        bool[] d = new bool[] { y>0         && !m[(y-1)*(width+1) + (x  )],
                                x>0         && !m[(y  )*(width+1) + (x-1)],
                                y<height-1  && !m[(y+1)*(width+1) + (x  )],
                                x<width-1   && !m[(y  )*(width+1) + (x+1)] };

        if( !(d[0] || d[1] || d[2] || d[3]) )
          list.Remove(c);
        else
        {
          while( !d[i=rand.Next(d.Length)] )
            /* null body */
            ;

          switch( i )
          {
            case 0:
              m[(int)list[list.Add((y-1)*(width+1) + (x))]] = true;
              for( int v=(y-1)*pathWidth; v<y*pathWidth; v++ )
                pic.SetPixel(x*pathWidth, v, TileColor[(int)FloorType.Wall]);
              break;

            case 1:
              m[(int)list[list.Add((y)*(width+1) + (x-1))]] = true;
              for( int v=(x-1)*pathWidth; v<x*pathWidth; v++ )
                pic.SetPixel(v, y*pathWidth, TileColor[(int)FloorType.Wall]);
              break;

            case 2:
              m[(int)list[list.Add((y+1)*(width+1) + (x))]] = true;
              for( int v=y*pathWidth; v<(y+1)*pathWidth; v++ )
                pic.SetPixel(x*pathWidth, v, TileColor[(int)FloorType.Wall]);
              break;

            case 3:
              m[(int)list[list.Add((y)*(width+1) + (x+1))]] = true;
              for( int v=x*pathWidth; v<(x+1)*pathWidth; v++ )
                pic.SetPixel(v, y*pathWidth, TileColor[(int)FloorType.Wall]);
              break;
          }

        }
      }

      //int doorCount = 0;
      //while(doorCount < 30)
      //{
      //  x = rand.Next(width);
      //  y = rand.Next(height);

      //  if(rand.Next(2)==0)
      //    x+= rand.Next(pathWidth-1) +1;
      //  else
      //    y+= rand.Next(pathWidth-1) +1;
      //}

      return TransposePicToGrid(pic);
    }

    #endregion

    #region HexTunneler - complete

    /// <summary>
    /// Generates a maze using a tunneling algorithm on a hex grid
    /// This method will generate the full number of cells (width*height)
    /// </summary>
    /// <param name="width"></param>
    /// <param name="height"></param>
    /// <param name="pathWidth"></param>
    /// <returns></returns>
    private static FloorTile[,] GenerateStyle_HexTunneler( int width, int height, int pathWidth )
    {
      return GenerateStyle_HexTunneler(width, height, pathWidth, 1.0f);
    }

    /// <summary>
    /// Generates a maze using a tunneling algorithm on a hex grid
    /// 
    ///  0,0   0,1   0,2   0,3   0,4
    ///     1,0   1,1   1,2   1,3
    ///  2,0   2,1   2,2   2,3   2,4
    ///     3,0   3,1   3,2   3,3
    ///     
    ///                              direction      NW-0       NE-1         E-2       SE-3       SW-4        W-5
    /// [r,c] even row (y=0,2,4...) : connects to (r-1,c-1), (r-1, c  ), (r  ,c+1), (r+1,c  ), (r+1,c-1), (r  ,c-1)
    /// [r,c] odd  row (y=1,3,5...) : connects to (r-1,c  ), (r-1, c+1), (r  ,c+1), (r+1,c+1), (r+1,c  ), (r  ,c-1)
    /// 
    /// This method will generate a specified percentage of the total numebr of cells, stopping when the count/total raio is met.
    /// </summary>
    /// <param name="width"></param>
    /// <param name="height"></param>
    /// <param name="pathWidth"></param>
    /// <param name="percentFull"></param>
    /// <returns></returns>
    private static FloorTile[,] GenerateStyle_HexTunneler( int width, int height, int pathWidth, float percentFull )
    {
      // Hex grids are a little pickier about the wall widths, since the diagonal walls are drawn on a square grid.
      // Therfore, I shall arbitrarily set some minimums for the pathwidth, to ensure the rooms (whose sizes are 
      //  determined by pathWidth)are big enough to connect to.
      pathWidth = Math.Max(4, pathWidth);

      // Hex mazes are also finicky about room connections (due to my poor algorith design), and very few rooms leads to odd attepmts at connections.
      //  Therefore, I shall compensate for my lack of planning with arbitrary minimum width/height values for the smallest number of rooms.
      width = Math.Max(3, (int)(width/Math.Sqrt(3)));
      height = Math.Max(3, (int)(height/1.5));

      Bitmap pic = new Bitmap((int)((width+0.0)*pathWidth*Math.Sqrt(3))+4, (int)((height+0.5)*pathWidth*1.5));

      Graphics g = Graphics.FromImage(pic);
      g.Clear(TileColor[(int)FloorType.Wall]);

      Pen wallsUp   = new Pen(TileColor[(int)FloorType.Wall], 2);
      Pen wallsDown = new Pen(TileColor[(int)FloorType.Floor], Math.Max(2, pathWidth/2));

      ArrayList rooms = new ArrayList();

      // Determine room corner coordinates based on the room's array index (center of the room)
      for( int j=0; j<height; j++ )
        for( int i=0; i<width-j%2; i++ )
        {
          // for each room, define the coordinates of the 6 corners of the room.
          // They'll be drawn after all room's corners have been calculated.
          PointF[] pts = new PointF[6];

          for( int s=0; s<6; s++ )
            pts[s] = new PointF((float)(pathWidth + Math.Sqrt(3)*pathWidth*i + (j%2)*Math.Sqrt(3)/2.0 * pathWidth + pathWidth*Math.Cos((s)*Math.PI/3.0 + Math.PI/6.0)),
                                 (float)(pathWidth +          1.5*pathWidth*j                                      + pathWidth*Math.Sin((s)*Math.PI/3.0 + Math.PI/6.0))
                               );
          // all 6 corners determined - add that room's corners to the array of rooms
          rooms.Add(pts);
        }

      // hollow out all rooms
      foreach( PointF[] roomPts in rooms )
        g.FillPolygon(wallsDown.Brush, roomPts);

      // draw all room walls back up.  Redundant, yes, but easier
      foreach( PointF[] roomPts in rooms )
        g.DrawPolygon(wallsUp, roomPts);


      // start generating maze
      bool[] m = new bool[width*height];  // flags indicating whether we've connected a cell or not
      ArrayList cells = new ArrayList();  // list of cells we've connected to that might still have more connections available
      int idx = 0;

      // initial cell to start at
      int x = width/2;            // start in middle - arbitrary.  Could be random.
      int y = height/2;

      m[y*width+x] = true;        // start cell flag - visited
      cells.Add(y*width+x);     // add reference to start cell to list of valid choices


      int c = 0;                  // will represent the current cell we're trying to connect from in the loop below
      int connected = 1;          // 1 cell in the total maze so far (the one we added to 'cells' a few lines back)

      // fancy-schmancy way of affecting the asthetic of the maze.
      // By chooing new cells to continue at from the end or the beginning of the list, you get wildly different paths in the maze.
      // This randomizer thing just kind of mixes it up a bit.
      int cellChoice = rand.Next(1, 3);

      while( cells.Count>0  && (float)connected/(float)(width*height) <= percentFull )
      {
        switch( cellChoice )
        {
          case 0:  // always pick the 1st cell in the list - tends to look like all paths converge at the start cell
            c = (int)cells[0];
            break;
          case 1:  // always pick the last cell in the list - leads to long, winding paths before you find a dead end
            c = (int)cells[cells.Count-1];
            break;
          default:  // pick a cell from anywhere in the list
            c = (int)cells[rand.Next(cells.Count)];
            break;
        }


        x = c%width;                  // get the x,y of that cell
        y = c/width;

        bool[] d; // array of valid directions from this cell

        if( y%2 == 0 )  // even-row cell connections
        {
          d = new bool[] { x>0        && y>0        && !m[(y-1)*width + (x-1)],   // valid connection to NW and NW cell not visited?
                           x<width-1  && y>0        && !m[(y-1)*width + (x  )],   // valid connection to NE and NE cell not visited?
                           x<width-1                && !m[(y  )*width + (x+1)],   // valid connection to  E and  E cell not visited?
                           x<width-1  && y<height-1 && !m[(y+1)*width + (x  )],   // valid connection to SE and SE cell not visited?
                           x>0        && y<height-1 && !m[(y+1)*width + (x-1)],   // valid connection to SW and SW cell not visited?
                           x>0                      && !m[(y  )*width + (x-1)]    // valid connection to  W and  W cell not visited?
                          };
        }
        else            // odd-row cell connections
        {
          d = new bool[] {               y>0        && !m[(y-1)*width + (x  )],   // valid connection to NW and NW cell not visited?
                           x<width-2  && y>0        && !m[(y-1)*width + (x+1)],   // valid connection to NE and NE cell not visited?
                           x<width-2                && !m[(y  )*width + (x+1)],   // valid connection to  E and  E cell not visited?
                           x<width-2  && y<height-1 && !m[(y+1)*width + (x+1)],   // valid connection to SE and SE cell not visited?
                           x>0        && y<height-1 && !m[(y+1)*width + (x  )],   // valid connection to SW and SW cell not visited?
                           x>0                      && !m[(y  )*width + (x-1)]    // valid connection to  W and  W cell not visited?
                          };
        }

        if( !(d[0] || d[1] || d[2] || d[3] || d[4] || d[5]) )
        {
          // no valid directions to choose from - this cell is a dead end.  Remove from list.
          cells.Remove(c);
        }
        else
        {
          // pick a random direction until e get a valid one
          while( !(d[idx=rand.Next(d.Length)]) )
            /* null body*/
            ;

          // direction chosen - use the idx of that direction to determine which wall to un-draw
          // Done by drawing a 'wallsDown' line from the current cell to the connected cell
          g.DrawLine(wallsDown, new PointF((float)(pathWidth + Math.Sqrt(3)*pathWidth*x + (y%2)*Math.Sqrt(3)/2.0*pathWidth /* + pathWidth*Math.Cos( 2*Math.PI/3.0 - (idx  )*Math.PI/3.0 + Math.PI/6.0 )*/ ),
                                             (float)(pathWidth +          1.5  *pathWidth*y                                      /* - pathWidth*Math.Sin( 2*Math.PI/3.0 - (idx  )*Math.PI/3.0 + Math.PI/6.0 )*/ )
                                           ),
                                 new PointF((float)(pathWidth + Math.Sqrt(3)*pathWidth*x + (y%2)*Math.Sqrt(3)/2.0*pathWidth + 2*pathWidth*Math.Cos(2*Math.PI/3.0 - (idx)*Math.PI/3.0 /*+ Math.PI/6.0 */)),
                                             (float)(pathWidth +          1.5  *pathWidth*y                                      - 2*pathWidth*Math.Sin(2*Math.PI/3.0 - (idx)*Math.PI/3.0 /*+ Math.PI/6.0 */))
                                           )
                   );

          // add the newly connected cell to the cells list
          if( y%2==0 )  // even row
          {
            if( idx==0 )
              m[(int)cells[cells.Add((y-1)*width + (x-1))]] = true;
            if( idx==1 )
              m[(int)cells[cells.Add((y-1)*width + (x))]] = true;
            if( idx==2 )
              m[(int)cells[cells.Add((y)*width + (x+1))]] = true;
            if( idx==3 )
              m[(int)cells[cells.Add((y+1)*width + (x))]] = true;
            if( idx==4 )
              m[(int)cells[cells.Add((y+1)*width + (x-1))]] = true;
            if( idx==5 )
              m[(int)cells[cells.Add((y)*width + (x-1))]] = true;
          }
          else          // odd row
          {
            if( idx==0 )
              m[(int)cells[cells.Add((y-1)*width + (x))]] = true;
            if( idx==1 )
              m[(int)cells[cells.Add((y-1)*width + (x+1))]] = true;
            if( idx==2 )
              m[(int)cells[cells.Add((y)*width + (x+1))]] = true;
            if( idx==3 )
              m[(int)cells[cells.Add((y+1)*width + (x+1))]] = true;
            if( idx==4 )
              m[(int)cells[cells.Add((y+1)*width + (x))]] = true;
            if( idx==5 )
              m[(int)cells[cells.Add((y)*width + (x-1))]] = true;
          }

          connected++;
        }
      }

      // super wasteful re-filling of non-connected rooms.
      //  If I had thunk ahead more, I'd have saved these values somplace useful
      // Determine room corner coordinates based on the room's array index (center of the room)
      rooms.Clear();

      for( int j=0; j<height; j++ )
        for( int i=0; i<width-j%2; i++ )
          if( !m[j*width + i] ) // cell not visited/connected?  Needs to be filled back in.
          {
            // for each room, define the coordinates of the 6 corners of the room.
            // They'll be drawn after all room's corners have been calculated.
            PointF[] pts = new PointF[6];

            for( int s=0; s<6; s++ )
              pts[s] = new PointF((float)(pathWidth + Math.Sqrt(3)*pathWidth*i + (j%2)*Math.Sqrt(3)/2.0 * pathWidth + pathWidth*Math.Cos((s)*Math.PI/3.0 + Math.PI/6.0)),
                                  (float)(pathWidth +          1.5*pathWidth*j                                      + pathWidth*Math.Sin((s)*Math.PI/3.0 + Math.PI/6.0))
                                 );
            // all 6 corners determined - add that room's corners to the array of rooms
            rooms.Add(pts);
          }

      // fill int rooms we decided weren't reachable
      foreach( PointF[] roomPts in rooms )
        g.FillPolygon(wallsUp.Brush, roomPts);

      rooms.Clear();

      return TransposePicToGrid(pic);
    }

    #endregion

    #region Tunneler - Complete

    /// <summary>
    /// Generates a maze using a tunneling algorithm on a square grid
    /// This method will generate the full number of cells (width*height)
    /// </summary>
    /// <param name="width"></param>
    /// <param name="height"></param>
    /// <param name="pathWidth"></param>
    /// <returns></returns>
    private static FloorTile[,] GenerateStyle_Tunneler( int width, int height, int pathWidth )
    {
      return GenerateStyle_Tunneler(width, height, pathWidth, 1.0f);
    }

    /// <summary>
    /// Generates a maze using a tunneling algorithm on a square grid
    /// </summary>
    /// <param name="width"></param>
    /// <param name="height"></param>
    /// <param name="pathWidth"></param>
    /// <param name="percentFull"></param>
    /// <returns></returns>
    private static FloorTile[,] GenerateStyle_Tunneler( int width, int height, int pathWidth, float percentFull )
    {
      Bitmap pic = new Bitmap(width*pathWidth+1, height*pathWidth+1);

      Graphics g = Graphics.FromImage(pic);
      g.Clear(TileColor[(int)FloorType.Floor]);

      Pen wallsUp   = new Pen(TileColor[(int)FloorType.Wall], 1);
      Pen wallsDown = new Pen(TileColor[(int)FloorType.Floor], 1);

      g.Clear(TileColor[(int)FloorType.Floor]);

      // Start with all walls up - draw grid
      for( int i=0; i<=width; i++ )
        g.DrawLine(wallsUp, i*pathWidth, 0*pathWidth, i*pathWidth, height*pathWidth);

      for( int j=0; j<=height; j++ )
        g.DrawLine(wallsUp, 0*pathWidth, j*pathWidth, width*pathWidth, j*pathWidth);

      // start generating maze
      bool[] m = new bool[width*height];          // flags indicating whether we've connected a cell or not
      ArrayList cells = new ArrayList();          // list of cells we've connected to that might still have more connections available
      int idx = 0;

      // initial cell to start at
      int x = width/2;            // start in middle - arbitrary.  Could be random.
      int y = height/2;

      m[y*width+x] = true;        // start cell flag - visited
      cells.Add(y*width+x);     // add reference to start cell to list of valid choices

      int c = 0;                  // will represent the current cell we're trying to connect from in the loop below
      int connected = 1;          // 1 cell in the total maze so far (the one we added to 'cells' a few lines back)

      // fancy-schmancy way of affecting the asthetic of the maze.
      // By chooing new cells to continue at from the end or the beginning of the list, you get wildly different paths in the maze.
      // This randomizer thing just kind of mixes it up a bit.
      int cellChoice = rand.Next(1, 3);

      while( cells.Count>0  && (float)connected/(float)(width*height) <= percentFull )
      {
        switch( cellChoice )
        {
          case 0:  // always pick the 1st cell in the list - tends to look like all paths converge at the start cell
            c = (int)cells[0];
            break;

          case 1:  // always pick the last cell in the list - leads to long, winding paths before you find a dead end
            c = (int)cells[cells.Count-1];
            break;

          default:  // pick a cell from anywhere in the list - leads to a fair mix of longish paths, but several path splits
            c = (int)cells[rand.Next(cells.Count)];
            break;
        }

        x = c%width;                  // get the x,y of that cell
        y = c/width;

        bool[] d; // array of valid directions from this cell

        d = new bool[] { y>0         && !m[(y-1)*width + (x  )],   // valid connection to N and N cell not visited?
                         x<width-1   && !m[(y  )*width + (x+1)],   // valid connection to E and E cell not visited?
                         y<height-1  && !m[(y+1)*width + (x  )],   // valid connection to S and S cell not visited?
                         x>0         && !m[(y  )*width + (x-1)]    // valid connection to W and W cell not visited?
                       };

        if( !(d[0] || d[1] || d[2] || d[3]) )
        {
          // no valid directions to choose from - this cell is a dead end.  Remove from list.
          cells.Remove(c);
        }
        else
        {
          // pick a random direction until e get a valid one
          while( !(d[idx=rand.Next(d.Length)]) )
            /* null body*/
            ;

          // direction chosen - use the idx of that direction to determine which wall to un-draw
          // Done by drawing a 'wallsDown' line from the current cell to the connected cell
          switch( idx )
          {
            case 0: // knock down North wall (x, y-1)
              g.DrawLine(wallsDown, pathWidth*(x)+1, pathWidth*(y), pathWidth*(x+1)-1, pathWidth*(y));
              m[(int)cells[cells.Add((y-1)*width + (x))]] = true;
              break;

            case 1: // knock down East wall (x+1, y)
              g.DrawLine(wallsDown, pathWidth*(x+1), pathWidth*(y)+1, pathWidth*(x+1), pathWidth*(y+1)-1);
              m[(int)cells[cells.Add((y)*width + (x+1))]] = true;
              break;

            case 2: // knock down South wall (x, y+1)
              g.DrawLine(wallsDown, pathWidth*(x)+1, pathWidth*(y+1), pathWidth*(x+1)-1, pathWidth*(y+1));
              m[(int)cells[cells.Add((y+1)*width + (x))]] = true;
              break;

            case 3: // knock down West wall (x-1, y)
              g.DrawLine(wallsDown, pathWidth*(x), pathWidth*(y)+1, pathWidth*(x), pathWidth*(y+1)-1);
              m[(int)cells[cells.Add((y)*width + (x-1))]] = true;
              break;
          }

          connected++;
        }
      }

      for( int i=0; i<width; i++ )
        for( int j=0; j<height; j++ )
          if( !m[j*width + i] ) // cell not visited/connected?  Needs to be filled back in.
            g.FillRectangle(wallsUp.Brush, i*pathWidth, j*pathWidth, pathWidth, pathWidth);

      return TransposePicToGrid(pic);
    }

    #endregion

    #region Curcular Tunneler - complete

    /// <summary>
    /// Circular Maze Tunneler
    /// </summary>
    /// <param name="width"></param>
    /// <param name="height"></param>
    /// <param name="pathWidth"></param>
    /// <param name="percentFull"></param>
    /// <returns></returns>
    private static FloorTile[,] GenerateStyle_CircularTunneler( int width, int height, int pathWidth, float percentFull )
    {
      // cell size limits
      int cs = Math.Max(6, pathWidth);
      int cw = Math.Max(6, pathWidth);

      // Path colors
      Color path = TileColor[(int)FloorType.Floor];
      Color wall = TileColor[(int)FloorType.Wall];

      // image to draw maze to
      Bitmap pic = new Bitmap(width*pathWidth, height*pathWidth);
      Graphics gp = Graphics.FromImage(pic);

      // maze area center
      int x = pic.Width/2;
      int y = pic.Height/2;

      // maze area large enough for at least 2 rings?  If not, return a default tunneler maze.
      int numRings = (int)(Math.Min(x, y)/cs);
      if( numRings<2 )
        return GenerateStyle_Tunneler(width, height, pathWidth, percentFull);

      // Maze size OK - declare all the rest of the variables required to continue.
      RingCell tmp;

      float   penWidth      = Math.Max(2.0f, cs/10.0f);
      Pen     p             = new Pen(path, penWidth);
      int     divFactor     = rand.Next(2, 5);
      int     split         = divFactor;        // initial cells in 1st ring
      bool    orderlySplits = rand.Next(2)==0;  // 1=yes, 0=no (will put as many cells per ring as the diameter allows)
      int     bias          = rand.Next(3);     // 0=none, 1=rings, 2=spokes

      int     cellsPerRing  = split;
      bool    splitHappened = false;

      int     startCell = 0;
      int     cellsLastRing = cellsPerRing;

      double  tiltOffset, wallAngle, wallWidth, penWidthDegrees, arcAngle, arcWidth;

      // Walls are RGB Red value=255 (using white in this method).  So, start by clearing to white (all wall), 
      //  then hollow out the target maze area in path (anything without a Red value of 255) from that.
      gp.Clear(wall);
      gp.FillEllipse(p.Brush, x-cs*numRings, y-cs*numRings, 2*cs*numRings, 2*cs*numRings);



      ArrayList cells = new ArrayList();
      cells.Add(new RingCell());

      if( orderlySplits )
      {
        for( int i=0; i<numRings; i++ )
          if( (int)(2*Math.PI*i*cs/cw) >= divFactor*cellsPerRing )
            cellsPerRing *= divFactor;
      }
      else
        cellsPerRing = (int)((2*Math.PI*(numRings-1)*cs)/cw);

      tiltOffset = Math.PI/2 + Math.PI/cellsPerRing;
      cellsPerRing = split;


      // --------------------------------------------------------------------------------------
      // Generate initial grid, multiplying number of cells/adding cells when radius allows.
      // --------------------------------------------------------------------------------------
      for( int i=1; i<=numRings; i++ )
      {
        startCell = cells.Count;
        cellsLastRing = cellsPerRing;

        if( orderlySplits )
        {
          if( splitHappened = ((int)(2*Math.PI*i*cs/cw) >= divFactor*cellsPerRing) )
            cellsPerRing *= divFactor;
        }
        else
        {
          cellsPerRing = (int)((2*Math.PI*i*cs)/cw);
          splitHappened = (cellsPerRing == cellsLastRing);
        }

        p.Color = wall;
        p.Width = penWidth;

        // Draw walls from inner ring to outer ring (mini spokes)
        for( int j=0; j<cellsPerRing; j++ )
        {
          wallAngle = (j*2*Math.PI/cellsPerRing) - tiltOffset;
          arcAngle  = wallAngle*180/Math.PI;
          arcWidth  = 360.0d/cellsPerRing;

          if( i != numRings )
          {
            tmp = new RingCell();
            tmp.Ring = i;
            tmp.Index = cells.Count;
            tmp.WallAngle = (wallAngle+2*Math.PI)%(2*Math.PI);
            tmp.ArcAngle = (arcAngle+360)%360;
            tmp.ArcWidth = (arcWidth+360)%360;
            cells.Add(tmp);

            gp.DrawLine(p,
                         (float)(x + cs*(i+0) * Math.Cos(tmp.WallAngle)),
                         (float)(y + cs*(i+0) * Math.Sin(tmp.WallAngle)),
                         (float)(x + cs*(i+1) * Math.Cos(tmp.WallAngle)),
                         (float)(y + cs*(i+1) * Math.Sin(tmp.WallAngle)));
          }
        }

        // draw outer ring wall - full circle
        gp.DrawArc(p, x - cs*i, y - cs*i, 2 * cs*i, 2 * cs*i, (float)(0), (float)(360));

        // Not at final ring?  Set number of cells for next ring, set connections from current ring to inner ring
        if( i!= numRings )
        {
          for( int j=startCell; j<startCell+cellsPerRing; j++ )
          {
            tmp = cells[j] as RingCell;

            if( startCell == 1 )
            {
              tmp.neighbors.Add(cells[0]);
              ((RingCell)cells[0]).neighbors.Add(tmp);
            }
            else //if( splitHappened )
            {
              for( int innerRing=startCell-cellsLastRing; innerRing<startCell; innerRing++ )
              {
                RingCell innerCell = cells[innerRing] as RingCell;
                double oca = tmp.ArcAngle+360;
                double ocw = tmp.ArcWidth;
                double ica = innerCell.ArcAngle+360;
                double icw = innerCell.ArcWidth;

                // cells must overlap by 50% of thier width to connect to each other when not on the same ring level.
                double overlap = 0.5;
                if( ica+icw>=oca+icw*overlap && ica<=oca+ocw-icw*overlap )
                {
                  tmp.neighbors.Add(cells[innerRing]);
                  ((RingCell)cells[innerRing]).neighbors.Add(tmp);
                }
              }
            }

            // add neighbor cells on same ring (right/left)
            tmp.neighbors.Add(cells[startCell + (j-startCell+cellsPerRing+1)%cellsPerRing]);
            tmp.neighbors.Add(cells[startCell + (j-startCell+cellsPerRing-1)%cellsPerRing]);
          }
        }
      }

      // --------------------------------------------------------------------------------------
      // Grid generated, all connections in place. Start generating maze by knocking walls down
      // --------------------------------------------------------------------------------------
      ArrayList travel = new ArrayList();
      travel.Add(cells[rand.Next(cells.Count)]);

      RingCell cell = travel[0] as RingCell;
      cell.Visited = true;

      RingCell c1, c2;
      p.Color = path;

      while( travel.Count > 0 )
      {
        // pick a new cell from the list to continue from (part of the maze)
        if( cell == null )
          cell = travel[rand.Next(travel.Count)] as RingCell;

        // No neighbors?  Remove cell from possibilities list
        if( cell.neighbors.Count == 0 )
        {
          travel.Remove(cell);
          cell = null;
          continue;
        }


        int tries = 0;
        do
        {
          // grab a possible neighbor cell from the current cell's list of neighbors
          tmp = cell.neighbors[rand.Next(cell.neighbors.Count)] as RingCell;
          if( tmp.Visited )
          {
            // Since we double-connect all paths, cehck to see if this neighbor has been visited.  If so, remove from neigbors list.
            cell.neighbors.Remove(tmp);
            tmp = null;
          }
          else if( bias!=0 && tries<3 && tmp!= null )
          {
            // If the bias is set (try to change rings OR try to not change rings), then
            //  try to pick a cell that fits the bias.  After 3 tries, just use whatever one we have.
            //  This lets changes happen, but heavily prefers to get cells that match the bias.
            if( (bias==1 && tmp.Ring==cell.Ring) || (bias==2 && tmp.Ring!=cell.Ring) )
              break;

            // still trying to get a bias-friendly cell?  Clear reference to tmp cell for loop.
            tries++;
            tmp = null;
          }
          else
          {
            // we have a non-visited neighbor cell and we don't care about the bias.
            //  Break out of the while loop and use the one we found.
            break;
          }
        } while( cell.neighbors.Count>0 );

        // referece cell null (ran out of neighbors, for example?)
        //  Remove from list of possibilities, clear reference to current cell, and start the main loop over to get a new 'start' cell.
        if( tmp == null )
        {
          travel.Remove(cell);
          cell = null;
          continue;
        }

        // Have current cell and a neighbor cell to connect (knock walls down).
        // Code below will 'undraw' the walls between them based on their angle and ring level)
        if( tmp.Ring == cell.Ring )
        {
          // Same ring - need to kncok down either a left or right wall.
          if( (cell.Index>tmp.Index && cell.Index-tmp.Index==1) 
            || (cell.Index<tmp.Index && tmp.Index-cell.Index >1) )
            c1 = cell;  // cell is to the right of tmp
          else
            c1 = tmp;   // tmp is to the right of cell

          // Drawing an arc over the wall - set width to cell size (height) minus the width of the walls.
          //  Path-colored arc will be drawn just between the inner and outer rings, and over the connecting wall.
          p.Width = cs-penWidth;
          gp.DrawArc(p,
                      (float)(x-cs*(c1.Ring+0.5)),
                      (float)(y-cs*(c1.Ring+0.5)),
                      (float)(2*cs*(c1.Ring+0.5)),
                      (float)(2*cs*(c1.Ring+0.5)),
                      (float)(c1.ArcAngle-c1.ArcWidth/2.0),
                      (float)(c1.ArcWidth/1.0));
        }
        else // different rings
        {
          // Set a temp reference to the outer cell as c1, and the inner cell as c2.
          //  That way, no matter which one was originally which, we can do all drawing based on c1'a ring for diameter.
          c1 = tmp.Ring > cell.Ring ? tmp : cell;
          c2 = tmp.Ring > cell.Ring ? cell : tmp;

          // wider pen to overlap the wall by 1 pixel on either side
          p.Width = penWidth+2;

          // 'fudge factor' - calculate the width of the L/R walls we're erasing the arc wall between, in degrees.
          penWidthDegrees = ((4.0*penWidth) / (2*Math.PI*c1.Ring*cs)) * 180.0/Math.PI;


          if( c2.Ring == 0 )
          {
            // special case for drawing the center ring - undraw based on c1's angle/width only
            wallAngle = c1.ArcAngle;
            wallWidth = c1.ArcWidth;
          }
          else
          {
            // starting wall segemnt to knock down (undraw) shall be the greater of the two ArcAngles
            wallAngle = Math.Max(c1.ArcAngle, c2.ArcAngle);

            // the width of the wall to undraw shall be only the parts of the cell that overlap
            wallWidth = Math.Abs(Math.Min(c1.ArcAngle+c1.ArcWidth, c2.ArcAngle+c2.ArcWidth) - wallAngle);
          }

          gp.DrawArc(p,
                      x-cs*c1.Ring,
                      y-cs*c1.Ring,
                      2*cs*c1.Ring,
                      2*cs*c1.Ring,
                      (float)(wallAngle + penWidthDegrees),     // add the wall width to the start position, and
                      (float)(wallWidth - 2*penWidthDegrees)); // subtract twice that width from the overall arc length.
        }// end if tmp.Ring != cell.Ring

        cell.neighbors.Remove(tmp);
        tmp.neighbors.Remove(cell);
        tmp.Visited = true;
        travel.Add(tmp);
        cell = tmp;
      }

      return TransposePicToGrid(pic);
    }

    #endregion

    #region Dungeon Style - complete

    /// <summary>
    /// Random room placement, then random rall connections
    /// 
    /// Pplace several rooms int the maze
    /// Pathing algorithm to connect wall cells to another room's wall cell
    ///  Unlike mob and player patihing, only horizontal and vertical moves are allowed.
    ///  Pathing weights
    ///    To create hallways, cells that neighbor already pathed cells will have a penalty to their weight.
    ///    If pathing tunels into an existing tunnel, existing tunnel cells will have a weight of 1/2 normal.
    /// 
    /// height*width*poathwidth will be used to create the total area to place rooms in
    /// rooms will be 1x to 2x pathWidth in size, i.e.: random( pathwidth, 2*pathWidth)
    /// </summary>
    /// <param name="width"></param>
    /// <param name="height"></param>
    /// <param name="pathWidth"></param>
    /// <returns></returns>
    private static FloorTile[,] GenerateStyle_Dungeon( int width, int height, int pathWidth )
    {
      DM_Uber_Tool.ProgressDialog dlg = new DM_Uber_Tool.ProgressDialog();
      dlg.status = "Initializing...";
      dlg.ShowProgressDialog();

      // set overall maze size
      Bitmap pic = new Bitmap(width*pathWidth, height*pathWidth);
      //FloorTile[,] grid = new FloorTile[width*pathWidth, height*pathWidth];
      grid = new FloorTile[width * pathWidth, height * pathWidth];

      for( int i=0; i<width*pathWidth; i++ )
        for( int j=0; j<pathWidth*height; j++ )
          grid[i, j] = new FloorTile(i, j);

      Graphics g = Graphics.FromImage(pic);
      g.Clear(TileColor[(int)FloorType.Wall]); // all walls up

      Pen wallsUp   = new Pen(TileColor[(int)FloorType.Wall], 1);
      Pen wallsDown = new Pen(TileColor[(int)FloorType.Floor], 1);

      ArrayList rooms = new ArrayList();

      // place rooms - must have a 'roomSpacing'-cell border to allow pathing along the edges
      int roomSpacing = 3;
      int rx=0, 
          ry=0, 
          rw=0, 
          rh=0;
      int tries = 0;
      int totalRooms = 0;


      while( tries < 50 )
      {
        rx = rand.Next(roomSpacing, width*pathWidth-1-roomSpacing);
        ry = rand.Next(roomSpacing, height*pathWidth-1-roomSpacing);
        rw = rand.Next(pathWidth, pathWidth*3);
        rh = rand.Next(pathWidth, pathWidth*3);

        bool valid = true;

        // check room boundaries - no overlaps, 3 cell gap between them all
        for( int i=rx-roomSpacing; i<rx+rw+roomSpacing && valid; i++ )
          for( int j=ry-roomSpacing; j<ry+rh+roomSpacing && valid; j++ )
          {
            valid = i >= 0 &&
                    i <  width*pathWidth &&
                    j >= 0 &&
                    j <  height*pathWidth &&
                    grid[i, j].type==FloorType.Wall;
          }

        if( valid )
        {
          ArrayList room = new ArrayList();

          // flags for checking new rooms
          for( int i=rx; i<rx+rw; i++ )
            for( int j=ry; j<ry+rh; j++ )
            {
              // flag set to part of the room
              grid[i, j].type = FloorType.Floor;

              // borders of room added to list of potential cells to connect to other rooms
              // elaborate conditions are to attempt to not use the corner cells of the room for connections
              //if(  ((i==rx || i==rx+rw-1) && (j>ry && j<ry+rh-1))
              //  || ((j==ry || j==ry+rh-1) && (i>rx && i<rx+rw-1))
              //  )
              //  room.Add( m[i, j] );  // edge wall - add to room border
              if( i==rx && (j>ry && j<ry+rh-1) )
                room.Add(grid[i-1, j]);

              if( i==rx+rw-1 && (j>ry && j<ry+rh-1) )
                room.Add(grid[i+1, j]);

              if( j==ry && (i>rx && i<rx+rw-1) )
                room.Add(grid[i, j-1]);

              if( j==ry+rh-1 && (i>rx && i<rx+rw-1) )
                room.Add(grid[i, j+1]);
            }

          rooms.Add(room);

          // picture of maze
          g.FillRectangle(wallsDown.Brush, rx, ry, rw, rh);
          totalRooms++;

          tries = 0;
        }
        else
          tries++;
      }

      dlg.flags = grid;
      dlg.status = string.Format("Connected {0}/{1} rooms...", totalRooms-rooms.Count, totalRooms);
      dlg.percentDone = (double)(totalRooms-rooms.Count)/totalRooms;

      // find center-most room
      ArrayList centerRoom = (ArrayList)rooms[0];
      ArrayList connected = new ArrayList();

      int r1x=0, r1y=0, r2x=0, r2y=0, dist = int.MaxValue;

      foreach( ArrayList room in rooms )
      {
        if( room == centerRoom )
          continue;

        r1x = ((FloorTile)room[0]).x;
        r1y = ((FloorTile)room[0]).y;
        r2x = grid.GetLength(0)/2;
        r2y = grid.GetLength(1)/2;

        int tmpDist = (int)(Math.Pow(r1x-r2x, 2)+Math.Pow(r1y-r2y, 2));

        if( tmpDist < dist )
        {
          centerRoom = room;
          dist = tmpDist;
        }
      }

      connected.Add(centerRoom);
      rooms.Remove(centerRoom);

      // rooms ready to connect - define pathing weights/rules
      int turningWeight=0;
      int continueStraight = 0;
      int pathNeighborWeight = 0;

      switch( rand.Next(3) )
      {
        case 0:
          // favor long, straight horiz and vert paths
          turningWeight += 10;
          break;

        case 1:
          // favor long diagonal paths (i.e. : left, up, left, up, left, up instead of left, left, left, left, up, up, up, up)
          continueStraight += 10;
          break;

        default:
          // no change - paths between rooms will be somewhat haphazard and jaggedy.
          break;
      }

      // 2-in-3 chance to bais _against_ tunneling through existing paths, but allows if needed.
      //  Will try to tunnel around existing paths when set, otherwise the most direct route (through paths or rooms alike) will be used.
      pathNeighborWeight  = rand.Next(3)==0 ? 5 : 20;


      /*
       * DEBUG - override random set values for specific pathing behavior.
       */
      //continueStraight = 0;
      //turningWeight = 20;
      //pathNeighborWeight = 50;


      object[] parms = new object[] { continueStraight, turningWeight, pathNeighborWeight };

      // for each room, pick 1 or more cells that will connect to the rest
      //  add these cells to the list of doorsToConnect and Doors
      while( rooms.Count > 0 )
      {
        // pick the closest room to center from rooms (unconnected),
        dist = int.MaxValue;
        ArrayList room1 = null;

        if( rand.Next(100) == 0 )  // 1% chance to grab ANY room to connect (leads to occasional long path to wherever, takes longer to path to)
        {
          room1 = (ArrayList)(rooms[rand.Next(rooms.Count)]);
        }
        else
        {
          foreach( ArrayList tmpRoom in rooms )
          {
            r1x = ((FloorTile)tmpRoom[0]).x;
            r1y = ((FloorTile)tmpRoom[0]).y;
            r2x = grid.GetLength(0)/2;
            r2y = grid.GetLength(1)/2;

            int tmpDist = (int)(Math.Pow(r1x-r2x, 2)+Math.Pow(r1y-r2y, 2));
            if( tmpDist < dist )
            {
              room1 = tmpRoom;
              dist = tmpDist;
            }
          }
        }

        // find closest connected room to room1 - these will be connected
        dist = int.MaxValue;
        ArrayList room2 = null;

        if( rand.Next(100) == 0 )  // 1% chance to grab ANY room to connect (leads to occasional long path to wherever, takes longer to path to)
        {
          room2 = (ArrayList)(connected[rand.Next(connected.Count)]);
        }
        else
        {
          foreach( ArrayList tmpRoom in connected )
          {
            r1x = ((FloorTile)tmpRoom[0]).x;
            r1y = ((FloorTile)tmpRoom[0]).y;
            r2x = ((FloorTile)room1[0]).x;
            r2y = ((FloorTile)room1[0]).y;

            int tmpDist = (int)(Math.Pow(r1x-r2x, 2)+Math.Pow(r1y-r2y, 2));
            if( tmpDist < dist )
            {
              room2 = tmpRoom;
              dist = tmpDist;
            }
          }
        }

        FloorTile start = (FloorTile)room1[rand.Next(room1.Count)];
        FloorTile end   = (FloorTile)room2[rand.Next(room2.Count)];

        connected.Add(room1);
        rooms.Remove(room1);

        dlg.flags  = grid;
        dlg.start  = start;
        dlg.end    = end;
        dlg.status = string.Format("Connected {0}/{1} rooms...", totalRooms-rooms.Count, totalRooms);
        dlg.percentDone = (double)(totalRooms-rooms.Count)/totalRooms;

        ArrayList path = FindPath(grid, start, end, dlg, parms);

        foreach( Point p in path )
        {
          grid[p.X, p.Y].type = FloorType.Floor;
          pic.SetPixel(p.X, p.Y, TileColor[(int)FloorType.Floor]);
        }
      }

      dlg.CloseProgressDialog();

      return TransposePicToGrid(pic);
    }

    /// <summary>
    /// Implimentation fo A* pathing algorithm to find a path from one floortile to another.
    /// Used for generation of the Dungeon style mazes to display the room connections.
    /// Modified from mob AI pathfinding to disallow diagonal paths.
    /// 
    /// parms (as selected in the calling method) is an array of weights to apply:
    ///  parms[0] = continueStraight
    ///  parms[1] = turningWeight
    ///  parms[2] = pathNeighborWeight
    /// </summary>
    /// <param name="maze"></param>
    /// <param name="start"></param>
    /// <param name="end"></param>
    /// <param name="dlg"></param>
    /// <param name="parms"></param>
    /// <returns></returns>
    private static ArrayList FindPath( FloorTile[,] maze, FloorTile start, FloorTile end, DM_Uber_Tool.ProgressDialog dlg, object[] parms )
    {
      // 
      // pathing applies the following weights to cells : 
      //

      // default weight to move at all.  Applied to all options.
      int baseWeight          =  10;

      // adjust to affect pathing - long straight paths, or semi-windy ones
      int continueStraight    = (int)parms[0];
      int turningWeight       = (int)parms[1];
      int pathNeighborWeight  = (int)parms[2];



      // Following implimentation for room connections :
      //  N/S/E/W only (no diagonals, unlike mob pathing) and 
      //  must be at leaast 1 cell away from the edge of the map (allow for border)


      // clear prev pathing
      for( int i=0; i<maze.GetLength(0); i++ )
        for( int j=0; j<maze.GetLength(1); j++ )
          maze[i, j].cameFrom = null;

      // Initially, all rooms are in open (unconnected) set.
      // Once connected,the room is moved to the closed (connected) set.
      ArrayList closedSet = new ArrayList();
      ArrayList openSet   = new ArrayList();

      maze[start.x, start.y].G = 0;                                                          // G-Score = distance from self to this cell.  Starting cell distance = 0;
      maze[start.x, start.y].H = 10*(Math.Abs(start.x-end.x) + Math.Abs(start.y-end.y)); // Guess at distance from start cell to hero - calculated with no diagonals, and a tile weight of 10 (arbitrary)
      maze[start.x, start.y].F = 0;                                                          // F-Score = total distance traveled from start to this cell.  Start cell traveled dist = 0

      openSet.Add(maze[start.x, start.y]);                                                 // Open set is to be evaluated.  Add starting cell to this set.

      // evaluate cells in openSet until the hero cell is found
      while( openSet.Count>0 )
      {
        openSet.Sort();                             // To always pick the next cell with the lowest actual traveled distance.

        FloorTile current = openSet[0] as FloorTile;  // grab the next path square with the lowest F-Score value

        // prgress dialog pointer set
        dlg.current = current;

        if( current == maze[end.x, end.y] )
        {
          // Path found : assemble path, return list of cells
          ArrayList path = new ArrayList();
          while( current.cameFrom != null )
          {
            path.Add(new Point(current.x, current.y));
            current = current.cameFrom;
          }
          path.Add(new Point(start.x, start.y));
          return path;
        }

        openSet.Remove(current);    // cell being evaluated is removed from the openSet
        closedSet.Add(current);     //  ... and added to the closedSet

        // add all valid neighbors-of-current cells to the openSet
        for( int i=-1; i<=1; i++ )
        {
          for( int j=-1; j<=1; j++ )
          {
            if( (i==-1||i==1)&&(j==-1||j==1) )
              continue; // no diagonal paths to connect rooms

            if( current.x+i <= 0
              ||current.x+i >= maze.GetLength(0)-1
              ||current.y+j <= 0
              ||current.y+j >= maze.GetLength(1)-1 )
              continue; // no cells on boarder or map

            FloorTile neighbor = maze[current.x+i, current.y+j];

            // if the neighbor has already been looked at, don't revisit it.
            if( closedSet.Contains(neighbor) )
              continue;

            // determine tenative G-Score for neighbor cell
            int tenativeGScore = current.G; // base value = current cell G-Score

            // Add a weight to move to the neighbor cell
            tenativeGScore += baseWeight;

            if( current.cameFrom != null && current.cameFrom.cameFrom != null )
            {
              if( (neighbor.x == current.x && current.x == current.cameFrom.x)
                ||(neighbor.y == current.y && current.y == current.cameFrom.y)
                )
                tenativeGScore += continueStraight;
              else
                tenativeGScore += turningWeight;
            }

            // Add a penalty for each path neighbor cell this cell has 
            //  (highly discourage traveling through rooms, try to avoid crossing paths)
            for( int ni=-1; ni<=1; ni++ )
              for( int nj=-1; nj<=1; nj++ )
                if( !(maze[neighbor.x+ni, neighbor.y+nj].type==FloorType.Wall) )
                  tenativeGScore += pathNeighborWeight;


            bool tenativeGScoreIsBetter = false;  // pessimisitc default

            if( !openSet.Contains(neighbor) )
            {
              openSet.Add(neighbor);
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
              neighbor.H = 10*(Math.Abs(neighbor.x-end.x) + Math.Abs(neighbor.y-end.y));  // arbitrary heuristic - new distance to goal cell, straight line
              neighbor.F = neighbor.G + neighbor.H;
            }
          }
        }
      }

      // Clean up after myself
      openSet.Clear();
      closedSet.Clear();

      return new ArrayList(); // no path found
    }

    #endregion

    #region Fracture - complete

    /// <summary>
    /// Randomly add walls at random angles from other existing random walls, without intersecting an existing wall.
    /// Algorithm stops after some number of failed attempts to add a wall from all known start points
    /// </summary>
    /// <param name="width"></param>
    /// <param name="height"></param>
    /// <param name="pathWidth"></param>
    /// <returns></returns>
    private static FloorTile[,] GenerateStyle_Fracture( int width, int height, int pathWidth )
    {
      Bitmap pic = new Bitmap(width*pathWidth, height*pathWidth);
      Graphics gP = Graphics.FromImage(pic);
      int[,] m = new int[pic.Width, pic.Height];
      List<Point> list = new List<Point>();
      Pen wall = new Pen(TileColor[(int)FloorType.Wall], 2);
      gP.Clear(TileColor[(int)FloorType.Floor]);

      /* Vars from user input in previous incarnations - set as random values */
      int     continueChance      = rand.Next(100);     // continue with last added wall, or pick new start point?
      int     takeFromTopChance   = rand.Next(100);     // take next start point from top of list, or randomly from the middle?
      bool    allowRandomTurn     = rand.Next(2) == 0;  // only turn left?
      bool    wallTurnConstraint  = rand.Next(2) == 0;  // Max change in direction from one wall to the next when continuing?

      int     steps               = 36;
      double  step                = 2*Math.PI / steps;
      int     wallTurnAngleSteps  = rand.Next(2, 8);  // sharpest turn a continuation wall can make, in 'step's

      int     wallLenMin          = pathWidth + rand.Next(pathWidth);
      int     wallLenMax          = wallLenMin + pathWidth + rand.Next(2*pathWidth);
      double  wallLenFound        = 0.0;


      double accuracy             = 0.5;

      int tmpx;
      int tmpy;

      bool continued = false; // local flag indicating that we were able to continue from the previous wall

      // set outline of maze (box) and choose (several?) start positions to choose from
      lock( pic )
      {
        for( int i=0; i<pic.Width; i++ )
          for( int j=0; j<pic.Height; j++ )
            if( i==0 || i==pic.Width-1 || j==0 || j==pic.Height-1 )  // Edge cell?  Set to 1.  else, set to 0.
            {
              m[i, j]=1;

              if( rand.Next(100) < 10 )          // randomly add this cell to the list of start positions?
                list.Add(new Point(i, j));

              gP.FillRectangle(wall.Brush, i-1, j-1, wall.Width, wall.Width);
            }
            else
              m[i, j] = 0;
      }

      // Make sure list() array has at least one entry.  Otherwise, remarkably little happens.
      // If list() has no entries, pick the middle pixel of one of the 4 outer walls
      if( list.Count == 0 )
        switch( rand.Next(4) )
        {
          case 0:
            list.Add(new Point(0, pic.Height/2));
            break;
          case 1:
            list.Add(new Point(pic.Width-1, pic.Height/2));
            break;
          case 2:
            list.Add(new Point(pic.Width/2, 0));
            break;
          default:
            list.Add(new Point(pic.Width/2, pic.Height-1));
            break;
        }

      //
      // Generation loop variables
      //
      Point   pt;             // start coords added to list() as Point objects, for simplicity (for me).

      double  angle = 0;      // Angle to add walls at from current position

      int     count = 0;      // used to check all possible angles from a start position if current angle is invalid

      bool    ok    = false;  // flag indicating a valid angle was found from the current start location

      int     turn  = 1;      // If allowRandomTurn = true, then randomly set to either +1 or -1.
      // If allowRandomTurn = false, this is always +1.
      // When a new wall direction is not valid, this determines which direction to start 
      //  turning 'step' radians to try a new angle.

      // while list still has stuff AND the stop flag hasn't been toggled, generate!
      while( list.Count>0 )
      {
        // get 'new' start point based on some previous entry in the list
        if( continued && rand.Next(100) < continueChance )
          pt = list[list.Count-1];           // last added point is continuation of previous line.
        else if( rand.Next(100) < takeFromTopChance )
          pt = list[0];
        else
          pt = list[rand.Next(list.Count)];

        // pull the X and Y values from that point
        int lx = pt.X;
        int ly = pt.Y;

        /* determine a wall direction... */
        ok = false;  // initial value for the 'A wall can go here' flag

        count = 0;   // count used to try all angles in a circle around the point to see if ANY direction can be used.
        // the first one found is used (and is affected by the allowRandomTurn flag

        if( allowRandomTurn )
          turn = rand.Next(2)==1 ? -1 : 1;  // direction to turn if current angle is no good

        // check the direction for open space to draw wall to.
        //  If OK(...) returns false, 
        //    add a step angle to the initial angle and try again for steps times
        //  if OK(...) returns true then
        //    the current angle is good, and will be used to draw the wall and add points to list()

        if( wallTurnConstraint && continued )
        {
          // limiting continuation wall angles?
          double tmp = angle; // don't chage inital angle yet...

          tmp += step * (rand.Next(2*wallTurnAngleSteps+1)-wallTurnAngleSteps); // add/subtract some number of 'step' radians to the angle...

          // check if that angle is OK.
          if( ok = OK(lx, ly, tmp, pathWidth, pathWidth, wallLenMin, wallLenMax, out wallLenFound, pic.Width, pic.Height, m) )
            angle = tmp;
          else
          {
            // If not, we'll start at one edge of the limit and fan through all possibilities
            angle -= wallTurnAngleSteps*turn*step;

            while( !(ok = OK(lx, ly, angle+=turn*step, pathWidth, pathWidth, wallLenMin, wallLenMax, out wallLenFound, pic.Width, pic.Height, m)) && count++<=wallTurnAngleSteps*2+1 )
              /* null body - all the work is done in the conditions */
              ;
          }
        }
        else
        {
          // don't care what angle things get added at.  Use first one available

          // initial direction to try
          angle = rand.Next(steps) * step;

          while( !(ok=OK(lx, ly, angle+=turn*step, pathWidth, pathWidth, wallLenMin, wallLenMax, out wallLenFound, pic.Width, pic.Height, m)) && count++<=steps )
            /* null body - all the work is done in the conditions */
            ;
        }

        if( ok )
        {
          // set flag indicating we could continue from this point
          continued = true;

          // a direction was found with room to draw a wall.
          // So, for every pixel in a line from our current position to the wallLenFound in the direction 'angle',
          //  add that pixel to list(), and fill the pixel with the color assigend to Pen 'pR'
          for( double r = 0.0d; r<wallLenFound; r+=accuracy )
          {
            tmpx = (int)Math.Round(Math.Cos(angle)*r + lx);
            tmpy = (int)Math.Round(Math.Sin(angle)*r + ly);
            if( m[tmpx, tmpy] == 0 )
            {
              list.Add(new Point(tmpx, tmpy));
              m[tmpx, tmpy] = 1;
            }
          }
        }
        else if( wallTurnConstraint && continued )
        {
          // Couldn't continue from this point, but that doesn't mean it's fully invalid.
          // Set continued to false to indicate we could not continue, but do not remove the point.
          continued = ok;
        }
        else
        {
          // no room in any direction for a wall - remove this point from the list of possible starting points
          list.Remove(pt);

          // color the cell black - final.  User feedback that this cell will have no new walls (cosmetic, final coloring of maze)
          lock( pic )
            gP.FillRectangle(wall.Brush, lx-wall.Width/2, ly-wall.Width/2, wall.Width, wall.Width);

          // set flag indicating we could not continue from this point
          continued = false;
        }
      }

      return TransposePicToGrid(pic);
    }

    /// <summary>
    /// Starting in the direction specified, check all pixels in a 'swath' wide line for 
    ///  the length of a wall + the path width.
    /// This allows for some interesting effect on the maze by waidening ot narrowing the 
    ///  minimum path with no other pixels to allow a new wall to be drawn.
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <param name="angle"></param>
    /// <param name="pathWidth"></param>
    /// <param name="swath"></param>
    /// <param name="wallLenMin"></param>
    /// <param name="wallLenMax"></param>
    /// <param name="wallLenFound"></param>
    /// <param name="X"></param>
    /// <param name="Y"></param>
    /// <param name="grid"></param>
    /// <returns></returns>
    private static bool OK( int x, int y, double angle, int pathWidth, int swath, int wallLenMin, int wallLenMax, out double wallLenFound, int X, int Y, int[,] grid )
    {
      bool ok = true;
      wallLenFound = rand.Next((int)wallLenMin, (int)wallLenMax);
      pathWidth = Math.Max(4, pathWidth);
      double r;
      double pw;
      double accuracy = 0.5d;

      // starting the radius at 1.5 is arbitrary, but must be greater than 0, 
      //  else the start point pixel would always be found and this would always return false.
      //  trouble is, since I'm truncating all these to ints, I'm not sure if this is 100% guaranteed to
      //  not leave gaps in the walls.  So far, so good though.
      for( r=1.5d; r<=wallLenFound+pathWidth && ok; r+=accuracy )
      {
        // swath = the width of pixels on either side of the potential wall line to make sure is clear.
        for( pw = -1*Math.Min(r, swath); pw<=Math.Min(r, swath) && ok; pw+=accuracy )
        {
          int tmpx = (int)Math.Round(Math.Cos(angle)*r + Math.Cos(angle+Math.PI/2)*pw + x);
          int tmpy = (int)Math.Round(Math.Sin(angle)*r + Math.Sin(angle+Math.PI/2)*pw + y);
          ok &= (tmpx>=0 && tmpx<X && tmpy>=0 && tmpy<Y && grid[tmpx, tmpy]==0);
        }
      }

      // not OK, but we made it past min length?  If so, return true anyway...
      //  we didn't get as far as we wanted, but we got far enough to count.
      if( !ok && r>wallLenMin+pathWidth )
      {
        wallLenFound = r-pathWidth;
        return true;
      }

      return ok;
    }

    #endregion


    /// <summary>
    /// For the mazes that are "drawn" rather than generated, this will convert 
    ///   the bitmap to a FloorTile grid, using the colors specified in GenerateMaze()
    /// </summary>
    /// <param name="pic"></param>
    /// <returns></returns>
    private static FloorTile[,] TransposePicToGrid( Bitmap pic )
    {
      //tileColor = new Color[Enum.GetValues(typeof(FloorType)).Length];

      //tileColor[(int)FloorType.DoorClosed] = Color.Brown;
      //tileColor[(int)FloorType.DoorOpen] = Color.Tan;

      //tileColor[(int)FloorType.Wall] = Color.DarkGray;
      //tileColor[(int)FloorType.DoorSecret] = Color.DarkGray;

      //tileColor[(int)FloorType.Floor] = Color.BlanchedAlmond;
      //tileColor[(int)FloorType.Trap] = Color.BlanchedAlmond;

      //tileColor[(int)FloorType.StairsDown] = Color.Green;
      //tileColor[(int)FloorType.StairsUp] = Color.DarkGreen;


      grid = new FloorTile[pic.Width, pic.Height];

      for( int i=0; i<pic.Width; i++ )
      {
        for( int j=0; j<pic.Height; j++ )
        {
          grid[i, j] = new FloorTile(i, j);

          if( i==0 || i==pic.Width-1 || j==0 || j==pic.Height-1 )
            grid[i, j].type = FloorType.Wall;
          else
          {
            Color pixelColor = pic.GetPixel(i, j);

            for( int c=0; c<TileColor.Length; c++ )
              if( pixelColor.ToArgb() == TileColor[c].ToArgb() )
              {
                grid[i, j].type = (FloorType)Enum.Parse(typeof(FloorType), c.ToString());
                break;
              }
          }
        }
      }
      return grid;
    }
  }
}