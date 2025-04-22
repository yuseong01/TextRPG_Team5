namespace week3;

class Program
{
    public static void Main(string[] args)
    {
        GameIntroUI intro = new GameIntroUI();
        intro.FirstIntro();
        intro.GameIntro(); 
        Console.ReadLine(); // 화면 유지
    }
}
