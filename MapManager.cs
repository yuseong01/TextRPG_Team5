using System.Security.Cryptography.X509Certificates;
using week3;

public class MapManager
{
    MapUI mapUI = new MapUI();
    MapObject[] mapObjects = new MapObject[4]; //맵오브젝트는 각각의 맵에 있는 사물. //5조, 매니저룸1,2,3 이렇게 총 4개 필요함

    public MapManager(){
        
    }
    public void ManageMap() //ManageMap(요기 불러오는거는 나중에 gameManager에서 하는걸로 변경하기)
    {
        mapUI.ShowMap(mapType.GroupFiveMap);
        InputManager.GetInt(1,3);   //플레이어스탯, 인벤토리, 오브젝트
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