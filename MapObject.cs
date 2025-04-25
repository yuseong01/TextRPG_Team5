using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace week3
{
    internal class MapObject
    {
        public string Name { get; private set; }
        public string Description { get; private set; }
        public ObjectType ObjectType { get; private set; }
        public bool IsOpen { get;  private set; }
        public MapObject(string name, string discription, ObjectType type)
        {
            Name = name;
            Description = discription;
            ObjectType = type;
            IsOpen = false;
        }

        void ChangeIsOpen()
        {
            IsOpen = !IsOpen;
        }

        List<MapObject> mapObjects = new List<MapObject>()
        {
            new MapObject("인벤토리","주머니를 확인해보자.", ObjectType.Inventory),
            new MapObject("컴퓨터", "컴퓨터가 켜져있다. ZEb 상점에 접속할 수 있을 것 같다.",ObjectType.Store),
            new MapObject("캐비넷", "안에 무언가 들어있다. 비밀번호를 입력하면 열 수 있을 것 같다.", ObjectType.Money),
            new MapObject("서류더미", "어지러이 놓여진 서류를 들춰보니 메모지가 보인다.", ObjectType.Quiz),
            new MapObject("쓰레기통","열쇠를 발견했다... 음? 열쇠가 움직인다...?", ObjectType.Monster)
        };

        
    }
    enum ObjectType
    {
        Monster,
        Money,
        Quiz, 
        Store,
        Inventory
    }
}
