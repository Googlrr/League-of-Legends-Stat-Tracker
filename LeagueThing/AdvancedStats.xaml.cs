using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace LoLStatTracker
{
    public partial class AdvancedStats : Window
    {
        string[] statTypes = { "Win Rate by Team mate", "Win Rate by Champion", "Best Champion Stats", "Show all Summoners by..." };
        LeagueWrapper league = new LeagueWrapper(ConfigurationManager.AppSettings["APIKEY"]);
        dbWrapper db = new dbWrapper();
        public AdvancedStats()
        {
            InitializeComponent();
            cboType.ItemsSource = statTypes;
            populateSummoners();
        }

        private void populateSummoners()
        {
            List<Summoner> s = db.getAllSummoners();
            foreach (Summoner _s in s)
            {
                cboSummoner.Items.Add(_s.name);
            }
        }
        private void btnDisplay_Click(object sender, RoutedEventArgs e)
        {
            int index = cboType.SelectedIndex;
            switch(index)
            {
                case 0:
                    WRbyTeam();
                    break;
                case 1:
                    WRbyChampion();
                    break;
                case 2:
                    BestChampStats();
                    break;
                case 3:
                    ShowAllBy();
                    break;
            }
        }

        public struct winloss
        {
            public int win, loss;
        }
        public struct champStats
        {
            public int kills, deaths, assists, wins, loss, totalPhysDamage, totalMagicDamage, totalDamage, minionKill, monsterKill, goldEarned;
            public int totalGames;
            public double kda;
        }
        private void BestChampStats()
        {
            Summoner _s = db.getSummonerInfo(cboSummoner.SelectedItem + "");
            Champions champs = db.getAllChampions();
            List<Game> games = db.getGames(_s, new Champion(), true);
            Dictionary<int, champStats> championStats = new Dictionary<int, champStats>();
            foreach(Game _g in games)
            {
                if(!championStats.ContainsKey(_g.championId))
                {
                    championStats.Add(_g.championId, new champStats());
                }
                var temp = championStats[_g.championId];
                temp.kills += _g.stats.championsKilled;
                temp.loss += _g.stats.numDeaths;
                temp.assists += _g.stats.assists;
                if (_g.stats.win) { temp.wins += 1; } else { temp.loss += 1; }
                temp.totalPhysDamage += _g.stats.physicalDamageDealtToChampions;
                temp.totalMagicDamage += _g.stats.magicDamageDealtToChampions;
                temp.totalDamage += _g.stats.totalDamageDealtToChampions;
                temp.minionKill += _g.stats.minionsKilled;
                temp.monsterKill += _g.stats.neutralMinionsKilled;
                temp.goldEarned += _g.stats.goldEarned;
                championStats[_g.championId] = temp;
            }

            champStats BestChampsIDs = new champStats();
            champStats BestChampStats = new champStats();
            foreach(Champion c in champs.champions)
            {
                var temp = championStats[c.id];
                if (temp.kills > BestChampStats.kills) { BestChampsIDs.kills = c.id; BestChampStats.kills = temp.kills; }
                if (temp.deaths > BestChampStats.deaths) { BestChampsIDs.deaths = c.id; BestChampStats.deaths = temp.deaths; }
                if (temp.assists > BestChampStats.assists) { BestChampsIDs.assists = c.id; BestChampStats.assists = temp.assists; }

                var kda = (!(temp.deaths == 0) ? (double)((temp.kills + temp.assists) / temp.deaths) : (double)((temp.kills + temp.assists) / 1));
                if (temp.kda > BestChampStats.kda) { BestChampsIDs.kda = c.id; BestChampStats.kda = temp.kda; }

                if (temp.wins > BestChampStats.wins) { BestChampsIDs.wins = c.id; BestChampStats.wins = temp.wins; }
                if (temp.loss > BestChampStats.loss) { BestChampsIDs.loss = c.id; BestChampStats.loss = temp.loss; }
                if (temp.assists > BestChampStats.assists) { BestChampsIDs.assists = c.id; BestChampStats.assists = temp.assists; }
                var totalGame = temp.wins + temp.loss;
                if (temp.totalGames > BestChampStats.totalGames) { BestChampsIDs.totalGames = c.id; BestChampStats.totalGames = temp.totalGames; }
                if (temp.totalPhysDamage > BestChampStats.totalPhysDamage) { BestChampsIDs.totalPhysDamage = c.id; BestChampStats.totalPhysDamage = temp.totalPhysDamage; }
                if (temp.totalMagicDamage > BestChampStats.totalMagicDamage) { BestChampsIDs.totalMagicDamage = c.id; BestChampStats.totalMagicDamage = temp.totalMagicDamage; }
                if (temp.totalDamage > BestChampStats.totalDamage) { BestChampsIDs.totalDamage = c.id; BestChampStats.totalDamage = temp.totalDamage; }

                if (temp.minionKill > BestChampStats.minionKill) { BestChampsIDs.minionKill = c.id; BestChampStats.minionKill = temp.minionKill; }
                if (temp.monsterKill > BestChampStats.monsterKill) { BestChampsIDs.monsterKill = c.id; BestChampStats.monsterKill = temp.monsterKill; }
                if (temp.goldEarned > BestChampStats.goldEarned) { BestChampsIDs.goldEarned = c.id; BestChampStats.goldEarned = temp.goldEarned; }
            }

        }
        private void ShowAllBy() { }
        private void WRbyChampion()
        {
            Summoner _s = db.getSummonerInfo(cboSummoner.SelectedItem + "");
            List<Game> games = db.getGames(_s, new Champion(), true);
            Dictionary<string, winloss> champion = new Dictionary<string, winloss>();
            foreach(Game _g in games)
            {
                string champname = db.getChampion(_g.championId).name;

                if (!champion.ContainsKey(champname))
                {
                    champion.Add(champname, new winloss());
                }

                var temp = champion[champname];

                if (_g.stats.win) { temp.win += 1; } else { temp.loss += 1; }

                champion[champname] = temp;
            }
            Form2 _form2 = new Form2();
            _form2.Text = "Win Rates by Champion";
            _form2.statsDisplay.ColumnCount = 3;
            _form2.statsDisplay.Columns[0].Name = "Win";
            _form2.statsDisplay.Columns[1].Name = "Loss";
            _form2.statsDisplay.Columns[2].Name = "Ratio";
            foreach(var d in champion)
            {
                var index = _form2.statsDisplay.Rows.Add();
                _form2.statsDisplay.Rows[index].HeaderCell.Value = d.Key;
                _form2.statsDisplay.Rows[index].Cells[0].Value = d.Value.win;
                _form2.statsDisplay.Rows[index].Cells[1].Value = d.Value.loss;
                _form2.statsDisplay.Rows[index].Cells[2].Value = (double)(d.Value.win) / (d.Value.win + d.Value.loss);
            }
            _form2.Show();
        }
        private void WRbyTeam()
        {
            Summoner _s = db.getSummonerInfo(cboSummoner.SelectedItem + "");
            List<Game> games = db.getGames(_s, new Champion(), true);
            Dictionary<int, winloss> player = new Dictionary<int, winloss>();
            List<int> IDs = new List<int>();
            foreach (Game _g in games)
            {
                if (_g.fellowPlayers != null)
                {
                    foreach (FellowPlayer _f in _g.fellowPlayers)
                    {
                        if (_f.teamId == _g.stats.team)
                        {
                            winloss _t;
                            if (player.ContainsKey(_f.summonerId))
                            {
                                _t = player[_f.summonerId];
                            }
                            else
                            {
                                _t = new winloss();
                            }
                            if (_g.stats.win) { _t.win += 1; }
                            else { _t.loss += 1; }

                            player[_f.summonerId] = _t;

                            if (!IDs.Contains(_f.summonerId))
                            {
                                IDs.Add(_f.summonerId);                            
                            }
                        }
                    }
                }
            }

            var summoners = league.getSummonerNameList(IDs);
            Form2 _form2 = new Form2();
            _form2.Text = "Win Rates by Team Mate";
            _form2.statsDisplay.ColumnCount = 3;
            _form2.statsDisplay.Columns[0].Name = "Win";
            _form2.statsDisplay.Columns[1].Name = "Loss";
            _form2.statsDisplay.Columns[2].Name = "Ratio";
            int i = 0;
            foreach (var __s in summoners)
            {
                var index = _form2.statsDisplay.Rows.Add();
                _form2.statsDisplay.Rows[index].HeaderCell.Value = __s.Value;
                _form2.statsDisplay.Rows[index].Cells[0].Value = player[int.Parse(__s.Key)].win;
                _form2.statsDisplay.Rows[index].Cells[1].Value = player[int.Parse(__s.Key)].loss;
                _form2.statsDisplay.Rows[index].Cells[2].Value = (double)(player[int.Parse(__s.Key)].win)/(player[int.Parse(__s.Key)].win+player[int.Parse(__s.Key)].loss);
                i++;
            }
            _form2.Show();
        }
    }
}
