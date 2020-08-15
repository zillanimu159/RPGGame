using System;
using System.Collections.Generic;
using Party.Actions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Party.Member
{
    public class Profession {
        public enum BaseClass { WARRIOR, WIZARD, TANK, SAGE }
        public BaseClass baseClass;
        public List<int> MoveLevels { get; set; }
        public List<Move> PossibleMoves { get; set; }
        public Statistic.Proficiency[] Proficiencies { get; set; }
        public Profession(BaseClass baseClass)
        {
            //prepare custom magic based on class, attack becomes attacking stat, changes based on class 7/1/20
            this.baseClass = baseClass;
            MoveLevels = new List<int>();
            PossibleMoves = new List<Move>();
            switch (baseClass) {
                case BaseClass.WARRIOR:
                    //str, def, int, men, health, mana
                    Proficiencies = new Statistic.Proficiency[] { Statistic.Proficiency.STRONG, Statistic.Proficiency.AVERAGE, Statistic.Proficiency.AVERAGE, Statistic.Proficiency.WEAK, Statistic.Proficiency.WEAK, Statistic.Proficiency.WEAK };
                    CreateClass("Strength", "Defense", 40, Move.Targets.SINGLE);
                    break;
                case BaseClass.WIZARD:
                    Proficiencies = new Statistic.Proficiency[] { Statistic.Proficiency.WEAK, Statistic.Proficiency.AVERAGE, Statistic.Proficiency.STRONG, Statistic.Proficiency.AVERAGE, Statistic.Proficiency.WEAK, Statistic.Proficiency.STRONG };
                    CreateClass("Intelligence", "Defense", 40, Move.Targets.SINGLE);
                    break;
                case BaseClass.TANK:
                    Proficiencies = new Statistic.Proficiency[] { Statistic.Proficiency.WEAK, Statistic.Proficiency.STRONG, Statistic.Proficiency.WEAK, Statistic.Proficiency.STRONG, Statistic.Proficiency.STRONG, Statistic.Proficiency.AVERAGE};
                    CreateClass("Strength", "Defense", 40, Move.Targets.SINGLE);
                    break;
                case BaseClass.SAGE:
                    Proficiencies = new Statistic.Proficiency[] { Statistic.Proficiency.WEAK, Statistic.Proficiency.STRONG, Statistic.Proficiency.WEAK, Statistic.Proficiency.STRONG, Statistic.Proficiency.STRONG, Statistic.Proficiency.STRONG };
                    CreateClass("Intelligence", "Mental", 20, Move.Targets.PARTY);
                    break;

            }
            
        }

        public bool AddBaseMove(int lvlLearned, Move move) {
            if (move == null) {
                return false;
            }
            MoveLevels.Add(lvlLearned);
            PossibleMoves.Add(move);
            return true;
        }

        private bool CreateClass(string basicAtkStat, string basicDefStat, int basicPower, Move.Targets basicTargets) {
            AddBaseMove(0, new DamageMove("BasicAttack", basicAtkStat, basicDefStat, basicPower, basicTargets, 0));
            return true;
        }
        //public bool LevelUp()
    }
}