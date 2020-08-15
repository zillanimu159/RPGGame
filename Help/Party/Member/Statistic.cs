using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Party.Member
{
    public abstract class Statistic
    {
        protected int baseStat;
        public enum Proficiency { WEAK, AVERAGE, STRONG }
        private Proficiency prof;
        public Statistic(int statNum, Proficiency proficiency) {
            baseStat = statNum;
            prof = proficiency;
        }
        public abstract bool AddToStat(int added);
        public abstract int GetStat();
    }
}
