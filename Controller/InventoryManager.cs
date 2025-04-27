using week3.Model;

namespace week3
{
    public class InventoryManager
    {
        Item item;
        PlayerBattleController playerBattleController;
        BossMonster_Battle battle;

        public List<Item> bossBattleItem = new List<Item>();
        public List<Item> alwaysAvailableItems= new List<Item>();
        public List<Item> equipmentItems = new List<Item>();

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
                // UseOrEquipItem(itemIndex);
            }
        }

        void AllItemList(bool isBattle)
        {
            Console.Clear();
            for (int i = 0; i < playerItems.Count; i++)
            {
                string index = (playerItems[i].IsEquip) ? "[E]" : $"{i + 1}.";

                string item = $"{index} {playerItems[i].Name,-10}| {playerItems[i].Description,-30}";
                Console.WriteLine(item);
            }
            Console.WriteLine("장착하거나 사용할 아이템 번호를 선택해주세요. \n나가려면 0을 눌러주세요.");
        }

        void UseItem(int itemIndex)
        {
            item = playerItems[itemIndex];

            if (item.Type == "무기" || item.Type == "방어구")
            {
                item.ToggleEquipStatus(item);

                if (item.IsEquip)
                {
                    playerBattleController.ApplyEquipmentStats(item);
                }
                else
                {
                    playerBattleController.RemoveEquipmentStats(item);
                }
            }
            else if (playerItems[itemIndex].Type == "회복")
            {
                UseItem(itemIndex);
            }
            else
            {
                Console.WriteLine($"{item.Name}을(를) 사용했다.");
                battle.ToggleIsIsAttackItemUsed();
            }
        }

        void UseItem(int itemIndex)
        {
            playerItems.Remove(playerItems[itemIndex]);
            Console.WriteLine($"{playerItems[itemIndex]}을(를) 사용했습니다.");
        }


    }
}
