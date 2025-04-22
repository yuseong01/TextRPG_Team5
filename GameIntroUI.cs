using System;
using System.Text;
using System.Threading;

namespace week3
{
    public class GameIntroUI
    {
        public void FirstIntro()
        {
            Console.Clear();
            Console.OutputEncoding = Encoding.UTF8;
            Console.SetWindowSize(120, 40);
            Console.SetBufferSize(120, 40);
            Console.CursorVisible = false;

            // 위쪽 여백
            for (int i = 0; i < 5; i++) Console.WriteLine();

            Console.ForegroundColor = ConsoleColor.DarkCyan;

            string[] splash = new string[]
            {
                " __       __            __                                                    ________  ________  _______  ",
                "/  |  _  /  |          /  |                                                  /        |/        |/       \\",
                "$$ | / \\ $$ |  ______  $$ |  _______   ______   _____  ____    ______        $$$$$$$$/ $$$$$$$$/ $$$$$$$  |",
                "$$ |/$  \\$$ | /      \\ $$ | /       | /      \\ /     \\/    \\  /      \\           /$$/  $$ |__    $$ |__$$ |",
                "$$ /$$$  $$ |/$$$$$$  |$$ |/$$$$$$$/ /$$$$$$  |$$$$$$ $$$$  |/$$$$$$  |         /$$/   $$    |   $$    $$< ",
                "$$ $$/$$ $$ |$$    $$ |$$ |$$ |      $$ |  $$ |$$ | $$ | $$ |$$    $$ |        /$$/    $$$$$/    $$$$$$$  |",
                "$$$$/  $$$$ |$$$$$$$$/ $$ |$$ \\_____ $$ \\__$$ |$$ | $$ | $$ |$$$$$$$$/        /$$/____ $$ |_____ $$ |__$$ |",
                "$$$/    $$$ |$$       |$$ |$$       |$$    $$/ $$ | $$ | $$ |$$       |      /$$      |$$       |$$    $$/ ",
                "$$/      $$/  $$$$$$$/ $$/  $$$$$$$/  $$$$$$/  $$/  $$/  $$/  $$$$$$$/       $$$$$$$$/ $$$$$$$$/ $$$$$$$/  ",
                "                                                                                                           ",
                "                                                                                                           ",
                "                                                                                                           "
            };

            foreach (string line in splash)
            {
                int padding = Math.Max(0, (Console.WindowWidth - line.Length) / 2);
                Console.SetCursorPosition(padding, Console.CursorTop);
                Console.WriteLine(line);
                Thread.Sleep(30);
            }

            Console.ResetColor();

            // 구분선 출력 (GameIntro 기준 위치 유지)
            Console.WriteLine();
            string separator = "====================================================================================================";
            int separatorPadding = Math.Max(0, (Console.WindowWidth - separator.Length) / 2);
            Console.SetCursorPosition(separatorPadding, Console.CursorTop);
            Console.WriteLine(separator);
            Thread.Sleep(1000);

            Console.ResetColor();
            Thread.Sleep(300);

            //지지직 효과 (아트 + 구분선 포함)
            Random rnd = new Random();

            for (int i = 0; i < 4; i++)
            {
                Console.SetCursorPosition(0, 0);
                for (int y = 0; y < splash.Length + 3; y++) // 아트 + 구분선 영역만 덮기
                {
                    Console.ForegroundColor = ConsoleColor.DarkGray;

                    int width = Console.WindowWidth;
                    string noiseLine = "";
                    for (int x = 0; x < width; x++)
                    {
                        noiseLine += (char)rnd.Next(33, 126); // 특수문자 범위
                    }

                    int padding = Math.Max(0, (Console.WindowWidth - width) / 2);
                    Console.SetCursorPosition(padding, Console.CursorTop);
                    Console.WriteLine(noiseLine);
                }

                Thread.Sleep(50);
                Console.Clear();
                Thread.Sleep(50);

                //전환
                Thread.Sleep(300);
            }
        }

        public void GameIntro()
        {
            Console.CursorVisible = false;

            Console.OutputEncoding = Encoding.UTF8;
            Console.SetWindowSize(120, 40);
            Console.SetBufferSize(120, 40);
            Console.Clear();

            string[] lines = new string[]
            {
                "  _______                       __              ________  ________  _______  ",
                " /       \\                     /  |            /        |/        |/       \\",
                " $$$$$$$  |  ______    ______  $$ |   __       $$$$$$$$/ $$$$$$$$/ $$$$$$$  |",
                " $$ |  $$ | /      \\  /      \\ $$ |  /  |          /$$/  $$ |__    $$ |__$$ |",
                " $$ |  $$ | $$$$$$  |/$$$$$$  |$$ |_/$$/          /$$/   $$    |   $$    $$< ",
                " $$ |  $$ | /    $$ |$$ |  $$/ $$   $$<          /$$/    $$$$$/    $$$$$$$  |",
                " $$ |__$$ |/$$$$$$$ |$$ |      $$$$$$  \\        /$$/____ $$ |_____ $$ |__$$ |",
                " $$    $$/ $$    $$ |$$ |      $$ | $$  |      /$$      |$$       |$$    $$/ ",
                " $$$$$$$/   $$$$$$$/ $$/       $$/   $$/       $$$$$$$$/ $$$$$$$$/ $$$$$$$/  ",
                "                                                                             ",
                "                                                                             "
            };

            for (int i = 0; i < 5; i++) Console.WriteLine();

            Console.ForegroundColor = ConsoleColor.Red;

            foreach (string line in lines)
            {
                int padding = Math.Max(0, (Console.WindowWidth - line.Length) / 2);
                Console.SetCursorPosition(padding, Console.CursorTop);
                Console.WriteLine(line);
                Thread.Sleep(50);
            }

            Console.ResetColor();
            Console.WriteLine();

            string[] menuItems = {
                "1. 게임 시작",
                "2. 시스템 설정",
                "3. 게임 종료"
            };

            for (int i = 0; i < 5; i++) Console.WriteLine();

            string separator = "====================================================================================================\n";
            int separatorPadding = Math.Max(0, (Console.WindowWidth - separator.Length) / 2);
            Console.SetCursorPosition(separatorPadding, Console.CursorTop);
            Console.WriteLine(separator);

            int menuStartRow = Console.CursorTop + 1;
            int separatorBottomRow = menuStartRow + menuItems.Length * 2 + 1;

            Console.SetCursorPosition(separatorPadding, separatorBottomRow);
            Console.WriteLine(separator);
            int selectedIndex = 0;

            ConsoleKey key;

            do
            {
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
                //Console.Beep();

                if ((key == ConsoleKey.UpArrow || key == ConsoleKey.LeftArrow) && selectedIndex > 0)
                    selectedIndex--;
                else if ((key == ConsoleKey.DownArrow || key == ConsoleKey.RightArrow) && selectedIndex < menuItems.Length - 1)
                    selectedIndex++;

            }
            while (key != ConsoleKey.Enter);

            Console.SetCursorPosition(0, separatorBottomRow + 2);
            Console.WriteLine($"선택한 메뉴: {menuItems[selectedIndex]}");
        }
    }
}
