/* Copyright (c) 2014 Trevor Carmichael
 * See the file license.txt for copying permission.
 */
using System;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using Newtonsoft.Json;
namespace LoLStatTracker
{
    class dbWrapper
    {      
        LeagueWrapper league = new LeagueWrapper(ConfigurationManager.AppSettings["APIKEY"]);
        string dbString = "Data Source = LeagueStats.sqlite;Version=3";

        /// <summary>
        /// Deletes the Champion table. This is required before creating the champion table
        /// to account for the addition of new champions. 
        /// </summary>
        public void deleteChampionTable()
        {
            using(SQLiteConnection con = new SQLiteConnection(dbString))
            {
                con.Open();
                try
                {
                    using (SQLiteCommand command = new SQLiteCommand("DROP TABLE Champions", con))
                    {
                        command.ExecuteNonQuery();
                    }
                }
                catch
                {}
            }
        }
        public void deleteAllTable()
        {
            using (SQLiteConnection con = new SQLiteConnection(dbString))
            {
                con.Open();
                try
                {
                    using (SQLiteCommand command = new SQLiteCommand("DROP TABLE Champions", con))
                    {
                        command.ExecuteNonQuery();
                    } 
                    using (SQLiteCommand command = new SQLiteCommand("DROP TABLE Games", con))
                    {
                        command.ExecuteNonQuery();
                    } 
                    using (SQLiteCommand command = new SQLiteCommand("DROP TABLE Summoner", con))
                    {
                        command.ExecuteNonQuery();
                    }
                }
                catch
                { }
            }
        }

        /// <summary>
        /// Creates the champion table by calling the
        /// League of legends API and adding them to
        /// the list using the AddChampion() function.
        /// </summary>
        public void createChampionTable()
        {
            using (SQLiteConnection con = new SQLiteConnection(dbString))
            {
                con.Open();
                try
                {
                    using (SQLiteCommand command = new SQLiteCommand("CREATE TABLE Champions(ID INT, Name varchar(255), PRIMARY KEY(ID))", con))
                    {
                        command.ExecuteNonQuery();
                        
                        Champions c = league.getChampionList();
                        foreach (Champion champs in c.champions)
                        {
                            AddChampion(champs);
                        }
                        
                    }
                }
                catch
                {
                }
            }
        }

        /// <summary>
        /// Adds the champion into the newly created
        /// Champion table. This function is only ever
        /// called by createChampionTable.
        /// </summary>
        /// <param name="c">Champion</param>
        private void AddChampion(Champion c)
        {
            using (SQLiteConnection con = new SQLiteConnection(dbString))
            {
                con.Open();
                try
                {
                    using (SQLiteCommand command = new SQLiteCommand("INSERT INTO Champions VALUES(@ID, @Name)", con))
                    {
                        command.Parameters.Add(new SQLiteParameter("ID", c.id));
                        command.Parameters.Add(new SQLiteParameter("Name", c.name));
                        command.ExecuteNonQuery();
                    }
                }
                catch
                {}
            }
        }

        /// <summary>
        /// Retrieves Champion data from database.
        /// Only returns ID and Name because those
        /// are the only 2 meaningful pieces of data.
        /// </summary>
        /// <param name="ID">ID of Champion</param>
        /// <returns>Champion object specified by ID</returns>
        public Champion getChampion(int ID)
        {
            using (SQLiteConnection con = new SQLiteConnection(dbString))
            {
                Champion c = new Champion(0, "");
                con.Open();
                try
                {
                    using (SQLiteCommand command = new SQLiteCommand("SELECT * FROM Champions WHERE ID = @ID", con))
                    {
                        command.Parameters.Add(new SQLiteParameter("ID", ID));
                        SQLiteDataReader reader = command.ExecuteReader();
                        while(reader.Read())
                        {
                            c.id = reader.GetInt32(0);
                            c.name = reader.GetString(1);
                        }
                    }
                    return c;
                }
                catch
                { 
                    c.name = "error";
                    return c;
                }
            }
        }

