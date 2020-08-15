using System;
using System.Collections;
using System.Collections.Generic;
using Party.Member;

namespace Party
{
    class CharacterParty
    {
        private Dictionary<string, Character> partyMembers = new Dictionary<string, Character>();
        public Character GetPlayerAt(string name) {
            if (name != null)
            {
                if(partyMembers.TryGetValue(name, out Character temp))
                {
                    return temp;
                }
            }
            return null;
        }
        public bool AddCharacter(Character player) {
            Character p = GetPlayerAt(player.Name);
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
        public bool RemoveCharacter(Character player) {
            return partyMembers.Remove(player.Name);
        }

        public bool Update(Character player) {
            if (player != null)
            {
                RemoveCharacter(player);
                return AddCharacter(player);
            }
            return false;
        }

        //private static Dictionary<string, Actions.Item> fullItemList = new Dictionary<string, Actions.Item>();

        private Dictionary<string, Actions.Item> items = new Dictionary<string, Actions.Item>();
        public Actions.Item GetItem(string name)
        {
            if (name != null)
            {
                if (items.TryGetValue(name, out Actions.Item temp))
                {
                    return temp;
                }
            }
            return null;
        }
        public bool AddItem(Actions.Item item)
        {
            Actions.Item aItem = GetItem(item.Name);
            if (aItem != null)
            {
                return false;
            }
            else
            {
                items.Add(item.Name, item);
                item.Quantity = 0;
                return true;
            }
        }
        public bool RemoveItem(Actions.Item item)
        {
            return partyMembers.Remove(item.Name);
        }

        public bool IncrementItem(Actions.Item item, int amount)
        {
            if (item != null)
            {
                item.Quantity += amount;
                return true;
            }
            else {
                AddItem(item);
                item.Quantity = amount;
            }
            return false;
        }

    }
}
