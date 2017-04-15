using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoLStatTracker
{
    public class Champion
    {
        public int id { get; set; }
        public string name { get; set; }
        public bool? active { get; set; }
        public int? attackRank { get; set; }
        public int? defenseRank { get; set; }
        public int? magicRank { get; set; }
        public int? difficultyRank { get; set; }
        public bool? botEnabled { get; set; }
        public bool? freeToPlay { get; set; }
        public bool? botMmEnabled { get; set; }
        public bool? rankedPlayEnabled { get; set; }
        public Champion()
        {
            id = 0; name = "";
        }
        public Champion(int _ID, string _name)
        {
            id = _ID;
            name = _name;
        }
    }

    public class Champions
    {
        public List<Champion> champions { get; set; }
    }
}
