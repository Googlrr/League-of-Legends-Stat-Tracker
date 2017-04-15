using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
namespace LoLStatTracker
{
    /// <summary>
    /// A RecentGames object is typically a players last 10 games.
    /// 
    /// Class generated from http://json2csharp.com/
    /// </summary>
    public class RecentGames
    {
        public List<Game> games { get; set; }
        public int summonerId { get; set; }

        /// <summary>
        /// Reorders the list of games to put more recent games first.
        /// </summary>
        public void reorder()
        {
            games.Sort((y, x) => x.gameId.CompareTo(y.gameId));
        }
    }

    /// <summary>
    /// Holds info of other players per game. Each Game object
    /// usually holds 6-9 FellowPlayer Objects.
    /// 
    /// Class generated from http://json2csharp.com/
    /// </summary>
    public class FellowPlayer
    {
        public int championId { get; set; }
        public int teamId { get; set; }
        public int summonerId { get; set; }
    }

    /// <summary>
    /// Holds all statistics about a particular game. Each
    /// Game object holds one Stats object
    /// 
    /// Class generated from http://json2csharp.com/
    /// </summary>
    public class Stats
    {
        public int totalDamageDealtToChampions { get; set; }
        public int goldEarned { get; set; }
        public int item2 { get; set; }
        public int item1 { get; set; }
        public int wardPlaced { get; set; }
        public int totalDamageTaken { get; set; }
        public int item0 { get; set; }
        public int trueDamageDealtPlayer { get; set; }
        public int physicalDamageDealtPlayer { get; set; }
        public int trueDamageDealtToChampions { get; set; }
        public int visionWardsBought { get; set; }
        public int killingSprees { get; set; }
        public int totalUnitsHealed { get; set; }
        public int level { get; set; }
        public int doubleKills { get; set; }
        public int magicDamageDealtToChampions { get; set; }
        public int magicDamageDealtPlayer { get; set; }
        public int assists { get; set; }
        public int magicDamageTaken { get; set; }
        public int numDeaths { get; set; }
        public int totalTimeCrowdControlDealt { get; set; }
        public int largestMultiKill { get; set; }
        public int physicalDamageTaken { get; set; }
        public bool win { get; set; }
        public int team { get; set; }
        public int totalDamageDealt { get; set; }
        public int largestKillingSpree { get; set; }
        public int totalHeal { get; set; }
        public int item4 { get; set; }
        public int item6 { get; set; }
        public int minionsKilled { get; set; }
        public int timePlayed { get; set; }
        public int physicalDamageDealtToChampions { get; set; }
        public int championsKilled { get; set; }
        public int trueDamageTaken { get; set; }
        public int goldSpent { get; set; }
        public int tripleKills { get; set; }
        public int neutralMinionsKilledYourJungle { get; set; }
        public int neutralMinionsKilledEnemyJungle { get; set; }
        public int item3 { get; set; }
        public int item5 { get; set; }
        public int neutralMinionsKilled { get; set; }
        public int barracksKilled { get; set; }
        public int turretsKilled { get; set; }
        public int wardKilled { get; set; }
        public int quadraKills { get; set; }
        public int pentaKills { get; set; }
        public int sightWardsBought { get; set; }

