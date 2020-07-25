using NUnit.Framework.Constraints;
using System;
using System.Collections.Generic;
using Party.Actions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Party.Character
{
    public class Player : CharacterBase
    {
        private string charClass;

        public Player(string fightClass)
        {
            BasicAttack = new Move("Whack", Attack, 40);
            actionList.Add("Attack");
            actionList.Add("Magic");
            actionList.Add("Bag");
            actionList.Add("Run");
            //prepare custom magic based on class, attack becomes attacking stat, changes based on class 7/1/20
            charClass = fightClass;
            magicList.Add(new Move("Explosion of Flame", Attack, 30));
        }
        //
    }
}