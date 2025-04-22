namespace week3
{


    public class Player
    {
        public string playerName;
        public string job;

        public float hp = 100f;
        public float spi = 80f; //정신력

        public float atk = 10f;
        public float def = 5f;

        public int coin = 0;
        public int gold = 2000;


        public Player (string playerName, string job, float hp, float atk, float def, float spi, int coin, int gold)
        {
            this.playerName = playerName;
            this.job = job;
            this.hp = hp;
            this.spi = spi;
            this.atk = atk;
            this.def = def;
            this.coin = coin;
            this.gold = gold;
        }

        public void ShowStatus() 
        {
           
        }
    }
}
