using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace week3
{
    class Monster
    {
        // 속성 (attributes)
        public string Name { get; private set; }
        public int MaxHealth { get; private set; }
        public int CurrentHealth { get; private set; }
        public int AttackPower { get; private set; }
        public int Defense { get; private set; }
        public int ExperienceReward { get; private set; }

        // 생성자 (Constructor)
        public Monster(string name, string question, string CorrectAnswer)
        {
            Name = name;
            question = question;
            CorrectAnswer = CorrectAnswer;
        }
        public class NormalMonster : Monster
        {
            public NormalMonster(string question, string correctAnswer)
                :base("노말 몬스터", question, correctAnswer) { }
        }
        public class HardMonster : Monster
        {
            public HardMonster(string question, string correctAnswer)
                :base("하드 몬스터", question, correctAnswer) { }
        }
    }
}
