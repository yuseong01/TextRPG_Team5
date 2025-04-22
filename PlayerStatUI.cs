using System.Xml.Linq;

namespace week3
{
    public class PlayerStatUI
    {
        public void ShowStatus(Player player)
        {
            Console.WriteLine($"이름: {player.Name}");
            Console.WriteLine($"직업: {player.Job}");
            Console.WriteLine($"HP: {player.HP}");
            Console.WriteLine($"정신력: {player.Spirit}");
            Console.WriteLine($"공격력: {player.Atk}");
            Console.WriteLine($"방어력: {player.Def}");
            Console.WriteLine($"ZEB 코인: {player.ZebCoin}");
            Console.WriteLine($"골드: {player.Gold}");
        }
    }
}
