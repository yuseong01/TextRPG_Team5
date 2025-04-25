using static week3.UIManager;

namespace week3
{
    public  class GameManager
    {
        UIManager uiManager= new UIManager();
        Player player;
        MapManager mapManager;
        
        public GameManager() 
        {
            player = new Player();
            mapManager = new MapManager();
        }
        public void GameStart() 
        {
            string playerName = uiManager.GetPlayerName();
            player.Name = playerName;

            //uiManager.ShowGameIntroUI();
            //Console.ReadLine(); // 화면 유지
            mapManager.LoadAllMap();
        }
    }
}
