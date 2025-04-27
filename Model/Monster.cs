using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using week3;

namespace week3
{
    public enum MonsterType { Normal, Hard }
    public class Monster
    {
        
        public string Name { get; private set; }
        public MonsterType Type { get; private set; }
        public string Question { get; private set; }
        public string CorrectAnswer { get; private set; } 

        public int MaxHealth { get; private set; } 
        public int CurrentHealth { get; private set; } 
        public int AttackPower { get; private set; } 
        public int Defense { get; private set; } 
        public int ExperienceReward { get; private set; } 
        public bool IsDefeated { get; private set; } // 추가

        public void Defeat() => IsDefeated = true;


        public Monster(string name, MonsterType type , string question, string correctAnswer, int maxHealth= 100, int attackPower = 10, int defense = 5, int expReward = 20 ) //수정중
        {
            Name = name;
            Type = type;
            Question = question;
            MaxHealth = maxHealth;
            CurrentHealth = maxHealth;
            AttackPower = attackPower;
            Defense = defense;
            ExperienceReward = expReward;
            CorrectAnswer = correctAnswer; 
        }
    }
}
