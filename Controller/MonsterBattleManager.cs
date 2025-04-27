using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using week3;
using System.Timers;
using Timer = System.Timers.Timer;
using week3.Model;

namespace week3
{
    public class MonsterBattleManager
    {
        Player player;
        PlayerBattleController playerBattleController;
        Random random = new Random();
        List<Monster> normalMonsters = new List<Monster>();
        List<Monster> hardMonsters = new List<Monster>();
        private bool timeOver = false;
        protected bool lastAnswerCorrect;
        private List<Monster> currentMonsters; // 현재 전투 몬스터 저장

        public MonsterBattleManager(Player player)
        {
            this.player = player;
            playerBattleController = new PlayerBattleController(player);

            // 몬스터 초기화
            normalMonsters.Add(new Monster("컴파일에러(CS1002)", MonsterType.Normal, Constants.MONSTER1_QUESTION, ";", 39, 12, 9, 10));
            normalMonsters.Add(new Monster("극한의 대문자 E", MonsterType.Normal, Constants.MONSTER2_QUESTION, "protected", 21, 17, 11, 10));
            normalMonsters.Add(new Monster("※△ㅁ쀓?뚫.뚫/딻?띫", MonsterType.Normal, Constants.MONSTER3_QUESTION, "FindNumberOptimized -> FindNumber", 45, 10, 16, 10));
            normalMonsters.Add(new Monster("{name}", MonsterType.Normal, Constants.MONSTER4_QUESTION, "!=", 12, 13, 6, 10));
            hardMonsters.Add(new Monster("ZebC□in", MonsterType.Hard, Constants.MONSTER5_QUESTION, "1, 2, 3, 4, 5", 80, 25, 18, 10));
            hardMonsters.Add(new Monster("{message}", MonsterType.Hard, Constants.MONSTER6_QUESTION, "override", 75, 36, 25, 10));
            hardMonsters.Add(new Monster("FakeCam", MonsterType.Hard, Constants.MONSTER7_QUESTION, "사과 한 개", 80, 24, 35, 10));
            hardMonsters.Add(new Monster("Codebraker", MonsterType.Hard, Constants.MONSTER8_QUESTION, "조건 통과!", 60, 28, 15, 10));
        }

        public void StartGroupBattle(Player player, bool isNormalMonster)
        {
            currentMonsters = GetRandomMonsters(isNormalMonster);

            while (true)
            {
                Thread.Sleep(700);
                Console.Clear();
                Console.WriteLine("===== [전투 시작] =====");

                DisplayMonsters(currentMonsters);
                DisplayPlayerInfo();

                if (currentMonsters.All(monster => monster.IsDefeated))
                {
                    Console.WriteLine("\nBattle!! - Result");
                    Console.WriteLine("Victory");
                    RewardPlayer(player);
                    break;
                }

                if (!player.IsPlayerAlive)
                {
                    Console.WriteLine("\nBattle!! - Result");
                    Console.WriteLine("You Lose");
                    player.Die();
                    break;
                }

                Console.WriteLine("1. 공격");
                Console.WriteLine("2. 아이템 사용");

                int choice = InputManager.GetInt(1, 2);

                if (choice == 1)
                {
                    Monster target = SelectTarget(currentMonsters);

                    if (AskQuestion(target))
                    {
                        Attack(target);
                    }
                    else
                    {
                        Console.WriteLine("\n[공격 실패] 데미지 없음!");
                    }
                }
                else if (choice == 2)
                {
                    UseBattleItem();
                }

                Console.WriteLine("\n[Enter]를 입력하세요");
                Console.ReadLine();
                Console.Clear();
                EnemyPhase(currentMonsters);
            }
        }

        public List<Monster> GetRandomMonsters(bool isNormalMonster)
        {
            List<Monster> randomMonsterList = new List<Monster>();
            int numberOfPicks = random.Next(1, 5); // 1~4 랜덤

            for (int i = 0; i < numberOfPicks; i++)
            {
                int randomIndex = random.Next(isNormalMonster ? normalMonsters.Count : hardMonsters.Count);
                randomMonsterList.Add(isNormalMonster ? normalMonsters[randomIndex] : hardMonsters[randomIndex]);
            }

            return randomMonsterList;
        }

        private void EnemyPhase(List<Monster> monsters)
        {
            Console.WriteLine("\n===== [Enemy Phase] =====");

            foreach (var monster in monsters.Where(m => !m.IsDefeated))
            {
                Console.Clear();

                DisplayMonsters(monsters);
                DisplayPlayerInfo();

                Console.ForegroundColor = GetMonsterColor(monster.Name); // ★ 몬스터 이름 색 적용
                Console.Write($"{monster.Name}");
                Console.ResetColor();
                Console.WriteLine("의 공격!");

                int damage = MonsterAttack(player, monster);

                if (!player.IsPlayerAlive)
                {
                    Console.WriteLine("\n플레이어가 쓰러졌습니다...");
                    break;
                }

                Console.WriteLine("\n0. 다음");
                Console.Write(">> ");
                Console.ReadLine();
            }
        }

