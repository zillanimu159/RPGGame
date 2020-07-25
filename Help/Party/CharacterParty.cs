using System;
using System.Collections;
using System.Collections.Generic;


namespace Party
{
    class CharacterParty
    {
        private Dictionary<string, Character.CharacterBase> partyMembers = new Dictionary<string, Character.CharacterBase>();
        public Character.CharacterBase getPlayerAt(string name) {
            if (name != null)
            {
                Character.CharacterBase temp;
                if(partyMembers.TryGetValue(name, out temp))
                {
                    return temp;
                }
            }
            return null;
        }
        public bool AddCharacter(Character.CharacterBase player) {
            Character.CharacterBase p = getPlayerAt(player.Name);
            if(p != null )
            {
                return false;
            }
            else
            {
                partyMembers.Add(player.Name, player);
                return true;
            }
        }
        public bool RemoveCharacter(Character.CharacterBase player) {
            return partyMembers.Remove(player.Name);
        }

        public bool Update(Character.CharacterBase player) {
            if (player != null)
            {
                RemoveCharacter(player);
                return AddCharacter(player);
            }
            return false;
        }
    }
}
