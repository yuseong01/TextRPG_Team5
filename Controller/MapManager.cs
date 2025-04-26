using System.Security.Cryptography.X509Certificates;
using week3;

public class MapManager
{
    UIManager uiManager;
    public List<Map> maps = new List<Map>();
    public List<MapObject> baseMapObject = new List<MapObject>();
    Map map = new Map();

    public MapManager(UIManager uiManager)
    {
        this.uiManager = uiManager;

        maps.Add(new Map());       //GroupFiveMap
        maps.Add(new Map());       //PassageMap
        maps.Add(new Map());       //Manager1RoomMap
        maps.Add(new Map());       //Manager2RoomMap
        maps.Add(new Map());       //Manager3RoomMap

        baseMapObject.Add(new MapObject("인벤토리", "주머니를 확인해보자.", ObjectType.Inventory));
        baseMapObject.Add(new MapObject("컴퓨터", "컴퓨터가 켜져있다. ZEb 상점에 접속할 수 있을 것 같다.", ObjectType.Store));
        baseMapObject.Add(new MapObject("캐비넷", "안에 무언가 들어있다. 비밀번호를 입력하면 열 수 있을 것 같다.", ObjectType.Money));
        baseMapObject.Add(new MapObject("서류더미", "어지러이 놓여진 서류를 들춰보니 메모지가 보인다.", ObjectType.Quiz));
        baseMapObject.Add(new MapObject("쓰레기통", "열쇠를 발견했다... 음? 열쇠가 움직인다...?", ObjectType.Monster));

        //맵이 가지고 있는 오브젝트리스트에 필요한 각각의 오브젝트를 추가
        maps[0].mapObjectList.Add(baseMapObject[0]);
        maps[0].mapObjectList.Add(baseMapObject[1]);

        maps[1].mapObjectList.Add(baseMapObject[0]);
        maps[1].mapObjectList.Add(baseMapObject[1]);
        maps[1].mapObjectList.Add(baseMapObject[3]);
        maps[1].mapObjectList.Add(baseMapObject[4]);

        maps[2].mapObjectList.Add(baseMapObject[0]);
        maps[2].mapObjectList.Add(baseMapObject[1]);
        maps[2].mapObjectList.Add(baseMapObject[2]);

        maps[3].mapObjectList.Add(baseMapObject[0]);
        maps[3].mapObjectList.Add(baseMapObject[1]);
        maps[3].mapObjectList.Add(baseMapObject[2]);

        maps[4].mapObjectList.Add(baseMapObject[0]);
        maps[4].mapObjectList.Add(baseMapObject[3]);
        maps[4].mapObjectList.Add(baseMapObject[4]);
    }
    public void LoadSelectedMap(mapType mapType) //ManageMap(요기 불러오는거는 나중에 gameManager에서 하는걸로 변경하기) 
    {

        switch (mapType)
        {
            case mapType.GroupFiveMap:
                uiManager.ShowMap(mapType);
                Choice(maps[0].mapObjectList);
                break;
            case mapType.PassageMap:
                uiManager.ShowMap(mapType);
                Choice(maps[1].mapObjectList);
                break;
            case mapType.Manager1RoomMap:
                uiManager.ShowMap(mapType);
                Choice(maps[2].mapObjectList);
                break;
            case mapType.Manager2RoomMap:
                uiManager.ShowMap(mapType);
                Choice(maps[3].mapObjectList);
                break;
            case mapType.Manager3RoomMap:
                uiManager.ShowMap(mapType);
                Choice(maps[3].mapObjectList);
                break;
        }
    }

    //곧 view랑 로직 분리해서 mapManager에 수정예정입니다 
    void Choice(List<MapObject> mapObjects)
    {
        Console.WriteLine("ㅇㅇ의 방에 들어왔다. 도움될만한 물건을 찾아보자.");

        for (int i = 0; i < mapObjects.Count; i++)
        {
            string list = $"{i + 1}. {mapObjects[i].Name}";
            string describe = $"{mapObjects[i].Description}";
            Console.WriteLine(list);
        }

        int choose = InputManager.GetInt(1, mapObjects.Count) - 1;

        string choosedObjectDiscribe = mapObjects[choose].Description;

        Console.WriteLine(choosedObjectDiscribe);
        Thread.Sleep(1000);
        Console.WriteLine("진행하려면 엔터를 누르세요.");
        Console.ReadLine();
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