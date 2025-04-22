namespace week3
{
    public  class GameManager
    {
        Player player;
        PlayerStatUI statUI;
        GameIntroUI intro;
        public GameManager() 
        {
            player = new Player("수민", "학생");
            statUI = new PlayerStatUI();
            intro = new GameIntroUI(player, statUI);
        }
        public void GameStart() 
        {
            intro.ShowGameIntroUI();
            Console.ReadLine(); // 화면 유지
        }
    }
}
