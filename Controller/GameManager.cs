using static week3.UIManager;

namespace week3
{
    public  class GameManager
    {
        SoundManager soundManager = new SoundManager();
        GameIntroUI gameIntroUI;
        UIManager uiManager= new UIManager();
        Player player;
        MapManager mapManager;
        
        public GameManager() 
        {
            gameIntroUI = new GameIntroUI(soundManager);
            player = new Player();
            mapManager = new MapManager(uiManager);
        }
        public void GameStart() 
        {
            gameIntroUI.ShowGameIntroUI();
            player.GetPlayerName();
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
