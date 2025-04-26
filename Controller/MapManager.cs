using System.Security.Cryptography.X509Certificates;
using week3;

public class MapManager
{
    UIManager uiManager;
    public List<Map> maps = new List<Map>();
    public List<MapObject> baseMapObject = new List<MapObject>();

    public MapManager(UIManager uiManager)
    {
        this.uiManager = uiManager;

        maps.Add(new Map("5조"));
        maps.Add(new Map("복도"));
        maps.Add(new Map("[박찬우]매니저님의 방"));
        maps.Add(new Map("[나영웅]매니저님의 방"));
        maps.Add(new Map("[한효승]매니저님의 방"));

        baseMapObject.Add(new MapObject("스텟창", "현재 내 상태를 볼까?", ObjectType.PlayerStat));
        baseMapObject.Add(new MapObject("인벤토리", "주머니를 확인해보자.", ObjectType.Inventory));
        baseMapObject.Add(new MapObject("컴퓨터", "컴퓨터가 켜져있다. ZEb 상점에 접속할 수 있을 것 같다.", ObjectType.Store));
        baseMapObject.Add(new MapObject("캐비넷", "안에 무언가 들어있다. 어라?", ObjectType.Money));
        baseMapObject.Add(new MapObject("서류더미", "어지러이 놓여진 서류를 들춰보니 메모지가 보인다.", ObjectType.Quiz));
        baseMapObject.Add(new MapObject("쓰레기통", "열쇠를 발견했다... 음? 열쇠가 움직인다...?", ObjectType.Monster));
        baseMapObject.Add(new MapObject("고장난 자판기", "버튼을 눌러보자 갑자기 기계가 요란하게 흔들리기 시작했다", ObjectType.HardMonster));

        maps[0].mapObjectList.Add(baseMapObject[0]);
        maps[0].mapObjectList.Add(baseMapObject[1]);
        maps[0].mapObjectList.Add(baseMapObject[2]);
        maps[0].mapObjectList.Add(baseMapObject[3]);

        //맵이 가지고 있는 오브젝트리스트에 필요한 각각의 오브젝트를 추가
        for (int i = 2; i < maps.Count; i++)   //5조와 복도는 뺌
        {
            for (int j = 0; j < 5; j++)    //(플레이어상태, 인벤토리, 상점, 돈, 서류더미)는 공통, 몬스터는 이지/하드로 나눠짐
            {
                maps[i].mapObjectList.Add(baseMapObject[j]);
            }
        }

        maps[2].mapObjectList.Add(baseMapObject[5]);  //그냥 몬스터
        maps[3].mapObjectList.Add(baseMapObject[5]);
        maps[4].mapObjectList.Add(baseMapObject[6]);    //하드몬스터
    }
    public void LoadSelectedMap(mapType mapType)
    {
        switch (mapType)
        {
            case mapType.GroupFiveMap:
                ShowPageForSelectedMap(0, mapType, maps[0].mapObjectList, maps[0].mapName);
                break;
            case mapType.PassageMap:
                ShowPageForSelectedMap(1, mapType, maps[1].mapObjectList, maps[1].mapName);
                break;
            case mapType.Manager1RoomMap:
                ClearIsOpen();
                ShowPageForSelectedMap(2, mapType, maps[2].mapObjectList, maps[2].mapName);
                //보스몬스터 전투 호출
                break;
            case mapType.Manager2RoomMap:
                ClearIsOpen();
                ShowPageForSelectedMap(3, mapType, maps[3].mapObjectList, maps[3].mapName);
                //보스몬스터 전투 호출
                break;
            case mapType.Manager3RoomMap:
                ClearIsOpen();
                ShowPageForSelectedMap(4, mapType, maps[4].mapObjectList, maps[4].mapName);
                //보스몬스터 전투 호출
                break;
        }
    }

    public void ClearIsOpen()
    {
        baseMapObject[3].IsOpen=false;
        baseMapObject[4].IsOpen=false;
        baseMapObject[5].IsOpen=false;
    }

    void ShowPageForSelectedMap(int mapTypeNumber, mapType mapType, List<MapObject> mapObjects, string mapName)
    {
        while (true)
        {
            CheckMapClear(maps[mapTypeNumber]);
            if(maps[mapTypeNumber].isClear)
            {
                
                break;
            }

            Thread.Sleep(700);
            Console.Clear();

            uiManager.ShowMap(mapType);

            uiManager.ShowMapDescriptionUI(mapName, mapObjects);

            int choose = InputManager.GetInt(1, mapObjects.Count) - 1;

            ActivateObject(mapObjects[choose]);
            mapObjects[choose].IsOpen = true;
        }
    }

    public void CheckMapClear(Map map)
    {
        Console.WriteLine($"현재 맵 이름: {map.mapName}");
        if (map.mapName == "5조")
        {
            if (map.mapObjectList[3].IsOpen)
            {
                Console.WriteLine("이제 여기서 나갈수 있는 것 같다. 한번 밖으로 나가보자");
                map.isClear = true;
            }
        }
        else if (map.mapName == "복도")
        {
            Thread.Sleep(700);
            Console.WriteLine("복도이다");
            map.isClear = true;
        }
        else
        {
            if (map.mapObjectList[3].IsOpen && map.mapObjectList[4].IsOpen && map.mapObjectList[5].IsOpen)
            {
                Console.WriteLine("이제 여기서 나갈수 있는 것 같다. 한번 밖으로 나가보자");
                //보스몬스터 등장
                map.isClear = true;
            }
        }
    }

    public void ActivateObject(MapObject mapObject)
    {
        Console.WriteLine(mapObject.Description);
        switch (mapObject.ObjectType)
        {
            case ObjectType.PlayerStat:
                //플레이어 스탯창 함수 호출
                Console.WriteLine("스탯창 보여줌");
                break;
            case ObjectType.Inventory:
                //인벤토리 함수 호출
                Console.WriteLine("인벤토리창 보여줌");
                break;
            case ObjectType.Store:
                //상점 함수 호출
                Console.WriteLine("상점 보여줌");
                break;
            case ObjectType.Money:
                //그냥 돈줌
                Console.WriteLine("러키머니 겟");
                break;
            case ObjectType.Quiz:
                //퀴즈나오는 함수 호출(맞췄을 경우에만 여기서 돈 받는 함수 호출)
                Console.WriteLine("퀴즈맞춰야 돈줌");
                break;
            case ObjectType.Monster:
                //몬스터 전투 함수 호출
                Console.WriteLine("일반몬스터");
                break;
            case ObjectType.HardMonster:
                //몬스터 전투 함수 호출
                Console.WriteLine("하드몬스터");
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