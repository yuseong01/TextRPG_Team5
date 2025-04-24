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

        private static Random randrange = new();

        private static int parkPhase = 0; // 스킬 페이즈 제어
        private static int parkCombo = 0; // 일반공격 성공 횟수

        // 테스트용 던전 진입 메서드

        public static void BossBattle()
        {
            Console.Clear();

            BossMonster_Data boss1 = new BossMonster_Data(BossMonster_Data.BossMonsterData["박찬우 매니저"]);

            Console.WriteLine($"{boss1.Name} : 어서오세요... 나의 공간에...");
            Console.WriteLine();
            Console.ReadKey(true);

            PrepareManager_Park(boss1);
            PlayerTurn(); // 그 다음 플레이어 턴
        }

        //private static void Boss_Select(string key, BossMonster_Data boss)
        //{
        //    switch (key)
        //    {
        //        case "박찬우 매니저": PrepareManager_Park(boss);
        //            break;
        //        case "나영웅 매니저": PrepareManager_Park(boss);
        //            break;
        //        case "한효승 매니저": PrepareManager_Park(boss);
        //            break;
        //        default: Console.WriteLine("보스 처리 실패 : 키 불일치.\n게임 종료.");
        //            break;
        //    }
        //}
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
        public static void PrepareManager_Park(BossMonster_Data boss1)
        {
            List<BossMonster_Skill> normals = new List<BossMonster_Skill>(); // 일반공격 스킬 리스트화
            var skill1 = FindSkillByName(boss1, "자유의 함성");
            var skill2 = FindSkillByName(boss1, "질서의 함성");
            var skill3 = FindSkillByName(boss1, "통제의 함성");
            
            int choice = randrange.Next(1, 101);

            foreach (var s in boss1.Skills)
            {
                if (s.Type == BossSkillType.Normal)
                {
                    normals.Add(s);
                }
            }
            if (normals.Count > 0)
            {
                var selected = normals[randrange.Next(normals.Count)];
                boss1.NextSkillHint = selected;
                boss1.IsNextSkill = true;
                HintDialogue(selected);
                parkCombo++;
            }
            else // 예외처리
            {
                boss1.IsNextSkill = false;
                Console.WriteLine("일반공격 불가 : 게임 종료");
            }

            if (boss1.HP <= 40 && parkPhase == 0 && skill1 != null)
            {
                boss1.NextSkillHint = skill1;
                boss1.IsNextSkill = true;
                HintDialogue(skill1);
                parkPhase = 1;
                parkCombo = 0;
                return;
            }
            if (parkPhase == 1 && parkCombo >= 3 && skill2 != null)
            {
                boss1.NextSkillHint = skill2;
                boss1.IsNextSkill = true;
                HintDialogue(skill2);
                parkPhase = 2;
                parkCombo = 0;
                return;
            }
            if (parkPhase == 2 && parkCombo >= 2 && skill3 != null)
            {
                boss1.NextSkillHint = skill3;
                boss1.IsNextSkill = true;
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
            Console.WriteLine("행동을 정해야한다.");
            Console.WriteLine("1. 공격");
            Console.WriteLine("2. 소비 아이템 사용");
            Console.WriteLine("3. 도주\n");

            string input = Console.ReadLine();

            switch (input)
            {
                case "1": Console.WriteLine("공격 아이템 출력 예정");
                    Console.ReadKey(true);
                    PlayerTurn();
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
            Console.ReadKey(true);
        }

        static void BossTurn()
        {
        }
    }
}