        /// <summary>
        /// Allows easy addition of stats objects. Some stats
        /// such as win cannot be added and are excluded from this.
        /// </summary>
        /// <param name="_a">First Stats object</param>
        /// <param name="_b">Second Stats object</param>
        /// <returns>Collective Stats object</returns>
        public static Stats operator +(Stats _a, Stats _b)
        {
            Stats _new = _a;

            _new.magicDamageDealtPlayer += _b.magicDamageDealtPlayer;
            _new.assists += _b.assists;
            _new.magicDamageTaken += _b.magicDamageTaken;
            _new.numDeaths += _b.numDeaths;
            _new.totalTimeCrowdControlDealt += _b.totalTimeCrowdControlDealt;
            _new.largestMultiKill += _b.largestMultiKill;
            _new.physicalDamageTaken += _b.physicalDamageTaken;
            _new.totalDamageDealt += _b.totalDamageDealt;
            _new.largestKillingSpree += _b.largestKillingSpree;
            _new.totalHeal += _b.totalHeal;
            _new.minionsKilled += _b.minionsKilled;
            _new.timePlayed += _b.timePlayed;
            _new.physicalDamageDealtToChampions += _b.physicalDamageDealtToChampions;
            _new.championsKilled += _b.championsKilled;
            _new.trueDamageTaken += _b.trueDamageTaken;
            _new.goldSpent += _b.goldSpent;
            _new.neutralMinionsKilledEnemyJungle += _b.neutralMinionsKilledEnemyJungle;
            _new.neutralMinionsKilledYourJungle += _b.neutralMinionsKilledYourJungle;
            _new.neutralMinionsKilled += _b.neutralMinionsKilled;
            _new.barracksKilled += _b.barracksKilled;
            _new.turretsKilled += _b.turretsKilled;
            _new.wardKilled += _b.wardKilled;
            _new.sightWardsBought += _b.sightWardsBought;
            _new.trueDamageDealtPlayer += _b.trueDamageDealtPlayer;
            _new.physicalDamageDealtPlayer += _b.physicalDamageDealtPlayer;
            _new.trueDamageDealtToChampions += _b.trueDamageDealtToChampions;
            _new.visionWardsBought += _b.visionWardsBought;
            _new.killingSprees += _b.killingSprees;
            _new.totalUnitsHealed += _b.totalUnitsHealed;
            _new.level += _b.level;
            _new.doubleKills += _b.doubleKills;
            _new.tripleKills += _b.tripleKills;
            _new.quadraKills += _b.quadraKills;
            _new.pentaKills += _b.pentaKills;
            _new.magicDamageDealtToChampions += _b.magicDamageDealtToChampions;
            _new.totalDamageDealtToChampions += _b.totalDamageDealtToChampions;
            _new.goldEarned += _b.goldEarned;
            _new.wardPlaced += _b.wardPlaced;
            _new.totalDamageTaken += _b.totalDamageTaken;

            return _new;

        }
    }

    /// <summary>
    /// Game object holding all game information.
    /// 
    /// Class generated from http://json2csharp.com/
    /// </summary>
    public class Game
    {
        public string gameType { get; set; }
        public List<FellowPlayer> fellowPlayers { get; set; }
        public Stats stats { get; set; }
        public int gameId { get; set; }
        public string createDateAsDate { get; set; }
        public int spell1 { get; set; }
        public int teamId { get; set; }
        public int spell2 { get; set; }
        public string gameMode { get; set; }
        public int mapId { get; set; }
        public int level { get; set; }
        public bool invalid { get; set; }
        public string subType { get; set; }
        public int championId { get; set; }
        public object createDate { get; set; }

        /// <summary>
        /// Converts the stats object back into JSON. This
        /// makes for easier saving into the database without having
        /// an extra 30 fields.
        /// </summary>
        /// <returns></returns>
        public String statsToJSON()
        {
            return JsonConvert.SerializeObject(stats);
        }

        public String fellowPlayersToJSON()
        {
            return JsonConvert.SerializeObject(fellowPlayers);
        }
        /// <summary>
        /// Allows easy addition of Games object. Only adds the most relevant
        /// attributes. Other attributes are irrelevant to the stats tracking
        /// </summary>
        /// <param name="_a">First Game Object</param>
        /// <param name="_b">Second Game Object</param>
        /// <returns>Collective Game Object</returns>
        public static Game operator +(Game _a, Game _b)
        {
            Game _new = _a;
            _new.stats += _b.stats;
            _new.level += _b.level;
            return _new;
        }
    }
}
