namespace DM_Uber_Tool
{
  partial class DmModuleNPCGenerator
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
      base.Dispose( disposing );
    }

    #region Component Designer generated code

    /// <summary> 
    /// Required method for Designer support - do not modify 
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
      this.btnGenerateNPC = new System.Windows.Forms.Button();
      this.txtDescription = new System.Windows.Forms.RichTextBox();
      this.button1 = new System.Windows.Forms.Button();
      this.button2 = new System.Windows.Forms.Button();
      this.treeView1 = new System.Windows.Forms.TreeView();
      this.SuspendLayout();
      // 
      // btnGenerateNPC
      // 
      this.btnGenerateNPC.Location = new System.Drawing.Point(3, 3);
      this.btnGenerateNPC.Name = "btnGenerateNPC";
      this.btnGenerateNPC.Size = new System.Drawing.Size(98, 23);
      this.btnGenerateNPC.TabIndex = 1;
      this.btnGenerateNPC.Text = "Generate NPC";
      this.btnGenerateNPC.UseVisualStyleBackColor = true;
      this.btnGenerateNPC.Click += new System.EventHandler(this.btnGenerateNPC_Click);
      // 
      // txtDescription
      // 
      this.txtDescription.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.txtDescription.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.txtDescription.Location = new System.Drawing.Point(350, 5);
      this.txtDescription.Name = "txtDescription";
      this.txtDescription.Size = new System.Drawing.Size(246, 411);
      this.txtDescription.TabIndex = 2;
      this.txtDescription.Text = "";
      // 
      // button1
      // 
      this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
      this.button1.Location = new System.Drawing.Point(3, 364);
      this.button1.Name = "button1";
      this.button1.Size = new System.Drawing.Size(98, 23);
      this.button1.TabIndex = 3;
      this.button1.Text = "Random Name";
      this.button1.UseVisualStyleBackColor = true;
      this.button1.Click += new System.EventHandler(this.button1_Click);
      // 
      // button2
      // 
      this.button2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
      this.button2.Location = new System.Drawing.Point(3, 393);
      this.button2.Name = "button2";
      this.button2.Size = new System.Drawing.Size(98, 23);
      this.button2.TabIndex = 4;
      this.button2.Text = "CodeGen";
      this.button2.UseVisualStyleBackColor = true;
      this.button2.Click += new System.EventHandler(this.GenerateNpcEnumDescriptions_Click);
      // 
      // treeView1
      // 
      this.treeView1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
      this.treeView1.Location = new System.Drawing.Point(107, 5);
      this.treeView1.Name = "treeView1";
      this.treeView1.Size = new System.Drawing.Size(237, 411);
      this.treeView1.TabIndex = 5;
      this.treeView1.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.NodeSelectionChanged_Handler);
      // 
      // DmModuleNPCGenerator
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.Controls.Add(this.treeView1);
      this.Controls.Add(this.button2);
      this.Controls.Add(this.button1);
      this.Controls.Add(this.txtDescription);
      this.Controls.Add(this.btnGenerateNPC);
      this.Name = "DmModuleNPCGenerator";
      this.Size = new System.Drawing.Size(598, 419);
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.Button btnGenerateNPC;
    private System.Windows.Forms.RichTextBox txtDescription;
    private System.Windows.Forms.Button button1;
    private System.Windows.Forms.Button button2;
    private System.Windows.Forms.TreeView treeView1;
  }
}
