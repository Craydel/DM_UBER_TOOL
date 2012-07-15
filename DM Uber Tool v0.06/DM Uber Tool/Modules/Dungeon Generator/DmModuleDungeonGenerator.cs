using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DM_Uber_Tool;
using System.IO;

namespace DM_Uber_Tool
{
  public partial class DmModuleDungeonGenerator : UserControl
  {
    /// <summary>
    /// used when modifying the map
    /// </summary>
    private FloorType floorPainting = FloorType.Wall;

    /// <summary>
    /// Constructor
    /// Sets initial values for dropdowns
    /// </summary>
    public DmModuleDungeonGenerator()
    {
      InitializeComponent();

      foreach( MazeType value in Enum.GetValues(typeof(MazeType)) )
      {
        cboDungeonType.Items.Add( value.ToString() );
      }
      cboDungeonType.SelectedIndex = 0;
      cboTileSize.SelectedIndex = 0;
    }

    /// <summary>
    /// Main map draw refresh
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void OnMainMapPaint( object sender, PaintEventArgs e )
    {
      Core.DrawLevel( e.Graphics );
    }

    /// <summary>
    /// MouseDown event handler - gets position for map editing
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void MouseDownHandler( object sender, MouseEventArgs e )
    {
      if( e.Button == MouseButtons.Left )
      {
        // get Core.grid[] floortile
        int x = e.X / Core.tileSize + Core.hero.x - Core.displayDistX;
        int y = e.Y / Core.tileSize + Core.hero.y - Core.displayDistY;

        if(  x>0
          && y>0
          && x<Core.grid.GetLength( 0 )-1
          && y<Core.grid.GetLength( 1 )-1
          )
        {
          if( rdoWalls.Checked )
          {
            Core.grid[x, y].type = (Core.grid[x, y].type==FloorType.Wall ? FloorType.Floor : FloorType.Wall);
          }
          else
          {
            Core.grid[x, y].type = (Core.grid[x, y].type==FloorType.DoorClosed ? FloorType.Wall : FloorType.DoorClosed);
          }
          floorPainting = Core.grid[x, y].type;

          Refresh();
        }
      }
      else if( e.Button == MouseButtons.Right )
      {
        // get Core.grid[] floortile
        int x = e.X / Core.tileSize + Core.hero.x - Core.displayDistX;
        int y = e.Y / Core.tileSize + Core.hero.y - Core.displayDistY;

        if(  x>0
          && y>0
          && x<Core.grid.GetLength( 0 )-1
          && y<Core.grid.GetLength( 1 )-1
          )
        {
          Core.grid[x,y].type = FloorType.Floor;
          Refresh();
        }
      }
    }


    /// <summary>
    /// MouseMove event handler - If the mouse button is pressed, this will draw/undraw walls in the maze
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void MouseMoveHandler( object sender, MouseEventArgs e )
    {
      if( e.Button == MouseButtons.Left )
      {
        // get Core.grid[] floortile
        int x = e.X / Core.tileSize + Core.hero.x - Core.displayDistX;
        int y = e.Y / Core.tileSize + Core.hero.y - Core.displayDistY;

        if(  x>0
          && y>0
          && x<Core.grid.GetLength( 0 )-1
          && y<Core.grid.GetLength( 1 )-1
          )
        {
          Core.grid[x, y].type = floorPainting;
          Refresh();
        }
      }
      else if( e.Button == MouseButtons.Right )
      {
        // get Core.grid[] floortile
        int x = e.X / Core.tileSize + Core.hero.x - Core.displayDistX;
        int y = e.Y / Core.tileSize + Core.hero.y - Core.displayDistY;

        if(  x>0
          && y>0
          && x<Core.grid.GetLength( 0 )-1
          && y<Core.grid.GetLength( 1 )-1
          )
        {
          Core.grid[x, y].type = FloorType.Floor;
          Refresh();
        }
      }
    }

