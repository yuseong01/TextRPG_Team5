namespace week3
{
    public class Item
    {
        public string Name { get; private set; }
        public string Description { get; private set; }
        public string Type { get; private set; }
        public string IsConsumable { get; set; }
        public int Value { get; private set; }
        public bool IsEquip {  get; private set; }
        public bool IsSold { get; private set; }
        public bool IsUsed { get; set; }
        public int Price { get; private set; }

        public Item(string name, string description, string type, int value, int price)
        {
            Name = name;
            Description = description;
            Type = type;
            Value = value;
            IsEquip = false;
            IsSold = false;
            IsUsed = false;
            Price = price;
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
