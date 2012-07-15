using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;

namespace Generator
{
  /// <summary>
  /// Main control class
  /// </summary>
  static class Core
  {
    public static string resourcesPath  = "";

    // List of floor tile images
    // Order is semi-important
    //  Wall, StairsDown and StairsUp should match Enum definition order 
    //  Floor tiles are alternating colors for visible to player and explored but not visible to player.
    public static string[] FloorFiles   = new string[] { "tile_wall.bmp",
                                                         "tile_stairsDown.bmp",
                                                         "tile_stairsUp.bmp",
                                                         "tile_floor1.bmp",        // visible
                                                         "tile_floor2.bmp",        // visible alt
                                                         "tile_floor3.bmp",        // explored, not in LoS
                                                         "tile_floor4.bmp",        // explored. not in LoS alt
                                                         "tile_door_ns.bmp",       // door for a E/W hallway (door is vertical)
                                                         "tile_door_ns_open.bmp",  // door for a E/W hallway (door is vertical)
                                                         "tile_door_ew.bmp",       // door for a N/S hallway (door is horizontal)
                                                         "tile_door_ew_open.bmp"   // door for a N/S hallway (door is horizontal)
                                                       };
    public static Bitmap[] FloorPics    = new Bitmap[FloorFiles.Length];

    // Should be in the same order as the enum above - enum values used as index into image lists
    public static string[] EntityFiles  = new string[] { "entity_hero.bmp",

                                                         "entity_gargoyle.bmp",
                                                         "entity_greensludge.bmp",
                                                         "entity_mage.bmp",
                                                         "entity_rogue.bmp",
                                                         "entity_warrior.bmp",
                                                         "entity_trog.bmp",
                                                         "entity_bloodslime.bmp"
                                                      }; 
    public static Bitmap[] EntityPics   = new Bitmap[EntityFiles.Length];

    // Should be in the same order as the enum above - enum values used as index into image lists
    public static string[] ItemFiles    = new string[]{ "item_money.bmp",

                                                        "item_sword.bmp",
                                                        "item_mace.bmp",
                                                        "item_staff.bmp",
                                                        "item_dagger.bmp",

                                                        "item_helm.bmp",
                                                        "item_shoulders.bmp",
                                                        "item_chest.bmp",
                                                        "item_legs.bmp",
                                                        "item_belt.bmp",
                                                        "item_gloves.bmp",
                                                        "item_boots.bmp",
                                                        "item_cloak.bmp",

                                                        "item_ring.bmp",
                                                        "item_neck.bmp",

                                                        "item_healthPotion.bmp",
                                                        "item_manapotion.bmp",

                                                        "item_treasurechest.bmp"
                                                      };
    public static Bitmap[] ItemPics     = new Bitmap[ItemFiles.Length];

    // the ever-present Random Number Generator of Doom
    private static Random random = new Random();

    public static MazeType     type           = MazeType.SubDivision;

    // Everything that happens... or the last 6-10 lines of it.
    private static ArrayList    history       = new ArrayList();

    // Maze generation limits
    public static FloorTile[,]  grid;
    public static bool[,]       visibility;
    public static Bitmap        miniMap       = null;
    public static int           mmScale       = 3;

    // arbitrary maze dimension limits
    public static int           minWidth      = 15;
    public static int           minHeight     = 15;

    public static int           maxWidth      = 30;
    public static int           maxHeight     = 30;

    // arbitrary path width limits
    public static int           minPathWidth  = 3;
    public static int           maxPathWidth  = 10;

    // initial values - 1st level is simplest maze, widest paths.
    private static int          dungeonLevel  = 1;
    private static int          width         = minWidth;
    private static int          height        = minHeight;
    private static int          pathWidth     = maxPathWidth;

    // display size and fog of war distance
    public static int           tileSize        = 16; // scaled icon size for...well...everything.
    public static int           displayDistX    = 12; // floor tiles from center (hero) to edge of display area
    public static int           displayDistY    = 12; // floor tiles from center (hero) to edge of display area

    // maze contents
    public static Hero          hero      = null;
    public static ArrayList     mobs      = null;
    public static ArrayList     items     = null;


