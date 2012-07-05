namespace DM_Uber_Tool
{
  partial class DmModuleItemGenerator
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
      this.txtItemList = new System.Windows.Forms.RichTextBox();
      this.btnGenerateRandom = new System.Windows.Forms.Button();
      this.btnGenerateMoney = new System.Windows.Forms.Button();
      this.btnGenerateFood = new System.Windows.Forms.Button();
      this.btnGeneratePack = new System.Windows.Forms.Button();
      this.btnGenerateEquipment = new System.Windows.Forms.Button();
      this.btnGenerateWeapon = new System.Windows.Forms.Button();
      this.btnGenerateArmor = new System.Windows.Forms.Button();
      this.btnGenerateClothing = new System.Windows.Forms.Button();
      this.button1 = new System.Windows.Forms.Button();
      this.btnClearList = new System.Windows.Forms.Button();
      this.SuspendLayout();
      // 
      // txtItemList
      // 
      this.txtItemList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.txtItemList.Location = new System.Drawing.Point(131, 5);
      this.txtItemList.Name = "txtItemList";
      this.txtItemList.Size = new System.Drawing.Size(506, 472);
      this.txtItemList.TabIndex = 0;
      this.txtItemList.Text = "";
      // 
      // btnGenerateRandom
      // 
      this.btnGenerateRandom.Location = new System.Drawing.Point(3, 3);
      this.btnGenerateRandom.Name = "btnGenerateRandom";
      this.btnGenerateRandom.Size = new System.Drawing.Size(122, 23);
      this.btnGenerateRandom.TabIndex = 1;
      this.btnGenerateRandom.Text = "Random Item";
      this.btnGenerateRandom.UseVisualStyleBackColor = true;
      this.btnGenerateRandom.Click += new System.EventHandler(this.GenerateButton_Click);
      // 
      // btnGenerateMoney
      // 
      this.btnGenerateMoney.Location = new System.Drawing.Point(3, 32);
      this.btnGenerateMoney.Name = "btnGenerateMoney";
      this.btnGenerateMoney.Size = new System.Drawing.Size(122, 23);
      this.btnGenerateMoney.TabIndex = 2;
      this.btnGenerateMoney.Text = "Money";
      this.btnGenerateMoney.UseVisualStyleBackColor = true;
      this.btnGenerateMoney.Click += new System.EventHandler(this.GenerateButton_Click);
      // 
      // btnGenerateFood
      // 
      this.btnGenerateFood.Location = new System.Drawing.Point(3, 90);
      this.btnGenerateFood.Name = "btnGenerateFood";
      this.btnGenerateFood.Size = new System.Drawing.Size(122, 23);
      this.btnGenerateFood.TabIndex = 3;
      this.btnGenerateFood.Text = "Food";
      this.btnGenerateFood.UseVisualStyleBackColor = true;
      this.btnGenerateFood.Click += new System.EventHandler(this.GenerateButton_Click);
      // 
      // btnGeneratePack
      // 
      this.btnGeneratePack.Location = new System.Drawing.Point(3, 61);
      this.btnGeneratePack.Name = "btnGeneratePack";
      this.btnGeneratePack.Size = new System.Drawing.Size(122, 23);
      this.btnGeneratePack.TabIndex = 4;
      this.btnGeneratePack.Text = "Container";
      this.btnGeneratePack.UseVisualStyleBackColor = true;
      this.btnGeneratePack.Click += new System.EventHandler(this.GenerateButton_Click);
      // 
      // btnGenerateEquipment
      // 
      this.btnGenerateEquipment.Location = new System.Drawing.Point(3, 119);
      this.btnGenerateEquipment.Name = "btnGenerateEquipment";
      this.btnGenerateEquipment.Size = new System.Drawing.Size(122, 23);
      this.btnGenerateEquipment.TabIndex = 5;
      this.btnGenerateEquipment.Text = "Misc Equipment";
      this.btnGenerateEquipment.UseVisualStyleBackColor = true;
      this.btnGenerateEquipment.Click += new System.EventHandler(this.GenerateButton_Click);
      // 
      // btnGenerateWeapon
      // 
      this.btnGenerateWeapon.Location = new System.Drawing.Point(3, 177);
      this.btnGenerateWeapon.Name = "btnGenerateWeapon";
      this.btnGenerateWeapon.Size = new System.Drawing.Size(122, 23);
      this.btnGenerateWeapon.TabIndex = 6;
      this.btnGenerateWeapon.Text = "Weapon";
      this.btnGenerateWeapon.UseVisualStyleBackColor = true;
      this.btnGenerateWeapon.Click += new System.EventHandler(this.GenerateButton_Click);
      // 
      // btnGenerateArmor
      // 
      this.btnGenerateArmor.Location = new System.Drawing.Point(3, 206);
      this.btnGenerateArmor.Name = "btnGenerateArmor";
      this.btnGenerateArmor.Size = new System.Drawing.Size(122, 23);
      this.btnGenerateArmor.TabIndex = 7;
      this.btnGenerateArmor.Text = "Armor";
      this.btnGenerateArmor.UseVisualStyleBackColor = true;
      this.btnGenerateArmor.Click += new System.EventHandler(this.GenerateButton_Click);
      // 
      // btnGenerateClothing
      // 
      this.btnGenerateClothing.Location = new System.Drawing.Point(3, 148);
      this.btnGenerateClothing.Name = "btnGenerateClothing";
      this.btnGenerateClothing.Size = new System.Drawing.Size(122, 23);
      this.btnGenerateClothing.TabIndex = 8;
      this.btnGenerateClothing.Text = "Clothing";
      this.btnGenerateClothing.UseVisualStyleBackColor = true;
      this.btnGenerateClothing.Click += new System.EventHandler(this.GenerateButton_Click);
      // 
      // button1
      // 
      this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
      this.button1.Location = new System.Drawing.Point(3, 454);
      this.button1.Name = "button1";
      this.button1.Size = new System.Drawing.Size(122, 23);
      this.button1.TabIndex = 9;
      this.button1.Text = "Desctiption CodeGen";
      this.button1.UseVisualStyleBackColor = true;
      this.button1.Click += new System.EventHandler(this.button1_Click);
      // 
      // btnClearList
      // 
      this.btnClearList.Location = new System.Drawing.Point(3, 235);
      this.btnClearList.Name = "btnClearList";
      this.btnClearList.Size = new System.Drawing.Size(75, 23);
      this.btnClearList.TabIndex = 10;
      this.btnClearList.Text = "Clea rList";
      this.btnClearList.UseVisualStyleBackColor = true;
      this.btnClearList.Click += new System.EventHandler(this.btnClearList_Click);
      // 
      // DmModuleItemGenerator
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.Controls.Add(this.btnClearList);
      this.Controls.Add(this.button1);
      this.Controls.Add(this.btnGenerateClothing);
      this.Controls.Add(this.btnGenerateArmor);
      this.Controls.Add(this.btnGenerateWeapon);
      this.Controls.Add(this.btnGenerateEquipment);
      this.Controls.Add(this.btnGeneratePack);
      this.Controls.Add(this.btnGenerateFood);
      this.Controls.Add(this.btnGenerateMoney);
      this.Controls.Add(this.btnGenerateRandom);
      this.Controls.Add(this.txtItemList);
      this.Name = "DmModuleItemGenerator";
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.RichTextBox txtItemList;
    private System.Windows.Forms.Button btnGenerateRandom;
    private System.Windows.Forms.Button btnGenerateMoney;
    private System.Windows.Forms.Button btnGenerateFood;
    private System.Windows.Forms.Button btnGeneratePack;
    private System.Windows.Forms.Button btnGenerateEquipment;
    private System.Windows.Forms.Button btnGenerateWeapon;
    private System.Windows.Forms.Button btnGenerateArmor;
    private System.Windows.Forms.Button btnGenerateClothing;
    private System.Windows.Forms.Button button1;
    private System.Windows.Forms.Button btnClearList;
  }
}
