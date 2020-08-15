using System;
using System.Collections;
using System.Collections.Generic;
using Party.Actions;
// filepath for saving: Application.persistentDataPath+"/saves/"+saveGameName+".da



namespace Party.Member
{
    public class Character
    {
        //update movelist based on class and level
        public string Name { get; set; }
        public int Level { get; set; }
        private int experience;
        public bool IsDead { get; set; }
        /*IF A STAT IS ADDED:
            Add stat to each profession preset
         IF IT IS A RESOURCE:
            Create new resource within Move class
            Create custom add/subtract functions
         */
        public static string[] statNames = { "Strength", "Defense", "Intelligence", "Mental", "Health", "Mana" };
        public static bool[] statIsResource = { false, false, false, false, true, true };
        public Profession charClass;
        public Dictionary<string, Statistic> stats;

        public List<string> actionList;
        public Dictionary<string, Move> magicDictionary;
        public Character(string name, Profession baseClass)
        {
            Name = name;
            charClass = baseClass;
            Level = 0;
            actionList = new List<string>();
            magicDictionary = new Dictionary<string, Move>();
            stats = new Dictionary<string, Statistic>();
            CreateStats(baseClass.Proficiencies);
            actionList.Add("Attack");
            actionList.Add("Magic");
            actionList.Add("Bag");
            actionList.Add("Run");
            magicDictionary.Add("Explosion of Flame", new DamageMove("Explosion of Flame", "Intelligence", "Mental", 30, Move.Targets.PARTY, 20));
            magicDictionary.Add("Heal", new HealingMove("Heal", "Intelligence", 30, Move.Targets.PARTY, 20));
            magicDictionary.Add("AtkBuff", new BuffMove("AtkBuff", "Strength", 10, Move.Targets.PARTY, 30));
            ReloadMoves();
        }
        public Character(int atk, int def, int health, int lvl)
        {
            Level = lvl;
            actionList = new List<string>();
            magicDictionary = new Dictionary<string, Move>();
            stats = new Dictionary<string, Statistic>()
            {
                { "Strength", new CombatStat(atk, Statistic.Proficiency.STRONG) },
                { "Defense", new CombatStat(def, Statistic.Proficiency.WEAK) },
                { "Intelligence", new CombatStat(atk, Statistic.Proficiency.AVERAGE) },
                { "Mental", new CombatStat(def, Statistic.Proficiency.AVERAGE) },
                { "Health", new CombatStat(health, Statistic.Proficiency.AVERAGE) },
                { "Mana", new CombatStat(health, Statistic.Proficiency.AVERAGE) }
            };
            ReloadMoves();
        }

        ///ORDER OF STATS: "Strength", "Defense", "Intelligence", "Mental", "Health", "Mana"
        private bool CreateStats(Statistic.Proficiency[] proficiency)
        {
            Random rng = new Random();
            for (int i = 0; i < statNames.Length; i++)
            {
                int statStart = 0;
                switch (proficiency[i])
                {
                    case Statistic.Proficiency.STRONG:
                        statStart = rng.Next(34, 36);
                        break;
                    case Statistic.Proficiency.AVERAGE:
                        statStart = rng.Next(32, 34);
                        break;
                    case Statistic.Proficiency.WEAK:
                        statStart = rng.Next(30, 32);
                        break;

                }
                if (statIsResource[i])
                {
                    stats.Add(statNames[i], new Resource(statStart, proficiency[i]));
                }
                else
                {
                    stats.Add(statNames[i], new CombatStat(statStart, proficiency[i]));
                }
            }
            return true;
        }

        public void ReloadMoves() {
            double playerStat;
            foreach (KeyValuePair<string,Move> pair in magicDictionary) {
                Move magic = pair.Value;
                playerStat = stats[magic.StatInvolved].GetStat();
                if (magic is DamageMove dmgMove)
                {
                    dmgMove.ReloadDamage(this);
                }
                else if (magic is HealingMove healMove)
                {
                    healMove.ReloadHealing(this);
                }
            }
        }

        public int AddXP(int expPoints)
        {
            experience += expPoints;
            //set up level up
            return experience;
        }
        //public void LevelUp
    }
}