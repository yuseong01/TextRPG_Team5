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
            //mapManager.LoadAllMap();
            while(true)
            {
                Thread.Sleep(700);
                Console.Clear();

                
            }
        }
    }
}
