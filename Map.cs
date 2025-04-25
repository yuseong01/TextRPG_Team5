using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace week3
{
    internal class Map
    {
        public void GroupFiveMap()
        {
            List<MapObject> groupFiveMapObject = new List<MapObject>();
            groupFiveMapObject.Add(mapObjectList[0]);
            groupFiveMapObject.Add(mapObjectList[1]);

            Choice(groupFiveMapObject);
        }

        public void PassageMap()
        {
            List<MapObject> passageMapObject = new List<MapObject>();
            passageMapObject.Add(mapObjectList[0]);
            passageMapObject.Add(mapObjectList[1]);
            passageMapObject.Add(mapObjectList[3]);
            passageMapObject.Add(mapObjectList[4]);

            Choice(passageMapObject);
        }
        public void Manager1RoomMap()
        {
            List<MapObject> manager1RoomObject = new List<MapObject>();
            manager1RoomObject.Add(mapObjectList[0]);
            manager1RoomObject.Add(mapObjectList[1]);
            manager1RoomObject.Add(mapObjectList[2]);

            Choice(manager1RoomObject);
        }
        public void Manager2RoomMap()
        {
            List<MapObject> manager2RoomObject = new List<MapObject>();
            manager2RoomObject.Add(mapObjectList[0]);
            manager2RoomObject.Add(mapObjectList[1]);
            manager2RoomObject.Add(mapObjectList[2]);

            Choice(manager2RoomObject);
        }
        public void Manager3RoomMap()
        {
            List<MapObject> manager3RoomObject = new List<MapObject>();
            manager3RoomObject.Add(mapObjectList[0]);
            manager3RoomObject.Add(mapObjectList[3]);
            manager3RoomObject.Add(mapObjectList[4]);

            Choice(manager3RoomObject);
        }

        void Choice(List<MapObject> maps)
        {
            int listCount = maps.Count;


            Console.Clear();

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

        List<MapObject> mapObjectList = new List<MapObject>()
        {
            new MapObject("인벤토리", "주머니를 확인해보자.", ObjectType.Inventory),
            new MapObject("컴퓨터", "컴퓨터가 켜져있다. ZEb 상점에 접속할 수 있을 것 같다.", ObjectType.Store),
            new MapObject("캐비넷", "안에 무언가 들어있다. 비밀번호를 입력하면 열 수 있을 것 같다.", ObjectType.Money),
            new MapObject("서류더미", "어지러이 놓여진 서류를 들춰보니 메모지가 보인다.", ObjectType.Quiz),
            new MapObject("쓰레기통", "열쇠를 발견했다... 음? 열쇠가 움직인다...?", ObjectType.Monster)
        };
    }
}
