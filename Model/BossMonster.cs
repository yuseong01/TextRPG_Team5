using System;
using System.Collections.Generic;
using week3;

namespace week3
{
    /*public class BossMonster
    {
        public string Name { get; set; }
        public string Script {get; set;}
        public int Atk { get; set; }
        public int Def { get; set; }
        public int MaxHP { get; set; }
        public int Hp{ get; set; }

        //각각 1마리씩 총 3마리
        //총 3마리는 각각 스킬셋을 가지고 있음
    
        public int Critical { get; set; }
        public int Evasion { get; set; }
        public bool IsBossDead { get; set; }
        public bool IsNextSkill { get; set; }
        public BossMonsterSkill NextSkill { get; set; }
        public List<BossMonsterSkill> Skills { get; set; } = new List<BossMonsterSkill>();
        public int parkPhase { get; set; } = 0; // 관리하는 박찬우 매니저 스킬 제어용
        public int parkCombo { get; set; } = 0; // 관리하는 박찬우 매니저 스킬 제어용
        public int heroTime { get; set; } = 0; // 메아리치는 나영웅 매니저 스킬 제어용
        public int reinforceZoomAtk { get; set; } = 0; // 공간 지배의 한효승 매니저 스킬 제어용
        public int reinforceZoomCri { get; set; } = 0; // 공간 지배의 한효승 매니저 스킬 제어용
        public int reinforceZoomEva { get; set; } = 0; // 공간 지배의 한효승 매니저 스킬 제어용

        Player player;
        public BossMonster(string name, string script, int atk, int def, int hp, int maxHp, int crt, int evasion) // 보스 몬스터 생성자
        {
            Name = name;
            Script = script;
            Atk = atk;
            Def = def;
            Hp = hp;
            MaxHP=maxHp;
            Critical = crt;
            Evasion = evasion;
        }

        

        public BossMonster_Data() // 스킬 정보에 대한 생성자
        {
            var park = BossMonsterData["관리하는 박찬우 매니저"];
            var hero = BossMonsterData["메아리치는 나영웅 매니저"];
            var zoom = BossMonsterData["공간 지배의 한효승 매니저"];

            // 기본공격 1
            var normalAtk1 = new BossMonster_Skill { SkillName = "존재 검사", SkillDamage = 10, Type = BossSkillType.Normal };
            normalAtk1.HintDialogue.Add("매니저의 눈이 좌우로 빠르게 움직이며 입술이 일그러진다.");
            normalAtk1.MainDialogue.AddRange(new[]
            {
                $"\"{player.Name}님, 지금 자리에 있으면 '자리에 있습니다.' 라고 반응하세요.\"\n매니저는 당신의 존재를 확인한다.",
                $"\"{player.Name}님, 지금 개인공부중이었다면 '공부중이었습니다.' 라고 반응하세요.\"\n매니저는 당신의 존재를 확인한다.",
                $"\"{player.Name}님, 지금 기기에 문제가 없다면 '캠과 마이크 모두 정상입니다.' 라고 반응하세요.\"\n매니저는 당신의 존재를 확인한다."
            });
            normalAtk1.SuccessDialogue.AddRange(new[]
            {
                $"\"존재의 증명을 할 수 없는 자는 응당한 벌을 받아야합니다.\"",
                "\"누가 멋대로 자리를 비우라고 했죠?\"",
                "\"놀러온게 아니실텐데 생각보다 간절하지 않나보군요.\""
            });
            normalAtk1.FailDialogue.AddRange(new[]
{
                "\"제가 너무 민감했나보네요.\"",
                "\"저희는 언제나 주기적으로 같은 루틴을 취할겁니다.\"",
                "매니저의 얼굴이 원래대로 돌아간다."
            });
            // 기본공격 2
            var normalAtk2 = new BossMonster_Skill { SkillName = "캠 검사", SkillDamage = 10, Type = BossSkillType.Normal };
            normalAtk2.HintDialogue.Add("매니저가 공허한 눈빛으로 동공을 확장시키기 시작했다.");
            normalAtk2.MainDialogue.AddRange(new[]
            {
                "\"당신의 모습을 확인하겠습니다.\"\n매니저는 당신의 움직임을 확인한다.",
            });
            normalAtk2.SuccessDialogue.AddRange(new[]
            {
                "\"누가 여기서 캠을 꺼도 된다고 허락했죠?\"",
                "\"당신만 유일하게 이곳에서 캠을 켜지 않는군요.\"",
                "\"계속 어둠 뒤에 숨어서 움직일 수 있을거라 생각했나요?\""
            });
            normalAtk2.FailDialogue.AddRange(new[]
{
                "\"...... 제법이군요.\"",
                "\"매니저의 동공이 원래대로 수축한다.\"",
                "\"다음에 또 검사하겠습니다.\"",
            });
            // 기본공격 3
            var normalAtk3 = new BossMonster_Skill { SkillName = "마이크 검사", SkillDamage = 10, Type = BossSkillType.Normal };
            normalAtk3.HintDialogue.Add("매니저의 귀가 점점 커지며 힘줄이 확장된다.");
            normalAtk3.MainDialogue.AddRange(new[]
            {
                $"\"{player.Name}, 대답하세요.\"\n매니저는 당신의 목소리를 확인한다.",
            });
            normalAtk3.SuccessDialogue.AddRange(new[]
            {
                "\"침묵의 울림은 정말 참을 수 없군요.\"",
                "\"저를 위한 침묵인가요?\"",
                "\"저는 대답을 하라고 했을텐데요?\""
            });
            normalAtk3.FailDialogue.AddRange(new[]
{
                "매니저의 귀가 원래의 크기로 돌아간다.",
                "\"제가 귀청소에 조금 소홀했나보군요.\"",
                "\"중요한 건 아니지만 확인이 필요했습니다.\"",
            });
            var normalSkills = new List<BossMonster_Skill> { normalAtk1, normalAtk2, normalAtk3 };

            foreach (var boss in new[] { park, hero, zoom })
            {
                foreach (var skill in normalSkills)
                {
                    var clonedSkill = new BossMonster_Skill // 스킬 대사 부분이 공유될 수 있어서 복사된 스킬 선언
                    {
                        SkillName = skill.SkillName,
                        Type = skill.Type,
                        HintDialogue = new List<string>(skill.HintDialogue),
                        MainDialogue = new List<string>(skill.MainDialogue),
                        SuccessDialogue = new List<string>(skill.SuccessDialogue),
                        FailDialogue = new List<string>(skill.FailDialogue)
                    };
                    boss.Skills.Add(clonedSkill);
                }
            }

            // 관리하는 박찬우 매니저 1번 스킬
            var parkSkill1 = new BossMonster_Skill { SkillName = "자유의 함성", Type = BossSkillType.Skill };
            parkSkill1.HintDialogue.Add("관리하는 박찬우 매니저가 눈을 번뜩인다.");
            parkSkill1.MainDialogue.Add($"\"자유에는 책임이 따른다... 아시죠?\"\n{park.BossMonsterName}가 \'자유의 함성\'을 외친다.");
            parkSkill1.SuccessDialogue.Add("\"봐드리는 것도 3번까지입니다.\"북소리가 들려온다.");
            parkSkill1.FailDialogue.Add("\"잘 하고 있군요.\"\n북소리가 사라진다.");
            park.Skills.Add(parkSkill1);
            // 관리하는 박찬우 매니저 2번 스킬
            var parkSkill2 = new BossMonster_Skill { SkillName = "질서의 함성", Type = BossSkillType.Skill };
            parkSkill2.HintDialogue.Add("관리하는 박찬우 매니저의 동공이 수축한다.");
            parkSkill2.MainDialogue.Add($"\"이정도면 꽤 넘어가드린 것 같습니다.\"\n{park.BossMonsterName}가 '질서의 함성'을 외친다.");
            parkSkill2.SuccessDialogue.Add("\"즐길땐 몰랐겠죠?\"\n북소리가 커졌다.");
            parkSkill2.FailDialogue.Add("\"제 착각이었나요...\"\n북소리가 사라진다.");
            park.Skills.Add(parkSkill2);
            // 관리하는 박찬우 매니저 3번 스킬
            var parkSkill3 = new BossMonster_Skill { SkillName = "통제의 함성", Type = BossSkillType.Skill };
            parkSkill3.HintDialogue.Add("관리하는 박찬우 매니저의 이마 위에 한줄기 힘줄이 선명하게 나타난다.");
            parkSkill3.MainDialogue.Add($"\"저의 인내심을 시험하는 겁니까아아아아아아!\"\n{park.BossMonsterName}가 '통제의 함성'을 외친다.");
            parkSkill3.SuccessDialogue.Add("\"개인 면담을 시작해보죠.\"\n북소리가 너무 커서 말소리가 잘 들리지 않는다.");
            parkSkill3.FailDialogue.Add("\"대머리 아니야!\"\n북소리가 사라진다.");
            park.Skills.Add(parkSkill3);
            // 메아리치는 나영웅 매니저 1번 스킬
            var heroSkill1 = new BossMonster_Skill { SkillName = "데스 노트", Type = BossSkillType.Skill };
            heroSkill1.HintDialogue.Add("메아리치는 나영웅 매니저가 데이터를 확인하기 시작한다.");
            heroSkill1.MainDialogue.AddRange(new[]
            {
                $"\"서약서는 제출 하셨나요?\"\n{hero.BossMonsterName}가 \'데스 노트\'를 꺼낸다.",
                $"\"TIL은 제출 하셨나요?\"\n{hero.BossMonsterName}가 '데스 노트'를 꺼낸다.",
                $"\"구글폼은 제출 하셨나요?\"\n{hero.BossMonsterName}가 '데스 노트'를 꺼낸다."
            });
            heroSkill1.SuccessDialogue.Add($"\"공지를 확인하지 않은 자는 죽어도 할 말이 없습니다.\"\n{hero.BossMonsterName}가 '데스 노트'에 무언가를 휘갈긴다.");
            heroSkill1.FailDialogue.Add("\"칫... 잘 제출하셨군요.\"");
            hero.Skills.Add(heroSkill1);
            // 메아리치는 나영웅 매니저 2번 스킬
            var heroSkill2 = new BossMonster_Skill { SkillName = "관통 사격", Type = BossSkillType.Skill };
            heroSkill2.HintDialogue.Add("메아리치는 나영웅 매니저의 안경이 밝게 빛난다.");
            heroSkill2.MainDialogue.Add($"\"저는 사격이 취미거든요. 쏘는걸 좋아하고 그걸 맞는 대상을 바라보는 것을 좋아하죠.\"\n{hero.BossMonsterName}는 품 속에서 권총을 꺼낸다.");
            heroSkill2.SuccessDialogue.Add("\"인간에겐 너무 아팠습니까?\"\n메아리치는 나영웅 매니저는 쾌감을 느끼며 사악하게 웃는다.");
            heroSkill2.FailDialogue.Add("\"조금 더 연습해야겠군요.\"\n총알이 간신히 빗나갔다.");
            hero.Skills.Add(heroSkill2);
            // 메아리치는 나영웅 매니저 3번 스킬
            var heroSkill3 = new BossMonster_Skill { SkillName = "장송곡", Type = BossSkillType.Skill };
            heroSkill3.HintDialogue.Add("메아리치는 나영웅 매니저가 음악이 나오는 클론 아바타를 소환하려 한다.");
            heroSkill3.MainDialogue.Add($"\"당신은 집어삼켜지겠죠... 이 음악과 함께 사라지시길...\"");
            heroSkill3.SuccessDialogue.Add($"\"당신을 위한, 3번 들으면 죽는 장송곡...\"\n{hero.BossMonsterName}의 클론이 소름끼치는 연주를 하기 시작했다.");
            heroSkill3.FailDialogue.Add($"\"제 테스트를 방해하다니...!\"\n{hero.BossMonsterName}의 클론이 사라진다.");
            hero.Skills.Add(heroSkill3);
            // 공간 지배의 한효승 매니저 1번 스킬
            var zoomSkill1 = new BossMonster_Skill { SkillName = "영역전개 : SOOW", Type = BossSkillType.Skill };
            zoomSkill1.HintDialogue.Add("공간 지배의 한효승 매니저가 ZOOM 필드를 구축하기 시작했다.\n막지 않으면 앞으로의 전투가 힘들어질 것 같다.");
            zoomSkill1.MainDialogue.Add($"\"모두 이곳으로 들어올 수 밖에 없습니다.\"\n{zoom.BossMonsterName}는 \'영역전개 : SOOW\'을 발동했다.");
            zoomSkill1.SuccessDialogue.Add($"\"다수를 위해 소수를 챙길 수 없는 법이죠. 여긴 나의 공간입니다.\"\n{zoom.BossMonsterName}의 체벌 강도가 올라간다.");
            zoomSkill1.FailDialogue.Add($"\"노트북을 미리 충전해뒀어야 했는데... 실수군요.\"\n{zoom.BossMonsterName}의 공간이 사라진다.");
            zoom.Skills.Add(zoomSkill1);
            // 공간 지배의 한효승 매니저 2번 스킬
            var zoomSkill2 = new BossMonster_Skill { SkillName = "주방 경험", Type = BossSkillType.Skill };
            zoomSkill2.HintDialogue.Add("공간 지배의 한효승 매니저의 품에서 날붙이가 번쩍인다.\n막지 않으면 앞으로의 전투가 힘들어질 것 같다.");
            zoomSkill2.MainDialogue.Add($"\"한번쯤은 요리해보고 싶었습니다. '특별한 재료'를 말이죠.\"\n{zoom.BossMonsterName}는 손에 쉐프의 칼을 쥐고있다.");
            zoomSkill2.SuccessDialogue.Add($"\"조금 많이 아플수도 있습니다?\"\n{zoom.BossMonsterName}의 공격이 치명적일 확률이 늘어난다.");
            zoomSkill2.FailDialogue.Add("\"제법이군요. 훌륭한 경계태세입니다.\"");
            zoom.Skills.Add(zoomSkill2);
            // 공간 지배의 한효승 매니저 3번 스킬
            var zoomSkill3 = new BossMonster_Skill { SkillName = "견문색", Type = BossSkillType.Skill };
            zoomSkill3.HintDialogue.Add("공간 지배의 한효승 매니저가 조용히 눈을 감는다.");
            zoomSkill3.MainDialogue.Add($"\"저도 캠프 출신이라는 것을 잊은건 아니겠죠? 하하.\"\n{zoom.BossMonsterName}의 회피능력이 올라간다.");
            zoomSkill3.SuccessDialogue.Add($"\"이미 다 파악했습니다.\"\n{zoom.BossMonsterName}는 당분간 높은 확률로 나의 공격을 피할 것 같다.");
            zoomSkill3.FailDialogue.Add("\"한시름 덜었군요. 제가 아니라 당신이요.\"");
            zoom.Skills.Add(zoomSkill3);
        }
        public void TakeDamageWithChance(int damage, int critRate, int evasionRate)
        {
            Random random = new Random();
            int rand = random.Next(100);

            if (rand < evasionRate)
            {
                Console.WriteLine("매니저는 대미지를 입지 않았다.");
                return;
            }

            if (rand < evasionRate + critRate)
            {
                damage *= 2;
                Console.WriteLine("회심의 공격!");
            }
            this.HP -= damage;
        }

        public void ShowBossStatus(BossMonster_Data boss)
        {
            Console.Clear();
            Console.WriteLine($"<매니저 스펙>");
            Console.WriteLine(new string('=', 35));
            Console.WriteLine($"{boss.BossMonsterName}");
            Console.WriteLine($"공격력: {boss.Atk}");
            Console.WriteLine($"방어력: {boss.Def}");
            Console.WriteLine($"체력: {boss.MaxHP}");
            Console.WriteLine($"정신력: {boss.MaxSpirit}");
            Console.WriteLine($"치명타율: {boss.Critical}%");
            Console.WriteLine($"회피율: {boss.Evasion}%");
            Console.WriteLine(new string('=', 35));
            Console.WriteLine();
            Console.ReadKey(true);
        }
    }*/
}