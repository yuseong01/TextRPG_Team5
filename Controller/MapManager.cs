using System.Security.Cryptography.X509Certificates;
using System.Threading.Channels;
using week3;

public class MapManager
{
    UIManager uiManager;
    public List<Map> maps = new List<Map>();
    public List<MapObject> baseMapObject = new List<MapObject>();
    Player player;
    Shop shop;
    MonsterBattleManager monsterBattleManager;

    public MapManager(UIManager uiManager, Player player, Shop shop, MonsterBattleManager monsterBattleManager)
    {
        this.player = player;
        this.uiManager = uiManager;
        this.shop = shop;
        this.monsterBattleManager = monsterBattleManager;

        maps.Add(new Map("5조"));
        maps.Add(new Map("복도"));
        maps.Add(new Map("[박찬우]매니저님의 방"));
        maps.Add(new Map("[나영웅]매니저님의 방"));
        maps.Add(new Map("[한효승]매니저님의 방"));

        baseMapObject.Add(new MapObject("스텟창", "현재 내 상태를 볼까?", ObjectType.PlayerStat));
        baseMapObject.Add(new MapObject("인벤토리", "주머니를 확인해보자.", ObjectType.Inventory));
        baseMapObject.Add(new MapObject("컴퓨터", "컴퓨터가 켜져있다. ZEb 상점에 접속할 수 있을 것 같다.", ObjectType.Store));
        baseMapObject.Add(new MapObject("캐비넷", "안에 무언가 들어있다. 어라?", ObjectType.Money));
        baseMapObject.Add(new MapObject("쓰레기통", "열쇠를 발견했다... 음? 열쇠가 움직인다...?", ObjectType.Monster));
        baseMapObject.Add(new MapObject("고장난 자판기", "버튼을 눌러보자 갑자기 기계가 요란하게 흔들리기 시작했다", ObjectType.HardMonster));

        maps[0].mapObjectList.Add(baseMapObject[0]);
        maps[0].mapObjectList.Add(baseMapObject[1]);
        maps[0].mapObjectList.Add(baseMapObject[2]);
        maps[0].mapObjectList.Add(baseMapObject[3]);

        //맵이 가지고 있는 오브젝트리스트에 필요한 각각의 오브젝트를 추가
        for (int i = 2; i < maps.Count; i++)   //5조와 복도는 뺌
        {
            for (int j = 0; j < 4; j++)    //(플레이어상태, 인벤토리, 상점, 돈)은 공통, 몬스터는 이지/하드로 나눠짐
            {
                maps[i].mapObjectList.Add(baseMapObject[j]);
            }
        }

        maps[2].mapObjectList.Add(baseMapObject[4]);  //그냥 몬스터
        maps[3].mapObjectList.Add(baseMapObject[4]);
        maps[4].mapObjectList.Add(baseMapObject[5]);    //하드몬스터
    }
    public void LoadSelectedMap(mapType mapType)
    {
        switch (mapType)
        {
            case mapType.GroupFiveMap:
                ShowPageForSelectedMap(0, mapType, maps[0].mapObjectList, maps[0].mapName);
                break;
            case mapType.PassageMap:
                uiManager.ShowMap(mapType.PassageMap);
                passageMapFlow();
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
        baseMapObject[3].IsOpen = false;
        baseMapObject[4].IsOpen = false;
        baseMapObject[5].IsOpen = false;
    }

    void ShowPageForSelectedMap(int mapTypeNumber, mapType mapType, List<MapObject> mapObjects, string mapName)
    {
        while (true)
        {
            CheckMapClear(maps[mapTypeNumber]);
            if (maps[mapTypeNumber].isClear)
            {
                break;
            }

            Thread.Sleep(700);
            Console.Clear();

            uiManager.ShowMap(mapType);

            uiManager.ShowMapDescriptionUI(mapName, mapObjects);

            int choose = InputManager.GetInt(1, mapObjects.Count) - 1;

            if (mapObjects[choose].IsOpen)
            {
                Console.WriteLine("더이상 조사할 필요는 없을 것 같다.");
            }
            else
            {
                ActivateObject(mapObjects[choose]);
                mapObjects[choose].IsOpen = true;
            }
        }
    }

    public void CheckMapClear(Map map)
    {
        if (map.mapName == "5조")
        {
            if (map.mapObjectList[3].IsOpen)
            {
                CanExit();
                map.isClear = true;
            }
        }
        else if (map.mapName == "복도")
        {
            Thread.Sleep(700);
        }
        else
        {
            if (map.mapObjectList[3].IsOpen && map.mapObjectList[4].IsOpen)
            {
                CanExit();
                //보스몬스터 등장
                map.isClear = true;
            }
        }
    }
    public void CanExit()
    {
        Console.WriteLine("\n\n이제 여기서 나갈수 있는 것 같다. 한번 밖으로 나가보자");
        Console.WriteLine("진행하려면 Enter를 누르세요.");
        Console.ReadLine();
    }

    public void passageMapFlow()
    {
        Console.WriteLine("복도로 나왔다. 어디로 갈것인가?");
        Console.WriteLine("1.왼쪽\n2.오른쪽\n3.직진");
        Console.Write("입력>");
        int inputNum = InputManager.GetInt(1, 3);

        Random random = new Random();
        int randomValue = random.Next(0, 3); // 0,1,2 중 랜덤

        if (randomValue == 0)
        {
            // 무서운 얼굴 배열 통째로 가져오기
            Console.WriteLine("어..? 무언가가 보인다");
            Thread.Sleep(700);
            Console.Clear();
            foreach (var face in Constants.SCARED_FACE_STRING)
            {
                Console.WriteLine(face);
                Thread.Sleep(700);
            }
            Thread.Sleep(700);
        }
        else if (randomValue == 1)
        {
            Thread.Sleep(700);
            Console.Clear();
            Console.WriteLine("반짝이는 Gold를 발견했다!");
            player.AddGold(1000);
            Console.WriteLine("진행하려면 [Enter]를 입력하세요");
            Console.ReadLine();
            Console.WriteLine("진행하려면 [Enter]를 입력하세요");
            Console.ReadLine();
        }
        else
        {
            Thread.Sleep(700);
            Console.Clear();
            Console.WriteLine("아무 일도 일어나지 않았다...");
            Console.WriteLine("진행하려면 [Enter]를 입력하세요");
            Console.ReadLine();
        }
        
    }



    public void ActivateObject(MapObject mapObject)
    {
        Console.WriteLine(mapObject.Description);
        switch (mapObject.ObjectType)
        {
            case ObjectType.PlayerStat:
                player.ShowPlayerStat();
                break;
            case ObjectType.Inventory:
                player.inventoryManager.ShowInventory(0);  //0은 비전투
                break;
            case ObjectType.Store:
                shop.ShowShop();
                break;
            case ObjectType.Money:
                player.AddGold(500);
                break;
            case ObjectType.Monster:
                monsterBattleManager.StartGroupBattle(player, true);
                break;
            case ObjectType.HardMonster:
                monsterBattleManager.StartGroupBattle(player, false);
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