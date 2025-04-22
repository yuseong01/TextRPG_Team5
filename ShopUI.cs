namespace week3
{
    public class ShopUI
    {
        private Shop shop;

        public ShopUI(Shop shop)
        {
            this.shop = shop;
        }

        public void ShowShop()
        {
            Console.WriteLine("=== 상점 목록 ===");
            for (int i = 0; i < shop.ShopItems.Count; i++)
            {
                Item item = shop.ShopItems[i];
                Console.WriteLine($"{i + 1}. {item.Name} ({item.Price}G) - {item.Description}");
            }
        }

        public void BuyItem(int index, InventoryUI inventoryUI, ref int gold)
        {
            if (index < 0 || index >= shop.ShopItems.Count)
            {
                Console.WriteLine("존재하지 않는 아이템입니다.");
                return;
            }

            Item selected = shop.ShopItems[index];
            if (gold < selected.Price)
            {
                Console.WriteLine("골드가 부족합니다!");
                return;
            }

            gold -= selected.Price;
            inventoryUI.AddItem(selected);
            Console.WriteLine($"{selected.Name}을(를) 구매했습니다!");
        }
    }
}
