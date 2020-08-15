using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Party.Actions
{
    public class DamageMove : Move
    {
        public string DefendingStat { get; set; }
        //pos or neg for change
        public int Power { get; set; }

        public int Damage { get; set; }
        // Stat used as hitter

        //Health alter move constructor
        /*bool AOE,*/
        public DamageMove(string moveName, string statAtk, string statDef, int power, Targets defenders, int cost)
        {
            Name = moveName;
            StatInvolved = statAtk;
            DefendingStat = statDef;
            Power = power;
            Targeted = defenders;
            Cost = cost;
            Type = MoveType.DAMAGE;
        }

        public int ReloadDamage(Member.Character player)
        {
            double moveDamage = Power;
            moveDamage *= player.stats[StatInvolved].GetStat();
            moveDamage *= (player.Level * 2.0 / 9) + 1.2;
            moveDamage /= 2;
            moveDamage = Math.Truncate(moveDamage + 1);
            Damage = (int)moveDamage;
            return (int)moveDamage;
        }
    }
}
