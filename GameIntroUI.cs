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
            ZebUI.WelcomeZEB();
        }
        public void GameIntro()
        {
            SelectMenuUI.ShowMenu(player, statUI);
        }
    }
}