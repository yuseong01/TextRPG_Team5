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
        public bool IsOpen { get; set; }

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
        
    }
    public enum ObjectType
    {
        PlayerStat,
        Inventory,
        Store,
        Money,
        Monster,
        HardMonster,
    }
}
