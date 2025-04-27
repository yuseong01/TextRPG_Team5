public class BossMonsterSkill
{
        public string SkillName { get; set; }
        public int SkillDamage { get; set; }
        public int Type { get; set; }
        public List<string> HintDialogue = new List<string>();
        public List<string> MainDialogue = new List<string>();
        public List<string> SuccessDialogue = new List<string>();
        public List<string> FailDialogue = new List<string>();
}