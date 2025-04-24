namespace week3;

class Program
{
    public static void Main(string[] args)
    {
        GameManager gameManager = new GameManager();
        gameManager.GameStart();

        //UI테스트 용이라 주석처리 (지우지마시오)
        //UIManager uiManager = new UIManager();
        //StoryText story = new StoryText(uiManager);
        //story.IntroText();
    }
}
