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
        
        //인벤토리
        private Inventory inventory = new Inventory();

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


        // 보상 시스템 추가

        public void AddZebCoin(int amount)
        {
            ZebCoin += amount;
            Console.WriteLine($"[시스템] {Name}의 ZebCoin이 {amount} 증가! (현재 : {ZebCoin})");
        }

        public void AddGold(int amount)
        {
            Gold += amount;
            Console.WriteLine($"[시스템] {Name}의 Gold가 {amount} 증가! (현재: {Gold})");
        }

        // 아이템 보상 시스템 추가 
        // itemName CS1503 오류 (타입 불일치) 로 인한 주석처리 -> Inventory 클래스가 Item 객체를 요구하도록 설계되었으나 문자열 전달
        public void AddItem(string itemName)
        {
            //inventory.AddItem(itemName);
            //Console.WriteLine($"[획득] {itemName}을(를) 얻었습니다!");
        }

        // 몬스터 데미지

        public void TakeDamage(float damage)
        {
            float reducedDamage = damage - Def;
            HP -= Math.Max(1, reducedDamage); //최소 1 데미지
            Console.WriteLine($"[전투] {Name}이(가) {Math.Max(1, reducedDamage)} 데미지 입음 (HP: {HP})");
        }
        //즉사 메서드
        public void InstantDeath()
        {
            HP = 0;
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
            this.HP -= damage;
        }
        public void ReduceSpirit(float amount)
        {
            Spirit -= amount;
            Console.WriteLine($"[전투] {Name}의 정신력 - {amount} (현재: {Spirit})");
        }
    }
}
