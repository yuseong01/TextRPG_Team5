namespace week3
{
    public class GameIntroUI
    {
        private Player player;
        private PlayerStatUI statUI;

        public GameIntroUI(Player player, PlayerStatUI statUI)
        {
            this.player = player;
            this.statUI = statUI;
        }

        public void FirstIntro() 
        {
            AsciiArt.WelcomeZEB();
        }
        public void GameIntro()
        {
            SelectMenu.ShowMenu(player, statUI);
        }
    }
}