using static week3.UIManager;

namespace week3
{
    public  class GameManager
    {
        Player player;
        UIManager uiManager;
        MapManager mapManager;
        public GameManager() 
        {
            player = new Player("수민", "학생");
            uiManager = new UIManager();
            mapManager = new MapManager();
        }
        public void GameStart() 
        {
            //uiManager.ShowGameIntroUI();
            //Console.ReadLine(); // 화면 유지

            mapManager.LoadSelectedMap(MapManager.mapType.GroupFiveMap); //0번째 맵으로 들어감(5조)
            mapManager.LoadSelectedMap(MapManager.mapType.PassageMap); //복도
            mapManager.LoadSelectedMap(MapManager.mapType.Manager1RoomMap); //매니저1방
            mapManager.LoadSelectedMap(MapManager.mapType.PassageMap); //복도
            mapManager.LoadSelectedMap(MapManager.mapType.Manager2RoomMap); //매니저2방
            mapManager.LoadSelectedMap(MapManager.mapType.PassageMap); //복도
            mapManager.LoadSelectedMap(MapManager.mapType.Manager3RoomMap); //매니저3방

            
        }
    }
}
