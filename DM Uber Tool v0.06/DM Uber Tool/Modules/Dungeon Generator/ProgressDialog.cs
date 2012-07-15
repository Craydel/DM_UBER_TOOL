using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;

namespace DM_Uber_Tool
{
  public partial class ProgressDialog : Form
  {
    private bool stopRequested = false;
    private Bitmap pic = null;
    public Thread thread;
    public string status = "";
    public double percentDone = 0.0;
    public FloorTile[,] flags;
    public FloorTile start   = null;
    public FloorTile end     = null;
    public FloorTile current = null;
    public bool CanClose     = false; // set by Caslte gen, which is the only generator that does not auo-close the progress dialog when it's done.

    public delegate void VoidTarget();

    public ProgressDialog()
    {
      InitializeComponent();
    }

    private void OnProgressBarPaint( object sender, PaintEventArgs e )
    {
      if( percentDone > 0 )
        e.Graphics.FillRectangle( Brushes.DarkBlue, 0, 0, (int)(pictureBox1.Width*percentDone), pictureBox1.Height );
    }

    private void OnPreviewPaint( object sender, PaintEventArgs e )
    {
      if( flags == null )
        return;

      if( pic == null )
      {
        pic = new Bitmap( flags.GetLength( 0 ), flags.GetLength( 1 ) );
        ClientSize = new Size( ClientRectangle.Width - pictureBox2.Width + pic.Width,
                               ClientRectangle.Height- pictureBox2.Height + pic.Height );
      }

      if( pic == null )
        return;

      for( int i=0; i<pic.Width; i++ )
        for( int j=0; j<pic.Height; j++ )
        {
          if(flags[i, j].cameFrom==null)
            pic.SetPixel(i, j, flags[i, j].type==FloorType.Wall ? Color.Black : Color.White);
          else
            pic.SetPixel(i, j, flags[i, j].type==FloorType.Wall ? Color.Gray  : Color.LightGray);
        }

      while( current != null )
      {
        pic.SetPixel( current.x, current.y, Color.Blue );
        current = current.cameFrom;
      }

      e.Graphics.DrawImageUnscaled( pic, 0, 0 );

      if( start != null && end != null )
        e.Graphics.DrawLine( Pens.Red, start.x, start.y, end.x, end.y );
    }

    public void ShowProgressDialog()
    {
      thread = new Thread( new ThreadStart( Run ) );
      thread.Start();
      Thread.Sleep(0);
    }

    private void Run()
    {
      this.Show();

      while( !Visible )
        Application.DoEvents();

      while( !stopRequested )
      {
        label1.Text = status;
        pictureBox1.Refresh();
        pictureBox2.Refresh();

        Application.DoEvents();
        //Thread.Sleep(0);
      }
    }

    public void CloseProgressDialog()
    {
      stopRequested = true;

      try
      {
        if( this.InvokeRequired )
        {
          Delegate d = new VoidTarget( CloseProgressDialog );
          Invoke( d );
        }
        else
          this.Close();
      }
      catch { }
    }

    private void CloseHandler( object sender, FormClosedEventArgs e )
    {
      stopRequested = true;
    }

    private void Click_Handler( object sender, EventArgs e )
    {
      if( CanClose )
        this.CloseProgressDialog();
    }
  }
}
