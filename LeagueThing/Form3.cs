/* Copyright (c) 2014 Trevor Carmichael
 * See the file license.txt for copying permission.
 */
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LoLStatTracker
{
    public partial class gameExplorer : Form
    {
        dbWrapper db = new dbWrapper();
        LeagueWrapper league = new LeagueWrapper("1757fa2d-433f-49f2-920c-5c8d7790d0f1");
        Summoner s;

        public gameExplorer()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Adds some info to the games list. Only enough
        /// info is added to distinguish a particular game.
        /// </summary>
        /// <param name="_s">Summoner object to get games of.</param>
        public void populateGamesList(Summoner _s)
        {
            List<Game> games = db.getGames(_s, new Champion(), true);
            s = _s;
            gamesList.ColumnCount = 6;
            gamesList.Columns[0].Name = "Champion";
            gamesList.Columns[1].Name = "Win/Loss";
            gamesList.Columns[2].Name = "Kills";
            gamesList.Columns[3].Name = "Deaths";
            gamesList.Columns[4].Name = "Assists";
            gamesList.Columns[5].Name = "gameID";

            foreach (Game _g in games)
            {
                int index = gamesList.Rows.Add();
                gamesList.Rows[index].Cells[0].Value = db.getChampion(_g.championId).name;
                gamesList.Rows[index].Cells[1].Value = (_g.stats.win ? "Win" : "Loss");
                gamesList.Rows[index].Cells[2].Value = _g.stats.championsKilled;
                gamesList.Rows[index].Cells[3].Value = _g.stats.numDeaths;
                gamesList.Rows[index].Cells[4].Value = _g.stats.assists;
                gamesList.Rows[index].Cells[5].Value = _g.gameId;
            }

        }

        /// <summary>
        /// Adds some info to the games list. Only enough
        /// info is added to distinguish a particular game. 
        /// This overload displays only games of a particular
        /// champion.
        /// </summary>
        /// <param name="_s">Summoner object to get games of.</param>
        /// <param name="_c">Champion object to get games of.</param>
        public void populateGamesList(Summoner _s, Champion _c)
        {
            List<Game> games = db.getGames(_s, _c);
            s = _s;
            gamesList.ColumnCount = 6;
            gamesList.Columns[0].Name = "Champion";
            gamesList.Columns[1].Name = "Win/Loss";
            gamesList.Columns[2].Name = "Kills";
            gamesList.Columns[3].Name = "Deaths";
            gamesList.Columns[4].Name = "Assists";
            gamesList.Columns[5].Name = "gameID";

            foreach (Game _g in games)
            {
                int index = gamesList.Rows.Add();
                gamesList.Rows[index].Cells[0].Value = db.getChampion(_g.championId).name;
                gamesList.Rows[index].Cells[1].Value = (_g.stats.win ? "Win" : "Loss");
                gamesList.Rows[index].Cells[2].Value = _g.stats.championsKilled;
                gamesList.Rows[index].Cells[3].Value = _g.stats.numDeaths;
                gamesList.Rows[index].Cells[4].Value = _g.stats.assists;
                gamesList.Rows[index].Cells[5].Value = _g.gameId;
            }
        }

        /// <summary>
        /// Gets a more detailed stats view of a specific game.
        /// </summary>
        private void btnGetGame_Click(object sender, EventArgs e)
        {

            int i = gamesList.CurrentCell.RowIndex;
            int gameID = int.Parse(gamesList.Rows[i].Cells[5].Value + "");
            
            Game game = db.getGameInfo(gameID, s);
            Champion _c = db.getChampion(game.championId);

            //Creates new Form2 object.
            Form2 _f = new Form2();
            _f.Text = _c.name;
            _f.statsDisplay.ColumnCount = 1;
            _f.statsDisplay.Columns[0].Name = "Stats";

            //Kills
            var index = _f.statsDisplay.Rows.Add();
            _f.statsDisplay.Rows[index].HeaderCell.Value = "Kills";
            _f.statsDisplay.Rows[index].Cells[0].Value = game.stats.championsKilled;

            //Deaths
            index = _f.statsDisplay.Rows.Add();
            _f.statsDisplay.Rows[index].HeaderCell.Value = "Deaths";
            _f.statsDisplay.Rows[index].Cells[0].Value = game.stats.numDeaths;

            //Assists
            index = _f.statsDisplay.Rows.Add();
            _f.statsDisplay.Rows[index].HeaderCell.Value = "Assists";
            _f.statsDisplay.Rows[index].Cells[0].Value = game.stats.assists;

            //Level
            index = _f.statsDisplay.Rows.Add();
            _f.statsDisplay.Rows[index].HeaderCell.Value = "Level";
            _f.statsDisplay.Rows[index].Cells[0].Value = game.stats.level;

            //Win
            index = _f.statsDisplay.Rows.Add();
            _f.statsDisplay.Rows[index].HeaderCell.Value = "Win";
            _f.statsDisplay.Rows[index].Cells[0].Value = (game.stats.win ? "Win" : "Loss");

            //Turrets Destroyed
            index = _f.statsDisplay.Rows.Add();
            _f.statsDisplay.Rows[index].HeaderCell.Value = "Turrets Destroyed";
            _f.statsDisplay.Rows[index].Cells[0].Value = game.stats.turretsKilled;

            //Minions Killed
            index = _f.statsDisplay.Rows.Add();
            _f.statsDisplay.Rows[index].HeaderCell.Value = "Minions";
            _f.statsDisplay.Rows[index].Cells[0].Value = game.stats.minionsKilled;

            //Jungle Monsters Killed
            index = _f.statsDisplay.Rows.Add();
            _f.statsDisplay.Rows[index].HeaderCell.Value = "Monsters";
            _f.statsDisplay.Rows[index].Cells[0].Value = game.stats.neutralMinionsKilled;

            //Double Kills
            index = _f.statsDisplay.Rows.Add();
            _f.statsDisplay.Rows[index].HeaderCell.Value = "Double Kills";
            _f.statsDisplay.Rows[index].Cells[0].Value = game.stats.doubleKills;

            //Triple Kills
            index = _f.statsDisplay.Rows.Add();
            _f.statsDisplay.Rows[index].HeaderCell.Value = "Triple Kills";
            _f.statsDisplay.Rows[index].Cells[0].Value = game.stats.tripleKills;

            //Quadra Kills
            index = _f.statsDisplay.Rows.Add();
            _f.statsDisplay.Rows[index].HeaderCell.Value = "Quadra Kills";
            _f.statsDisplay.Rows[index].Cells[0].Value = game.stats.quadraKills;

            //Penta Kills
            index = _f.statsDisplay.Rows.Add();
            _f.statsDisplay.Rows[index].HeaderCell.Value = "Penta Kills";
            _f.statsDisplay.Rows[index].Cells[0].Value = game.stats.pentaKills;

            //Physical Damage Dealt to Champions
            index = _f.statsDisplay.Rows.Add();
            _f.statsDisplay.Rows[index].HeaderCell.Value = "Phys. Damage Dealt to Champions";
            _f.statsDisplay.Rows[index].Cells[0].Value = game.stats.physicalDamageDealtToChampions;

            //Magic Damage Dealt to Champions
            index = _f.statsDisplay.Rows.Add();
            _f.statsDisplay.Rows[index].HeaderCell.Value = "Magic Damage Dealt to Champions";
            _f.statsDisplay.Rows[index].Cells[0].Value = game.stats.magicDamageDealtToChampions;

            //True Damage Dealt to Champions
            index = _f.statsDisplay.Rows.Add();
            _f.statsDisplay.Rows[index].HeaderCell.Value = "True Damage Dealt to Champions";
            _f.statsDisplay.Rows[index].Cells[0].Value = game.stats.trueDamageDealtToChampions;

            //Wards Placed
            index = _f.statsDisplay.Rows.Add();
            _f.statsDisplay.Rows[index].HeaderCell.Value = "Wards Placed";
            _f.statsDisplay.Rows[index].Cells[0].Value = game.stats.wardPlaced;

            //Wards Killed
            index = _f.statsDisplay.Rows.Add();
            _f.statsDisplay.Rows[index].HeaderCell.Value = "Wards Killed";
            _f.statsDisplay.Rows[index].Cells[0].Value = game.stats.assists;

            //Vision Wards Bought
            index = _f.statsDisplay.Rows.Add();
            _f.statsDisplay.Rows[index].HeaderCell.Value = "Vision Wards Bought";
            _f.statsDisplay.Rows[index].Cells[0].Value = game.stats.visionWardsBought;

            //Total Crowd Control Dealt
            index = _f.statsDisplay.Rows.Add();
            _f.statsDisplay.Rows[index].HeaderCell.Value = "Crowd Control Dealt";
            _f.statsDisplay.Rows[index].Cells[0].Value = game.stats.totalTimeCrowdControlDealt;

            //Game Length
            index = _f.statsDisplay.Rows.Add();
            _f.statsDisplay.Rows[index].HeaderCell.Value = "Game Length (minutes)";
            _f.statsDisplay.Rows[index].Cells[0].Value = Math.Round((double)game.stats.timePlayed / 60, 2);

            //Physical Damage Taken
            index = _f.statsDisplay.Rows.Add();
            _f.statsDisplay.Rows[index].HeaderCell.Value = "Phys. Damage Taken";
            _f.statsDisplay.Rows[index].Cells[0].Value = game.stats.physicalDamageTaken;

            //Magical Damage Taken
            index = _f.statsDisplay.Rows.Add();
            _f.statsDisplay.Rows[index].HeaderCell.Value = "Magic Damage Taken";
            _f.statsDisplay.Rows[index].Cells[0].Value = game.stats.magicDamageTaken;

            //True Damage Taken
            index = _f.statsDisplay.Rows.Add();
            _f.statsDisplay.Rows[index].HeaderCell.Value = "True Damage Taken";
            _f.statsDisplay.Rows[index].Cells[0].Value = game.stats.trueDamageTaken;

            //Team ID
            index = _f.statsDisplay.Rows.Add();
            _f.statsDisplay.Rows[index].HeaderCell.Value = "Team ID";
            _f.statsDisplay.Rows[index].Cells[0].Value = game.teamId;

            //Displays the new form
            _f.Show();

        }

    }
} //Namespace LoLStatTracker

