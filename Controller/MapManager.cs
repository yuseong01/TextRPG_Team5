using System.Security.Cryptography.X509Certificates;
using week3;

public class MapManager
{
    UIManager uiManager;
    Map map = new Map();

    public MapManager(UIManager uiManager)
    {
        this.uiManager = uiManager;
    }
    public void LoadSelectedMap(mapType mapType) //ManageMap(요기 불러오는거는 나중에 gameManager에서 하는걸로 변경하기) 
    {

        switch (mapType)
        {
            case mapType.GroupFiveMap:
                
                uiManager.ShowMap(mapType);
                map.GroupFiveMap();
                break;
            case mapType.PassageMap:
                uiManager.ShowMap(mapType);
                map.PassageMap();
                break;
            case mapType.Manager1RoomMap:
                uiManager.ShowMap(mapType);
                map.Manager1RoomMap();
                break;
            case mapType.Manager2RoomMap:
                uiManager.ShowMap(mapType);
                map.Manager2RoomMap();
                break;
            case mapType.Manager3RoomMap:
                uiManager.ShowMap(mapType);
                map.Manager3RoomMap();
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