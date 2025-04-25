namespace week3
{
    public class Player
    {
        public string Name { get; set; }
        public int BaseAttackPower { get; set; }
        public int BaseDefense { get; set; }
        public int additionalAttackPower {get; set;}
        public int additionalDefensePoser {get; set;}
        public int AttackPower { get; set; }
        public int Defense { get; set; }

        public int Hp { get; private set; }
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
            Hp = 90;
            Gold = 1500;
            ZebCoin = 0;
        }

        // 이름을 입력 받는 메서드
        public void GetPlayerName() // UI매니저에 있는 거 지우기
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
            Console.WriteLine($"[시스템] {Name}의 Gold가 {value}G 감소! (현재: {Gold})");
        }

        //스탯 감소 메서드
        public void TakeDamage(int amount)
        {
            Hp -= amount;
        }

        //스탯 증가 메서드
        public void Heal(int amount)
        {
            Hp += amount;
        }
        // 몬스터 데미지

        public void TakeDamage(int damage)
        {
            int reducedDamage = damage - Defense;
            Hp -= Math.Max(1, reducedDamage); //최소 1 데미지
            Console.WriteLine($"[전투] {Name}이(가) {Math.Max(1, reducedDamage)} 데미지 입음 (HP: {Hp})");
        }
        //즉사 메서드
        public void InstantDeath()
        {
            Hp = 0;
            Console.WriteLine("Game Over");
        }
        // 보스 대미지 계산 메서드
        public void TakeDamageWithChance(int damage, int critRate, int evasionRate)
        {
            Random randrange = new Random();
            int rand = randrange.Next(100);

            if (rand < evasionRate)
            {
                Console.WriteLine("다행히 매니저의 공격을 피했다.");
                return;
            }

            if (rand < evasionRate + critRate)
            {
                damage *= 2;
                Console.WriteLine("매니저의 눈이 번뜩이며 평소보다 강한 공격이 들어온다!");
            }
            this.Hp -= damage;
        }
        public void ReduceSpirit(int amount)
        {
            Spirit -= amount;
            Console.WriteLine($"[전투] {Name}의 정신력 - {amount} (현재: {Spirit})");
        }
    }
}
