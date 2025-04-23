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
        public Monster(string name, MonsterType type , string question, string CorrectAnswer, int maxHealth= 100, int attackPower = 10 ) //수정중
        {
            Name = name;
            Type = type;
            Question = question;
            MaxHealth = maxHealth;
            AttackPower = attackPower;
            Defense = Defense;
            CorrectAnswer = CorrectAnswer; 
        }

        // 몬스터 서브클래스
        
        // 일반 몬스터 (수정중)
        public class NormalMonster : Monster
        {
            public NormalMonster(string question, string correctAnswer)
                :base("노말 몬스터", MonsterType.Normal, question, correctAnswer) { }
        }

        // 하드 몬스터 (수정중)
        public class HardMonster : Monster
        {
            public HardMonster(string question, string correctAnswer)
                :base("하드 몬스터", MonsterType.Hard, question, correctAnswer) { }
        }        
    }
}
