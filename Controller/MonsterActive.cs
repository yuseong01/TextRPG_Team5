using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using week3;
using week3.Model;
using Timer = System.Timers.Timer;
namespace week3
{

    // 몬스터의 전투 행동을 관리하는 클래스
    public class MonsterActive
    {
        PlayerController playerController;
        protected Monster monster; 
        protected bool lastAnswerCorrect; 

        private Timer timer;
        private bool timeOver = false;

        public MonsterActive(Monster monster)
        {
            this.monster = monster;

            // 타이머 초기화 (5초 제한)
            timer = new Timer(5000); 
            timer.Elapsed += (sender, EventArgs) => 
            {
                timeOver = true;
                Console.WriteLine("\n 시간 초과!");
            };
            timer.AutoReset = false; 
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
            timer.Start(); 

            string input = Console.ReadLine();

            timer.Stop();

            // 시간 초과 여부 확인
            if (timeOver) input = "";

            // 정답 판별
            lastAnswerCorrect = (input == monster.CorrectAnswer);

            if (lastAnswerCorrect)
            {
                //zeb코인
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
                monster.Defeat();
                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.WriteLine($"[] {monster.Name} 처치 완료!");
                Console.ResetColor();

                //int zebReward = new Random().Next(10, 31);
                //player.AddZebCoin(zebReward);
                //Player.GainExperience(monster.ExperienceReward);
            }
            else
            {
                Console.WriteLine("오답입니다! 몬스터가 공격합니다!");

                switch (monster.Type)
                {
                    case MonsterType.Normal:
                        Console.WriteLine("노말 몬스터의 기본 공격!");
                        playerController.TakeDamageWithDefense(monster.AttackPower);
                        break;

                    case MonsterType.Hard:
                        Console.WriteLine("하드 몬스터의 강한 정신 공격!");
                        playerController.TakeDamageWithDefense(monster.AttackPower * 2);
                        player.ReduceSpirit(10);
                        break;

                    default:
                        Console.WriteLine("해당 몬스터 타입은 존재하지 않습니다");
                        break;

                }
            }           
        }

        public static void StartGroupBattle(List<Monster> monsters, Player player)
        {
            List<Monster> randomMonsters = MonsterManager.GetRandomMonsters(new Random().Next(1, 5));
            Console.WriteLine("===== [전투 시작] =====");

            while (randomMonsters.Any(monster => !monster.IsDefeated) && player.IsAlive)
            {
                DisplayMonsters(randomMonsters);
                Monster target = SelectTarget(randomMonsters);
                if (target == null) continue;

                bool isCorrect = AskQuestion(target);
                if (isCorrect)
                {
                    Console.WriteLine($"[공격] {target.Name}에게 데미지!");
                }
                else
                {
                    Console.WriteLine("[공격 실패] 데미지 0!");
                }
                // 모든 몬스터가 플레이어 공격 (오름차순)
                foreach (var m in monsters.Where(m => !m.IsDefeated))
                {
                    player.TakeDamage(m.AttackPower);
                    if (!player.IsAlive) break;
                }
            }

            
            Console.WriteLine(player.IsAlive ? " ===== [승리!] =====" : " ===== [패배....] =====");
        }

        private static void DisplayMonsters(List<Monster> monsters)
        {
            Console.WriteLine("\n ===== 몬스터 목록 =====");

            // 1. 몬스터 목록 표시 (사망 시 회색)

            for (int i = 0; i < monsters.Count; i++)
            {
                if (monsters[i].IsDefeated)
                {
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    Console.WriteLine($"[X] {monsters[i].Name}(사망)");
                    Console.ResetColor();
                }
                else
                {
                    Console.WriteLine($"[{i + 1}] {monsters[i].Name}(HP: {monsters[i].CurrentHealth})");
                }
            }
        }
        private static Monster SelectTarget(List<Monster> monsters)
        {
            Console.Write("\n 공격할 몬스터 번호: ");
            if (int.TryParse(Console.ReadLine(), out int index) && index <= monsters.Count)
            {
                Monster target = monsters[index - 1];
                if (target.IsDefeated)
                {
                    Console.WriteLine("[오류] 이미 처치된 몬스터입니다!");
                    return null;
                }
                return target;
            }
            Console.WriteLine("[오류] 잘못된 번호!");
            return null;
        }
        private static void AttackMenu(List<Monster> aliveMonsters, Player player)
        {
            Console.Write("\n공격할 몬스터 번호 선택:");
            if (int.TryParse(Console.ReadLine(), out int selectedIndex) &&
                selectedIndex >= 1 && selectedIndex <= aliveMonsters.Count)
            {
                new MonsterActive(aliveMonsters[selectedIndex - 1]).AttackPlayer(player);
            }
            else
            {
                Console.WriteLine("잘못된 입력! 자동으로 첫 번째 몬스터를 공격합니다.");
                new MonsterActive(aliveMonsters[0]).AttackPlayer(player);
            }
        }

        private static bool AskQuestion(Monster monster)
        {
            Console.WriteLine($"\n{monster.Name}의 문제: {monster.Question}");
            Console.Write("정답 입력:");
            return Console.ReadLine() == monster.CorrectAnswer;
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
                    Console.WriteLine("정신력이 깎입니다! (하드몬 특수효과");
                    player.ReduceSpirit(1);
                    // 플레이어 정신력 깎는 코드
                }
            }
        }
    }
}
