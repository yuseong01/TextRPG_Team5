using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using week3;

namespace week3
{
    public class BossMonster_Battle
    {
        BossMonster_Data Manager_Park = BossMonster_Data.BossMonsterData["박찬우 매니저"];
        BossMonster_Data Manager_Hero = BossMonster_Data.BossMonsterData["나영웅 매니저"];
        BossMonster_Data Manager_Zoom = BossMonster_Data.BossMonsterData["한효승 매니저"];

        private static Player player = new Player("Name", "Job");
        private static BossMonster_Data currentBoss;
        private static Random randrange = new();

        private static int parkPhase = 0; // 스킬 페이즈 제어
        private static int parkCombo = 0; // 일반공격 성공 횟수
        private static int PlayerDamage = 0; // 보스 피격 대미지 저장용
        private static int BossDamage = 0; // 플레이어 피격 대미지 저장용
        private static bool isBossAttackSuccess;

        static void PrintStatusBar() // 체력바
        {
            Console.WriteLine(new string('=', 75));
            Console.WriteLine($"내 체력 : {player.HP} / 100\n{currentBoss.Name}의 체력 : {currentBoss.HP} / {currentBoss.MaxHP}");
            Console.WriteLine(new string('=', 75));
        }

        // 테스트용 던전 진입 메서드

        public static void BossBattle()
        {
            Console.Clear();

            BossMonster_Data boss = new BossMonster_Data();
            boss.CopyFrom(BossMonster_Data.BossMonsterData["박찬우 매니저"]);
            currentBoss = boss;

            Console.WriteLine($"{boss.Name} : 어서오세요... 나의 공간에...");
            Console.WriteLine();
            Console.ReadKey(true);

            PrepareManager_Park(boss);
            PlayerTurn(); // 그 다음 플레이어 턴
        }
        private static void HintDialogue(BossMonster_Skill skill) // 힌트대사 랜덤으로 돌리는 메서드
        {
            if (skill.HintDialogue.Count > 0)
            {
                string hint = skill.HintDialogue[randrange.Next(skill.HintDialogue.Count)];
                Console.ReadKey(true);
                Console.WriteLine($"{hint}");
                Console.ReadKey(true);
            }
        }
        private static BossMonster_Skill FindSkillByName(BossMonster_Data boss, string name) // 스킬 찾는 메서드
        {
            foreach (var s in boss.Skills)
            {
                if (s.Name == name)
                {
                    return s;
                }
            }
            return null;
        }
        public static void PrepareManager_Park(BossMonster_Data boss)
        {
            List<BossMonster_Skill> normals = new List<BossMonster_Skill>(); // 일반공격 스킬 리스트화
            var skill1 = FindSkillByName(boss, "자유의 함성");
            var skill2 = FindSkillByName(boss, "질서의 함성");
            var skill3 = FindSkillByName(boss, "통제의 함성");
            
            int choice = randrange.Next(1, 101);

            foreach (var s in boss.Skills)
            {
                if (s.Type == BossSkillType.Normal)
                {
                    normals.Add(s);
                }
            }
            if (normals.Count > 0)
            {
                var selected = normals[randrange.Next(normals.Count)];
                boss.NextSkill = selected;
                boss.IsNextSkill = false;
                HintDialogue(selected);
                parkCombo++;
            }
            else // 예외처리
            {
                boss.IsNextSkill = false;
                Console.WriteLine("일반공격 불가 : 게임 종료");
            }

            if (boss.HP <= 40 && parkPhase == 0 && skill1 != null)
            {
                boss.NextSkill = skill1;
                boss.IsNextSkill = true;
                HintDialogue(skill1);
                parkPhase = 1;
                parkCombo = 0;
                return;
            }
            if (parkPhase == 1 && parkCombo >= 3 && skill2 != null)
            {
                boss.NextSkill = skill2;
                boss.IsNextSkill = true;
                HintDialogue(skill2);
                parkPhase = 2;
                parkCombo = 0;
                return;
            }
            if (parkPhase == 2 && parkCombo >= 2 && skill3 != null)
            {
                boss.NextSkill = skill3;
                boss.IsNextSkill = true;
                HintDialogue(skill3);
                parkPhase = 0;
                parkCombo = 0;
                return;
            }
        }
        public static void PrepareManager_Hero(BossMonster_Data boss)
        {
            if (boss.HP <= 25)
            {
                var HeroSkillGloomySong = 1;
            }

        }
        public static void PrepareManager_Zoom(BossMonster_Data boss)
        {

        }

