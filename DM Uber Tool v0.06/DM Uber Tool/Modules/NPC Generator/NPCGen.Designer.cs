namespace DM_Uber_Tool
{
    partial class NPCGen
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
      this.dmModuleNPCGenerator1 = new DM_Uber_Tool.DmModuleNPCGenerator();
      this.SuspendLayout();
      // 
      // dmModuleNPCGenerator1
      // 
      this.dmModuleNPCGenerator1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.dmModuleNPCGenerator1.Location = new System.Drawing.Point(13, 13);
      this.dmModuleNPCGenerator1.Name = "dmModuleNPCGenerator1";
      this.dmModuleNPCGenerator1.Size = new System.Drawing.Size(629, 172);
      this.dmModuleNPCGenerator1.TabIndex = 0;
      // 
      // NPCGen
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.BackColor = System.Drawing.Color.Black;
      this.ClientSize = new System.Drawing.Size(654, 197);
      this.Controls.Add(this.dmModuleNPCGenerator1);
      this.Name = "NPCGen";
      this.Text = "NPCGen";
      this.ResumeLayout(false);

        }

        #endregion

        private DmModuleNPCGenerator dmModuleNPCGenerator1;

    }
}