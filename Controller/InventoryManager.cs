using week3.Model;

namespace week3
{
    public class InventoryManager
    {
        Item item;
        Player player;
        public List<Item> playerItems = new List<Item>();   //장착아이템
        public List<Item> nomalBattleItemList;  //회복아이템
        public List<Item> bossBattleItemList;   //보스아이템

        public InventoryManager()
        {

        }

        public void ShowInventory(int inventoryType) // inventory 입장 메서드
        {
            while (true)
            {
                Console.Clear();
                if(inventoryType==0)    //비전투
                {
                    for (int i = 0; i < playerItems.Count; i++)
                        {
                            string index = (playerItems[i].IsEquip) ? $"[E]{i + 1}." : $"{i + 1}.";
                            Console.WriteLine($"{index} {playerItems[i].Name,-10}| {playerItems[i].Description,-30}");
                        }
                        Console.Write("장착할 아이템 번호를 선택해주세요. \n나가려면 0을 눌러주세요.>");

                        int inputNum = InputManager.GetInt(0, playerItems.Count);
                        if (inputNum == 0) break;
                        EquipItem(playerItems[inputNum-1]);
                }
                else if(inventoryType==1)   //일반전투
                {
                    for (int i = 0; i < nomalBattleItemList.Count; i++)
                    {
                        Console.WriteLine($"{i + 1} {playerItems[i].Name,-10}| {playerItems[i].Description,-30}");
                    }
                    Console.Write("사용할 아이템 번호를 선택해주세요. \n나가려면 0을 눌러주세요.>");
                    int inputNum = InputManager.GetInt(0, playerItems.Count);
                    if (inputNum == 0) break;
                    UseItem(nomalBattleItemList[inputNum-1]);
                    break;
                }
                else if(inventoryType==2)   //보스전투
                {
                    for (int i = 0; i < bossBattleItemList.Count; i++)
                        {
                            Console.WriteLine($"{i + 1} {bossBattleItemList[i].Name,-10}| {bossBattleItemList[i].Description,-30}");
                        }
                        Console.Write("사용할 아이템 번호를 선택해주세요. \n나가려면 0을 눌러주세요.>");
                        int inputNum = InputManager.GetInt(0, playerItems.Count);
                        if (inputNum == 0) break;
                        UseItem(bossBattleItemList[inputNum-1]);
                        break;
                }
            }
        }

        void EquipItem(Item item)
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

        void UseItem(Item item)
        {
            if(item.Type == "회복")
            {
                nomalBattleItemList.Remove(item);
                Console.WriteLine($"{item.Name}을(를) 사용했습니다.");
            }
            else if(item.Type=="보스")
            {
                bossBattleItemList.Remove(item);
                //아이템 사용 여부 bool값을 바꿔?주기.
                Console.WriteLine($"{item.Name}을(를) 사용했습니다.");
            }
        }
    }
}
