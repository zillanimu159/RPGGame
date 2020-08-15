using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Party.Actions
{
    public class Move : ActionBase
    {
        public string Name { get; set; }
        public string StatInvolved { get; set; }
        public enum Targets { SINGLE, PARTY }
        public Targets Targeted { get; set; }
        public enum MoveType { DAMAGE, HEALING, STATUS }
        public MoveType Type { get; set; }
        //public enum resource
        //public resource resource
        public int Cost { get; set; }
        //public bool AOE;
    }
}
