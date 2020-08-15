using Party;
using Party.Member;
using Party.Actions;
using System;
using System.Collections.Generic;

namespace Battle.BattleEngine
{
    class BattleEngine
    {

        public CharacterParty CreateDefaultParty() {
            CharacterParty temp = new CharacterParty();
            temp.AddCharacter(new Character("dig", new Profession(Profession.BaseClass.WIZARD)));
            temp.AddCharacter(new Character("waaaa", new Profession(Profession.BaseClass.WARRIOR)));
            temp.AddCharacter(new Character("Pikachu", new Profession(Profession.BaseClass.WIZARD)));
            temp.AddItem(new Item("Potion", Item.Effect.RESOURCE_HEAL, 30, "Health", 10));
            temp.AddItem(new Item("Mana Potion", Item.Effect.RESOURCE_HEAL, 30, "Mana", 10));
            temp.AddItem(new Item("Steroids", Item.Effect.STAT_CHANGE, 10, "Strength", 10));
            temp.AddItem(new Item("Feather", Item.Effect.RESURRECT, 50, "Health", 10));
            return temp;
        }

        public CharacterParty CreateEnemyParty()
        {
            CharacterParty temp = new CharacterParty();
            temp.AddCharacter(new Character("dig", new Profession(Profession.BaseClass.WIZARD)));
            temp.AddCharacter(new Character("waaaa", new Profession(Profession.BaseClass.WIZARD)));
            temp.AddCharacter(new Character("Pikachu", new Profession(Profession.BaseClass.WIZARD)));
            return temp;
        }

        public List<Change> ProcessSingleAction(ActionBase action, Character mover, List<Character> affected) {
            List<Change> changes = new List<Change>();
            if (action is Move move) {
                switch (move.Type) {
                    case Move.MoveType.DAMAGE:
                        DamageMove dmgMove = (DamageMove)action;
                        foreach (Character character in affected)
                        {
                            int damage = CalculateDamage(dmgMove.Damage, character, dmgMove.DefendingStat);
                            changes.Add(new Change(character.Name, "Health", -1 * damage, character.stats["Health"].GetStat()));
                            ((Resource)character.stats["Health"]).LoseResource(damage);
                        }
                        break;
                    case Move.MoveType.HEALING:
                        HealingMove healMove = (HealingMove)action;
                        foreach (Character character in affected)
                        {
                            changes.Add(new Change(character.Name, "Health", healMove.Healing, character.stats["Health"].GetStat()));
                            ((Resource)character.stats["Health"]).GainResource(healMove.Healing);
                        }
                        break;
                    //figure out how to deal in player
                    case Move.MoveType.STATUS:
                        BuffMove statMove = (BuffMove)action;
                        foreach (Character character in affected)
                        {
                            int preStat = character.stats[statMove.StatInvolved].GetStat();
                            int raised = ((CombatStat)character.stats[statMove.StatInvolved]).AddToBuff(statMove.ChangePercent);
                            character.ReloadMoves();
                            double statChange = character.stats[statMove.StatInvolved].GetStat() * (raised / 100.0);
                            statChange = Math.Truncate(statChange + 1);
                            changes.Add(new Change(character.Name, statMove.StatInvolved, (int)statChange, preStat));
                        }
                        break;
                }
                changes.Add(new Change(mover.Name, "Mana", move.Cost, mover.stats["Mana"].GetStat()));
                ((Resource)mover.stats["Mana"]).LoseResource(move.Cost);
            }
            else if(action is Item item){
                switch (item.ItemEffect) {
                    case Item.Effect.RESOURCE_HEAL:
                        foreach (Character character in affected)
                        {
                            int healing = item.EffectPower;
                            Resource resource = (Resource)character.stats[item.StatChanged];
                            changes.Add(new Change(character.Name, item.StatChanged, healing, resource.GetStat()));
                            resource.GainResource(healing);
                        }
                        break;
                    case Item.Effect.STAT_CHANGE:
                        foreach (Character character in affected) {
                            CombatStat combatStat = (CombatStat) character.stats[item.StatChanged];
                            int preStat = combatStat.GetStat();
                            combatStat.AddToBuff(item.EffectPower);
                            character.ReloadMoves();
                            double statChange = combatStat.GetStat() * (item.EffectPower / 100.0);
                            statChange = Math.Truncate(statChange + 1);
                            changes.Add(new Change(character.Name, item.StatChanged, (int)statChange, preStat));
                        }
                        break;
                    case Item.Effect.RESURRECT:
                        foreach (Character character in affected) {
                            Resource resource = (Resource) character.stats["Health"];
                            int healing = (int)(resource.GetStat() * (item.EffectPower / 100.0));
                            character.IsDead = false;
                            resource.GainResource(healing);
                            changes.Add(new Change(character.Name, "Health", healing, 0));
                        }
                        break;
                }
                item.Quantity -= 1;
            }
            return changes;
        }

        private int CalculateDamage(int damage, Character selectedEnemy, string defStat) {
            Random rng = new Random();
            double finalDamage = damage;
            finalDamage /= selectedEnemy.stats[defStat].GetStat();
            finalDamage += 15;
            finalDamage *= rng.Next(90, 100) / 100.0;
            finalDamage = Math.Truncate(finalDamage + 1);
            return (int)finalDamage;
        }
        
    }
}
