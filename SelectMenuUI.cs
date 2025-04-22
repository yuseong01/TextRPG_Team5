using System;

namespace week3
{
    public static class SelectMenuUI
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
            bool firstDraw = true;
            int menuStartRow = 0;
            int separatorBottomRow = 0;
            string separator = "====================================================================================================";
            int separatorPadding = Math.Max(0, (Console.WindowWidth - separator.Length) / 2);

            while (true)
            {
                if (firstDraw)
                {
                    Console.Clear();
                    ZebUI.DarkZEB();

                    for (int i = 0; i < 5; i++) Console.WriteLine();

                    Console.SetCursorPosition(separatorPadding, Console.CursorTop);
                    Console.WriteLine(separator);

                    menuStartRow = Console.CursorTop + 1;
                    separatorBottomRow = menuStartRow + menuItems.Length * 2 + 1;

                    Console.SetCursorPosition(separatorPadding, separatorBottomRow);
                    Console.WriteLine(separator);

                    firstDraw = false;
                }

                // 메뉴 출력만 업데이트
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
                    StartMenuUI.SelectMenuUI(selectedIndex, player, statUI);
                    firstDraw = true; // 메뉴로 돌아왔을 때 다시 전체 그림
                }
            }
        }
    }
}
