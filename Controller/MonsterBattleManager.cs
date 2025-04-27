using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using week3;
using System.Timers; // 타이머 기능을 사용하기 위한 네임스페이스
using Timer = System.Timers.Timer;
using week3.Model; // 이름 충돌 방지용 별칭



namespace week3
{
    // 몬스터를 관리하는 매니저 클래스
    public class MonsterBattleManager
    {
        Player player;
        PlayerBattleController playerBattleController;
        Random random = new Random();
        List<Monster> normalMonsters = new List<Monster>(); //-lv.2 lv4 lv10 lv6  <-한전투에 계속 등장할 애들 (랜덤값으로 1-4마리가 나와요)  
        List<Monster> hardMonsters = new List<Monster>();
        private Timer timer; // 5초 입력 제한 타이머
        private bool timeOver = false; // 타이머 초과 여부 확인용
        protected bool lastAnswerCorrect; // 마지막 문제 정답 여부 저장

        public MonsterBattleManager(Player player)
        {
            this.player=player;
            playerBattleController = new PlayerBattleController(player);
            timer = new Timer();
            timer.AutoReset = false;

            normalMonsters.Add(new Monster("컴파일에러(CS1002)", MonsterType.Normal, Constants.MONSTER1_QUESTION, ";", 39, 12, 9,10));
            normalMonsters.Add(new Monster("극한의 대문자 E", MonsterType.Normal, Constants.MONSTER2_QUESTION, "protected", 21, 17, 11,10));
            normalMonsters.Add(new Monster("※△ㅁ쀓?뚫.뚫/딻?띫", MonsterType.Normal, Constants.MONSTER3_QUESTION, "FindNumberOptimized -> FindNumber", 45, 10, 16,10));
            normalMonsters.Add(new Monster("{name}", MonsterType.Normal, Constants.MONSTER4_QUESTION, "!=", 12, 13, 6,10));

            hardMonsters.Add(new Monster("ZebC□in", MonsterType.Hard, Constants.MONSTER5_QUESTION, "1, 2, 3, 4, 5", 80, 25, 18,10));
            hardMonsters.Add(new Monster("{message}", MonsterType.Hard, Constants.MONSTER6_QUESTION, "override", 75, 36, 25,10));
            hardMonsters.Add(new Monster("FakeCam", MonsterType.Hard, Constants.MONSTER7_QUESTION, "사과 한 개", 80, 24, 35,10));
            hardMonsters.Add(new Monster("Codebraker", MonsterType.Hard, Constants.MONSTER8_QUESTION, "조건 통과!", 60, 28, 15,10));

        }

        public void StartGroupBattle(Player player, bool isNormalMonster)
        {
            List<Monster> randomMonsters = GetRandomMonsters(isNormalMonster);
            while (true)
            {
                Thread.Sleep(700);
                Console.Clear();
                Console.WriteLine("===== [전투 시작] =====");

                DisplayMonsters(randomMonsters); // 현재 살아있는 몬스터 보여주기
                Monster target = SelectTarget(randomMonsters); // 유저가 타겟 선택

                if (AskQuestion(target))
                {
                    Attack(target);
                }
                else
                {
                    Console.WriteLine("[공격 실패] 데미지 0!");
                }
                // 살아있는 모든 몬스터가 순서대로 플레이어 공격
                foreach (var monster in randomMonsters.Where(m => !m.IsDefeated))
                {
                    MonsterAttack(player, monster);
                    if (!player.IsPlayerAlive)
                    {
                        player.Die();
                    }

                }
                if (randomMonsters.Any(monster => !monster.IsDefeated) && player.IsPlayerAlive)
                {
                    Console.WriteLine("전투 승리!");
                    RewardPlayer(player);
                    break;
                }
            }
        }

        // 랜덤으로 몬스터를 randomMonsterList에 넣어주는 함수
        public List<Monster> GetRandomMonsters(bool isNormalMonster)
        {
            List<Monster> randomMonsterList = new List<Monster>();
            int numberOfPicks = 4;

            for (int i = 0; i < numberOfPicks; i++)
            {
                int randomIndex = random.Next(isNormalMonster ? normalMonsters.Count : hardMonsters.Count);
                randomMonsterList.Add(isNormalMonster ? normalMonsters[randomIndex] : hardMonsters[randomIndex]);
            }

            return randomMonsterList;
        }
        private void DisplayMonsters(List<Monster> monsters)
        {
            Console.WriteLine("\n ===== 몬스터 목록 =====");

            // 1. 몬스터 목록 표시 (사망 시 회색)
            for (int i = 0; i < monsters.Count; i++)
            {
                var monster = monsters[i];
                if (monster.IsDefeated)
                {
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    Console.WriteLine($"[X] {monsters[i].Name}(사망)");
                    Console.ResetColor();
                }
                else
                {
                    Console.WriteLine($"[{i + 1}] {monsters[i].Name}(HP: {monsters[i].Hp})");
                }
            }
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
            Console.Write("정답 입력:");
            
            string input = Console.ReadLine();

            return input == monster.CorrectAnswer;
        }

        public void Attack(Monster monster)
        {
            double errorRange = player.Attack * 0.1;
            int error = (int)Math.Ceiling(errorRange); // 소수점은 무조건 올림

            Random random = new Random();
            int finalAttack = random.Next(player.Attack - error, player.Attack + error + 1); 

            monster.Hp -= finalAttack;

            if (monster.Hp <= 0){
                monster.Hp = 0; 
                monster.Defeat(); // 맞췄으면 몬스터 처치
            }

            Console.WriteLine($"플레이어가 {monster.Name}에게 {finalAttack}의 데미지를 입혔습니다!");
            Console.WriteLine($"{monster.Name}몬스터의 남은 체력: {monster.Hp}");
        }

        private void RewardPlayer(Player player)
        {
            int zebReward = random.Next(10, 31);
            player.AddZebCoin(zebReward);

            int goldReward = random.Next(100, 301);
            player.AddGold(goldReward);
        }

        private void MonsterAttack(Player player, Monster monster)
        {
            switch (monster.Type)
            {
                case MonsterType.Normal:
                    Console.WriteLine("[노말 몬스터] 기본 공격!");
                    playerBattleController.TakeDamageWithDefense(monster.AttackPower);
                    break;
                case MonsterType.Hard:
                    Console.WriteLine("[하드 몬스터] 강한 정신 공격!");
                    playerBattleController.TakeDamageWithDefense(monster.AttackPower * 2);
                    break;
                default:
                    Console.WriteLine("[에러] 알 수 없는 몬스터 타입");
                    break;
            }
        }
    }
}
