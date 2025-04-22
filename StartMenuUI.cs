namespace week3
{
    public class StartMenuUI
    {
        public static void SelectMenuUI(int index, Player player, PlayerStatUI statUI)
        {
            switch (index)
            {
                case 0:
                    Console.Clear();
                    statUI.ShowStatus(player);
                    Console.ReadKey(); // 일시정지
                    break;

                case 1:
                    Console.Clear();
                    Console.WriteLine("설정 화면 준비 중...");
                    Console.ReadKey();
                    break;

                case 2:
                    Console.Clear();
                    Console.WriteLine("게임을 종료합니다.");
                    Environment.Exit(0);
                    break;

                default:
                    Console.WriteLine("잘못된 입력입니다.");
                    break;
            }
        }
    }
}
