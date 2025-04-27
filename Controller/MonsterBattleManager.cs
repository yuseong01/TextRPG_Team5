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
    public class MonsterManager
    {
        private PlayerBattleController playerBattleController = new PlayerBattleController(); // 플레이어 행동(방어 등)을 조작하기 위한 컨트롤러 (현재 연결이 안 돼 있음)
        Random random = new Random();
        List<Monster> normalMonsters = new List<Monster>(); //-lv.2 lv4 lv10 lv6  <-한전투에 계속 등장할 애들 (랜덤값으로 1-4마리가 나와요)  
        List<Monster> hardMonsters = new List<Monster>();
        private Timer timer; // 5초 입력 제한 타이머
        private bool timeOver = false; // 타이머 초과 여부 확인용
        protected bool lastAnswerCorrect; // 마지막 문제 정답 여부 저장

        public MonsterManager()
        {
            normalMonsters.Add(new Monster("컴파일에러(CS1002)", MonsterType.Normal, Constants.MONSTER1_QUESTION, ";", 39, 12, 9));
            normalMonsters.Add(new Monster("극한의 대문자 E", MonsterType.Normal, Constants.MONSTER2_QUESTION, "protected", 21, 17, 11));
            normalMonsters.Add(new Monster("※△ㅁ쀓?뚫.뚫/딻?띫", MonsterType.Normal, Constants.MONSTER3_QUESTION, "FindNumberOptimized -> FindNumber", 45, 10, 16));
            normalMonsters.Add(new Monster("{name}", MonsterType.Normal, Constants.MONSTER4_QUESTION, "!=", 12, 13, 6));

            hardMonsters.Add(new Monster("ZebC□in", MonsterType.Hard, Constants.MONSTER5_QUESTION, "1, 2, 3, 4, 5", 80, 25, 18));
            hardMonsters.Add(new Monster("{message}", MonsterType.Hard, Constants.MONSTER6_QUESTION, "override", 75, 36, 25));
            hardMonsters.Add(new Monster("FakeCam", MonsterType.Hard, Constants.MONSTER7_QUESTION, "사과 한 개", 80, 24, 35));
            hardMonsters.Add(new Monster("Codebraker", MonsterType.Hard, Constants.MONSTER8_QUESTION, "조건 통과!", 60, 28, 15));

        }

        // 다수 몬스터와 그룹 배틀 시작
        public void StartGroupBattle(Player player, bool isNormalMonster)
        {
            List<Monster> randomMonsters = GetRandomMonsters(isNormalMonster);
            while (true)
            {
                Console.WriteLine("===== [전투 시작] =====");

                DisplayMonsters(randomMonsters); // 현재 살아있는 몬스터 보여주기
                Monster target = SelectTarget(randomMonsters); // 유저가 타겟 선택

                if (AskQuestion(target))
                {
                    Console.WriteLine($"[공격] {target.Name}에게 데미지!");
                    target.Defeat(); // 맞췄으면 몬스터 처치
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
                        Console.WriteLine("===== [패배....] =====");
                        Console.WriteLine("안타깝게도 당신은 영원히 Zeb세상에 살게되었습니다..."); //이거 나중에 UI함수 호출해서 출력하면 좋을듯
                        return; // 플레이어 사망 시 게임종료
                    }
                    
                }
                if(randomMonsters.Any(monster => !monster.IsDefeated) && player.IsPlayerAlive)
                {
                    Console.WriteLine("전투 승리!");
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
                    Console.WriteLine($"[{i + 1}] {monsters[i].Name}(HP: {monsters[i].CurrentHealth})");
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
            Console.Write("정답 입력(5초 제한):");

            timeOver = false;
            timer = new Timer(5000);
            timer.Elapsed += OnTimedEvent;
            timer.AutoReset = false; // 반복 호출 금지 (한 번만)
            timer.Start();

            string input = Console.ReadLine();
            timer.Stop();

            if (timeOver)
            {
                Console.WriteLine("\n 시간 초과! 자동으로 오답 처리됩니다.");
                return false;
            }

            return input == monster.CorrectAnswer;

        }

        private void OnTimedEvent(object source, ElapsedEventArgs eventArgs)
        {
            timeOver = true;
        }

        // 플레이어가 공격하는 메인 함수
        public void PlayerAttack(Player player, Monster monster)
        {
            Console.WriteLine($"{monster.Name}의 질문: {monster.Question}");
            Console.Write("당신의 답변 (5초 제한): ");

            timeOver = false;
            timer.Start();
            string input = Console.ReadLine();
            timer.Stop();

            if (timeOver)
                input = "";

            lastAnswerCorrect = (input == monster.CorrectAnswer);

            if (lastAnswerCorrect)
            {
                Console.WriteLine("[정답] 몬스터를 물리쳤습니다!");
                RewardPlayer(player); // [추가] : 보상 지급 함수 분리

                monster.Defeat();
                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.WriteLine($"[] {monster.Name} 처치 완료!");
                Console.ResetColor();
            }
            else
            {
                Console.WriteLine("[오답] 공격을 실패했습니다");
            }
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
