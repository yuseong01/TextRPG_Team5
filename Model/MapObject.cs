using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace week3
{
    public class MapObject
    {

        public string Name { get; private set; }
        public string Description { get; private set; }
        public ObjectType ObjectType { get; private set; }
        public bool IsOpen { get; private set; }

        public MapObject() { }

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

        public void ActivateObject(int chooseNum)
        {
            switch(chooseNum)
            {
                case 0:
                    //보통 몬스터 호출
                    break;
                case 1:
                    //하드 몬스터 호출
                    break;
                case 2:
                    //퀴즈나오는 함수 호출(맞췄을 경우에만 여기서 돈 받는 함수 호출)
                    break;
                case 3: 
                    //인벤토리 호출
                    break;
                case 4:
                    //상점 호출
                    break;
                case 5:
                    //플레이어 스탯창(돈도 보여줌)
                    break;
            }
        }

        
    }
    public enum ObjectType
    {
        Monster,
        Money,
        Quiz,
        Store,
        Inventory
    }
}
