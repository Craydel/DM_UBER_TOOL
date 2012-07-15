namespace DM_Uber_Tool
{
    partial class DungeonGen
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
      this.dmModuleDungeonGenerator1 = new DM_Uber_Tool.DmModuleDungeonGenerator();
      this.SuspendLayout();
      // 
      // dmModuleDungeonGenerator1
      // 
      this.dmModuleDungeonGenerator1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.dmModuleDungeonGenerator1.Location = new System.Drawing.Point(13, 13);
      this.dmModuleDungeonGenerator1.Name = "dmModuleDungeonGenerator1";
      this.dmModuleDungeonGenerator1.Size = new System.Drawing.Size(681, 454);
      this.dmModuleDungeonGenerator1.TabIndex = 0;
      // 
      // DungeonGen
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.BackColor = System.Drawing.SystemColors.Control;
      this.ClientSize = new System.Drawing.Size(706, 479);
      this.Controls.Add(this.dmModuleDungeonGenerator1);
      this.Location = new System.Drawing.Point(12, 9);
      this.Name = "DungeonGen";
      this.Text = "DungeonGen";
      this.ResumeLayout(false);

        }

        #endregion

        private DmModuleDungeonGenerator dmModuleDungeonGenerator1;

    }
}