        /// <summary>
        /// Retrieves Champion data from database.
        /// Only returns ID and Name because those
        /// are the only 2 meaningful pieces of data.
        /// </summary>
        /// <param name="name">Name of Champion</param>
        /// <returns>Champion object specified by Name</returns>
        public Champion getChampion(string name)
        {
            Champion c = new Champion();
            using (SQLiteConnection con = new SQLiteConnection(dbString))
            {
                con.Open();
                try
                {
                    using (SQLiteCommand command = new SQLiteCommand("SELECT * FROM Champions WHERE Name = @Name", con))
                    {
                        command.Parameters.Add(new SQLiteParameter("Name", name));
                        SQLiteDataReader reader = command.ExecuteReader();
                        while (reader.Read())
                        {
                            c.id = reader.GetInt32(0);
                            c.name = reader.GetString(1);
                        }
                    }
                    return c;
                }
                catch
                {
                    return new Champion(0, "error");
                }
            }
        }

        /// <summary>
        /// Gets all of the Champions from the database.
        /// </summary>
        /// <returns>Champions object holding a list of Champion objects.</returns>
        public Champions getAllChampions()
        {
            List<Champion> c = new List<Champion>();
            using (SQLiteConnection con = new SQLiteConnection(dbString))
            {
                con.Open();
                try
                {
                    using (SQLiteCommand command = new SQLiteCommand("SELECT * FROM Champions", con))
                    {
                        SQLiteDataReader reader = command.ExecuteReader();
                        while (reader.Read())
                        {
                            Champion champ = new Champion();
                            champ.id = reader.GetInt32(0);
                            champ.name = reader.GetString(1);
                            c.Add(champ);
                        }
                    }
                    Champions champs = new Champions();
                    champs.champions = c;
                    return champs;
                }
                catch
                {
                    return league.getChampionList();
                }
            }
        }

        /// <summary>
        /// Creates the table to store Summoner data.
        /// </summary>
        public void createSummonerTable()
        {
            using (SQLiteConnection con = new SQLiteConnection(dbString))
            {
                con.Open();
                try
                {
                    using (SQLiteCommand command = new SQLiteCommand("CREATE TABLE Summoner(ID INT, username varchar(255), PRIMARY KEY(ID))", con))
                    {
                        command.ExecuteNonQuery();
                    }
                }
                catch
                {}
            }
        }

        /// <summary>
        /// Adds a summoners data to the database. This way we can save on
        /// API calls by storing unchanging data locally.
        /// </summary>
        /// <param name="s">Summoner object to be saved.</param>
        public void addSummonerInfo(Summoner s)
        {
            using (SQLiteConnection con = new SQLiteConnection(dbString))
            {
                con.Open();
                try
                {
                    using (SQLiteCommand command = new SQLiteCommand("INSERT INTO Summoner VALUES(@ID, @username)", con))
                    {
                        command.Parameters.Add(new SQLiteParameter("ID", s.id));
                        command.Parameters.Add(new SQLiteParameter("username", s.name));
                        command.ExecuteNonQuery();
                    }
                }
                catch
                {}
            }
        }

        /// <summary>
        /// Gets a summoners data specified by their ID.
        /// If data isn't found, access API and add to DB for
        /// future use.
        /// </summary>
        /// <param name="ID">ID of summoner</param>
        /// <returns>Summoner object containing ID and Name</returns>
        public Summoner getSummonerInfo(int ID)
        {
            Summoner s = new Summoner();
            using (SQLiteConnection con = new SQLiteConnection(dbString))
            {
                con.Open();
                try
                {
                    using (SQLiteCommand command = new SQLiteCommand("SELECT * FROM Summoner WHERE ID = @ID", con))
                    {
                        command.Parameters.Add(new SQLiteParameter("ID", ID));
                        SQLiteDataReader reader = command.ExecuteReader();
                        while (reader.Read())
                        {
                            s.id = reader.GetInt32(0);
                            s.name = reader.GetString(1);
                        }
                        if (s.name == null)
                        {
                            s = league.getSummoner(ID);
                            return s;
                        }
                    }
                    return s;
                }
                catch
                {
                    s.name = "error";
                    return s;
                }
            }
        }