    /// <summary>
    /// Creates a new instance of everything.  New hero, clears mobs and items
    /// </summary>
    public static void InitMaze()
    {
      GetResourcesPath();
      ReloadImages();

      if(items == null)
        items = new ArrayList();

      if(mobs == null)
        mobs = new ArrayList();

      hero = new Hero();
      items.Clear();
      mobs.Clear();

      GenerateMaze();
    }

    /// <summary>
    /// Reads the pref file to get the resources path for sprites
    /// </summary>
    public static void GetResourcesPath()
    {
      // read resourcesPath
      if(!File.Exists(Application.StartupPath + "\\prefs.txt"))
      {
        File.Create("prefs.txt").Close(); // create the file, and immediately release it
      }

      StreamReader reader = new StreamReader(Application.StartupPath + "\\prefs.txt");
      if((resourcesPath = reader.ReadLine()) == null || !(new DirectoryInfo(resourcesPath).Exists))
      {
        FolderBrowserDialog dlg = new FolderBrowserDialog();
        dlg.SelectedPath = Application.StartupPath;
        dlg.Description = "Please locate the 'Sprites' directory (about 3 levels up from the debug directory) :";

        if(dlg.ShowDialog() == DialogResult.Cancel)
          Application.Exit();

        resourcesPath = dlg.SelectedPath;
        reader.Close();

        StreamWriter output = new StreamWriter(Application.StartupPath + "\\prefs.txt");
        output.WriteLine(resourcesPath);
        output.Close();
      }
      else
      {
        reader.Close();
      }

      Core.resourcesPath = resourcesPath;
    }

    /// <summary>
    /// Updates the history event text
    /// </summary>
    /// <param name="evt"></param>
    public static void UpdateHistory(string evt)
    {
      history.Add(evt);

      while(history.Count > 8)
        history.RemoveAt(0);
    }

    /// <summary>
    /// formats the history event text
    /// </summary>
    /// <returns></returns>
    public static string History()
    {
      string str = string.Empty;

      foreach(string tmp in history)
        str += (str!=string.Empty ? Environment.NewLine : string.Empty) + tmp;

      return str;
    }

    /// <summary>
    /// Generates a single level of the dungeon.
    /// Creates the maze, sets walls, places stair tiles, items, mobs and the hero.
    /// Does noterase the inventory of the hero, but will clear the items and mobs lists prior to placement.
    /// </summary>
    public static void GenerateMaze()
    {
      if( hero == null )
        hero = new Hero();

      // new level means old lists are gone forever
      items.Clear();
      mobs.Clear();

      if(grid == null)
      {
        // no existing dungeon - set initial values
        dungeonLevel = 1;
        width = minWidth;
        height = minHeight;
        pathWidth = maxPathWidth;
      }
      else
      {
        // dungeon exists - figure new values based on which way the Hero went (up or down)
        if(grid[hero.x, hero.y].type == FloorType.StairsDown)
          dungeonLevel++;
        else if(grid[hero.x, hero.y].type == FloorType.StairsUp)
          if(--dungeonLevel < 1) // can't go to level zero or -1 or something silly
            dungeonLevel = 1;

        pathWidth = Math.Max(maxPathWidth-dungeonLevel, minPathWidth);
        width     = Math.Max(minWidth, Math.Min(dungeonLevel, maxWidth));
        height    = Math.Max(minHeight, Math.Min(dungeonLevel, maxHeight));
      }








      // request a maze from MazeGenerator with the new parameters
      //grid = MazeGenerator.GenerateMaze( width,
      //                                   height,
      //                                   pathWidth,
      //                                   1.25f - Random(50)/100.0f  // yields 75% to 125%, so a 50% chance to have less than 100% of the maze full.
      //                                 );

      //
      // DEBUG Stuff
      //
      //Generator.MazeType debugMazeType = Generator.MazeType.Castle;
      float debugPercentFull = 1.25f - Random(50) / 100.0f;
      //grid = MazeGenerator.GenerateMaze(width,
      //                                   height,
      //                                   pathWidth,
      //                                   debugMazeType,
      //                                   debugPercentFull
      //                                 );
      grid = MazeGenerator.GenerateMaze(width,
                                         height,
                                         pathWidth,
                                         type,
                                         debugPercentFull
                                       );



      //
      // End DEBUG stuff
      //



      // define new visibility mask for minimap
      visibility = new bool[grid.GetLength(0), grid.GetLength(1)];

      // define now minimap
      if(miniMap != null)
        miniMap.Dispose();

      miniMap = new Bitmap(mmScale*grid.GetLength(0), mmScale*grid.GetLength(1));
      Graphics mmg = Graphics.FromImage(miniMap);
      mmg.Clear(Color.Black);
      mmg.Dispose();

      // set each floorTile to the right type (wall/floor) based on mazeMask values (true = wall up, false = wall down = floor)
      //for(int i=0; i<grid.GetLength(0); i++)
      //  for(int j=0; j<grid.GetLength(1); j++)
      //    grid[i, j] = new FloorTile(i, j, mazeMask[i, j] ? FloorType.Wall : FloorType.Floor );




      //
      // DEBUG - testing purposes, only place the hero for now.
      //

      // put all the stuff that makes it a level.
      hero.x = grid.GetLength(0)/2;
      hero.y = grid.GetLength(1)/2;

      if(!true)
      {
        PlaceHero();                  // hero 1st - we check their position when placing mobs.  If they're not there, this check will cause a crash.
        PlaceStairs(width / 2, 1);    // warp to another level
        PlaceMobs(width * 2);       // throw some bad guys in for the Hero to beat up
        PlaceItems(width);          // OMGWTFPWN3DBBQLootz!!1!
      }
    }

