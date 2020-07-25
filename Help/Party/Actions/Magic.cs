using System.Collections;
using System.Collections.Generic;

namespace Party.Actions
{
    class Spell
    {
        // always at least 2
        private List<MagicComponent> components;
        public double Damage { get; }
        public string Name { get; }
        private string ChangeType(string Name, int part)
        {
            string finalName = "";
            switch (part)
            {
                case 0:
                    finalName = Name + "-based bolt";
                    break;
                case 1:
                    finalName = Name + " infused, ";
                    break;
                case 2:
                    finalName = Name + "-covered, ";
                    break;
                default:
                    break;
            }
            return finalName;
        }
        public Spell(List<MagicComponent> componentList)
        {
            Damage = 0;
            Name = "";
            for (int i = (componentList.Count - 1); i >= 0; i--)
            {
                Damage += componentList[i].Damage * ((i + 1.0) / 3.0);
                Name += ChangeType(componentList[i].DamageType, i);
            }
            Name.ToLower();
            Name = char.ToUpper(Name[0]) + Name.Substring(1);
        }
        public string to_string()
        {
            return "This is a " + Name + " that does " + Damage + " Damage.";

        }
    };

    class MagicComponent
    {
        //0: CORE 1: TAIL 2: SHELL
        public int SpellPart { get; }
        public double Damage { get; }
        public string DamageType { get; set; }
        public MagicComponent()
        {
            SpellPart = 0;
            Damage = 10;
            DamageType = "Lightning";
        }
        public MagicComponent(int part, int dmg, string dmgtype)
        {
            SpellPart = part;
            Damage = dmg;
            DamageType = dmgtype;
        }

    };
}