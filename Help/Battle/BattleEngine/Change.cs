using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battle.BattleEngine
{
    class Change
    {
        public string ChangedCharKey { get; set; }
        public string Statistic { get; set; }
        public int Amount { get; set; }

        public Change(string key, string stat, int amount) {
            ChangedCharKey = key;
            Statistic = stat;
            Amount = amount;
        }
    }
}