    /// <summary>
    /// Places stairs by changing the tile type of random cells in the maze
    /// </summary>
    /// <param name="down"></param>
    /// <param name="up"></param>
    private static void PlaceStairs(int down, int up)
    {
      // minimum number of stairs checking
      if(down<= 0)
        down = 1;

      if(up<=0)
        if(dungeonLevel>1)
          up = 1;

      // Random placement for StairsDown
      for(int i=0; i<down; i++)
      {
        Point p = GetEmptyCell();
        grid[p.X, p.Y].type = FloorType.StairsDown;
      }

      // Random placement for StairsUp
      for(int i=0; i<up; i++)
      {
        Point p = GetEmptyCell();
        grid[p.X, p.Y].type = FloorType.StairsUp;
      }
    }

    /// <summary>
    /// Places items as random emoty cells
    /// </summary>
    /// <param name="num"></param>
    public static void PlaceItems(int num)
    {
      if(num<0)
        return;

      for(int i=0; i<num; i++)
      {
        Point p = GetEmptyCell();
        Item item = ItemFactory.CreateItem();
        item.x = p.X;
        item.y = p.Y;
        items.Add(item);
      }
    }

    /// <summary>
    /// Places mobs at random empty cells
    /// </summary>
    /// <param name="num"></param>
    public static void PlaceMobs(int num)
    {
      Point p;

      for(int i=0; i<num; i++)
      {
        p = GetEmptyCell();

        // arbitrary - don;t place mobs within 3 cells of the player.  That's kinda unfair.
        while(Math.Abs(p.X-hero.x)<=3 && p.Y-hero.y<=3)
          p = GetEmptyCell();

        Mob mob = MobFactory.CreateMob();
        mob.x = p.X;
        mob.y = p.Y;
        mobs.Add(mob);
      }
    }

    /// <summary>
    /// Places the hero at a ranomd empty cell
    /// </summary>
    public static void PlaceHero()
    {
      Point p = GetEmptyCell();
      hero.x = p.X;
      hero.y = p.Y;
    }