        static void PrepareBossTurn(BossMonster_Data boss) // 보스 행동 예고용 메서드
        {
            Random rand = new Random();

            switch (boss.Name)
            {
                case "박찬우 매니저": PrepareManager_Park(boss); break;
                case "나영웅 매니저": PrepareManager_Hero(boss); break;
                case "한효승 매니저": PrepareManager_Zoom(boss); break;
            }
        }

        static void PlayerTurn()
        {
            Console.Clear();
            PrintStatusBar();
            Console.WriteLine("행동을 정해야한다.");
            Console.WriteLine("1. 공격");
            Console.WriteLine("2. 소비 아이템 사용");
            Console.WriteLine("3. 도주\n");

            string input = Console.ReadLine();

            switch (input)
            {
                case "1": Console.WriteLine("공격 테스트");
                    Console.WriteLine("1. 적 HP -25");
                    Console.WriteLine("2. 내 HP -55");
                    string attackInput = Console.ReadLine();

                    if (attackInput == "1")
                    {
                        PlayerDamage = 25;
                        isBossAttackSuccess = false;
                    }
                    else if (attackInput == "2")
                    {
                        BossDamage = 55;
                        isBossAttackSuccess = true;
                    }
                    else
                    {
                        Console.WriteLine("다시 생각했다.");
                        Console.ReadKey(true);
                        PlayerTurn();
                    }
                    BossTurn();
                    break;
                case "2": Console.WriteLine("소비 아이템 출력 예정");
                    Console.ReadKey(true); 
                    PlayerTurn();
                    break;
                case "3": Console.WriteLine("도주할 수 없다.");
                    Console.ReadKey(true);
                    PlayerTurn();
                    break;
                default: Console.WriteLine("잘못된 입력");
                    Console.ReadKey(true);
                    PlayerTurn();
                    break;
            }
        }

        static void BossTurn()
        {
            Console.Clear();
            Console.WriteLine(currentBoss.NextSkill.MainDialogue[0]);
            Console.ReadKey(true);


            if (isBossAttackSuccess) // 보스가 공격을 성공했다면
            {
                Console.WriteLine("나는 대처할 방법이 없다.");
                Console.ReadKey(true);
                Console.WriteLine("...");
                Console.ReadKey(true);
                Console.WriteLine("...");
                Console.ReadKey(true);
                player.TakeDamage(BossDamage);
                if (currentBoss.NextSkill != null && currentBoss.NextSkill.SuccessDialogue.Count > 0)
                {
                    Console.WriteLine(currentBoss.NextSkill.SuccessDialogue[0]);
                }
                else
                {
                    Console.WriteLine("대미지 계산 버그");
                }
                BossDamage = 0;
            }
            else // 보스가 공격을 실패했다면
            {
                Console.WriteLine("나는 {item}을 사용해서 대처했다.");
                Console.ReadKey(true);
                Console.WriteLine("...");
                Console.ReadKey(true);
                Console.WriteLine("...");
                Console.ReadKey(true);
                currentBoss.HP -= PlayerDamage;
                if (currentBoss.NextSkill != null && currentBoss.NextSkill.FailDialogue.Count > 0)
                {
                    Console.WriteLine(currentBoss.NextSkill.FailDialogue[0]);
                }
                else
                {
                    Console.WriteLine("대미지 계산 버그");
                }
                PlayerDamage = 0;
            }
            Console.ReadKey(true);
            Console.Clear();
            if (currentBoss.HP <= 0)
            {
                Console.WriteLine("이미 당신에게 문제가 있다는 걸 알기 때문에 여기서 물러나진 않을겁니다.");
                return;
            }
            else if (player.HP <= 0)
            {
                Console.WriteLine("당신은 '제적'입니다.");
                return;
            }
            else
            {
                PrepareManager_Park(currentBoss);
                PlayerTurn();
            }
        }
    }
}
