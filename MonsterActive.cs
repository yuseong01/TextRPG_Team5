using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using week3;

using Timer = System.Timers.Timer;
namespace week3
{
    // 몬스터의 전투 행동을 관리하는 클래스
    class MonsterActive
    {
        protected Monster monster; // 연결된 몬스터 객체
        protected bool lastAnswerCorrect; // 마지막 정답 여부 기록

        private Timer timer; // 타이머 객체 (Timer timer을 할 경우 C#에서는 여러 종류의 Timer가 존재하고 있기 때문에 컴파일 에러가 발생한다.)
        private bool timeOver = false; // 시간 초과 여부

        public MonsterActive(Monster monster)
        {
            this.monster = monster;

            // 타이머 초기화 (5초 제한)
            timer = new Timer(5000); // 5000ms = 5초
            timer.Elapsed += (sender, EventArgs) => // 익명 함수로 바로 연결
            {
                timeOver = true;
                Console.WriteLine("\n 시간 초과!");
            };
            timer.AutoReset = false; // 한 번만 실행
        }

        public void StartBattle()
        {
            timer.Start();
            Console.Write("답변 입력 (5초 제한):");
            string input = Console.ReadLine();
            timer.Stop();

            if (timeOver)
            {
                Console.WriteLine("자동 오답 처리!");
            }
            else
            {
                // 정답 판별 로직
            }
        }

        private void OnTimedEvent(object source, ElapsedEventArgs eventArgs)
        {
            timeOver = true;
            Console.WriteLine("\n 시간 초과! 자동으로 오답 처리됩니다.");
        }

        // 플레이어 공격 메인 로직
        public void AttackPlayer(Player player)
        {
            // 문제 출제
            Console.WriteLine($"{monster.Name}의 질문: {monster.Question}");
            Console.Write("당신의 답변 (5초 제한):");

            timeOver = false;
            timer.Start(); // 타이머 시작

            string input = Console.ReadLine();

            timer.Stop(); // 타이머 중지

            // 시간 초과 여부 확인
            if (timeOver) input = "";// 빈 문자열로 처리

            // 정답 판별
            lastAnswerCorrect = (input == monster.CorrectAnswer);

            if (lastAnswerCorrect)
            {
                Console.WriteLine("정답입니다! 몬스터를 물리쳤습니다.");
                int zebReward = new Random().Next(10, 31);
                player.AddZebCoin(zebReward);

                int goldReward = new Random().Next(100, 301);
                player.AddGold(goldReward);

                if (new Random().Next(0, 100) <= 30)
                {
                    string[] commonItems = { };
                    string randomItem = commonItems[new Random().Next(commonItems.Length)];
                    player.AddItem(randomItem);
                }
                //Player.GainExperience(monster.ExperienceReward); // 플레이어에서 메서드 생성 해줘야함 [개인 골드] 임시파일 생성
            }
            else
            {
                Console.WriteLine("오답임니다! 몬스터가 공격합니다!");

                // 타입별 다른 공격 효과
                switch (monster.Type)
                {
                    case MonsterType.Normal:
                        Console.WriteLine("노말 몬스터의 기본 공격!");
                        player.TakeDamage(monster.AttackPower);
                        break;

                    case MonsterType.Hard:
                        Console.WriteLine("하드 몬스터의 강한 정신 공격!");
                        player.TakeDamage(monster.AttackPower * 2);
                        player.ReduceSpirit(10); // ReducementalHealth -> ReduceSpirit으로 변경
                        break;

                    default:
                        throw new ArgumentOutOfRangeException();

                }
            }
        }

        // 몬스터별 행동 클래스

        // 일반 몬스터 전용
        public class NormalMonsterActive : MonsterActive
        {
            public NormalMonsterActive(Monster monster) : base(monster) { }  
        }


        // 하드 몬스터 전용
        public class HardMonsterActive : MonsterActive
        {
            public HardMonsterActive(Monster monster) : base(monster) { }
            public new void AttackPlayer(Player player)
            {
                base.AttackPlayer(player);
                if (!lastAnswerCorrect)
                {
                    base.AttackPlayer(player);        
                    if (!lastAnswerCorrect)
                    {
                        Console.WriteLine("정신력이 깎입니다! (하드몬 특수효과");
                        player.ReduceSpirit(1);
                    }
                    // 플레이어 정신력 깎는 코드
                }
            }
        }
    }
}
