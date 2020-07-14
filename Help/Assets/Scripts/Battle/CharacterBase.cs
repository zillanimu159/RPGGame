using System;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using Magic;
// filepath for saving: Application.persistentDataPath+"/saves/"+saveGameName+".da



namespace Character
{
    public abstract class CharacterBase
    {
        public int Attack { get; set; }
        public int Defense { get; set; }
        public int CurrentHealth { get; set; }
        public int Health { get; set; }
        public int Level { get; set; }
        public Move BasicAttack { get; set; }
        public Character()
        {
            actionList = new List<string>();
            magicList = new List<Move>();
        }
        public Character(int atk, int def, int currentHealth, int health, int lvl)
        {
            Attack = atk;
            Defense = def;
            CurrentHealth = currentHealth;
            Health = health;
            Level = lvl;
            actionList = new List<string>();
            magicList = new List<Move>();
        }
        public List<string> actionList;
        public List<Move> magicList;
        public int TakeDamage(int damage)
        {
            CurrentHealth -= damage;
            return CurrentHealth;
        }
    }


    public class Player : CharacterBase
    {
        private string charClass;

        public Player(string fightClass)
        {
            actionList.Add("Attack");
            //set custom name by class (ENUM)
            BasicAttack = new Move("Whack", Attack, 40);
            actionList.Add("Magic");
            actionList.Add("Defend");
            actionList.Add("Bag");
            actionList.Add("Run");
            //prepare custom magic based on class, attack becomes attacking stat, changes based on class 7/1/20
            charClass = fightClass;
            magicList.Add(new Move("LightningBolt", Attack, 30));
        }
        //
    }
    public class Enemy : CharacterBase
    {
        public Enemy()
        {
            BasicAttack = new Move("wopck", Attack, 30);
        }
    }

    public class Move
    {
        public string Name { get; set; }
        public int Power { get; set; }
        public Move(string moveName, int stat, int power)
        {
            Name = moveName;
            //balance Damage later - 6/23/2020
            Power = power;
        }
    }
}

namespace GameModel {
    [Serializable]
    public class Party { 
        public List<Character.Player> Characters { get; set; }


        public bool Save(string filename) {
            string data = JsonConvert.SerializeObject(this);

        }
    }
}