        /// <summary>
        /// Gets a summoners data specified by their username.
        /// If data isn't found, access API and add to DB for
        /// future use.
        /// </summary>
        /// <param name="name">Name of summoner</param>
        /// <returns>Summoner object containing ID and Name</returns>
        public Summoner getSummonerInfo(String name)
        {
            Summoner s = new Summoner();
            using (SQLiteConnection con = new SQLiteConnection(dbString))
            {
                con.Open();
                try
                {
                    using (SQLiteCommand command = new SQLiteCommand("SELECT * FROM Summoner WHERE username=@username", con))
                    {
                        command.Parameters.Add(new SQLiteParameter("username", name));
                        SQLiteDataReader reader = command.ExecuteReader();
                        while (reader.Read())
                        {
                            s.id = reader.GetInt32(0);
                            s.name = reader.GetString(1);
                        }
                        if (s.name == null)
                        {
                            s = league.getSummoner(name);
                            return s;
                        }
                    }
                    return s;
                }
                catch
                {
                    s.name = "error";
                    return s;
                }
            }
        }

        /// <summary>
        /// Gets all of the Summoners stored in the database.
        /// </summary>
        /// <returns>List of Summoner objects.</returns>
        public List<Summoner> getAllSummoners()
        {
            List<Summoner> s = new List<Summoner>();
            using (SQLiteConnection con = new SQLiteConnection(dbString))
            {
                con.Open();
                try
                {
                    using (SQLiteCommand command = new SQLiteCommand("SELECT * FROM Summoner", con))
                    {
                        SQLiteDataReader reader = command.ExecuteReader();
                        while (reader.Read())
                        {
                            Summoner _s = new Summoner();
                            _s.id = reader.GetInt32(0);
                            _s.name = reader.GetString(1);
                            s.Add(_s);
                        }
                    }
                    return s;
                }
                catch
                {
                    return s;
                }
            }
        }

        /// <summary>
        /// Creates the Games table.
        /// </summary>
        public void createGameTable()
        {
            using (SQLiteConnection con = new SQLiteConnection(dbString))
            {
                con.Open();
                try
                {
                    using (SQLiteCommand command = new SQLiteCommand("CREATE TABLE Games (ID INT, gameType varchar(30), spell1 INT, spell2 INT, gameMode varchar(30), mapID INT, level INT, subType varchar(30), championID INT, stats varchar(2000), fellowPlayers varchar(1000), summonerID INT, uniqueKey varchar(45), PRIMARY KEY(uniqueKey))", con))
                    {
                        command.ExecuteNonQuery();
                    }
                }
                catch
                { }
            }
        }

        /// <summary>
        /// Adds all of the games from a summoners RecentGames to the database.
        /// 
        /// </summary>
        /// <param name="_r"></param>
        /// <param name="s"></param>
        public void addGameInfo(RecentGames _r, Summoner s)
        {
            using (SQLiteConnection con = new SQLiteConnection(dbString))
            {
                con.Open();
                try
                {
                    foreach (Game g in _r.games)
                    {
                        using (SQLiteCommand command = new SQLiteCommand("INSERT INTO Games VALUES(@ID, @gameType, @spell1, @spell2, @gameMode, @mapID, @level, @subType, @championID, @stats, @fellowPlayers, @summonerID, @uniqueKey)", con))
                        {
                            command.Parameters.Add(new SQLiteParameter("ID", g.gameId));
                            command.Parameters.Add(new SQLiteParameter("gameType", g.gameType));
                            command.Parameters.Add(new SQLiteParameter("spell1", g.spell1));
                            command.Parameters.Add(new SQLiteParameter("spell2", g.spell2));
                            command.Parameters.Add(new SQLiteParameter("gameMode", g.gameMode));
                            command.Parameters.Add(new SQLiteParameter("mapID", g.mapId));
                            command.Parameters.Add(new SQLiteParameter("level", g.level));
                            command.Parameters.Add(new SQLiteParameter("subType", g.subType));
                            command.Parameters.Add(new SQLiteParameter("championID", g.championId));
                            command.Parameters.Add(new SQLiteParameter("stats", g.statsToJSON()));
                            try
                            {
                                command.Parameters.Add(new SQLiteParameter("fellowPlayers", g.fellowPlayersToJSON()));
                            }
                            catch { }
                            command.Parameters.Add(new SQLiteParameter("summonerID", s.id));
                            command.Parameters.Add(new SQLiteParameter("uniqueKey", g.gameId + " " + s.id));
                            command.ExecuteNonQuery();
                        }
                    }
                }
                catch
                { }
            }
        }

