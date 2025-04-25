namespace week3
{
    public class Shop
    {
        private List<Item> shopItems = new List<Item>();
        private Inventory inventory;
        
        public Shop(Inventory inventory)
        {
            this.inventory = inventory;
            // 초기 상점 아이템 구성
            shopItems.Add(new Item("이름1", "설명1", 1100));
            shopItems.Add(new Item("이름2", "설명2", 1200));
            shopItems.Add(new Item("이름3", "설명3", 1300));
        }
        
        public void ShowShop()
        {
            Console.WriteLine("=== 상점 목록 ===");
            for (int i = 0; i < shopItems.Count; i++)
            {
                Item item = shopItems[i];
                Console.WriteLine($"{i + 1}. {item.Name} ({item.Price}G) - {item.Description}");
            }
        }

        public void BuyItem(int index, ref int gold)
        {
            if (index < 0 || index >= shopItems.Count)
            {
                Console.WriteLine("존재하지 않는 아이템입니다.");
                return; 
            }

            Item selected = shopItems[index];
            if (gold < selected.Price)
            {
                Console.WriteLine("골드가 부족합니다!");
                return;
            }

            gold -= selected.Price;
            inventory.AddItem(selected);
            Console.WriteLine($"{selected.Name}을(를) 구매했습니다!");
        }
    }
}
