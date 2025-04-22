namespace week3
{
    public class PlayerStatUI
    {
        public void ShowStatus(Player player)
        {
            Console.WriteLine("====================================================================================================");
            Console.WriteLine($"\n[{player.playerName}님의 현재 상태]");
            Console.WriteLine($"직업: {player.job}");
            Console.WriteLine($"체력: {player.hp}");
            Console.WriteLine($"공격력: {player.atk}");
            Console.WriteLine($"방어력: {player.def}");
            Console.WriteLine($"정신력: {player.spi}");
            Console.WriteLine($"코인: {player.coin}");
            Console.WriteLine($"골드: {player.gold}");
        }
    }
}