        private void DisplayMonsters(List<Monster> monsters)
        {
            Console.WriteLine("\n ===== 몬스터 목록 =====");

            for (int i = 0; i < monsters.Count; i++)
            {
                var monster = monsters[i];

                if (monster.IsDefeated)
                {
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    Console.WriteLine($"[X] {monster.Name} (Dead)");
                    Console.ResetColor();
                }
                else
                {
                    Console.ForegroundColor = GetMonsterColor(monster.Name);
                    Console.WriteLine($"[{i + 1}] {monster.Name} (HP: {monster.Hp})");
                    Console.ResetColor();
                }
            }
        }

        private void DisplayPlayerInfo()
        {
            Console.WriteLine("\n===== 내 정보 =====");
            Console.WriteLine($"Name:{player.Name}");
            Console.WriteLine($"HP: {player.CurrentHp}/{player.MaxHp}");
            Console.WriteLine("===================");
        }

        private Monster SelectTarget(List<Monster> monsters)
        {
            while (true)
            {
                Console.Write("\n 공격할 몬스터 번호: ");
                int index = InputManager.GetInt(1, monsters.Count + 1);

                Monster target = monsters[index - 1];

                if (target.IsDefeated)
                {
                    Console.WriteLine("[오류] 이미 처치된 몬스터입니다! 다시 입력해주세요");
                }
                else
                {
                    return target;
                }
            }
        }

        private bool AskQuestion(Monster monster)
        {
            Console.WriteLine($"\n{monster.Name}의 문제: {monster.Question}");
            Console.Write("정답 입력: ");
            string input = Console.ReadLine();
            return input == monster.CorrectAnswer;
        }

        public void Attack(Monster monster)
        {
            Console.Clear();

            DisplayMonsters(currentMonsters);
            DisplayPlayerInfo();

            double errorRange = player.Attack * 0.1;
            int error = (int)Math.Ceiling(errorRange);
            Random random = new Random();
            int finalAttack = random.Next(player.Attack - error, player.Attack + error + 1);

            monster.Hp -= finalAttack;

            if (monster.Hp <= 0)
            {
                monster.Hp = 0;
                monster.Defeat();
            }

            Console.WriteLine();
            Console.Write("플레이어가 ");
            Console.ForegroundColor = GetMonsterColor(monster.Name);
            Console.Write($"{monster.Name}");
            Console.ResetColor();
            Console.WriteLine($" 에게 {finalAttack}의 데미지를 입혔습니다!");

            DisplayMonsters(new List<Monster> { monster });
        }

        private void UseBattleItem()
        {
            Console.Clear();
            Console.WriteLine("===== 아이템 사용 =====");

            if (player.inventoryManager.healItemList.Count == 0)
            {
                Console.WriteLine("사용할 수 있는 아이템이 없습니다!");
                return;
            }

            for (int i = 0; i < player.inventoryManager.healItemList.Count; i++)
            {
                var item = player.inventoryManager.healItemList[i];
                Console.WriteLine($"{i + 1}. {item.Name} - {item.Description}");
            }

            Console.Write("사용할 아이템 번호를 입력하세요 (0. 취소) : ");
            int input = InputManager.GetInt(0, player.inventoryManager.healItemList.Count);

            if (input == 0)
            {
                Console.WriteLine("아이템 사용을 취소했습니다.");
                return;
            }

            Item selectedItem = player.inventoryManager.healItemList[input - 1];

            player.inventoryManager.UseItem(selectedItem);
        }

        private void RewardPlayer(Player player)
        {
            int zebReward = random.Next(10, 31);
            player.AddZebCoin(zebReward);

            int goldReward = random.Next(100, 301);
            player.AddGold(goldReward);
        }

        private int MonsterAttack(Player player, Monster monster)
        {
            int damage = 0;
            switch (monster.Type)
            {
                case MonsterType.Normal:
                    Console.WriteLine("[노말 몬스터] 기본 공격!");
                    damage = monster.AttackPower;
                    playerBattleController.TakeDamageWithDefense(damage);
                    break;
                case MonsterType.Hard:
                    Console.WriteLine("[하드 몬스터] 강한 정신 공격!");
                    damage = monster.AttackPower * 2;
                    playerBattleController.TakeDamageWithDefense(damage);
                    break;
                default:
                    Console.WriteLine("[에러] 알 수 없는 몬스터 타입");
                    break;
            }
            return damage;
        }
        private ConsoleColor GetMonsterColor(string monsterName)
        {
            switch (monsterName)
            {
                case "컴파일에러(CS1002)":
                    return ConsoleColor.Cyan;
                case "극한의 대문자 E":
                    return ConsoleColor.Magenta;
                case "※△ㅁ쀓?뚫.뚫/딻?띫":
                    return ConsoleColor.Yellow;
                case "{name}":
                    return ConsoleColor.Green;
                case "ZebC□in":
                    return ConsoleColor.Red;
                case "{message}":
                    return ConsoleColor.Blue;
                case "FakeCam":
                    return ConsoleColor.DarkYellow;
                case "Codebraker":
                    return ConsoleColor.DarkCyan;
                default:
                    return ConsoleColor.White;
            }
        }

    }
}
