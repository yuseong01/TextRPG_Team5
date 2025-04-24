namespace week3;

class Program
{
    public static void Main(string[] args)
    {
        //GameManager gameManager = new GameManager();
        //gameManager.GameStart();
        UIManager uiManager = new UIManager();
        StoryText story = new StoryText(uiManager);
        story.IntroText();
    }
}
