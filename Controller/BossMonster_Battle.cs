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
        Player player;
        Item item;
        InventoryManager inventory;
        BossMonster_Data bossMonster;
        BossMonster_Status bossStatus = new BossMonster_Status();
        Random random = new Random();

        List<BossMonster_Skill> normals = new List<BossMonster_Skill>(); // 일반공격 스킬 리스트화
        int playerDamage = 0; // 보스 피격 대미지 저장용
        int bossDamage = 0; // 플레이어 피격 대미지 저장용
        bool isBossAttackSuccess = true;


        void PrintStatusBar() // 체력바
        {
            Console.WriteLine(new string('=', 55));
            Console.WriteLine($"내 체력 : {player.CurrentHp} / {player.MaxHp}  {bossMonster.BossMonsterName}의 체력 : {bossMonster.HP} / {bossMonster.MaxHP}");
            Console.WriteLine(new string('=', 55));
        }
        // 보스 몬스터 조우
        void BossBattle_Park()
        {
            Console.Clear();
            bossMonster.CopyFrom(bossMonster.BossMonsterData["관리하는 박찬우 매니저"]);

            Console.WriteLine($"{bossMonster.BossMonsterName} : 우오오오오오오오!!! 우오오오오오오오오!!");
            Console.WriteLine();
            Console.ReadKey(true);

            PrepareManager_Park(bossMonster);
            PlayerTurn();
        }
        void BossBattle_Hero()
        {
            Console.Clear();

            bossMonster.CopyFrom(bossMonster.BossMonsterData["메아리치는 나영웅 매니저"]);

            Console.WriteLine($"{bossMonster.BossMonsterName} : 인간적인 감정이 없었다면 어려움도 없었겠죠.\n제가 당신의 감정을 없애드리죠.");
            Console.WriteLine();
            Console.ReadKey(true);

            PrepareManager_Hero(bossMonster);
            PlayerTurn();
        }
        void BossBattle_Zoom()
        {
            Console.Clear();

            bossMonster.CopyFrom(bossMonster.BossMonsterData["공간 지배의 한효승 매니저"]);

            Console.WriteLine($"{bossMonster.BossMonsterName} : 그렇게 계속 청개구리처럼 행동할겁니까?");
            Console.WriteLine();
            Console.ReadKey(true);

            PrepareManager_Zoom(bossMonster);
            PlayerTurn();
        }
        BossMonster_Skill FindSkillByName(BossMonster_Data boss, string name) // 스킬 찾는 메서드
        {
            foreach (var s in boss.Skills)
            {
                if (s.SkillName == name)
                {
                    return s;
                }
            }
            return null;
        }
        // 플레이어턴 들어가기 전 각 보스별 행동계산 메서드
        List<BossMonster_Skill> GetNormalSkills(BossMonster_Data boss)
        {
            return boss.Skills.Where(s => s.Type == BossSkillType.Normal).ToList();
        }
        void PrepareManager_Park(BossMonster_Data boss)
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
                    var selected = normals[random.Next(normals.Count)];
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
        void PrepareManager_Hero(BossMonster_Data boss)
        {
            int rand = random.Next(1, 101);
            int rand2 = random.Next(1, 101);

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
                    var selected = normals[random.Next(normals.Count)];
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
        void PrepareManager_Zoom(BossMonster_Data boss)
        {
            int rand = random.Next(1, 101);

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
                    var selected = normals[random.Next(normals.Count)];
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
        public void PlayerTurn()
        {
            Console.Clear();
            PrintStatusBar();
            if (bossMonster.NextSkill != null && bossMonster.NextSkill.HintDialogue.Count > 0)
            {
                string hint = bossMonster.NextSkill.HintDialogue[random.Next(bossMonster.NextSkill.HintDialogue.Count)];
                Console.WriteLine(hint);
            }
            Console.WriteLine("인벤토리를 열어 행동을 정해야한다.");
            Console.WriteLine("1. 인벤토리 확인");
            Console.WriteLine("2. 매니저 스펙 확인");
            Console.WriteLine("3. 도주\n");

            int input = InputManager.GetInt(1, 3);

            switch (input)
            {
                case 1: Console.WriteLine("대응 방식 선택");
                    Console.WriteLine("1. 아이템 사용");
                    Console.WriteLine("2. 돌아가기");
                    int attackInput = InputManager.GetInt(1, 2);

                    if (attackInput == 1)
                    {
                        inventory.ShowInventory();
                        if (item.IsAttackItemUsed)
                        {
                            playerDamage = 25;
                            isBossAttackSuccess = false;
                        }
                        else
                        {
                            bossDamage = 35;
                            isBossAttackSuccess = true;
                        }
                        BossTurn();
                    }
                    else if (attackInput == 2)
                    {
                        Console.WriteLine("다시 생각했다.");
                        Console.ReadKey(true);
                        PlayerTurn();
                    }
                    break;
                case 2:
                    bossStatus.ShowBossStatus(bossMonster); //이거 어딧죠...? 제가 지웠나..? 안지운거같은데
                    break;

                case 3: Console.WriteLine("도주할 수 없다.");
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
         void BossTurn()
        {
            int totalDamage = 0;

            Console.Clear();
            if (bossMonster.NextSkill?.MainDialogue?.Count > 0)
            {
                Console.WriteLine(bossMonster.NextSkill.MainDialogue[random.Next(bossMonster.NextSkill.MainDialogue.Count)]);
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
                player.TakeDamage(bossDamage);
                if (bossMonster.NextSkill != null && bossMonster.NextSkill.SuccessDialogue.Count > 0)
                {
                    Console.WriteLine(bossMonster.NextSkill.SuccessDialogue[random.Next(bossMonster.NextSkill.SuccessDialogue.Count)]);
                }
                else
                {
                    Console.WriteLine("대미지 계산 버그");
                    Environment.Exit(0);
                }
                bossDamage = 0;
            }
            else // 보스가 공격을 실패했다면
            {
                Console.WriteLine("나는 {item}을 사용해서 대처했다.");
                Console.ReadKey(true);
                Console.WriteLine("...");
                Console.ReadKey(true);
                Console.WriteLine("...");
                Console.ReadKey(true);
                bossMonster.HP -= playerDamage;
                if (bossMonster.NextSkill != null && bossMonster.NextSkill.FailDialogue.Count > 0)
                {
                    Console.WriteLine(bossMonster.NextSkill.FailDialogue[random.Next(bossMonster.NextSkill.FailDialogue.Count)]);
                }
                else
                {
                    Console.WriteLine("대미지 계산 버그");
                    Environment.Exit(0);
                }
                playerDamage = 0;
            }
            Console.ReadKey(true);
            Console.Clear();
            if (bossMonster.HP <= 0)
            {
                switch (bossMonster.BossMonsterName)
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
            else if (player.CurrentHp <= 0)
            {
                Console.WriteLine("당신은 '제적'입니다.");
                return;
            }
            else if (bossMonster.parkPhase == 3)
            {
                Console.WriteLine("로마에 가면 로마 법을 따르라!!!! 우오오오오오!!!!");
                Console.ReadKey(true);
                player.Die();
                return;
            }
            else if (bossMonster.heroTime == 3)
            {
                Console.WriteLine("지옥에 가서도 그 곡을 항상 떠올리며 부지런해지길 바랍니다.");
                Console.ReadKey(true);
                player.Die();
                return;
            }
            else
            {
                switch (bossMonster.BossMonsterName)
                {
                    case "관리하는 박찬우 매니저":
                        if (bossMonster.parkPhase == 1)
                        {
                            Console.WriteLine("어디선가 작은 북소리가 들린다...");
                            Console.ReadKey(true);
                        }
                        else if (bossMonster.parkPhase == 2)
                        {
                            Console.WriteLine("북소리가 점점 커지고있다...");
                            Console.ReadKey(true);
                        }
                        PrepareManager_Park(bossMonster);
                        break;
                    case "메아리치는 나영웅 매니저":
                        bossMonster.Def++;
                        Console.WriteLine($"{bossMonster.BossMonsterName}는 점점 단단해지고 있다.\n전투가 길어지면 불리할 것 같다.");
                        Console.ReadKey(true);
                        if (bossMonster.heroTime > 0)
                        {
                            bossMonster.heroTime++;
                            Console.WriteLine($"{bossMonster.BossMonsterName}의 장송곡이 끝나기까지 앞으로 {bossMonster.heroTime}번.");
                            Console.ReadKey(true);
                        }
                        PrepareManager_Hero(bossMonster);
                        break;
                    case "공간 지배의 한효승 매니저":
                        bossMonster.reinforceZoomEva++;
                        Console.WriteLine($"{bossMonster.BossMonsterName}는 매우 빠르게 좌우로 움직이고 있다.");
                        Console.ReadKey(true);
                        if (bossMonster.reinforceZoomAtk == 1)
                        {
                            bossMonster.Atk = 25;
                        }
                        if (bossMonster.reinforceZoomCri == 1)
                        {
                            bossMonster.Critical = 50;
                        }   
                        if (bossMonster.reinforceZoomEva != 0)
                        {
                            bossMonster.Evasion = 50;
                        }
                        else
                        {
                            bossMonster.Evasion = 10;
                        }
                        if (bossMonster.reinforceZoomEva == 3)
                        {
                            bossMonster.reinforceZoomEva = 0;
                            Console.WriteLine($"{bossMonster.BossMonsterName}의 움직임이 원래대로 돌아왔다.");
                        }
                        PrepareManager_Zoom(bossMonster);
                        break;
                }
                PlayerTurn();
            }
        }
    }
}
