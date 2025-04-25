namespace week3
{
    public class InventoryManager
    {
        //여기에 아이템이 있으면 "if 아이템있음?이렇게" 전투에서 사용 
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

    }
}
