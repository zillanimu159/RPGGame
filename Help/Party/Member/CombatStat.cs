using System;
using System.Collections.Generic;
using System.Text;

namespace Party.Member
{
    public class CombatStat : Statistic
    {
        private int buffPercent;
        public CombatStat(int statNum, Proficiency proficiency) : base(statNum, proficiency) {
            buffPercent = 0;
        }
        public int AddToBuff(int amount)
        {
            if (buffPercent == 50)
            {
                return 0;
            }
            buffPercent += amount;
            if (buffPercent > 50)
            {
                int raised = buffPercent - 50;
                buffPercent = 50;
                return raised;
            }
            return amount;
        }
        public bool ResetBuff()
        {
            buffPercent = 0;
            return true;
        }
        public override bool AddToStat(int added)
        {
            baseStat += added;
            return true;
        }
        public override int GetStat()
        {
            double calculateStat = baseStat;
            calculateStat *= ((buffPercent / 100.0) + 1);
            int finalStat = (int)Math.Truncate(calculateStat + 1);
            return finalStat;
        }
    }
}
