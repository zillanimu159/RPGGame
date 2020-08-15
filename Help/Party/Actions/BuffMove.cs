using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Party.Actions
{
    class BuffMove : Move
    {
        public int ChangePercent { get; set; }
        public BuffMove(string moveName, string statChanged, int statChangePercent, /*bool AOE,*/ Targets defenders, int cost)
        {
            Name = moveName;
            StatInvolved = statChanged;
            ChangePercent = statChangePercent;
            Targeted = defenders;
            Cost = cost;
            Type = MoveType.STATUS;
        }
    }
}