        /// <summary>
        /// Gets a specific game from the database.
        /// 
        /// Requires both gameID and a summoner object
        /// in case two different summoners were a part
        /// of the same game.
        /// </summary>
        /// <param name="gameID">ID of the game</param>
        /// <param name="_s">Summoner object of the summoner whos stats you want</param>
        /// <returns>Game object requested</returns>
        public Game getGameInfo(int gameID, Summoner _s)
        {
            Game _game = new Game();
            using (SQLiteConnection con = new SQLiteConnection(dbString))
            {
                con.Open();
                try
                {
                    using (SQLiteCommand command = new SQLiteCommand("SELECT * FROM Games WHERE ID = @ID AND summonerID = @summonerID", con))
                    {

                        command.Parameters.Add(new SQLiteParameter("ID", gameID));
                        command.Parameters.Add(new SQLiteParameter("summonerID", _s.id));

                        SQLiteDataReader reader = command.ExecuteReader();
                        while (reader.Read())
                        {
                            _game.gameId = reader.GetInt32(0);
                            _game.gameType = reader.GetString(1);
                            _game.spell1 = reader.GetInt32(2);
                            _game.spell2 = reader.GetInt32(3);
                            _game.gameMode = reader.GetString(4);
                            _game.mapId = reader.GetInt32(5);
                            _game.level = reader.GetInt32(6);
                            _game.subType = reader.GetString(7);
                            _game.championId = reader.GetInt32(8);
                            _game.stats = JsonConvert.DeserializeObject<Stats>(reader.GetString(9));
                            _game.fellowPlayers = JsonConvert.DeserializeObject<List<FellowPlayer>>(reader.GetString(10));
                            return _game;
                        }
                    }
                    return _game;
                }
                catch
                {
                    return _game;
                }
            }
        }

        /// <summary>
        /// Returns a list of games for a particular summoner. Can either return all
        /// of the games they have been a part of or only games where
        /// they used a specific champion.
        /// </summary>
        /// <param name="s">Summoner oject of desired summoner</param>
        /// <param name="c">Champion to retrieve stats of</param>
        /// <param name="allChamps">Decides whether all champions are returned or not</param>
        /// <returns>List of Game objects</returns>
        public List<Game> getGames(Summoner s, Champion c, bool allChamps = false)
        {
            List<Game> games = new List<Game>();
            using (SQLiteConnection con = new SQLiteConnection(dbString))
            {
                con.Open();
                try
                {

                    string query =  ( allChamps ? "SELECT * FROM Games WHERE summonerID=@summonerID ORDER BY ID DESC" : "SELECT * FROM Games WHERE ChampionID=@ChampionID AND summonerID=@summonerID ORDER BY ID DESC");
                    using (SQLiteCommand command = new SQLiteCommand(query, con))
                    {

                        command.Parameters.Add(new SQLiteParameter("ChampionID", c.id));
                        command.Parameters.Add(new SQLiteParameter("summonerID", s.id));

                        SQLiteDataReader reader = command.ExecuteReader();
                        while (reader.Read())
                        {
                            Game g = new Game();
                            g.gameId = reader.GetInt32(0);
                            g.gameType = reader.GetString(1);
                            g.spell1 = reader.GetInt32(2);
                            g.spell2 = reader.GetInt32(3);
                            g.gameMode = reader.GetString(4);
                            g.mapId = reader.GetInt32(5);
                            g.level = reader.GetInt32(6);
                            g.subType = reader.GetString(7);
                            g.championId = reader.GetInt32(8);
                            g.stats = JsonConvert.DeserializeObject<Stats>(reader.GetString(9));
                            try { g.fellowPlayers = JsonConvert.DeserializeObject<List<FellowPlayer>>(reader.GetString(10)); } catch { }
                            games.Add(g);
                        }
                    }
                    return games;
                }
                catch{ return games; }
            }
        }

        public void deleteSummoner(string name)
        {
            using (SQLiteConnection con = new SQLiteConnection(dbString))
            {
                con.Open();
                try
                {
                    using(SQLiteCommand command = new SQLiteCommand("DELETE FROM Summoner WHERE username=@username", con))
                    {
                        command.Parameters.Add(new SQLiteParameter("username", name));
                        command.ExecuteNonQuery();
                    }
                }
                catch {}
            }
        }
    }
}
