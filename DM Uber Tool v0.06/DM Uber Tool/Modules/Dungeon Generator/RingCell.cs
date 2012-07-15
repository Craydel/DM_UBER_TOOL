using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Generator
{
  /// <summary>
  /// Maze cell object for use with Curcular mazes.
  /// Each cell in a curcular maze may or may not connect to multiple cells on it's neighboring outer ring.
  /// Each cell will connect to only one cell on it's neighboring inner ring, with the exception of the center 'ring'
  /// </summary>
  public class RingCell
  {
    public ArrayList  neighbors = new ArrayList();
    public int        Ring;
    public int        Index;
    public double     WallAngle;
    public double     ArcAngle;
    public double     ArcWidth;
    public bool       Visited   = false;
  }
}
