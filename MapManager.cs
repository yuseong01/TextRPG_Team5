using System.Security.Cryptography.X509Certificates;
using week3;

public class MapManager
{
    public MapObject[] map = new MapObject[4];
    public MapManager()
    {
        
    }

    public void ShowMap(mapType mapType)
    {
        switch(mapType)
        {
            case mapType.GroupFiveMap:
                Console.OutputEncoding = System.Text.Encoding.UTF8;
                Console.WriteLine(value: Constants.GROUP_FIVE_UI_STRING[0]);
                break;
            case mapType.PassageMap:
                Console.OutputEncoding = System.Text.Encoding.UTF8;
                Console.WriteLine(value: Constants.PASSAGE_UI_STRING[0]);
                break;
            case mapType.Manager1RoomMap:
                Console.OutputEncoding = System.Text.Encoding.UTF8;
                Console.WriteLine(value: Constants.MANAGER_ROOM_UI_STRING[0]);
                break;
            case mapType.Manager2RoomMap:
                Console.OutputEncoding = System.Text.Encoding.UTF8;
                Console.WriteLine(value: Constants.MANAGER_ROOM_UI_STRING[0]);
                break;
            case mapType.Manager3RoomMap:
                Console.OutputEncoding = System.Text.Encoding.UTF8;
                Console.WriteLine(value: Constants.MANAGER_ROOM_UI_STRING[0]);
                break;
            default:
                break;

        }

    }

    public enum mapType
    {
        GroupFiveMap,
        PassageMap,
        Manager1RoomMap,
        Manager2RoomMap,
        Manager3RoomMap

    }
 
}