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
            Console.WriteLine($"체력 : {player.CurrentHp}");
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
        //보스 몬스터
        public  void ShowBossStatus(BossMonster_Data boss)
        {
            Console.Clear();
            Console.WriteLine($"<매니저 스펙>");
            Console.WriteLine(new string('=', 35));
            Console.WriteLine($"{boss.Name}");
            Console.WriteLine($"공격력: {boss.Atk}");
            Console.WriteLine($"방어력: {boss.Def}");
            Console.WriteLine($"체력: {boss.MaxHP}");
            Console.WriteLine($"정신력: {boss.MaxSpirit}");
            Console.WriteLine($"치명타율: {boss.Critical}%");
            Console.WriteLine($"회피율: {boss.Evasion}%");
            Console.WriteLine(new string('=', 35));
            Console.WriteLine();
            Console.ReadKey(true);
            BossMonster_Battle.PlayerTurn();
        }
        
        //곧 view랑 로직 분리해서 mapManager에 수정예정입니다 
        void Choice(List<MapObject> maps)
        {
            int listCount = maps.Count;

            Console.WriteLine("ㅇㅇ의 방에 들어왔다. 도움될만한 물건을 찾아보자.");

            for (int i = 0; i < listCount; i++)
            {
                string list = $"{i + 1}. {maps[i].Name}";
                string describe = $"{maps[i].Description}";
                Console.WriteLine(list);
            }

            int choose = InputManager.GetInt(1, listCount) - 1;

            string choosedObjectDiscribe = maps[choose].Description;

            Console.WriteLine(choosedObjectDiscribe);
            Thread.Sleep(1000);
            Console.WriteLine("진행하려면 엔터를 누르세요.");
            Console.ReadLine();
        }
    }

}




