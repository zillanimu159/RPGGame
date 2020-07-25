using System;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
// filepath for saving: Application.persistentDataPath+"/saves/"+saveGameName+".da



namespace Party.Character
{
    public abstract class CharacterBase
    {
        public string Name { get; set; }
        public int Attack { get; set; }
        public int Defense { get; set; }
        public int CurrentHealth { get; set; }
        public int Health { get; set; }
        public int Level { get; set; }
        public Actions.Move BasicAttack { get; set; }

        public List<string> actionList;
        public List<Actions.Move> magicList;
        public CharacterBase()
        {
            actionList = new List<string>();
            magicList = new List<Actions.Move>();
        }
        public CharacterBase(int atk, int def, int currentHealth, int health, int lvl)
        {
            Attack = atk;
            Defense = def;
            CurrentHealth = currentHealth;
            Health = health;
            Level = lvl;
            actionList = new List<string>();
            magicList = new List<Actions.Move>();
        }
        
        public int TakeDamage(int damage)
        {
            CurrentHealth -= damage;
            return CurrentHealth;
        }
    }
}