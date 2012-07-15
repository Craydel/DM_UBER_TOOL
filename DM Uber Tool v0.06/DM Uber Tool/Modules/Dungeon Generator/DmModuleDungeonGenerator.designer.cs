namespace DM_Uber_Tool
{
  partial class DmModuleDungeonGenerator
  {
    /// <summary> 
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary> 
    /// Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose( bool disposing )
    {
      if( disposing && (components != null) )
      {
        components.Dispose();
      }
      base.Dispose(disposing);
    }

    #region Component Designer generated code

    /// <summary> 
    /// Required method for Designer support - do not modify 
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
      this.pictureBox1 = new System.Windows.Forms.PictureBox();
      this.txtPathWidth = new System.Windows.Forms.TextBox();
      this.lblPathWidth = new System.Windows.Forms.Label();
      this.cboDungeonType = new System.Windows.Forms.ComboBox();
      this.btnGenerate = new System.Windows.Forms.Button();
      this.hScrollBar1 = new System.Windows.Forms.HScrollBar();
      this.vScrollBar1 = new System.Windows.Forms.VScrollBar();
      this.label1 = new System.Windows.Forms.Label();
      this.txtWidth = new System.Windows.Forms.TextBox();
      this.label2 = new System.Windows.Forms.Label();
      this.txtHeight = new System.Windows.Forms.TextBox();
      this.lblTotalSize = new System.Windows.Forms.Label();
      this.cboTileSize = new System.Windows.Forms.ComboBox();
      this.lblTileSize = new System.Windows.Forms.Label();
      this.rdoWalls = new System.Windows.Forms.RadioButton();
      this.rdoDoors = new System.Windows.Forms.RadioButton();
      this.rdoTraps = new System.Windows.Forms.RadioButton();
      ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
      this.SuspendLayout();
      // 
      // pictureBox1
      // 
      this.pictureBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.pictureBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.pictureBox1.Location = new System.Drawing.Point(146, 4);
      this.pictureBox1.Name = "pictureBox1";
      this.pictureBox1.Size = new System.Drawing.Size(362, 314);
      this.pictureBox1.TabIndex = 0;
      this.pictureBox1.TabStop = false;
      this.pictureBox1.Paint += new System.Windows.Forms.PaintEventHandler(this.OnMainMapPaint);
      this.pictureBox1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.MouseDownHandler);
      this.pictureBox1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.MouseMoveHandler);
      // 
      // txtPathWidth
      // 
      this.txtPathWidth.Location = new System.Drawing.Point(97, 31);
      this.txtPathWidth.Name = "txtPathWidth";
      this.txtPathWidth.Size = new System.Drawing.Size(43, 20);
      this.txtPathWidth.TabIndex = 10;
      this.txtPathWidth.Text = "5";
      this.txtPathWidth.TextChanged += new System.EventHandler(this.MazeParams_OnChange);
      this.txtPathWidth.Enter += new System.EventHandler(this.TextBox_OnEnter);
      // 
      // lblPathWidth
      // 
      this.lblPathWidth.Location = new System.Drawing.Point(3, 31);
      this.lblPathWidth.Name = "lblPathWidth";
      this.lblPathWidth.Size = new System.Drawing.Size(88, 20);
      this.lblPathWidth.TabIndex = 2;
      this.lblPathWidth.Text = "Path Width";
      this.lblPathWidth.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // cboDungeonType
      // 
      this.cboDungeonType.FormattingEnabled = true;
      this.cboDungeonType.Location = new System.Drawing.Point(3, 4);
      this.cboDungeonType.Name = "cboDungeonType";
      this.cboDungeonType.Size = new System.Drawing.Size(137, 21);
      this.cboDungeonType.Sorted = true;
      this.cboDungeonType.TabIndex = 0;
      // 
      // btnGenerate
      // 
      this.btnGenerate.Location = new System.Drawing.Point(3, 139);
      this.btnGenerate.Name = "btnGenerate";
      this.btnGenerate.Size = new System.Drawing.Size(137, 23);
      this.btnGenerate.TabIndex = 20;
      this.btnGenerate.Text = "Generate";
      this.btnGenerate.UseVisualStyleBackColor = true;
      this.btnGenerate.Click += new System.EventHandler(this.btnGenerate_Click);
      // 
      // hScrollBar1
      // 
      this.hScrollBar1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.hScrollBar1.Location = new System.Drawing.Point(146, 318);
      this.hScrollBar1.Name = "hScrollBar1";
      this.hScrollBar1.Size = new System.Drawing.Size(362, 15);
      this.hScrollBar1.TabIndex = 5;
      this.hScrollBar1.ValueChanged += new System.EventHandler(this.ScrollBar_ValueChanged);
      // 
      // vScrollBar1
      // 
      this.vScrollBar1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.vScrollBar1.Location = new System.Drawing.Point(508, 4);
      this.vScrollBar1.Name = "vScrollBar1";
      this.vScrollBar1.Size = new System.Drawing.Size(15, 314);
      this.vScrollBar1.TabIndex = 6;
      this.vScrollBar1.ValueChanged += new System.EventHandler(this.ScrollBar_ValueChanged);
      // 
      // label1
      // 
      this.label1.Location = new System.Drawing.Point(3, 57);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(88, 20);
      this.label1.TabIndex = 8;
      this.label1.Text = "Width";
      this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // txtWidth
      // 
      this.txtWidth.Location = new System.Drawing.Point(97, 57);
      this.txtWidth.Name = "txtWidth";
      this.txtWidth.Size = new System.Drawing.Size(43, 20);
      this.txtWidth.TabIndex = 11;
      this.txtWidth.Text = "15";
      this.txtWidth.TextChanged += new System.EventHandler(this.MazeParams_OnChange);
      this.txtWidth.Enter += new System.EventHandler(this.TextBox_OnEnter);
      // 
      // label2
      // 
      this.label2.Location = new System.Drawing.Point(3, 83);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(88, 20);
      this.label2.TabIndex = 10;
      this.label2.Text = "Height";
      this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // txtHeight
      // 
      this.txtHeight.Location = new System.Drawing.Point(97, 83);
      this.txtHeight.Name = "txtHeight";
      this.txtHeight.Size = new System.Drawing.Size(43, 20);
      this.txtHeight.TabIndex = 12;
      this.txtHeight.Text = "15";
      this.txtHeight.TextChanged += new System.EventHandler(this.MazeParams_OnChange);
      this.txtHeight.Enter += new System.EventHandler(this.TextBox_OnEnter);
      // 
      // lblTotalSize
      // 
      this.lblTotalSize.Location = new System.Drawing.Point(3, 116);
      this.lblTotalSize.Name = "lblTotalSize";
      this.lblTotalSize.Size = new System.Drawing.Size(137, 20);
      this.lblTotalSize.TabIndex = 11;
      this.lblTotalSize.Text = "Total Cells";
      this.lblTotalSize.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // cboTileSize
      // 
      this.cboTileSize.FormattingEnabled = true;
      this.cboTileSize.Items.AddRange(new object[] {
            "4",
            "8",
            "16",
            "32",
            "64"});
      this.cboTileSize.Location = new System.Drawing.Point(97, 168);
      this.cboTileSize.Name = "cboTileSize";
      this.cboTileSize.Size = new System.Drawing.Size(43, 21);
      this.cboTileSize.TabIndex = 30;
      this.cboTileSize.SelectedValueChanged += new System.EventHandler(this.TileSize_OnChange);
      // 
      // lblTileSize
      // 
      this.lblTileSize.Location = new System.Drawing.Point(3, 169);
      this.lblTileSize.Name = "lblTileSize";
      this.lblTileSize.Size = new System.Drawing.Size(88, 20);
      this.lblTileSize.TabIndex = 13;
      this.lblTileSize.Text = "Tile Size";
      this.lblTileSize.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // rdoWalls
      // 
      this.rdoWalls.Checked = true;
      this.rdoWalls.Location = new System.Drawing.Point(6, 195);
      this.rdoWalls.Name = "rdoWalls";
      this.rdoWalls.Size = new System.Drawing.Size(134, 17);
      this.rdoWalls.TabIndex = 40;
      this.rdoWalls.TabStop = true;
      this.rdoWalls.Text = "Wall / Floor";
      this.rdoWalls.UseVisualStyleBackColor = true;
      // 
      // rdoDoors
      // 
      this.rdoDoors.Location = new System.Drawing.Point(6, 218);
      this.rdoDoors.Name = "rdoDoors";
      this.rdoDoors.Size = new System.Drawing.Size(134, 17);
      this.rdoDoors.TabIndex = 41;
      this.rdoDoors.Text = "Door / Wall";
      this.rdoDoors.UseVisualStyleBackColor = true;
      // 
      // rdoTraps
      // 
      this.rdoTraps.Location = new System.Drawing.Point(6, 241);
      this.rdoTraps.Name = "rdoTraps";
      this.rdoTraps.Size = new System.Drawing.Size(134, 17);
      this.rdoTraps.TabIndex = 42;
      this.rdoTraps.Text = "Trap / Floor";
      this.rdoTraps.UseVisualStyleBackColor = true;
      // 
      // DmModuleDungeonGenerator
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.Controls.Add(this.rdoTraps);
      this.Controls.Add(this.rdoDoors);
      this.Controls.Add(this.rdoWalls);
      this.Controls.Add(this.lblTileSize);
      this.Controls.Add(this.cboTileSize);
      this.Controls.Add(this.lblTotalSize);
      this.Controls.Add(this.label2);
      this.Controls.Add(this.txtHeight);
      this.Controls.Add(this.label1);
      this.Controls.Add(this.txtWidth);
      this.Controls.Add(this.vScrollBar1);
      this.Controls.Add(this.hScrollBar1);
      this.Controls.Add(this.btnGenerate);
      this.Controls.Add(this.cboDungeonType);
      this.Controls.Add(this.lblPathWidth);
      this.Controls.Add(this.txtPathWidth);
      this.Controls.Add(this.pictureBox1);
      this.DoubleBuffered = true;
      this.Name = "DmModuleDungeonGenerator";
      this.Size = new System.Drawing.Size(523, 336);
      this.SizeChanged += new System.EventHandler(this.SizeChanged_Handler);
      this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.KeyDownHandler);
      ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.PictureBox pictureBox1;
    private System.Windows.Forms.TextBox txtPathWidth;
    private System.Windows.Forms.Label lblPathWidth;
    private System.Windows.Forms.ComboBox cboDungeonType;
    private System.Windows.Forms.Button btnGenerate;
    private System.Windows.Forms.HScrollBar hScrollBar1;
    private System.Windows.Forms.VScrollBar vScrollBar1;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.TextBox txtWidth;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.TextBox txtHeight;
    private System.Windows.Forms.Label lblTotalSize;
    private System.Windows.Forms.ComboBox cboTileSize;
    private System.Windows.Forms.Label lblTileSize;
    private System.Windows.Forms.RadioButton rdoWalls;
    private System.Windows.Forms.RadioButton rdoDoors;
    private System.Windows.Forms.RadioButton rdoTraps;
  }
}
