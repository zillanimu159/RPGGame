using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Party.Actions
{
    public class Item : ActionBase
    {
        public string Name { get; set; }
        public enum Effect { RESOURCE_HEAL, STAT_CHANGE, RESURRECT }
        public Effect ItemEffect { get; set; }
        public int EffectPower { get; set; }
        public string StatChanged { get; set; }
        public int Quantity { get; set; }

        public Item(string name, Effect itemEffect, int power, string changedStat, int quantity) {
            Name = name;
            ItemEffect = itemEffect;
            EffectPower = power;
            StatChanged = changedStat;
            Quantity = quantity;
        }
    }
}
