using System;
using System.Collections.Generic;
using System.Text;

namespace Party.Member
{
    public class Resource : Statistic
    {
        private int amount;
        public Resource(int statNum, Proficiency proficiency) : base(statNum, proficiency) {
            amount = statNum;
        }
        public override bool AddToStat(int added)
        {
            baseStat += added;
            return true;
        }
        public override int GetStat()
        {
            return amount;
        }
        public int LoseResource(int loss)
        {
            amount -= loss;
            if (amount <= 0)
            {
                amount = 0;
            }
            return amount;
        }
        public int GainResource(int gain)
        {
            amount += gain;
            if (amount > baseStat) {
                amount = baseStat;
            }
            return amount;
        }
        public int GetMaxStat()
        {
            return baseStat;
        }
    }
}
