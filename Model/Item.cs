namespace week3
{
    public class Item
    {
        public string Name { get; private set; }
        public string Description { get; private set; }
        public string Type { get; private set; }
        public int Value { get; private set; }
        public bool IsEquip {  get; private set; }
        public bool IsSold { get; private set; }
        public int Price { get; private set; }
        public bool AttackItemUsed { get; private set; }


        //보스 몬스터 전투 전용
        public Item(string name, string description, string type, int price)
        {
            Name = name;
            Description = description;
            Type = type;
            IsSold = false;
            AttackItemUsed = false;
            Price = price;
        }

        public Item(string name, string description, string type, int value, int price)
        {
            Name = name;
            Description = description;
            Type = type;
            Value = value;
            IsEquip = false;
            IsSold = false;
            Price = price;
        }

        public void ToggleAttackItemUsed(Item item)
        {
            AttackItemUsed = !AttackItemUsed;
        }
        public void ToggleEquipStatus(Item item)
        {
            IsEquip = !IsEquip;
        }
        public void ToggleSoldStatus(Item item)
        {
            IsSold = !IsSold;
        }
    }

}
