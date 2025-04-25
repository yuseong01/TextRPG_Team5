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

        public int Hp { get; set; }
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


        // 보상 시스템 추가
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

        // 골드 차감
        public void SpendGold(int value)
        {
            Gold -= value;
            Console.WriteLine($"[시스템] {Name}의 Gold가 {value}G 감소! (현재: {Gold})");
        }


        // 아이템 보상 시스템 추가 
        // itemName CS1503 오류 (타입 불일치) 로 인한 주석처리 -> Inventory 클래스가 Item 객체를 요구하도록 설계되었으나 문자열 전달
        public void AddItem(int index)
        {
            //inventory.AddItem(itemName);
            //Console.WriteLine($"[획득] {itemName}을(를) 얻었습니다!");
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
