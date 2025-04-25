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
            mapManager = new MapManager();
        }
        public void GameStart() 
        {
            gameIntroUI.ShowGameIntroUI();
            player.GetPlayerName();
            //uiManager.ShowGameIntroUI();
            //Console.ReadLine(); // 화면 유지
            mapManager.LoadAllMap();
        }
    }
}
