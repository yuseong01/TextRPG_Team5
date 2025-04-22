namespace week3
{
    public class Player
    {
        // 이름과 직업은 입력받음
        public string Name { get; private set; }
        public string Level { get; private set; }
        public string Job { get; private set; }

        // 스탯들은 기본값 설정
        public float HP { get; private set; }
        public float Spirit { get; private set; }
        public float Atk { get; private set; }
        public float Def { get; private set; }

        public int ZebCoin { get; private set; }
        public int Gold { get; private set; }

        // 생성자에서 이름과 직업만 설정
        public Player(string name, string job)
        {
            Name = name;
            Job = job;
            HP = 100f;
            Spirit = 80f;
            Atk = 10f;
            Def = 5f;
            ZebCoin = 0;
            Gold = 2000;
        }
    }
}
