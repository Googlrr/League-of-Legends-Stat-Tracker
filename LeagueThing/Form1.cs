/* Copyright (c) 2014 Trevor Carmichael
 * See the file license.txt for copying permission.
 */
using System;
using System.Configuration;
using System.Collections.Specialized;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using Newtonsoft.Json;

namespace LoLStatTracker
{
    public partial class Form1 : Form
    {


        dbWrapper db = new dbWrapper();
        LeagueWrapper league = new LeagueWrapper(ConfigurationManager.AppSettings["APIKEY"]);
        System.Timers.Timer timer = new System.Timers.Timer();
        
        int refreshRate = 2;
        const int ONE_HOUR = (1000) * (60) * (60);

        public Form1()
        {
            try
            {
                InitializeComponent();

                db.createChampionTable();
                db.createSummonerTable();
                db.createGameTable();

                populateChampionList();
                populateUserLists();
                addGames();
                startTimer();
                cboRefresh.SelectedIndex = 2;
                cboReportChampion.SelectedIndex = 0;
            }
            catch (Exception e)
            {
                MessageBox.Show("" + e);
                this.Close();
            }
        }

        /// <summary>
        /// Adds the entered username into the list of 
        /// Summoners to be tracked. 
        /// </summary>
        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (txtUser.Text != "")
            {
                Summoner _s = league.getSummoner(txtUser.Text);
                db.addSummonerInfo(_s);
                lstUsers.Items.Add(_s.name);
                cboReportUser.Items.Add(_s.name);

                db.addGameInfo(league.getRecentGames(_s.id), _s);
            }
        }

        /// <summary>
        /// Adds all of the previously entered Summoners
        /// into the forms. This is called at the start
        /// of the program.
        /// </summary>
        private void populateUserLists()
        {
            foreach (Summoner _s in db.getAllSummoners())
            {
                lstUsers.Items.Add(_s.name);
                cboReportUser.Items.Add(_s.name);
            }
        }

        /// <summary>
        /// Changes the refresh rate of the program
        /// depending on the Combo Box.
        /// </summary>
        private void cboRefresh_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (cboRefresh.SelectedIndex)
            {
                case 0:
                    refreshRate = 6;
                    break;
                case 1:
                    refreshRate = 4;
                    break;
                case 2:
                    refreshRate = 2;
                    break;
            }

