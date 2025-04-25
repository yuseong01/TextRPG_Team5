using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using NAudio.Codecs;
using week3;

namespace week3
{
    public class BossMonster_Battle
    {
        private static Player player;
        private static InventoryManager inventory = new InventoryManager();
        private static BossMonster_Data currentBoss;
        private static Random randrange = new();

        static List<BossMonster_Skill> normals = new List<BossMonster_Skill>(); // 일반공격 스킬 리스트화
        private static int PlayerDamage = 0; // 보스 피격 대미지 저장용
        private static int BossDamage = 0; // 플레이어 피격 대미지 저장용
        private static bool isBossAttackSuccess = true;
        private static bool isAttackItemUsed = false;

        static void PrintStatusBar() // 체력바
        {
            Console.WriteLine(new string('=', 55));
            Console.WriteLine($"내 체력 : {player.Hp} / 100\n{currentBoss.Name}의 체력 : {currentBoss.HP} / {currentBoss.MaxHP}");
            Console.WriteLine(new string('=', 55));
        }
        // 보스 몬스터 조우
        public static void BossBattle_Park()
        {
            Console.Clear();

            BossMonster_Data boss = new BossMonster_Data();
            boss.CopyFrom(BossMonster_Data.BossMonsterData["관리하는 박찬우 매니저"]);
            currentBoss = boss;

            Console.WriteLine($"{boss.Name} : 우오오오오오오오!!! 우오오오오오오오오!!");
            Console.WriteLine();
            Console.ReadKey(true);

            PrepareManager_Park(boss);
            PlayerTurn();
        }
        public static void BossBattle_Hero()
        {
            Console.Clear();

            BossMonster_Data boss = new BossMonster_Data();
            boss.CopyFrom(BossMonster_Data.BossMonsterData["메아리치는 나영웅 매니저"]);
            currentBoss = boss;

            Console.WriteLine($"{boss.Name} : 인간적인 감정이 없었다면 어려움도 없었겠죠.\n제가 당신의 감정을 없애드리죠.");
            Console.WriteLine();
            Console.ReadKey(true);

            PrepareManager_Hero(boss);
            PlayerTurn();
        }
        public static void BossBattle_Zoom()
        {
            Console.Clear();

            BossMonster_Data boss = new BossMonster_Data();
            boss.CopyFrom(BossMonster_Data.BossMonsterData["공간 지배의 한효승 매니저"]);
            currentBoss = boss;

            Console.WriteLine($"{boss.Name} : 그렇게 계속 청개구리처럼 행동할겁니까?");
            Console.WriteLine();
            Console.ReadKey(true);

            PrepareManager_Zoom(boss);
            PlayerTurn();
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
        // 플레이어턴 들어가기 전 각 보스별 행동계산 메서드
        private static List<BossMonster_Skill> GetNormalSkills(BossMonster_Data boss)
        {
            return boss.Skills.Where(s => s.Type == BossSkillType.Normal).ToList();
        }
        public static void PrepareManager_Park(BossMonster_Data boss)
        {            
            var skill1 = FindSkillByName(boss, "자유의 함성");
            var skill2 = FindSkillByName(boss, "질서의 함성");
            var skill3 = FindSkillByName(boss, "통제의 함성");
            
            if (boss.HP <= 40 && boss.parkPhase == 0 && skill1 != null)
            {
                boss.NextSkill = skill1;
                boss.IsNextSkill = true;
                boss.parkPhase = 1;
                boss.parkCombo = 0;
                return;
            }
            else if (boss.parkPhase == 1 && boss.parkCombo >= 3 && skill2 != null)
            {
                boss.NextSkill = skill2;
                boss.IsNextSkill = true;
                boss.parkPhase = 2;
                boss.parkCombo = 0;
                return;
            }
            else if (boss.parkPhase == 2 && boss.parkCombo >= 2 && skill3 != null)
            {
                boss.NextSkill = skill3;
                boss.IsNextSkill = true;
                boss.parkPhase = 3;
                boss.parkCombo = 0;
                return;
            }
            else
            {
                normals = GetNormalSkills(boss);
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
                    boss.parkCombo++;
                }
                else // 예외처리
                {
                    boss.IsNextSkill = false;
                    Console.WriteLine("일반공격 불가 : 게임 종료");
                }
            }
        }
        public static void PrepareManager_Hero(BossMonster_Data boss)
        {
            int rand = randrange.Next(1, 101);
            int rand2 = randrange.Next(1, 101);

            var skill1 = FindSkillByName(boss, "데스 노트");
            var skill2 = FindSkillByName(boss, "관통 사격");
            var skill3 = FindSkillByName(boss, "장송곡");

            if (boss.HP <= 25 && boss.heroTime == 0 && skill3 != null)
            {
                boss.NextSkill = skill3;
                boss.IsNextSkill = true;
                boss.heroTime = 1;
                return;
            }
            if ( rand <= 15)
            {
                if (rand2 <= 65 && skill1 != null)
                {
                    boss.NextSkill = skill1;
                    boss.IsNextSkill = true;
                    return;
                }
                else if (rand2 > 65 && skill2 != null)
                {
                    boss.NextSkill = skill2;
                    boss.IsNextSkill = true;
                    return;
                }
            }
            else
            {
                normals = GetNormalSkills(boss);
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
                }
                else // 예외처리
                {
                    boss.IsNextSkill = false;
                    Console.WriteLine("일반공격 불가 : 게임 종료");
                }
            }
        }
        public static void PrepareManager_Zoom(BossMonster_Data boss)
        {
            int rand = randrange.Next(1, 101);

            var skill1 = FindSkillByName(boss, "영역전개 : SOOW");
            var skill2 = FindSkillByName(boss, "주방 경험");
            var skill3 = FindSkillByName(boss, "견문색");

            if (boss.HP <= 50 && boss.reinforceZoomEva == 0 && skill3 != null)
            {
                boss.NextSkill = skill3;
                boss.IsNextSkill = true;
                boss.reinforceZoomEva = 1;
                return;
            }
            if (rand <= 30 && boss.reinforceZoomAtk == 0 && skill1 != null)
            {
                boss.NextSkill = skill1;
                boss.IsNextSkill = true;
                boss.reinforceZoomAtk = 1;
                return;
            }
            else if (rand > 30 && boss.reinforceZoomCri == 0 && rand <= 45 && skill2 != null)
            {
                boss.NextSkill = skill2;
                boss.IsNextSkill = true;
                boss.reinforceZoomCri = 1;
                return;
            }
            else
            {
                normals = GetNormalSkills(boss);
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
                }
                else // 예외처리
                {
                    boss.IsNextSkill = false;
                    Console.WriteLine("일반공격 불가 : 게임 종료");
                }
            }
        }
        // 플레이어 턴 행동 선택
        public static void PlayerTurn()
        {
            Console.Clear();
            isAttackItemUsed = false;
            PrintStatusBar();
            if (currentBoss.NextSkill != null && currentBoss.NextSkill.HintDialogue.Count > 0)
            {
                string hint = currentBoss.NextSkill.HintDialogue[randrange.Next(currentBoss.NextSkill.HintDialogue.Count)];
                Console.WriteLine(hint);
            }
            Console.WriteLine("행동을 정해야한다.");
            Console.WriteLine("1. 공격");
            Console.WriteLine("2. 소비 아이템 사용");
            Console.WriteLine("3. 매니저 스펙 확인");
            Console.WriteLine("4. 도주\n");

            string input = Console.ReadLine();

            switch (input)
            {
                case "1": Console.WriteLine("대응 방식 선택");
                    Console.WriteLine("1. 아이템 사용");
                    Console.WriteLine("2. 돌아가기");
                    string attackInput = Console.ReadLine();

                    if (attackInput == "1")
                    {
                        BossMonster_Inventory.BattleItemMenu(player, inventory, ref isAttackItemUsed);
                        if (isAttackItemUsed)
                        {
                            PlayerDamage = 25;
                            isBossAttackSuccess = false;
                        }
                        else
                        {
                            BossDamage = 55;
                            isBossAttackSuccess = true;
                        }
                        BossTurn();
                    }
                    else if (attackInput == "2")
                    {
                        Console.WriteLine("다시 생각했다.");
                        Console.ReadKey(true);
                        PlayerTurn();
                    }
                    else
                    {
                        Console.WriteLine("잘못된 입력");
                        Console.ReadKey(true);
                        PlayerTurn();
                    }
                    break;
                case "2": Console.WriteLine("소비 아이템");
                    Console.ReadKey(true);
                    BossMonster_Inventory.ConsumableItemMenu(player, inventory);
                    BossTurn();
                    break;
                case "3":
                    BossMonster_Status.ShowBossStatus(currentBoss);
                    break;
                case "4": Console.WriteLine("도주할 수 없다.");
                    Console.ReadKey(true);
                    PlayerTurn();
                    break;
                default: Console.WriteLine("잘못된 입력");
                    Console.ReadKey(true);
                    PlayerTurn();
                    break;
            }
        }
        // 보스 턴 전투 계산
        static void BossTurn()
        {
            int totalDamage = 0;

            Console.Clear();
            if (currentBoss.NextSkill?.MainDialogue?.Count > 0)
            {
                Console.WriteLine(currentBoss.NextSkill.MainDialogue[randrange.Next(currentBoss.NextSkill.MainDialogue.Count)]);
            }
            else
            {
                Console.WriteLine("보스 행동 버그 : 게임 종료");
                Environment.Exit(0);
            }
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
                    Console.WriteLine(currentBoss.NextSkill.SuccessDialogue[randrange.Next(currentBoss.NextSkill.SuccessDialogue.Count)]);
                }
                else
                {
                    Console.WriteLine("대미지 계산 버그");
                    Environment.Exit(0);
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
                    Console.WriteLine(currentBoss.NextSkill.FailDialogue[randrange.Next(currentBoss.NextSkill.FailDialogue.Count)]);
                }
                else
                {
                    Console.WriteLine("대미지 계산 버그");
                    Environment.Exit(0);
                }
                PlayerDamage = 0;
            }
            Console.ReadKey(true);
            Console.Clear();
            if (currentBoss.HP <= 0)
            {
                switch (currentBoss.Name)
                {
                    case "관리하는 박찬우 매니저":
                        Console.WriteLine("자유에는... 질서와 통제가... 필요...");
                        break;
                    case "메아리치는 나영웅 매니저":
                        Console.WriteLine("제가 사라져도 언제나 지켜보고 있을겁니다.");
                        break;
                    case "공간 지배의 한효승 매니저":
                        Console.WriteLine("그 실력에 잠이 옵니까? 이해할 수 없군요! 이해할, 수, 크아아아악!");
                        break;

                }
            }
            else if (player.Hp <= 0)
            {
                Console.WriteLine("당신은 '제적'입니다.");
                return;
            }
            else if (currentBoss.parkPhase == 3)
            {
                Console.WriteLine("로마에 가면 로마 법을 따르라!!!! 우오오오오오!!!!");
                Console.ReadKey(true);
                player.InstantDeath();
                return;
            }
            else if (currentBoss.heroTime == 3)
            {
                Console.WriteLine("지옥에 가서도 그 곡을 항상 떠올리며 부지런해지길 바랍니다.");
                Console.ReadKey(true);
                player.InstantDeath();
                return;
            }
            else
            {
                switch (currentBoss.Name)
                {
                    case "관리하는 박찬우 매니저":
                        if (currentBoss.parkPhase == 1)
                        {
                            Console.WriteLine("어디선가 작은 북소리가 들린다...");
                            Console.ReadKey(true);
                        }
                        else if (currentBoss.parkPhase == 2)
                        {
                            Console.WriteLine("북소리가 점점 커지고있다...");
                            Console.ReadKey(true);
                        }
                        PrepareManager_Park(currentBoss);
                        break;
                    case "메아리치는 나영웅 매니저":
                        currentBoss.Def++;
                        Console.WriteLine($"{currentBoss.Name}는 점점 단단해지고 있다.\n전투가 길어지면 불리할 것 같다.");
                        Console.ReadKey(true);
                        if (currentBoss.heroTime > 0)
                        {
                            currentBoss.heroTime++;
                            Console.WriteLine($"{currentBoss.Name}의 장송곡이 끝나기까지 앞으로 {currentBoss.heroTime}번.");
                            Console.ReadKey(true);
                        }
                        PrepareManager_Hero(currentBoss);
                        break;
                    case "공간 지배의 한효승 매니저":
                        currentBoss.reinforceZoomEva++;
                        Console.WriteLine($"{currentBoss.Name}는 매우 빠르게 좌우로 움직이고 있다.");
                        Console.ReadKey(true);
                        if (currentBoss.reinforceZoomAtk == 1)
                        {
                            currentBoss.Atk = 25;
                        }
                        if (currentBoss.reinforceZoomCri == 1)
                        {
                            currentBoss.Critical = 50;
                        }
                        if (currentBoss.reinforceZoomEva != 0)
                        {
                            currentBoss.Evasion = 50;
                        }
                        else
                        {
                            currentBoss.Evasion = 10;
                        }
                        if (currentBoss.reinforceZoomEva == 3)
                        {
                            currentBoss.reinforceZoomEva = 0;
                            Console.WriteLine($"{currentBoss.Name}의 움직임이 원래대로 돌아왔다.");
                        }
                        PrepareManager_Zoom(currentBoss);
                        break;
                }
                PlayerTurn();
            }
        }
    }
}
