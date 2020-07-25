using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Party.Actions
{
    public class Move
    {
        public string Name { get; set; }
        public int Stat { get; set; }
        public int Power { get; set; }
        public Move(string moveName, int stat, int power)
        {
            Name = moveName;
            Stat = stat;
            Power = power;
        }
    }
}