    /// <summary>
    /// Returns the x,y of a floor tile that does not have stairs, mobs, items or the hero on it
    /// WARNING : Infinite loop if every cell in the maze is occupied.
    /// </summary>
    /// <returns></returns>
    public static Point GetEmptyCell()
    {
      // initial values = since I generate the maze with a border on purpose, cell 0,0 will _always_ be a wall.
      //  This guarantees that the first check through the while loop will randomly pick a new cell.
      int x = 0;
      int y = 0;

      while( grid[x, y].type != FloorType.Floor  // if cell x,y is not a plain floor tile,
        ||   MobAt(x, y)     != null             // or there is a mob at cell x,y,
        ||   ItemAt(x, y)    != null             // or there is an item at cell x,y,
        ||  (hero.x==x && hero.y==y))            // or the Hero is at cell x,y
      {
        x = Core.Random(grid.GetLength(0));      // then pick a new cell.
        y = Core.Random(grid.GetLength(1));
      }

      return new Point(x, y);                 // made it out of the loop - return the x,y of the empty cell we found
    }

    /// <summary>
    /// Returns the first Mob object that has the specified location, or null if none.
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <returns></returns>
    public static Mob MobAt(int x, int y)
    {
      foreach(Mob obj in mobs)
        if(obj.x == x && obj.y == y)
          return obj;

      return null;
    }

    /// <summary>
    /// Returns the first Item object that has the specified location, or null if none.
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <returns></returns>
    public static Item ItemAt(int x, int y)
    {
      foreach(Item obj in items)
        if(obj.x == x && obj.y == y)
          return obj;

      return null;
    }

    /// <summary>
    /// Update all mobs (move towards player if player is within sight range of the mob)
    /// Randomly spawn new mobs/items appropriate for that dungeoum level
    /// </summary>
    public static void UpdateMobs()
    {
      // Each mob shall do as it pleases
      mobs.Sort(); // closest to furthest

      // remove dead mobs so pathing doesn't happen around them
      for(int i=mobs.Count-1; i>=0; i--)
        if(((Mob)mobs[i]).isDead)
          mobs.RemoveAt(i);

      foreach(Mob mob in mobs)
        mob.UpdateSelf();

      // randomly uncover a new item?
      if(Random(1000)==0)
        PlaceItems(1);

      // randomly spawn a new mob?
      if(Random(1000)==0)
        PlaceMobs(1);
    }

    /// <summary>
    /// Draws the maze and all items visible to the player position
    /// </summary>
    /// <param name="g"></param>
    public static void DrawLevel(Graphics g)
    {
      if( g == null || hero == null )
        return;

      int idx = 0;
      g.Clear(Color.Black);
      g.ResetTransform();
      g.TranslateTransform(tileSize * -(hero.x - displayDistX), -tileSize*(hero.y - displayDistY));

      // draw floor tiles for everything
      for(int i=Math.Max(0, hero.x - Math.Max(displayDistX, hero.sightRange)); i<=Math.Min(grid.GetLength(0)-1, hero.x + Math.Max(displayDistX, hero.sightRange)); i++)
      {
        for(int j=Math.Max(0, hero.y - Math.Max(displayDistY, hero.sightRange)); j<=Math.Min(grid.GetLength(1)-1, hero.y + Math.Max(displayDistY, hero.sightRange)); j++)
        {
          // 0  "tile_wall.bmp",
          // 1  "tile_stairsDown.bmp",
          // 2  "tile_stairsUp.bmp",
          // 3  "tile_floor1.bmp",        // visible
          // 4  "tile_floor2.bmp",        // visible alt
          // 5  "tile_floor3.bmp",        // explored, not in LoS
          // 6  "tile_floor4.bmp",        // explored. not in LoS alt
          // 7  "tile_door_ns.bmp",       // door for a E/W hallway (door is vertical)
          // 8  "tile_door_ns_open.bmp",  // door for a E/W hallway (door is vertical)
          // 9  "tile_door_ew.bmp",       // door for a N/S hallway (door is horizontal)
          // 10 "tile_door_ew_open.bmp"   // door for a N/S hallway (door is horizontal)

          bool tileVisible = hero.TileVisible(i, j);  // this call will update the visibility[,] mask for fog of war

          if(!tileVisible && !visibility[i, j])
          {
            // not visible or explored?  Solid black.
            g.FillRectangle(Brushes.Black, i*tileSize, j*tileSize, tileSize, tileSize);
          }
          else
          {
            //if(hero.TileVisible(i, j))
            //{
            switch(grid[i, j].type)
            {
              case FloorType.Floor:
                if( tileVisible )
                  idx = (i%2==0 && j%2==0) || (i%2==1 && j%2==1) ? 3 : 4;
                else
                  idx = (i%2==0 && j%2==0) || (i%2==1 && j%2==1) ? 5 : 6;
                break;

              case FloorType.DoorClosed:
              case FloorType.DoorOpen:
                if( (grid[i-1, j].type == FloorType.Wall || grid[i-1, j].type == FloorType.DoorClosed || grid[i-1, j].type == FloorType.DoorOpen) 
                 && (grid[i+1, j].type == FloorType.Wall || grid[i+1, j].type == FloorType.DoorClosed || grid[i+1, j].type == FloorType.DoorOpen)
                  )
                  idx = 9;
                else //if(grid[i, j-1].type == FloorType.Wall && grid[i, j+1].type==FloorType.Wall)
                  idx = 7;
                //else
                //{
                //  grid[i, j].type = FloorType.Floor;  // not really in a hallway... replace tile.
                //  idx = 0;
                //}

                if(grid[i, j].type == FloorType.DoorOpen)
                  idx++;

                break;

              case FloorType.StairsDown:
                idx = 1;
                break;
              case FloorType.StairsUp:
                idx = 2;
                break;

              default: // FloorType.Wall
                idx = 0;
                break;
            }
            g.DrawImage(FloorPics[idx], i*tileSize, j*tileSize, tileSize, tileSize);
          }
        }
      }

      foreach(Item item in items)
        if(hero.TileVisible(item.x, item.y))
          item.DrawSelf(g);

      foreach(Mob mob in mobs)
        if(hero.TileVisible(mob.x, mob.y))
          mob.DrawSelf(g);

      hero.DrawSelf(g);

      //UpdateMiniMap(false);
    }

