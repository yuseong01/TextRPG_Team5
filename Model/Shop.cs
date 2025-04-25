using static System.Runtime.InteropServices.JavaScript.JSType;

namespace week3
{
    public class Shop
    {
        private List<Item> shopItems = new List<Item>();
        private InventoryManager inventory;
        private Player player;
        int number = 0;
        public Shop(InventoryManager inventory)
        {
            this.inventory = inventory;
            // 초기 상점 아이템 구성
            shopItems.Add(inventory.ItemData[10]);
            shopItems.Add(inventory.ItemData[11]);
            shopItems.Add(inventory.ItemData[12]);
            shopItems.Add(inventory.ItemData[13]);
            shopItems.Add(inventory.ItemData[14]);
            shopItems.Add(inventory.ItemData[15]);
        }

        public void ShopItemList()
        {
            number = shopItems.Count;
            Console.WriteLine("=== 상점 목록 ===");
            for (int i = 0; i < number; i++)
            {
                Item item = shopItems[i];
                Console.WriteLine($"{i + 1}. {item.Name} ({item.Price}G) - {item.Description}");
            }

        }


        public void BuyItem()
        {
            Console.WriteLine("구매를 원하시는 아이템의 번호를 입력해 주세요.");
            int index = InputManager.GetInt(1, number) - 1;
            int gold = player.Gold;
            if (gold < shopItems[index].Price)
            {
                Console.WriteLine("골드가 부족합니다!");
                ShopItemList();
            }
            else
            {
                inventory.AddItem(index);
                // 플레이어 클래스에 골드를 차감하는 함수를 추가한 후 여기에 넣어주세요.(매개변수로 int gold를 넣어주세요)
                Console.WriteLine($"{shopItems[index].Name}을(를) 구매했습니다!");
                ShopItemList();
            }
        }
    }
}
