using System;
using week3;

namespace week3
{
    public class NormalMonster_Battle
    {
        private Player player;
        private Monster monster;
        private Random random = new Random();

        public NormalMonster_Battle(Player player)
        {
            this.player = player;
        }

        public void StartBattle(Monster monster)
        {
            this.monster = monster;
            Console.Clear();
            Console.WriteLine($"❗ {monster.Name} 이(가) 나타났다!");
            Console.WriteLine();
            Console.ReadKey(true); // 등장 연출

            BattleLoop();
        }

        private void PrintStatusBar()
        {
            Console.WriteLine(new string('=', 50));
            Console.WriteLine($"[플레이어] HP: {player.CurrentHp}/{player.MaxHp}");
            Console.WriteLine($"[{monster.Name}] HP: {monster.CurrentHealth}/{monster.MaxHealth}");
            Console.WriteLine(new string('=', 50));
        }

        private void BattleLoop()
        {
            while (player.CurrentHp > 0 && !monster.IsDefeated)
            {
                Console.Clear();
                PrintStatusBar();

                Console.WriteLine(monster.Question);
                Console.Write("\n✏️ 당신의 답: ");
                string playerAnswer = Console.ReadLine();

                ProcessPlayerAnswer(playerAnswer);
            }

            EndBattle();
        }

        private void ProcessPlayerAnswer(string answer)
        {
            if (answer.Trim().Equals(monster.CorrectAnswer.Trim(), StringComparison.OrdinalIgnoreCase))
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("\n 정답입니다! 몬스터에게 피해를 입혔습니다!");
                Console.ResetColor();

                monster.TakeDamage(25);
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\n 오답입니다! 몬스터의 공격을 받습니다!");
                Console.ResetColor();

                player.TakeDamage(monster.AttackPower);
            }

            Console.WriteLine("\n(계속하려면 아무 키나 누르세요)");
            Console.ReadKey(true);
        }

        private void EndBattle()
        {
            Console.Clear();
            PrintStatusBar();

            if (player.CurrentHp <= 0)
            {
                Console.WriteLine("\n 당신은 쓰러졌습니다...");
            }
            else if (monster.IsDefeated)
            {
                Console.WriteLine($"\n{monster.Name} 을(를) 쓰러뜨렸습니다!");
            }

            Console.WriteLine("\n(계속하려면 아무 키나 누르세요)");
            Console.ReadKey(true);
        }
    }
}
