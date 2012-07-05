namespace DM_Uber_Tool
{
    partial class Form1
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
          System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
          this.btnBooks = new System.Windows.Forms.Button();
          this.btnDice = new System.Windows.Forms.Button();
          this.btnDungeon = new System.Windows.Forms.Button();
          this.btnPC = new System.Windows.Forms.Button();
          this.btnNPC = new System.Windows.Forms.Button();
          this.btnLoot = new System.Windows.Forms.Button();
          this.btnDragon = new System.Windows.Forms.Button();
          this.SuspendLayout();
          // 
          // btnBooks
          // 
          this.btnBooks.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
          this.btnBooks.Location = new System.Drawing.Point(466, 12);
          this.btnBooks.Name = "btnBooks";
          this.btnBooks.Size = new System.Drawing.Size(102, 23);
          this.btnBooks.TabIndex = 0;
          this.btnBooks.Text = "Arcane Repository";
          this.btnBooks.UseVisualStyleBackColor = true;
          this.btnBooks.Click += new System.EventHandler(this.btnBooks_Click);
          // 
          // btnDice
          // 
          this.btnDice.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
          this.btnDice.Location = new System.Drawing.Point(466, 41);
          this.btnDice.Name = "btnDice";
          this.btnDice.Size = new System.Drawing.Size(102, 23);
          this.btnDice.TabIndex = 1;
          this.btnDice.Text = "Rollin\' Bones";
          this.btnDice.UseVisualStyleBackColor = true;
          this.btnDice.Click += new System.EventHandler(this.btnDice_Click);
          // 
          // btnDungeon
          // 
          this.btnDungeon.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
          this.btnDungeon.Location = new System.Drawing.Point(466, 70);
          this.btnDungeon.Name = "btnDungeon";
          this.btnDungeon.Size = new System.Drawing.Size(102, 23);
          this.btnDungeon.TabIndex = 2;
          this.btnDungeon.Text = "Dungeon Gen";
          this.btnDungeon.UseVisualStyleBackColor = true;
          this.btnDungeon.Click += new System.EventHandler(this.btnDungeon_Click);
          // 
          // btnPC
          // 
          this.btnPC.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
          this.btnPC.Location = new System.Drawing.Point(466, 99);
          this.btnPC.Name = "btnPC";
          this.btnPC.Size = new System.Drawing.Size(102, 23);
          this.btnPC.TabIndex = 3;
          this.btnPC.Text = "Player Characters";
          this.btnPC.UseVisualStyleBackColor = true;
          this.btnPC.Click += new System.EventHandler(this.btnPC_Click);
          // 
          // btnNPC
          // 
          this.btnNPC.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
          this.btnNPC.Location = new System.Drawing.Point(466, 128);
          this.btnNPC.Name = "btnNPC";
          this.btnNPC.Size = new System.Drawing.Size(102, 23);
          this.btnNPC.TabIndex = 4;
          this.btnNPC.Text = "Summon NPC";
          this.btnNPC.UseVisualStyleBackColor = true;
          this.btnNPC.Click += new System.EventHandler(this.btnNPC_Click);
          // 
          // btnLoot
          // 
          this.btnLoot.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
          this.btnLoot.Location = new System.Drawing.Point(466, 157);
          this.btnLoot.Name = "btnLoot";
          this.btnLoot.Size = new System.Drawing.Size(102, 23);
          this.btnLoot.TabIndex = 5;
          this.btnLoot.Text = "Summon Loot";
          this.btnLoot.UseVisualStyleBackColor = true;
          this.btnLoot.Click += new System.EventHandler(this.btnLoot_Click);
          // 
          // btnDragon
          // 
          this.btnDragon.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
          this.btnDragon.Location = new System.Drawing.Point(466, 186);
          this.btnDragon.Name = "btnDragon";
          this.btnDragon.Size = new System.Drawing.Size(102, 23);
          this.btnDragon.TabIndex = 6;
          this.btnDragon.Text = "Pile \'O\' Dragons";
          this.btnDragon.UseVisualStyleBackColor = true;
          this.btnDragon.Click += new System.EventHandler(this.btnDragon_Click);
          // 
          // Form1
          // 
          this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
          this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
          this.BackColor = System.Drawing.Color.Black;
          this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
          this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
          this.ClientSize = new System.Drawing.Size(584, 359);
          this.Controls.Add(this.btnDragon);
          this.Controls.Add(this.btnLoot);
          this.Controls.Add(this.btnNPC);
          this.Controls.Add(this.btnPC);
          this.Controls.Add(this.btnDungeon);
          this.Controls.Add(this.btnDice);
          this.Controls.Add(this.btnBooks);
          this.DoubleBuffered = true;
          this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
          this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
          this.MaximizeBox = false;
          this.MaximumSize = new System.Drawing.Size(590, 387);
          this.MinimumSize = new System.Drawing.Size(590, 387);
          this.Name = "Form1";
          this.Text = "DM Uber Tool";
          this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnBooks;
        private System.Windows.Forms.Button btnDice;
        private System.Windows.Forms.Button btnDungeon;
        private System.Windows.Forms.Button btnPC;
        private System.Windows.Forms.Button btnNPC;
        private System.Windows.Forms.Button btnLoot;
        private System.Windows.Forms.Button btnDragon;
    }
}

