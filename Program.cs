namespace week3;

class Program
{
    public static void Main(string[] args)
    {
        Player player = new Player("수민", "학생", 100f, 10f, 5f, 80, 0, 2000);
        PlayerStatUI statUI = new PlayerStatUI(); 

        GameIntroUI intro = new GameIntroUI(player, statUI); // 전달
        intro.FirstIntro();
        intro.GameIntro(); 
        Console.ReadLine(); // 화면 유지
    }
}