    /// <summary>
    /// Updates minimap with Line of Sight
    /// </summary>
    /// <param name="wholeMap"></param>
    public static void UpdateMiniMap(bool wholeMap)
    {
      Color mmColor = MazeGenerator.TileColor[(int)FloorType.Wall];
      int maxX = grid.GetLength(0)-1;
      int maxY = grid.GetLength(1)-1;

      int xStart = wholeMap ? 0 : hero.x-hero.sightRange;
      int xEnd = wholeMap ? grid.GetLength(0)-1 : hero.x+hero.sightRange;
      int yStart = wholeMap ? 0 : hero.y-hero.sightRange;
      int yEnd = wholeMap ? grid.GetLength(1)-1 : hero.y+hero.sightRange;

      // update minimap within hero's sight distance only - not the whole effing thing.
      for(int i=xStart; i<=xEnd; i++)
      {
        for(int j=yStart; j<=yEnd; j++)
        {
          if(i<0 || i>maxX || j<0 || j>maxY)
          {
            continue;
          }
          else
          {
            if(visibility[i, j])
            {
              mmColor = MazeGenerator.TileColor[(int)grid[i, j].type];

              // draw the minimap cell
              for(int x=0; x<mmScale; x++)
                for(int y=0; y<mmScale; y++)
                  miniMap.SetPixel(mmScale*i+x, mmScale*j+y, mmColor);
            }
          }
        }
      }

      // hero dot - after rest of minmap is drawn
      //for(int hx=0; hx<mmScale; hx++)
      //  for(int hy=0; hy<mmScale; hy++)
      //    miniMap.SetPixel(mmScale*hero.x+hx, mmScale*hero.y+hy, Color.DarkRed);
    }

    /// <summary>
    /// Change scale for smaller number of tiles in view, but tiles are larger
    /// </summary>
    public static void ZoomIn()
    {
      //displayDistX /= 2;
      //displayDistY /= 2;

      //if(displayDistX < 4)
      //{
      //  displayDistX = 4;
        tileSize = 64;
      //}
      //else
        tileSize *= 2;

      ReloadImages();
    }

