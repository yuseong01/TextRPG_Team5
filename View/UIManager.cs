using System.Security.Cryptography.X509Certificates;
using week3;
﻿using System.Numerics;

namespace week3
{
    public class UIManager
    {
        // 플레이어 관련
        public void ShowStatus(Player player)
        {
            Console.WriteLine($"이름: {player.Name}");
            Console.WriteLine($"체력 : {player.Hp}");
            Console.WriteLine($"정신력: {player.Spirit}");
            Console.WriteLine($"공격력 : {player.BaseAttackPower + player.additionalAttackPower} (+{player.additionalAttackPower})");
            Console.WriteLine($"방어력 : {player.BaseDefense + player.additionalDefensePoser} (+{player.additionalDefensePoser})");
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
                    Console.WriteLine(value: Constants.MANAGER_ROOM_UI_STRING[0]);
                    break;
                case MapManager.mapType.Manager2RoomMap:
                    Console.OutputEncoding = System.Text.Encoding.UTF8;
                    Console.WriteLine(value: Constants.MANAGER_ROOM_UI_STRING[0]);
                    break;
                case MapManager.mapType.Manager3RoomMap:
                    Console.OutputEncoding = System.Text.Encoding.UTF8;
                    Console.WriteLine(value: Constants.MANAGER_ROOM_UI_STRING[0]);
                    break;
                default:
                    break;
            }
        }
    }

}




