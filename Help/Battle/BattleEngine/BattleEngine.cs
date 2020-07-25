using Party;
using Party.Character;
using Party.Actions;
using System;
using System.Collections.Generic;

namespace Battle.BattleEngine
{
    class BattleEngine
    {
        public CharacterParty CreateDefaultParty() {
            CharacterParty temp = new CharacterParty();
            temp.AddCharacter(new Player("dig"));
            temp.AddCharacter(new Player("waaaa"));
            temp.AddCharacter(new Player("Pikachu"));
            return temp;
        }

        //public List<Change> changes ProcessSingleAction(int selectedIndex, CharacterParty allyParty, Player mover, List<CharacterBase> affected) { }

        private int CalculateDamage(Move selectedMove, CharacterBase currentPlayer, CharacterBase selectedEnemy) {
            Random rng = new Random();
            double finalDamage = selectedMove.Power;
            finalDamage *= (double) selectedMove.Stat / selectedEnemy.Defense;
            finalDamage *= (currentPlayer.Level * 2.0 / 9) + 1.2;
            finalDamage /= 2;
            finalDamage = (int)Math.Truncate(finalDamage + 1);
            finalDamage -= 15;
            finalDamage *= rng.Next(90, 100) / 100;
            return (int)Math.Truncate(finalDamage + 1) /* * selectedMove.actionIntent*/;
        }

    }
}
