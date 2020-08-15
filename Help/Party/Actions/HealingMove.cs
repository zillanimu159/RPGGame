using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Party.Actions
{
    class HealingMove : Move
    {
        public int Power { get; set; }
        public int Healing { get; set; }
        public HealingMove(string moveName, string statAtk, int power, /*bool AOE,*/ Targets receivers, int cost)
        {
            Name = moveName;
            StatInvolved = statAtk;
            Power = power;
            Targeted = receivers;
            Cost = cost;
            Type = MoveType.HEALING;
        }

        public int ReloadHealing(Member.Character player) {
            int playerStat = player.stats[StatInvolved].GetStat();
            double moveHealing = Power;
            moveHealing *= playerStat;
            moveHealing /= 50;
            moveHealing += 10;
            moveHealing = Math.Truncate(moveHealing + 1);
            Healing = (int)moveHealing;
            return (int)moveHealing;
        }
    }
}
