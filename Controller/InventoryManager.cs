using week3.Model;

namespace week3
{
    public class InventoryManager
    {

        Player player;
        public List<Item> epuipItemList = new List<Item>();   //장착아이템
        public List<Item> healItemList = new List<Item>();  //회복아이템
        public List<Item> bossBattleItemList = new List<Item>();  //보스아이템

        public InventoryManager(Player player)
        {
            this.player = player;
        }



        public void ShowInventory(int inventoryType) // inventory 입장 메서드
        {
            while (true)
            {
                Console.Clear();

                if (inventoryType == 0)    //비전투
                {

                    Console.WriteLine("원하시는 기능을 선택해 주세요.\n나가려면 0을 눌러주세요.>");
                    Console.WriteLine("1. 장비 아이템");
                    Console.WriteLine("2. 회복 아이템");
                    Console.WriteLine("0. 나가기");

                    int inputNum = InputManager.GetInt(0, 2);

                    if (inputNum == 0)
                    {
                        break;
                    }
                    else if (inputNum == 1)
                    {
                        while (true)
                        {
                        ShowEquipItemList();
                        Console.WriteLine("장착할 아이템 번호를 선택해주세요. \n나가려면 0을 눌러주세요.>");
                        inputNum = InputManager.GetInt(0, epuipItemList.Count);
                        if (inputNum == 0) break;
                        EquipItem(epuipItemList[inputNum - 1]);
                        }
                    }
                    else
                    {
                        while(true)
                        { 
                        ShowHealItemList();
                        Console.WriteLine("사용할 아이템 번호를 선택해주세요. \n나가려면 0을 눌러주세요.>");
                        inputNum = InputManager.GetInt(0, healItemList.Count);
                        if (inputNum == 0) break;
                        UseItem(healItemList[inputNum - 1]);
                        }
                    }

                }
                else if (inventoryType == 1)   //일반전투
                {
                    while(true)
                    {
                    int inputNum = 0;
                    ShowHealItemList();
                    Console.WriteLine("사용할 아이템 번호를 선택해주세요. \n나가려면 0을 눌러주세요.>");
                    inputNum = InputManager.GetInt(0, healItemList.Count);
                    if (inputNum == 0) break;
                    UseItem(healItemList[inputNum - 1]);
                    }

                }
                else if (inventoryType == 2)   //보스전투
                {
                    int inputNum = 0;
                    Console.WriteLine("원하시는 기능을 선택해 주세요.\n나가려면 0을 눌러주세요.>");
                    Console.WriteLine("1. 보스몬스터 전용 아이템");
                    Console.WriteLine("2. 회복 아이템");
                    Console.WriteLine("0. 나가기");

                    inputNum = InputManager.GetInt(0, 2);

                    if (inputNum == 0)
                    {
                        break;
                    }
                    else if (inputNum == 1)
                    {

                        ShowBossBattleItemList();
                        Console.WriteLine("사용할 아이템 번호를 선택해주세요. \n나가려면 0을 눌러주세요.>");
                        inputNum = InputManager.GetInt(0, bossBattleItemList.Count);
                        if (inputNum == 0) break;
                        bossBattleItemList[inputNum - 1].IsUsed = true;
                        UseItem(bossBattleItemList[inputNum - 1]);

                    }
                    else
                    {
                        while (true)
                        {
                            ShowHealItemList();
                            Console.WriteLine("사용할 아이템 번호를 선택해주세요. \n나가려면 0을 눌러주세요.>");
                            inputNum = InputManager.GetInt(0, healItemList.Count);
                            if (inputNum == 0) break;
                            UseItem(healItemList[inputNum - 1]);
                        }
                    }
                }
            }



            void ShowEquipItemList()
            {
                Console.Clear();
                for (int i = 0; i < epuipItemList.Count; i++)
                {
                    string valueText = "";
                    if (epuipItemList[i].Type == "무기")
                    {
                        valueText = $"공격력 +{epuipItemList[i].Value}";
                    }
                    else if (epuipItemList[i].Type == "방어구")
                    {
                        valueText = $"방어력 +{epuipItemList[i].Value}";
                    }

                    string index = (epuipItemList[i].IsEquip) ? $"[E]{i + 1}." : $"{i + 1}.";
                    Console.WriteLine($"{index} {epuipItemList[i].Name,-10}|{valueText}|{epuipItemList[i].Description,-30}");
                    Console.WriteLine();
                }
            }

            void ShowHealItemList()
            {
                Console.Clear();
                for (int i = 0; i < healItemList.Count; i++)
                {
                    Console.WriteLine($"{i + 1}. {healItemList[i].Name,-10}|회복 +{healItemList[i].Value}|{healItemList[i].Description,-30}");
                    Console.WriteLine();
                }
            }

            void ShowBossBattleItemList()
            {
                Console.Clear();
                for (int i = 0; i < bossBattleItemList.Count; i++)
                {
                    Console.WriteLine($"{i + 1}. {bossBattleItemList[i].Name,-10}|{bossBattleItemList[i].Description,-30}");
                    Console.WriteLine();
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
                if (item.Type == "회복")
                {
                    healItemList.Remove(item);
                    int itemValue = item.Value;
                    player.Heal(itemValue);
                    Console.WriteLine($"{item.Name}을(를) 사용했습니다.");
                }
                else if (item.Type == "보스")
                {
                    bossBattleItemList.Remove(item);
                    //아이템 사용 여부 bool값을 바꿔?주기.
                    Console.WriteLine($"{item.Name}을(를) 사용했습니다.");
                }
            }
        }
    }
}
