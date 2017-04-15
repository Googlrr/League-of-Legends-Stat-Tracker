/* Copyright (c) 2014 Trevor Carmichael
 * See the file license.txt for copying permission.
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Net;

namespace LoLStatTracker
{

    /// <summary>
    /// A wrapper class for the League of Legends API. Converts JSON files into C# classes.
    /// </summary>
    class LeagueWrapper
    {
        string _key;
        string region = "na";
        /// <summary>
        /// Constructor needs nothing except for API key to function.
        /// </summary>
        /// <param name="APIKey">API key provided by developer.riotgames.com</param>
        public LeagueWrapper(string APIKey)
        {
            _key = APIKey;
        }

        public Dictionary<string, string> getSummonerNameList(List<int> ids)
        {
            string summonerstring = String.Join(",", ids);
            string request, jsonString;

            request = String.Format("https://prod.api.pvp.net/api/lol/{0}/v1.3/summoner/{1}/name?api_key={2}", region, summonerstring, _key);

            using (var web = new WebClient())
            {
                jsonString = web.DownloadString(request);
            }
            return JsonConvert.DeserializeObject<Dictionary<string, string>>(jsonString);
        }

        /// <summary>
        /// Returns the entire champion pool as a Champions object. 
        /// </summary>
        /// <param name="_free">If true only returns champions free that week.</param>
        /// <returns>Returns a Champions object.</returns>
        public Champions getChampionList(bool _free = false)
        {
            string request = String.Format("http://prod.api.pvp.net/api/lol/{0}/v1.1/champion?freeToPlay={1}&api_key={2}", region, _free, _key);
            string jsonString;
            using (var web = new WebClient())
            {
                jsonString = web.DownloadString(request);
            }

            return JsonConvert.DeserializeObject<Champions>(jsonString);
        }

        /// <summary>
        /// Retrieves Summoner info by Name
        /// </summary>
        /// <param name="_name">In game summoner name</param>
        /// <param name="_region">Game region of summoner</param>
        /// <returns>Summoner object</returns>
        public Summoner getSummoner(string _name)
        {
            string request = String.Format("http://prod.api.pvp.net/api/lol/{0}/v1.2/summoner/by-name/{1}?api_key={2}", region, _name, _key);
            string jsonString;
            using (var web = new WebClient())
            {
                jsonString = web.DownloadString(request);
            }
            return JsonConvert.DeserializeObject<Summoner>(jsonString);
        }

        /// <summary>
        /// Retrieves Summoner info by ID
        /// </summary>
        /// <param name="_id">ID of summoner</param>
        /// <param name="_region">Game region of summoner</param>
        /// <returns>Summoner object</returns>
        public Summoner getSummoner(int _id)
        {
            string request = String.Format("http://prod.api.pvp.net/api/lol/{0}/v1.2/summoner/{1}?api_key={2}", region, _id, _key);
            string jsonString;
            using (var web = new WebClient())
            {
                jsonString = web.DownloadString(request);
            }
            return JsonConvert.DeserializeObject<Summoner>(jsonString);
        }

        /// <summary>
        /// Retrieves the past 10 games of summoner.
        /// </summary>
        /// <param name="_id">ID of summoner</param>
        /// <param name="_region">Game region of summoner (NA, EUW, EUNE)</param>
        /// <returns>RecentGames object</returns>
        public RecentGames getRecentGames(int _id)
        {
            string request = String.Format("http://prod.api.pvp.net/api/lol/{0}/v1.3/game/by-summoner/{1}/recent?api_key={2}", region, _id, _key);
            string jsonString;
            RecentGames recentGames;
            using (var web = new WebClient())
            {
                jsonString = web.DownloadString(request);
            }
            recentGames = JsonConvert.DeserializeObject<RecentGames>(jsonString);
            recentGames.reorder();
            return recentGames;
        }
        public void changeRegion(string newRegion = "na")
        {
            region = newRegion;
        }
    }


}
