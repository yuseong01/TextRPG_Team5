namespace week3
{
    public  class GameManager
    {
        Player player;
        UIManager _uiManager;
        public GameManager() 
        {
            player = new Player("수민", "학생");
            _uiManager = new UIManager();
        }
        public void GameStart() 
        {
            _uiManager.ShowGameIntroUI();
            Console.ReadLine(); // 화면 유지
        }
    }
}