            timer.Interval = ONE_HOUR * refreshRate;
        }

        /// <summary>
        /// Starts the timer. This is called at
        /// the start of the program.
        /// </summary>
        private void startTimer()
        {
            timer.Interval = ONE_HOUR * refreshRate;
            timer.Elapsed += new System.Timers.ElapsedEventHandler(t_tick);
            timer.Start();
        }

        /// <summary>
        /// Runs everytime the timers interval has elapsed.
        /// </summary>
        void t_tick(object sender, EventArgs e)
        {
            addGames();
        }

        /// <summary>
        /// Adds all recent games of all summoners into
        /// the database to be tracked. This is called
        /// by t_tick or when the user clicks to
        /// refresh the games.
        /// </summary>
        public void addGames()
        {
            foreach (string s in lstUsers.Items)
            {
                Summoner _s = db.getSummonerInfo(s);
                RecentGames _r = league.getRecentGames(_s.id);
                db.addGameInfo(_r, _s);
            }
            if (trayIcon.Visible)
            {
                trayIcon.BalloonTipText = "Updating Games!";
                trayIcon.ShowBalloonTip(5000);
            }
        }

        /// <summary>
        /// Retrieves all champions from the database
        /// and adds them into the Combo Box. This is called at the
        /// start of the program.
        /// </summary>
        private void populateChampionList()
        {
            foreach (Champion c in db.getAllChampions().champions)
            {
                cboReportChampion.Items.Add(c.name);
            }
        }

        /// <summary>
        /// Displays a new form with all of the stats requested.
        /// 
        /// Stats are displayed in a DataGridView.
        /// </summary>
        private void btnReport_Click(object sender, EventArgs e)
        {
            if (cboReportUser.SelectedIndex >= 0)
            {

                //Creates a summoner object from the selected username
                Summoner _s = db.getSummonerInfo(cboReportUser.SelectedItem + "");

                //Creates a champion object from the selected champion
                Champion _c = db.getChampion(cboReportChampion.SelectedItem + "");


                List<Game> _games = db.getGames(_s, _c, ckAllChamps.Checked);

                if (_games.Count > 0)
                {

                    Game _totalStats = _games[0];

                    //Initial values for the stats to track.
                    int maxKills = _games[0].stats.championsKilled;
                    int maxDeath = _games[0].stats.numDeaths;
                    int numWins = (_games[0].stats.win ? 1 : 0);

                    for (int i = 1; i < _games.Count; i++)
                    {
                        _totalStats = _totalStats + _games[i];
                        if (_games[i].stats.championsKilled > maxKills)
                        {
                            maxKills = _games[i].stats.championsKilled;
                        }
                        if (_games[i].stats.numDeaths > maxDeath)
                        {
                            maxDeath = _games[i].stats.numDeaths;
                        }
                        if (_games[i].stats.win)
                        {
                            numWins += 1;
                        }

                    }

                    //Creates a new Form2 object. 
                    Form2 _f = new Form2();

                    //Sets the Title of the new form.
                    _f.Text = (ckAllChamps.Checked ? "All Champions Stats" : _c.name + " Stats");
                    _f.statsDisplay.ColumnCount = 2;
                    _f.statsDisplay.Columns[0].Name = "Total Stats";
                    _f.statsDisplay.Columns[1].Name = "Average Stats";

                    //Kills
                    var index = _f.statsDisplay.Rows.Add();
                    _f.statsDisplay.Rows[index].HeaderCell.Value = "Kills";
                    _f.statsDisplay.Rows[index].Cells[0].Value = _totalStats.stats.championsKilled;
                    _f.statsDisplay.Rows[index].Cells[1].Value = Math.Round(_totalStats.stats.championsKilled / (double)_games.Count, 2);

                    //Deaths
                    index = _f.statsDisplay.Rows.Add();
                    _f.statsDisplay.Rows[index].HeaderCell.Value = "Deaths";
                    _f.statsDisplay.Rows[index].Cells[0].Value = _totalStats.stats.numDeaths;
                    _f.statsDisplay.Rows[index].Cells[1].Value = Math.Round(_totalStats.stats.numDeaths / (double)_games.Count, 2);

                    //Assists
                    index = _f.statsDisplay.Rows.Add();
                    _f.statsDisplay.Rows[index].HeaderCell.Value = "Assists";
                    _f.statsDisplay.Rows[index].Cells[0].Value = _totalStats.stats.assists;
                    _f.statsDisplay.Rows[index].Cells[1].Value = Math.Round(_totalStats.stats.assists / (double)_games.Count, 2);

                    //Level
                    index = _f.statsDisplay.Rows.Add();
                    _f.statsDisplay.Rows[index].HeaderCell.Value = "Level";
                    _f.statsDisplay.Rows[index].Cells[0].Value = _totalStats.stats.level;
                    _f.statsDisplay.Rows[index].Cells[1].Value = Math.Round(_totalStats.stats.level / (double)_games.Count, 2);

                    //Number of Wins
                    index = _f.statsDisplay.Rows.Add();
                    _f.statsDisplay.Rows[index].HeaderCell.Value = "Wins";
                    _f.statsDisplay.Rows[index].Cells[0].Value = numWins;
                    _f.statsDisplay.Rows[index].Cells[1].Value = Math.Round(numWins / (double)(_games.Count), 2) + "%";

                    //Turrets Destroyed
                    index = _f.statsDisplay.Rows.Add();
                    _f.statsDisplay.Rows[index].HeaderCell.Value = "Turrets Destroyed";
                    _f.statsDisplay.Rows[index].Cells[0].Value = _totalStats.stats.turretsKilled;
                    _f.statsDisplay.Rows[index].Cells[1].Value = Math.Round(_totalStats.stats.turretsKilled / (double)_games.Count, 2);

                    //Minions Killed
                    index = _f.statsDisplay.Rows.Add();
                    _f.statsDisplay.Rows[index].HeaderCell.Value = "Minions";
                    _f.statsDisplay.Rows[index].Cells[0].Value = _totalStats.stats.minionsKilled;
                    _f.statsDisplay.Rows[index].Cells[1].Value = Math.Round(_totalStats.stats.minionsKilled / (double)_games.Count, 2);

                    //Jungle Monsters Killed
                    index = _f.statsDisplay.Rows.Add();
                    _f.statsDisplay.Rows[index].HeaderCell.Value = "Monsters";
                    _f.statsDisplay.Rows[index].Cells[0].Value = _totalStats.stats.neutralMinionsKilled;
                    _f.statsDisplay.Rows[index].Cells[1].Value = Math.Round(_totalStats.stats.neutralMinionsKilled / (double)_games.Count, 2);

                    //Double Kills
                    index = _f.statsDisplay.Rows.Add();
                    _f.statsDisplay.Rows[index].HeaderCell.Value = "Double Kills";
                    _f.statsDisplay.Rows[index].Cells[0].Value = _totalStats.stats.doubleKills;
                    _f.statsDisplay.Rows[index].Cells[1].Value = Math.Round(_totalStats.stats.doubleKills / (double)_games.Count, 2);

                    //Triple Kills
                    index = _f.statsDisplay.Rows.Add();
                    _f.statsDisplay.Rows[index].HeaderCell.Value = "Triple Kills";
                    _f.statsDisplay.Rows[index].Cells[0].Value = _totalStats.stats.tripleKills;
                    _f.statsDisplay.Rows[index].Cells[1].Value = Math.Round(_totalStats.stats.tripleKills / (double)_games.Count, 2);

                    //Quadra Kills
                    index = _f.statsDisplay.Rows.Add();
                    _f.statsDisplay.Rows[index].HeaderCell.Value = "Quadra Kills";
                    _f.statsDisplay.Rows[index].Cells[0].Value = _totalStats.stats.quadraKills;
                    _f.statsDisplay.Rows[index].Cells[1].Value = Math.Round(_totalStats.stats.quadraKills / (double)_games.Count, 2);

                    //Penta Kills
                    index = _f.statsDisplay.Rows.Add();
                    _f.statsDisplay.Rows[index].HeaderCell.Value = "Penta Kills";
                    _f.statsDisplay.Rows[index].Cells[0].Value = _totalStats.stats.pentaKills;
                    _f.statsDisplay.Rows[index].Cells[1].Value = Math.Round(_totalStats.stats.pentaKills / (double)_games.Count, 2);

                    //Physical Damage Dealt to Champions
                    index = _f.statsDisplay.Rows.Add();
                    _f.statsDisplay.Rows[index].HeaderCell.Value = "Phys. Damage Dealt to Champions";
                    _f.statsDisplay.Rows[index].Cells[0].Value = _totalStats.stats.physicalDamageDealtToChampions;
                    _f.statsDisplay.Rows[index].Cells[1].Value = Math.Round(_totalStats.stats.physicalDamageDealtToChampions / (double)_games.Count, 2);

                    //Magic Damage Dealt to Champions
                    index = _f.statsDisplay.Rows.Add();
                    _f.statsDisplay.Rows[index].HeaderCell.Value = "Magic Damage Dealt to Champions";
                    _f.statsDisplay.Rows[index].Cells[0].Value = _totalStats.stats.magicDamageDealtToChampions;
                    _f.statsDisplay.Rows[index].Cells[1].Value = Math.Round(_totalStats.stats.magicDamageDealtToChampions / (double)_games.Count, 2);

                    //True Damage Dealt to Champions
                    index = _f.statsDisplay.Rows.Add();
                    _f.statsDisplay.Rows[index].HeaderCell.Value = "True Damage Dealt to Champions";
                    _f.statsDisplay.Rows[index].Cells[0].Value = _totalStats.stats.trueDamageDealtToChampions;
                    _f.statsDisplay.Rows[index].Cells[1].Value = Math.Round(_totalStats.stats.trueDamageDealtToChampions / (double)_games.Count, 2);

                    //Wards Placed
                    index = _f.statsDisplay.Rows.Add();
                    _f.statsDisplay.Rows[index].HeaderCell.Value = "Wards Placed";
                    _f.statsDisplay.Rows[index].Cells[0].Value = _totalStats.stats.wardPlaced;
                    _f.statsDisplay.Rows[index].Cells[1].Value = Math.Round(_totalStats.stats.wardPlaced / (double)_games.Count, 2);

                    //Wards Killed
                    index = _f.statsDisplay.Rows.Add();
                    _f.statsDisplay.Rows[index].HeaderCell.Value = "Wards Killed";
                    _f.statsDisplay.Rows[index].Cells[0].Value = _totalStats.stats.assists;
                    _f.statsDisplay.Rows[index].Cells[1].Value = Math.Round(_totalStats.stats.assists / (double)_games.Count, 2);

                    //Vision Wards Bought
                    index = _f.statsDisplay.Rows.Add();
                    _f.statsDisplay.Rows[index].HeaderCell.Value = "Vision Wards Bought";
                    _f.statsDisplay.Rows[index].Cells[0].Value = _totalStats.stats.visionWardsBought;
                    _f.statsDisplay.Rows[index].Cells[1].Value = Math.Round(_totalStats.stats.visionWardsBought / (double)_games.Count, 2);

                    //Total Crowd Control Dealt
                    index = _f.statsDisplay.Rows.Add();
                    _f.statsDisplay.Rows[index].HeaderCell.Value = "Crowd Control Dealt";
                    _f.statsDisplay.Rows[index].Cells[0].Value = _totalStats.stats.totalTimeCrowdControlDealt;
                    _f.statsDisplay.Rows[index].Cells[1].Value = Math.Round(_totalStats.stats.totalTimeCrowdControlDealt / (double)_games.Count, 2);

                    //Length of the game
                    index = _f.statsDisplay.Rows.Add();
                    _f.statsDisplay.Rows[index].HeaderCell.Value = "Game Length (minutes)";
                    _f.statsDisplay.Rows[index].Cells[0].Value = Math.Round((double)_totalStats.stats.timePlayed / 60, 2);
                    _f.statsDisplay.Rows[index].Cells[1].Value = Math.Round(((double)_totalStats.stats.timePlayed / 60) / (double)_games.Count, 2);

                    //Physical Damage Taken
                    index = _f.statsDisplay.Rows.Add();
                    _f.statsDisplay.Rows[index].HeaderCell.Value = "Phys. Damage Taken";
                    _f.statsDisplay.Rows[index].Cells[0].Value = _totalStats.stats.physicalDamageTaken;
                    _f.statsDisplay.Rows[index].Cells[1].Value = Math.Round(_totalStats.stats.physicalDamageTaken / (double)_games.Count, 2);

                    //Magic Damage Taken
                    index = _f.statsDisplay.Rows.Add();
                    _f.statsDisplay.Rows[index].HeaderCell.Value = "Magic Damage Taken";
                    _f.statsDisplay.Rows[index].Cells[0].Value = _totalStats.stats.magicDamageTaken;
                    _f.statsDisplay.Rows[index].Cells[1].Value = Math.Round(_totalStats.stats.magicDamageTaken / (double)_games.Count, 2);

                    //True Damage Taken
                    index = _f.statsDisplay.Rows.Add();
                    _f.statsDisplay.Rows[index].HeaderCell.Value = "True Damage Taken";
                    _f.statsDisplay.Rows[index].Cells[0].Value = _totalStats.stats.trueDamageTaken;
                    _f.statsDisplay.Rows[index].Cells[1].Value = Math.Round(_totalStats.stats.trueDamageTaken / (double)_games.Count, 2);

                    //Displays the form after all of the information has been added.
                    _f.Show();
                }

            }
        }

        /// <summary>
        /// Enables or Disables the Champions drop down box
        /// if All Champions is checked.
        /// </summary>
        private void ckAllChamps_CheckedChanged(object sender, EventArgs e)
        {
            if (ckAllChamps.Checked)
            {
                cboReportChampion.Enabled = false;
            }
            else
            {
                cboReportChampion.Enabled = true;
            }
        }

        /// <summary>
        /// Shows a list of all games in an explorer form.
        /// Either shows all of the games or only specific champions
        /// </summary>
        private void btnGamesList_Click(object sender, EventArgs e)
        {
            gameExplorer explorer = new gameExplorer();

            Summoner _s = db.getSummonerInfo(cboReportUser.Text);
            if (ckAllChamps.Checked)
            {
                explorer.populateGamesList(_s);
            }
            else
            {
                explorer.populateGamesList(_s, db.getChampion(cboReportChampion.SelectedItem + ""));
            }
            explorer.Show();
        }

        /// <summary>
        /// Refreshes the games into the database instantly
        /// without waiting for the timer to tick.
        /// </summary>
        private void btnRefresh_Click(object sender, EventArgs e)
        {
            addGames();
            timer.Stop();
            timer.Start();
        }

        /// <summary>
        /// When the form is minimized it is minimized
        /// into the system tray. 
        /// </summary>
        private void Form1_Resize(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Minimized)
            {
                trayIcon.Visible = true;
                trayIcon.BalloonTipText = "League of Legends Stat Tracker is still running in the background! Click icon to reopen.";
                trayIcon.ShowBalloonTip(5000);
                this.ShowInTaskbar = false;
            }
        }

        /// <summary>
        /// When the trayIcon is clicked the form will reappear
        /// and the trayIcon will be removed.
        /// </summary>
        private void trayIcon_MouseClick(object sender, MouseEventArgs e)
        {
            trayIcon.Visible = false;
            this.WindowState = FormWindowState.Normal;
            this.ShowInTaskbar = true;
        }

        /// <summary>
        /// Shows the About page.
        /// </summary>
        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutBox1 about = new AboutBox1();
            about.Show();
        }

        /// <summary>
        /// Exits the program.
        /// </summary>
        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void refreshChampionTableToolStripMenuItem_Click(object sender, EventArgs e)
        {
            db.deleteChampionTable();
            db.createChampionTable();
        }

        private void btnAdv_Click(object sender, EventArgs e)
        {
            AdvancedStats adv = new AdvancedStats();
            adv.Show();
        }

        private void lstUsers_SelectedIndexChanged(object sender, EventArgs e)
        {
                string txt = "Do you want to remove " + lstUsers.SelectedItem + " from tracking?";
                string caption = "Remove User";
                MessageBoxButtons button = MessageBoxButtons.YesNoCancel;
                MessageBoxIcon icon = MessageBoxIcon.Warning;
                var result = MessageBox.Show(txt, caption, button, icon);
                switch (result)
                {
                    case DialogResult.Yes:
                        db.deleteSummoner("" + lstUsers.SelectedItem);
                        break;
                    case DialogResult.No:
                    case DialogResult.Cancel:
                        break;
                }
            }
    }
}
