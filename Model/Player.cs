namespace week3
{
    public class Player
    {
        public string Name { get; private set; }
        public int BaseAttackPower { get; set; }
        public int BaseDefense { get; set; }
        public int additionalAttackPower {get; set;}
        public int additionalDefensePoser {get; set;}
        public int AttackPower { get; set; }
        public int Defense { get; set; }

        public int CurrentHp { get; private set; }
        public int MaxHp { get; private set; }
        public int Gold { get; private set; }
        public int ZebCoin { get; set; }

        public int Spirit { get; private set; }


        public InventoryManager inventory = new InventoryManager();

        // 생성자에서 이름과 직업만 설정
        public Player()
        {
            BaseAttackPower = 10;
            BaseDefense = 5;
            AttackPower = 0;
            Defense = 0;
            CurrentHp = 90;
            MaxHp = 100;
            Gold = 1500;
            ZebCoin = 0;
        }

        public void GetPlayerName() 
        {
            Console.Write("당신의 이름은? :");
            string name = Console.ReadLine();
            Name = name;
        }


        // 보상 시스템
        public void AddZebCoin(int value)
        {
            ZebCoin += value;
            Console.WriteLine($"[시스템] {Name}의 ZebCoin이 {value} 증가! (현재 : {ZebCoin})");
        }

        public void AddGold(int value)
        {
            Gold += value;
            Console.WriteLine($"[시스템] {Name}의 Gold가 {value}G 증가! (현재: {Gold})");
        }

        // 재화 차감 시스템
        public void SpendGold(int value)
        {
            Gold -= value;
            if( Gold < 0 ) { Gold = 0; }
            Console.WriteLine($"[시스템] {Name}의 Gold가 {value}G 감소! (현재: {Gold})");
        }

        //스탯 증가 메서드
        public void Heal(int amount)
        {
            CurrentHp += amount;

            if( CurrentHp > MaxHp ) { CurrentHp = MaxHp; }
        }

        //스탯 감소 메서드
        public void TakeDamage(int amount)
        {
            CurrentHp -= amount;

            if (CurrentHp < 0)
            {
                CurrentHp = 0;
                Die();
            }
        }

        //정신력 감소 메서드
        public void ReduceSpirit(int amount)
        {
            Spirit -= amount;
            if( Spirit < 0 )
            { 
                Spirit = 0;
                Die();
            }

            Console.WriteLine($"[전투] {Name}의 정신력 - {amount} (현재: {Spirit})");
        }


        //사망 판정
        public void Die()
        {
            Console.WriteLine("... 몸이 점점 무거워지고 눈 앞이 흐려진다... 출석... 해야하는데...");
            Console.WriteLine("GAME OVER");
            // 시작화면? 호출? 적절한 메서드 호출하기.
        }
    }
}