    /// <summary>
    /// Keypress handler for user input
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void KeyDownHandler( object sender, KeyEventArgs e )
    {
      if( e.KeyCode == Keys.Shift )
        return;

      switch( e.KeyCode )
      {
        case Keys.D1:
          for( int i=-3; i<=3; i++ )
            for( int j=-3; j<=3; j++ )
              if( i+Core.hero.x>0                                                  // 1 or more from the left
                && i+Core.hero.x<Core.grid.GetLength(0)-1                         // ...but no closer than 1 from the right
                && j+Core.hero.y>0                                                  // 1 or more from the top
                && j+Core.hero.y<Core.grid.GetLength(1)-1                         // ...but no closer than 1 from the bottom
                && Core.grid[i+Core.hero.x, j+Core.hero.y].type == FloorType.Floor  // the tile must be a Floor (no walls, no stairs)
                && Core.ItemAt(i+Core.hero.x, j+Core.hero.y) == null              // There cannot be a mob there
                && Core.MobAt(i+Core.hero.x, j+Core.hero.y) == null )             // and there cannot be an item there
              {
                // if all those conditions are met, them drop a new random item on the floor
                Item itm = ItemFactory.CreateItem();  // create a random item
                itm.x = i+Core.hero.x;                // set the location of said item to the appropriate offsets from Hero
                itm.y = j+Core.hero.y;
                Core.items.Add(itm);                // Add the item to the list that Core maintans (which will draw it next refresh)
              }
          break;

        case Keys.Oemplus:
          Core.ZoomIn();            // Zoom In View (icons bigger, fewer of them)
          ScrollBar_ValueChanged(null, null);
          SizeChanged_Handler(null, null);
          break;

        case Keys.OemMinus:         // Zoom Out view (icons smaller, more of them)
          Core.ZoomOut();
          ScrollBar_ValueChanged(null, null);
          SizeChanged_Handler(null, null);
          break;

        case Keys.D0:               // Increase Line of Sight range
          Core.hero.sightRange += 5;
          if( Core.hero.sightRange>100 )
            Core.hero.sightRange = 5;
          break;

        case Keys.OemOpenBrackets:  // Place StairsDown at hero
          Core.grid[Core.hero.x, Core.hero.y].type = FloorType.StairsDown;
          break;

        case Keys.OemCloseBrackets: // place StairsUp at hero
          Core.grid[Core.hero.x, Core.hero.y].type = FloorType.StairsUp;
          break;

        case Keys.V:                // Magic-Mapping
          for( int i=0; i<Core.visibility.GetLength(0); i++ )
            for( int j=0; j<Core.visibility.GetLength(1); j++ )
              Core.visibility[i, j] = true;

          Core.UpdateMiniMap(true); // Update the whole friggin thing
          break;

        case Keys.X:
          Core.hero.xRayVision = !Core.hero.xRayVision;
          break;

        case Keys.G:
          Core.hero.GiveItem(ItemFactory.CreateItem());
          break;

        case Keys.N:
          Core.GenerateMaze();
          break;

        default:                    // Standard key event handling - See if Hero can use the keypress.
          Core.hero.Move(e);
          break;
      }

      // Update visibility and mobs
      Core.UpdateMobs();

      // update control positions
      //ResizeControls();

      // redraw form1
      pictureBox1.Refresh();  // main map
      //pictureBox2.Refresh();  // mini-map
    }

    private void btnGenerate_Click( object sender, EventArgs e )
    {
      int w = 0;
      int h = 0;
      int p = 0;

      if(  int.TryParse( txtPathWidth.Text, out p )
        && int.TryParse( txtWidth.Text    , out w )
        && int.TryParse( txtHeight.Text   , out h )
        && w>=3 
        && h>=3 
        && p>=3
        )
      {
        // set maze type
        Core.type = (MazeType)(Enum.Parse(typeof(MazeType), cboDungeonType.Items[cboDungeonType.SelectedIndex].ToString()));

        // set dimensions
        Core.minWidth     = Core.maxWidth     = w;
        Core.minHeight    = Core.maxHeight    = h;
        Core.minPathWidth = Core.maxPathWidth = p;

        Core.tileSize     = int.Parse( cboTileSize.Items[cboTileSize.SelectedIndex].ToString() );

        // generate
        Core.InitMaze();

        // set all cells visible
        for( int i=0; i<Core.visibility.GetLength(0); i++ )
          for( int j=0; j<Core.visibility.GetLength(1); j++ )
            Core.visibility[i, j] = true;

        // update scroll bars and display
        SizeChanged_Handler(null, null);
      }

    }

