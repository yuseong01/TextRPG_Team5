namespace week3
{
    public class Item
    {
        public string Name { get; private set; }
        public string Description { get; private set; }
        public string Type { get; private set; }
        public int Price { get; private set; }

        public Item(string name, string description, string type, int price)
        {
            Name = name;
            Description = description;
            Type = type;
            Price = price;
        }
    }

}
