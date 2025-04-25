namespace week3
{
    public class Inventory
    {
        private List<Item> items = new List<Item>();
        
        public void AddItem(Item item)
        {
            //Items.Add(item);
            Console.WriteLine($"{item.Name}을(를) 인벤토리에 추가했습니다.");
        }

        public void RemoveItem(Item item)
        {
            //Remove(item);
            Console.WriteLine($"{item.Name}을(를) 인벤토리에서 제거했습니다.");
        }

        public void ShowInventory()
        {
            Console.WriteLine("=== 인벤토리 ===");
            if (items.Count == 0)
            {
                Console.WriteLine("인벤토리가 비어 있습니다.");
                return;
            }

            foreach (Item item in items)
            {
                Console.WriteLine($"- {item.Name} ({item.Price}G) : {item.Description}");
            }
        }
    }
}
