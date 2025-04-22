using System;

namespace week3
{
    public static class SelectMenu
    {
        public static void ShowMenu(Player player, PlayerStatUI statUI)
        {
            string[] menuItems = {
                "1. 게임 시작",
                "2. 시스템 설정",
                "3. 게임 종료"
            };

            ConsoleKey key;
            int selectedIndex = 0;

            while (true)
            {
                Console.Clear();
                AsciiArt.DarkZEB();

                for (int i = 0; i < 5; i++) Console.WriteLine();

                string separator = "====================================================================================================";
                int separatorPadding = Math.Max(0, (Console.WindowWidth - separator.Length) / 2);
                Console.SetCursorPosition(separatorPadding, Console.CursorTop);
                Console.WriteLine(separator);

                int menuStartRow = Console.CursorTop + 1;
                int separatorBottomRow = menuStartRow + menuItems.Length * 2 + 1;
                Console.SetCursorPosition(separatorPadding, separatorBottomRow);
                Console.WriteLine(separator);

                for (int i = 0; i < menuItems.Length; i++)
                {
                    int row = menuStartRow + i * 2;
                    Console.SetCursorPosition(0, row);
                    Console.Write(new string(' ', Console.WindowWidth));

                    int itemPadding = Math.Max(0, (Console.WindowWidth - menuItems[i].Length - 2) / 6);
                    Console.SetCursorPosition(itemPadding, row);

                    if (i == selectedIndex)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write($"> {menuItems[i]}");
                        Console.ResetColor();
                    }
                    else
                    {
                        Console.Write($"  {menuItems[i]}");
                    }
                }

                key = Console.ReadKey(true).Key;

                if ((key == ConsoleKey.UpArrow || key == ConsoleKey.LeftArrow) && selectedIndex > 0)
                    selectedIndex--;
                else if ((key == ConsoleKey.DownArrow || key == ConsoleKey.RightArrow) && selectedIndex < menuItems.Length - 1)
                    selectedIndex++;
                else if (key == ConsoleKey.Enter)
                {
                    Console.Clear();
                    MenuUI.SelectMenuUI(selectedIndex, player, statUI);
                }
            }
        }
    }
}
