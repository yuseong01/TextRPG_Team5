using static System.Runtime.InteropServices.JavaScript.JSType;

namespace week3
{
    public class Shop
    {
        List<Item> shopItems;
        InventoryManager inventory;
        Player player;
        Item item;

        int number;

        public Shop()
        {
            shopItems = new List<Item>();

            // 초기 상점 아이템 구성
            shopItems.Add(inventory.ItemData[12]);
            shopItems.Add(inventory.ItemData[13]);
            shopItems.Add(inventory.ItemData[14]);
            shopItems.Add(inventory.ItemData[15]);
            shopItems.Add(inventory.ItemData[16]);
            shopItems.Add(inventory.ItemData[17]);
        }

        private void AllShopItemList()
        {
            number = shopItems.Count;

            Console.WriteLine("=== 상점 목록 ===");
            for (int i = 0; i < number; i++)
            {
                Item item = shopItems[i];
                string isSold = (shopItems[number].IsSold) ? "판매 완료" : $"{item.Price,-4}";
                 

                Console.WriteLine($"{i + 1}. {item.Name,-10}| {item.Description,-30}|({item.Price,-4}G)");
            }
        }


        private void BuyItem()
        {
            Console.WriteLine("구매를 원하시는 아이템의 번호를 입력해 주세요.\n나가고 싶으시다면 0을 눌러주세요.");
            int inputNum = InputManager.GetInt(1, number);

            if (inputNum == 0) return;

            int index = inputNum - 1;
            int gold = player.Gold;

            Item selectedItem = shopItems[index];

            if (gold < selectedItem.Price)
            {
                Console.WriteLine("골드가 부족합니다!");
                Thread.Sleep(1500);
                Console.Clear();
                AllShopItemList();
            }
            else if (selectedItem.IsSold)
            {
                Console.WriteLine("이미 구매한 아이템 입니다.");
                Thread.Sleep(1500);
                Console.Clear();
                AllShopItemList();
            }
            else
            {
             
                inventory.AddItem(index);
                player.SpendGold(selectedItem.Price);
                if (selectedItem.Type == "회복") // 아이템 타입을 확인하고 회복 아이템이면 판매완료 문구 띄우지 않음.
                {

                }
                else
                {
                    item.ToggleSoldStatus(selectedItem);
                }

                Thread.Sleep(1500);
                Console.Clear();
                AllShopItemList();
            }
        }

        public void EnterShop()
        {
            AllShopItemList();
            BuyItem();
        }
    }
}