    /// <summary>
    /// Change scale for larger number of tiles in view, but tiles are smaller
    /// </summary>
    public static void ZoomOut()
    {
      //displayDistance *= 2;

      //if(displayDistance > 64)
      //{
      //  displayDistance = 64;
      //  tileSize = 4;
      //}
      //else
        tileSize /= 2;

      if( tileSize < 4 )
        tileSize = 4;

      ReloadImages();
    }

    #region Image Load

    /// <summary>
    /// Load/reload all images at current tileSize scale.
    /// </summary>
    public static void ReloadImages()
    {
      if( resourcesPath == "" )
        return;

      // load resources
      LoadFloorTiles();
      LoadEntityTiles();
      LoadItemTiles();

      if(hero != null)
        hero.LoadEquippedGearImages();
    }

    /// <summary>
    /// Loads and creates scaled images for the floortiles
    /// </summary>
    public static void LoadFloorTiles()
    {
      Bitmap tmp;

      for(int i=0; i< FloorPics.Length; i++)
        if(FloorPics[i] != null)
          FloorPics[i].Dispose();

      for(int i=0; i<FloorFiles.Length; i++)
      {
        tmp = new Bitmap(resourcesPath + "\\" + FloorFiles[i]);
        FloorPics[i] = (Bitmap)tmp.GetThumbnailImage(tileSize, tileSize, new Image.GetThumbnailImageAbort(Callback), IntPtr.Zero);
        tmp.Dispose();
      }

      for(int i=0; i<FloorPics.Length; i++)
        FloorPics[i].MakeTransparent(Color.White);
    }

    /// <summary>
    /// Loads and creates scaled images for the Mob and Hero tiles
    /// </summary>
    public static void LoadEntityTiles()
    {
      Bitmap tmp;

      for(int i=0; i< EntityPics.Length; i++)
        if(EntityPics[i] != null)
          EntityPics[i].Dispose();

      EntityPics = new Bitmap[EntityFiles.Length];

      for(int i=0; i<EntityFiles.Length; i++)
      {
        tmp = new Bitmap(resourcesPath + "\\" + EntityFiles[i]);
        EntityPics[i] = (Bitmap)tmp.GetThumbnailImage(tileSize, tileSize, new Image.GetThumbnailImageAbort(Callback), IntPtr.Zero);
        tmp.Dispose();
      }

      for(int i=0; i<EntityPics.Length; i++)
        EntityPics[i].MakeTransparent(Color.White);
    }

    /// <summary>
    /// Loads and creates scaled images for the Item tiles
    /// </summary>
    public static void LoadItemTiles()
    {
      Bitmap tmp;

      for(int i=0; i< ItemPics.Length; i++)
        if(ItemPics[i] != null)
          ItemPics[i].Dispose();

      ItemPics = new Bitmap[ItemFiles.Length];

      for(int i=0; i<ItemFiles.Length; i++)
      {
        tmp = new Bitmap(resourcesPath + "\\" + ItemFiles[i]);
        ItemPics[i] = (Bitmap)tmp.GetThumbnailImage(tileSize, tileSize, new Image.GetThumbnailImageAbort(Callback), IntPtr.Zero);
        tmp.Dispose();
      }

      for(int i=0; i<ItemPics.Length; i++)
        ItemPics[i].MakeTransparent(Color.White);
    }

    #endregion

    #region Wrappers
    /// <summary>
    /// Simple wrappers for common utility functions. Lets all methods use the same instance of a Random() object
    /// </summary>
    /// <param name="min"></param>
    /// <param name="max"></param>
    /// <returns></returns>
    public static int Random(int min, int max)
    {
      return random.Next(min, max);
    }

    /// <summary>
    /// Simple wrappers for common utility functions. Lets all methods use the same instance of a Random() object
    /// </summary>
    /// <param name="max"></param>
    /// <returns></returns>
    public static int Random(int max)
    {
      return random.Next(max);
    }

    /// <summary>
    ///  Required for the Bitmap.GetThumbnailImage() call.
    /// </summary>
    /// <returns></returns>
    public static bool Callback()
    {
      return false;
    }
    #endregion

  }
}
