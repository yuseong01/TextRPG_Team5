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
            Console.ReadLine(); // 화면 유지
            mapManager.ManageMap();
        }
    }
}
