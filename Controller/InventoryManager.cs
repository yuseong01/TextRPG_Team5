using week3.Model;

namespace week3
{
    public class InventoryManager
    {
        Item item;
        Player player;
        public List<Item> playerItems = new List<Item>();
        public List<Item> battleItemList;
        public List<Item> nonBattleItemList;

        public InventoryManager()
        {
            
        }

        public void ShowInventory(bool isBattle) // inventory 입장 메서드
        {
            while (true)
            {
                AllItemList(isBattle);
                int inputNum = InputManager.GetInt(0, playerItems.Count);
                if (inputNum == 0) break;
                int itemIndex = inputNum - 1;
                UseOrEquipItem(itemIndex);
            }
        }

        void AllItemList(bool isBattle)
        {
            Console.Clear();
            if(isBattle)
            {
                battleItemList = playerItems.Where(item => item.Type == "보스" || item.Type == "회복").ToList();
                for (int i = 0; i < battleItemList.Count; i++)
                {
                    string item = $"{i + 1} {battleItemList[i].Name,-10}| {battleItemList[i].Description,-30}";
                    Console.WriteLine(item);
                }
                Console.WriteLine("사용할 아이템 번호를 선택해주세요. \n나가려면 0을 눌러주세요.");
            }
            else
            {
                nonBattleItemList = playerItems.Where(item => item.Type == "회복" || item.Type == "방어구" || item.Type == "무기").ToList();
                for (int i = 0; i < nonBattleItemList.Count; i++)
                {
                    string index = (nonBattleItemList[i].IsEquip) ? "[E]" : $"{i + 1}.";

                    string item = $"{index} {nonBattleItemList[i].Name,-10}| {nonBattleItemList[i].Description,-30}";
                    Console.WriteLine(item);
                }
                Console.WriteLine("장착하거나 사용할 아이템 번호를 선택해주세요. \n나가려면 0을 눌러주세요.");
            }
                
        }

        void UseOrEquipItem(int itemIndex)
        {
            item = playerItems[itemIndex];

            if (item.Type == "무기" || item.Type == "방어구")
            {
                item.ToggleEquipStatus(item);

                if (item.IsEquip)
                {
                    player.ApplyEquipmentStats(item);
                }
                else
                {
                    player.RemoveEquipmentStats(item);
                }
            }
            else if (playerItems[itemIndex].Type == "회복"|| playerItems[itemIndex].Type =="보스")
            {
                UseItem(itemIndex);
            }
            else
            {
                Console.WriteLine($"{item.Name}을(를) 사용했다.");
                bossBattle.ToggleIsIsAttackItemUsed();
            }
        }

        void UseItem(int itemIndex)
        {
            playerItems.Remove(playerItems[itemIndex]);
            Console.WriteLine($"{playerItems[itemIndex]}을(를) 사용했습니다.");
        }


    }
}
