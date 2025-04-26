using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Timers; // 타이머 기능을 사용하기 위한 네임스페이스
using week3;
using week3.Model;
using Timer = System.Timers.Timer; // 이름 충돌 방지용 별칭
namespace week3
{

    // 몬스터의 전투 행동을 담당하는 메인 클래스
    public class MonsterActive
    {
        private PlayerController playerController; // 플레이어 행동(방어 등)을 조작하기 위한 컨트롤러 (현재 연결이 안 돼 있음)
        protected Monster monster; // 현재 조작하는 몬스터 객체
        protected bool lastAnswerCorrect; // 마지막 문제 정답 여부 저장

        private Timer timer; // 5초 입력 제한 타이머
        private bool timeOver = false; // 타이머 초과 여부 확인용
        private readonly Random random; // [추가] : Random 인스턴스 필드로 추가

        // 몬스터를 받아 초기화할 때 실행되는 생성자
        public MonsterActive(Monster monster, PlayerController playerController)
        {
            this.monster = monster;
            this.playerController = playerController; // 사용하려면 생성할 때 new MosnterActive (monster, playerController) 식으로 연결

            // 타이머 초기화 (5초 제한)
            timer = new Timer(5000);
            timer.Elapsed += (sender, EventArgs) =>
            {
                timeOver = true; // 5초 초과 시 플래그 설정
                Console.WriteLine("\n 시간 초과!");
            };
            timer.AutoReset = false; // 반복 호출 금지 (한 번만)
        }
        public void StartBattle()
        {
            timer.Start();
            Console.Write("답변 입력 (5초 제한):");
            string input = Console.ReadLine();
            timer.Stop();

            if (timeOver)
            {
                Console.WriteLine("자동 오답 처리!"); // 타임아웃시 자동 실패 처리
            }
            else
            {
                // 정답 판별 로직 아직 X
            }
        }

        private void OnTimedEvent(object source, ElapsedEventArgs eventArgs)
        {
            timeOver = true;
            Console.WriteLine("\n 시간 초과! 자동으로 오답 처리됩니다.");
        }

        // 플레이어를 공격하는 메인 함수
        public void AttackPlayer(Player player)
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
                Console.WriteLine("[오답] 몬스터가 공격합니다!");
                MonsterAttack(player); // [추가] : 공격 로직 분리
            }
        }

        private void RewardPlayer(Player player)
        {
            int zebReward = random.Next(10, 31);
            player.AddZebCoin(zebReward);

            int goldReward = random.Next(100, 301);
            player.AddGold(goldReward);

            if (random.Next(100) < 30)
            {
                string[] commonItems = { /* 아이템 추가 예정 */ }; // [주석] : 아이템 추가 필요
                if (commonItems.Length > 0) // [추가] : 예외 방지
                {
                    string randomItem = commonItems[random.Next(commonItems.Length)];
                    player.AddItem(randomItem);
                }
            }
        }


        private void MonsterAttack(Player player)
        {
            switch (monster.Type)
            {
                case MonsterType.Normal:
                    Console.WriteLine("[노말 몬스터] 기본 공격!");
                    playerController.TakeDamageWithDefense(monster.AttackPower);
                    break;
                case MonsterType.Hard:
                    Console.WriteLine("[하드 몬스터] 강한 정신 공격!");
                    playerController.TakeDamageWithDefense(monster.AttackPower * 2);
                    player.ReduceSpirit(10); // [주의] : 이거 나중에 삭제 고려?
                    break;
                default:
                    Console.WriteLine("[에러] 알 수 없는 몬스터 타입");
                    break;
            }
        }

        // 다수 몬스터와 그룹 배틀 시작
        public void StartGroupBattle(List<Monster> monsters, Player player)
        {
            MonsterManager monsterManager = new MonsterManager();
            List<Monster> randomMonsters = monsterManager.GetRandomMonsters(new Random().Next(1, 5));
            // 랜덤 1~4마리 생성

            Console.WriteLine("===== [전투 시작] =====");

            // 몬스터가 다 죽거나 플레이어가 죽을 때까지 반복

            while (randomMonsters.Any(monster => !monster.IsDefeated) && player.IsPlayerAlive)
            {
                DisplayMonsters(randomMonsters); // 현재 살아있는 몬스터 보여주기
                Monster target = SelectTarget(randomMonsters); // 유저가 타겟 선택
                if (target == null) continue; // 잘못 고르면 넘어감 (자동 선택을 구현하려고 했음)

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
                foreach (var m in monsters.Where(m => !m.IsDefeated))
                {
                    player.TakeDamage(m.AttackPower);
                    if (!player.IsPlayerAlive) break; // 플레이어 사망 시 루프 중단
                }
            }

            // 전투 결과 출력
            Console.WriteLine(player.IsPlayerAlive ? " ===== [승리!] =====" : " ===== [패배....] =====");
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

        // SelectTarget == AttackMenu ? 확인해야함 몬스터 번호 선택 관련 및 문제 204 - 241 무언가 뒤틀려있음
        private Monster SelectTarget(List<Monster> monsters)
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

        private void AttackMenu(List<Monster> aliveMonsters, Player player, PlayerController playerController)
        {
            Console.Write("\n공격할 몬스터 번호 선택:");
            if (int.TryParse(Console.ReadLine(), out int selectedIndex) &&
                selectedIndex >= 1 && selectedIndex <= aliveMonsters.Count)
            {
                new MonsterActive(aliveMonsters[selectedIndex - 1], playerController).AttackPlayer(player);
            }
            else
            {
                Console.WriteLine("잘못된 입력! 자동으로 첫 번째 몬스터를 공격합니다.");
                new MonsterActive(aliveMonsters[0], playerController).AttackPlayer(player);
            }
        }

        private bool AskQuestion(Monster monster)
        {
            Console.WriteLine($"\n{monster.Name}의 문제: {monster.Question}");
            Console.Write("정답 입력:");
            return Console.ReadLine() == monster.CorrectAnswer;
        }



        // 몬스터 타입별 세부 클래스

        // 일반 몬스터 전용 액티브
        public class NormalMonsterActive : MonsterActive
        {
            public NormalMonsterActive(Monster monster, PlayerController playerController) : base(monster, playerController) { }
            // 특별한 오버라이드 없음 (일반 행동 그대로)
        }


        // 하드 몬스터 전용 액티브
        public class HardMonsterActive : MonsterActive
        {
            public HardMonsterActive(Monster monster, PlayerController playerController) : base(monster, playerController) { }
            // AttackPlayer 함수 오버라이드
            public new void AttackPlayer(Player player)
            {
                base.AttackPlayer(player); // 원래 공격 로직 수행

                // 추가 패널티 부여 (오답시 정신력 깎기) 삭제할 예정
                if (!lastAnswerCorrect)
                {
                    Console.WriteLine("정신력이 깎입니다! (하드몬 특수효과)");
                    player.ReduceSpirit(1);
                }
            }
        }
    }
}
