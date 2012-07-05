namespace DM_Uber_Tool
{
    partial class LootGen
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
          this.dmModuleItemGenerator1 = new DM_Uber_Tool.DmModuleItemGenerator();
          this.SuspendLayout();
          // 
          // dmModuleItemGenerator1
          // 
          this.dmModuleItemGenerator1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                      | System.Windows.Forms.AnchorStyles.Left)
                      | System.Windows.Forms.AnchorStyles.Right)));
          this.dmModuleItemGenerator1.Location = new System.Drawing.Point(7, 4);
          this.dmModuleItemGenerator1.Name = "dmModuleItemGenerator1";
          this.dmModuleItemGenerator1.Size = new System.Drawing.Size(543, 427);
          this.dmModuleItemGenerator1.TabIndex = 0;
          // 
          // LootGen
          // 
          this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
          this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
          this.BackColor = System.Drawing.Color.Black;
          this.ClientSize = new System.Drawing.Size(556, 434);
          this.Controls.Add(this.dmModuleItemGenerator1);
          this.Name = "LootGen";
          this.Padding = new System.Windows.Forms.Padding(3, 0, 3, 0);
          this.Text = "LootGen";
          this.ResumeLayout(false);

        }

        #endregion

        private DmModuleItemGenerator dmModuleItemGenerator1;

    }
}