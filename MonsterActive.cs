using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace week3
{
    class MonsterActive
    {
        protected Monster monster;

        public MonsterActive(Monster monster)
        {
            this.monster = monster;
        }
        public virtualvoid AttackPlayer()
        {
            Console.WriteLine($"{monster.Name}의 질문: {monster.Question}");
            Console.Write("답변:");
            string input = Console.ReadLine();

            if (input == monster.CorrectAnswer)
            {
                Console.WriteLine("정답입니다! 몬스터를 물리쳤습니다.");
            }
            else
            {
                Console.WriteLine("오답임니다! 몬스터가 공격합니다!");
            }
        }
        public class NormalMonsterActive : MonsterActive
        {
            public NormalMonsterActive(Monster monster) : base(monster) { }
            public override void AttackPlayer()
            {
                base.AttackPlayer();
                // 추가 효과가 있다면 여기 구현
            }
        }
        public class HardMonsterActive : MonsterActive
        {
            public HardMonsterActive(Monster monster) : base(monster)
            {
                base.AttackPlayer();
                if(monster.CorrectAnswer != Console.ReadLine())
                {
                    Console.WriteLine("정신력이 깎입니다! (하드몬 특수효과)");
                    // vmffpdldj cpfur RkRsms zhem emd
                }
            }
        }
    }
}
