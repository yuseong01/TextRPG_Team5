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
    //몬스터 타입 열거형 (문자열 대신 MonsterType 열거형 사용)
    public enum MonsterType { Normal, Hard }
    class Monster
    {
        // 속성 (attributes)
        public string Name { get; private set; } // 몬스터 이름
        public MonsterType Type { get; private set; } // 몬스터 타입
        public string Question { get; private set; } // 플레이어에게 낼 문제
        public string CorrectAnswer { get; private set; } // 문제의 정답

        public int MaxHealth { get; private set; } // 최대 체력
        public int CurrentHealth { get; private set; } // 현재 체력
        public int AttackPower { get; private set; } // 공격력
        public int Defense { get; private set; } // 방어력
        public int ExperienceReward { get; private set; } // 처치 시 보상 아이템~ (수정해야함)


        // 생성자 (Constructor)
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

        // 몬스터 서브클래스
        
        // 일반 몬스터 (수정중) 마주치면 문제 냄
        public class NormalMonster : Monster
        {
            public NormalMonster(string question, string correctAnswer)
                :base("노말 몬스터", MonsterType.Normal, question, correctAnswer, maxHealth: 80, attackPower: 8, defense: 3, expReward: 15) { }
        }

        // 하드 몬스터 (수정중) 미믹 
        public class HardMonster : Monster
        {
            public HardMonster(string question, string correctAnswer)
                :base("하드 몬스터", MonsterType.Hard, question, correctAnswer, maxHealth: 150, attackPower: 15, defense: 8, expReward: 30) { }
        }        
    }
}
