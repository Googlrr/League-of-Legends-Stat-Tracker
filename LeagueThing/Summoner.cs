using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoLStatTracker
{
    public class Summoner
    {
        public Summoner() { }
        public Summoner(int _id, string _name)
        {
            id = _id;
            name = _name;
        }
        public int id { get; set; }
        public string name { get; set; }
        public int? profileIconId { get; set; }
        public long? revisionDate { get; set; }
        public int? summonerLevel { get; set; }
    }
}
