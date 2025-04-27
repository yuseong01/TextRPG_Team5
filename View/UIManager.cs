using System.Security.Cryptography.X509Certificates;
using week3;
using System.Numerics;

namespace week3
{
    public class UIManager
    {
        //BossMonster_Data boss;
        // 플레이어 관련
        public void ShowStatus(Player player)
        {
            Console.WriteLine($"이름: {player.Name}");
            Console.WriteLine($"체력 : {player.CurrentHp}");
            Console.WriteLine($"정신력: {player.Spirit}");
            Console.WriteLine($"공격력 : {player.Attack} (+{player.AdditionalAttackPower})");
            Console.WriteLine($"방어력 : {player.Defense} (+{player.AdditionalDefensePower})");
            Console.WriteLine($"ZEB 코인: {player.ZebCoin}");
            Console.WriteLine($"골드: {player.Gold}");

            Console.WriteLine("\n0. 나가기");
        }

        //인벤토리
        public void ShowInventory(List<Item> items)
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

        //맵관련
        public void ShowMap(MapManager.mapType mapType)
        {
            switch (mapType)
            {
                case MapManager.mapType.GroupFiveMap:
                    Console.OutputEncoding = System.Text.Encoding.UTF8;
                    Console.WriteLine(value: Constants.GROUP_FIVE_UI_STRING[0]);
                    break;
                case MapManager.mapType.PassageMap:
                    Console.OutputEncoding = System.Text.Encoding.UTF8;
                    Console.WriteLine(value: Constants.PASSAGE_UI_STRING[0]);
                    break;
                case MapManager.mapType.Manager1RoomMap:
                    Console.OutputEncoding = System.Text.Encoding.UTF8;
                    Console.WriteLine(value: Constants.MANAGER1_ROOM_UI_STRING[0]);
                    break;
                case MapManager.mapType.Manager2RoomMap:
                    Console.OutputEncoding = System.Text.Encoding.UTF8;
                    Console.WriteLine(value: Constants.MANAGER2_ROOM_UI_STRING[0]);
                    break;
                case MapManager.mapType.Manager3RoomMap:
                    Console.OutputEncoding = System.Text.Encoding.UTF8;
                    Console.WriteLine(value: Constants.MANAGER3_ROOM_UI_STRING[0]);
                    break;
                default:
                    break;
            }
        }
        public void ShowMapDescriptionUI(string mapName, List<MapObject> mapObjects)
        {
            Console.WriteLine($"{mapName}에 들어왔다. 도움될만한 물건을 찾아보자.");

            for (int i = 0; i < mapObjects.Count; i++)
            {
                string list = $"{i + 1}. {mapObjects[i].Name}";
                string describe = $"{mapObjects[i].Description}";
                Console.WriteLine(list);
            }
        }
    }
}