    private void SizeChanged_Handler( object sender, EventArgs e )
    {
      if( Core.grid == null )
        return;

      hScrollBar1.Enabled = (pictureBox1.Width <= Core.grid.GetLength(0)*Core.tileSize);
      vScrollBar1.Enabled = (pictureBox1.Height<= Core.grid.GetLength(1)*Core.tileSize);

      Core.displayDistX = pictureBox1.Width  / Core.tileSize / 2;
      Core.displayDistY = pictureBox1.Height / Core.tileSize / 2;
      
      double ratio = 0;

      if( hScrollBar1.Enabled )
      {
        ratio = (double)hScrollBar1.Value / (double)hScrollBar1.Maximum;
        hScrollBar1.Minimum = Core.displayDistX;
        hScrollBar1.Maximum = Core.grid.GetLength(0)-Core.displayDistX + hScrollBar1.LargeChange-((pictureBox1.Width  / Core.tileSize)%2==0 ? 1 : 2);
        hScrollBar1.Value = (int)(Math.Max(hScrollBar1.Minimum, Math.Min(hScrollBar1.Maximum, hScrollBar1.Value)));
      }
      else
      {
        Core.hero.x = Core.grid.GetLength(0)/2;
      }

      if( vScrollBar1.Enabled )
      {
        ratio = (double)vScrollBar1.Value / (double)vScrollBar1.Maximum;
        vScrollBar1.Minimum = Core.displayDistY;
        vScrollBar1.Maximum = Core.grid.GetLength(1)-Core.displayDistY + vScrollBar1.LargeChange-((pictureBox1.Height / Core.tileSize)%2==0 ? 1 : 2);
        vScrollBar1.Value = (int)(Math.Max(vScrollBar1.Minimum, Math.Min(vScrollBar1.Maximum, vScrollBar1.Value)));
      }
      else
      {
        Core.hero.y = Core.grid.GetLength(1)/2;
      }

      // trigger
      ScrollBar_ValueChanged(null, null);
    }

    private void ScrollBar_ValueChanged( object sender, EventArgs e )
    {
      if( Core.hero == null )
        return;

      if( hScrollBar1.Enabled )
        Core.hero.x = hScrollBar1.Value;

      if( vScrollBar1.Enabled )
        Core.hero.y = vScrollBar1.Value;

      pictureBox1.Invalidate();
    }

    private void MazeParams_OnChange( object sender, EventArgs e )
    {
      int w = 0;
      int h = 0;
      int p = 0;

      if( int.TryParse(txtPathWidth.Text, out p)
        && int.TryParse(txtWidth.Text, out w)
        && int.TryParse(txtHeight.Text, out h)
        && w>=3 
        && h>=3 
        && p>=3
        )
      {
        lblTotalSize.Text = string.Format( "Total size [ {0}, {1} ]", w*p+1, h*p+1 );
        btnGenerate.Enabled = true;
      }
      else
      {
        lblTotalSize.Text = "Total size [ x, y ]";
        btnGenerate.Enabled = false;
      }
    }

    private void TileSize_OnChange( object sender, EventArgs e )
    {
      Core.tileSize     = int.Parse( cboTileSize.Items[cboTileSize.SelectedIndex].ToString() );
      Core.ReloadImages();

      // trigger a refresh of scrollbars and form drawing
      SizeChanged_Handler(null, null);
    }

    private void TextBox_OnEnter( object sender, EventArgs e )
    {
      ((TextBox)sender).SelectAll();
    }

  }
}
