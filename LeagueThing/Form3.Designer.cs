namespace LoLStatTracker
{
    partial class gameExplorer
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
            this.gamesList = new System.Windows.Forms.DataGridView();
            this.btnGetGame = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.gamesList)).BeginInit();
            this.SuspendLayout();
            // 
            // gamesList
            // 
            this.gamesList.AllowUserToAddRows = false;
            this.gamesList.AllowUserToDeleteRows = false;
            this.gamesList.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.gamesList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gamesList.Location = new System.Drawing.Point(13, 13);
            this.gamesList.Name = "gamesList";
            this.gamesList.ReadOnly = true;
            this.gamesList.Size = new System.Drawing.Size(419, 223);
            this.gamesList.TabIndex = 0;
            // 
            // btnGetGame
            // 
            this.btnGetGame.Location = new System.Drawing.Point(13, 242);
            this.btnGetGame.Name = "btnGetGame";
            this.btnGetGame.Size = new System.Drawing.Size(419, 23);
            this.btnGetGame.TabIndex = 1;
            this.btnGetGame.Text = "View Selected Game Stats";
            this.btnGetGame.UseVisualStyleBackColor = true;
            this.btnGetGame.Click += new System.EventHandler(this.btnGetGame_Click);
            // 
            // gameExplorer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(444, 275);
            this.Controls.Add(this.btnGetGame);
            this.Controls.Add(this.gamesList);
            this.Name = "gameExplorer";
            this.Text = "Games Explorer";
            ((System.ComponentModel.ISupportInitialize)(this.gamesList)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView gamesList;
        private System.Windows.Forms.Button btnGetGame;
    }
}