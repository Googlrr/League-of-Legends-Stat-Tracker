namespace LoLStatTracker
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.panel1 = new System.Windows.Forms.Panel();
            this.lstUsers = new System.Windows.Forms.ListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cboRefresh = new System.Windows.Forms.ComboBox();
            this.btnAdd = new System.Windows.Forms.Button();
            this.txtUser = new System.Windows.Forms.TextBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnAdv = new System.Windows.Forms.Button();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.btnGamesList = new System.Windows.Forms.Button();
            this.btnReport = new System.Windows.Forms.Button();
            this.ckAllChamps = new System.Windows.Forms.CheckBox();
            this.cboReportChampion = new System.Windows.Forms.ComboBox();
            this.cboReportUser = new System.Windows.Forms.ComboBox();
            this.trayIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.refreshChampionTableToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.lstUsers);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.cboRefresh);
            this.panel1.Controls.Add(this.btnAdd);
            this.panel1.Controls.Add(this.txtUser);
            this.panel1.Location = new System.Drawing.Point(12, 27);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(135, 220);
            this.panel1.TabIndex = 0;
            // 
            // lstUsers
            // 
            this.lstUsers.FormattingEnabled = true;
            this.lstUsers.Location = new System.Drawing.Point(6, 99);
            this.lstUsers.Name = "lstUsers";
            this.lstUsers.Size = new System.Drawing.Size(120, 108);
            this.lstUsers.TabIndex = 5;
            this.lstUsers.SelectedIndexChanged += new System.EventHandler(this.lstUsers_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 55);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(70, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Refresh Rate";
            // 
            // cboRefresh
            // 
            this.cboRefresh.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboRefresh.FormattingEnabled = true;
            this.cboRefresh.Items.AddRange(new object[] {
            "6 Hours",
            "4 Hours",
            "2 Hours"});
            this.cboRefresh.Location = new System.Drawing.Point(6, 71);
            this.cboRefresh.Name = "cboRefresh";
            this.cboRefresh.Size = new System.Drawing.Size(121, 21);
            this.cboRefresh.TabIndex = 3;
            this.cboRefresh.SelectedIndexChanged += new System.EventHandler(this.cboRefresh_SelectedIndexChanged);
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(6, 29);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(121, 23);
            this.btnAdd.TabIndex = 2;
            this.btnAdd.Text = "Add User";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // txtUser
            // 
            this.txtUser.Location = new System.Drawing.Point(6, 3);
            this.txtUser.Name = "txtUser";
            this.txtUser.Size = new System.Drawing.Size(121, 20);
            this.txtUser.TabIndex = 1;
            // 
            // panel2
            // 
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.btnAdv);
            this.panel2.Controls.Add(this.btnRefresh);
            this.panel2.Controls.Add(this.btnGamesList);
            this.panel2.Controls.Add(this.btnReport);
            this.panel2.Controls.Add(this.ckAllChamps);
            this.panel2.Controls.Add(this.cboReportChampion);
            this.panel2.Controls.Add(this.cboReportUser);
            this.panel2.Location = new System.Drawing.Point(154, 27);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(231, 220);
            this.panel2.TabIndex = 1;
            // 
            // btnAdv
            // 
            this.btnAdv.Location = new System.Drawing.Point(7, 184);
            this.btnAdv.Name = "btnAdv";
            this.btnAdv.Size = new System.Drawing.Size(216, 23);
            this.btnAdv.TabIndex = 12;
            this.btnAdv.Text = "Advanced Stats >>";
            this.btnAdv.UseVisualStyleBackColor = true;
            this.btnAdv.Click += new System.EventHandler(this.btnAdv_Click);
            // 
            // btnRefresh
            // 
            this.btnRefresh.Location = new System.Drawing.Point(7, 126);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(105, 23);
            this.btnRefresh.TabIndex = 11;
            this.btnRefresh.Text = "Refresh Games";
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // btnGamesList
            // 
            this.btnGamesList.Location = new System.Drawing.Point(118, 126);
            this.btnGamesList.Name = "btnGamesList";
            this.btnGamesList.Size = new System.Drawing.Size(105, 23);
            this.btnGamesList.TabIndex = 9;
            this.btnGamesList.Text = "View Games List";
            this.btnGamesList.UseVisualStyleBackColor = true;
            this.btnGamesList.Click += new System.EventHandler(this.btnGamesList_Click);
            // 
            // btnReport
            // 
            this.btnReport.Location = new System.Drawing.Point(7, 155);
            this.btnReport.Name = "btnReport";
            this.btnReport.Size = new System.Drawing.Size(216, 23);
            this.btnReport.TabIndex = 3;
            this.btnReport.Text = "Generate Report";
            this.btnReport.UseVisualStyleBackColor = true;
            this.btnReport.Click += new System.EventHandler(this.btnReport_Click);
            // 
            // ckAllChamps
            // 
            this.ckAllChamps.AutoSize = true;
            this.ckAllChamps.Checked = true;
            this.ckAllChamps.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ckAllChamps.Location = new System.Drawing.Point(131, 32);
            this.ckAllChamps.Name = "ckAllChamps";
            this.ckAllChamps.Size = new System.Drawing.Size(92, 17);
            this.ckAllChamps.TabIndex = 2;
            this.ckAllChamps.Text = "All Champions";
            this.ckAllChamps.UseVisualStyleBackColor = true;
            this.ckAllChamps.CheckedChanged += new System.EventHandler(this.ckAllChamps_CheckedChanged);
            // 
            // cboReportChampion
            // 
            this.cboReportChampion.Enabled = false;
            this.cboReportChampion.FormattingEnabled = true;
            this.cboReportChampion.Location = new System.Drawing.Point(4, 32);
            this.cboReportChampion.Name = "cboReportChampion";
            this.cboReportChampion.Size = new System.Drawing.Size(121, 21);
            this.cboReportChampion.Sorted = true;
            this.cboReportChampion.TabIndex = 1;
            // 
            // cboReportUser
            // 
            this.cboReportUser.FormattingEnabled = true;
            this.cboReportUser.Location = new System.Drawing.Point(3, 4);
            this.cboReportUser.Name = "cboReportUser";
            this.cboReportUser.Size = new System.Drawing.Size(121, 21);
            this.cboReportUser.Sorted = true;
            this.cboReportUser.TabIndex = 0;
            this.cboReportUser.Text = "Select User";
            // 
            // trayIcon
            // 
            this.trayIcon.BalloonTipText = "League of Legends Stat Tracker is still running in the background!";
            this.trayIcon.BalloonTipTitle = "LoL Stat Tracker";
            this.trayIcon.Icon = ((System.Drawing.Icon)(resources.GetObject("trayIcon.Icon")));
            this.trayIcon.Text = "League of Legends Stat Tracker";
            this.trayIcon.MouseClick += new System.Windows.Forms.MouseEventHandler(this.trayIcon_MouseClick);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(397, 24);
            this.menuStrip1.TabIndex = 2;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboutToolStripMenuItem,
            this.refreshChampionTableToolStripMenuItem,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(204, 22);
            this.aboutToolStripMenuItem.Text = "About";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
            // 
            // refreshChampionTableToolStripMenuItem
            // 
            this.refreshChampionTableToolStripMenuItem.Name = "refreshChampionTableToolStripMenuItem";
            this.refreshChampionTableToolStripMenuItem.Size = new System.Drawing.Size(204, 22);
            this.refreshChampionTableToolStripMenuItem.Text = "Refresh Champion Table";
            this.refreshChampionTableToolStripMenuItem.Click += new System.EventHandler(this.refreshChampionTableToolStripMenuItem_Click);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(204, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(397, 253);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.menuStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.Text = "League of Legends Stat Tracker";
            this.Resize += new System.EventHandler(this.Form1_Resize);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox txtUser;
        private System.Windows.Forms.ComboBox cboRefresh;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ListBox lstUsers;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button btnReport;
        private System.Windows.Forms.CheckBox ckAllChamps;
        private System.Windows.Forms.ComboBox cboReportChampion;
        private System.Windows.Forms.ComboBox cboReportUser;
        private System.Windows.Forms.Button btnGamesList;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.NotifyIcon trayIcon;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem refreshChampionTableToolStripMenuItem;
        private System.Windows.Forms.Button btnAdv;


    }
}

