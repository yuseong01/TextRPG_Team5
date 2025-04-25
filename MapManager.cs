using System.Security.Cryptography.X509Certificates;
using week3;

public class MapManager
{
    MapUI mapUI = new MapUI();
    List<MapObject> mapObjectList = new List<MapObject>();

    public MapManager()
    {
        mapObjectList.Add(new MapObject("인벤토리", "주머니를 확인해보자.",  ObjectType.Inventory));
        mapObjectList.Add(new MapObject("컴퓨터", "컴퓨터가 켜져있다. ZEb 상점에 접속할 수 있을 것 같다.", ObjectType.Store));
        mapObjectList.Add(new MapObject("캐비넷", "안에 무언가 들어있다. 비밀번호를 입력하면 열 수 있을 것 같다.", ObjectType.Money));
        mapObjectList.Add(new MapObject("서류더미", "어지러이 놓여진 서류를 들춰보니 메모지가 보인다.", ObjectType.Quiz));
        mapObjectList.Add(new MapObject("쓰레기통", "열쇠를 발견했다... 음? 열쇠가 움직인다...?", ObjectType.Monster));
    }

    public void LoadSelectedMap(mapType mapType) //ManageMap(요기 불러오는거는 나중에 gameManager에서 하는걸로 변경하기) 
    {
        mapUI.ShowMap(mapType);
        Console.WriteLine("ㅇㅇ의 방에 들어왔다. 도움될만한 물건을 찾아보자.");
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