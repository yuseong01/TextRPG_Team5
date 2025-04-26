using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace week3
{
    public class Map
    {
        public List<MapObject> mapObjectList = new List<MapObject>();
        public string mapName {get; set;}
        public bool isClear {get; set;}

        public Map(string mapName)
        {
            this.mapName = mapName;
            isClear = false;
        }
    }   
}
