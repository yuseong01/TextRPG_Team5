using System.Text;

namespace week3
{
    public static class ZebUI
    {
        public static void WelcomeZEB()
        {
            Console.Clear();
            Console.OutputEncoding = Encoding.UTF8;
            Console.SetWindowSize(120, 40);
            Console.SetBufferSize(120, 40);
            Console.CursorVisible = false;

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
            Console.WriteLine();
            string separator = "====================================================================================================";
            int separatorPadding = Math.Max(0, (Console.WindowWidth - separator.Length) / 2);
            Console.SetCursorPosition(separatorPadding, Console.CursorTop);
            Console.WriteLine(separator);
            Thread.Sleep(1000);
            Console.ResetColor();
            Thread.Sleep(300);

            Random rnd = new Random();
            for (int i = 0; i < 4; i++)
            {
                Console.SetCursorPosition(0, 0);
                for (int y = 0; y < splash.Length + 3; y++)
                {
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    int width = Console.WindowWidth;
                    string noiseLine = "";
                    for (int x = 0; x < width; x++)
                    {
                        noiseLine += (char)rnd.Next(33, 126);
                    }
                    int padding = Math.Max(0, (Console.WindowWidth - width) / 2);
                    Console.SetCursorPosition(padding, Console.CursorTop);
                    Console.WriteLine(noiseLine);
                }
                Thread.Sleep(50);
                Console.Clear();
                Thread.Sleep(50);
                Thread.Sleep(300);
            }
        }
        public static void DarkZEB()
        {
            string[] art = new string[]
            {
                "  _______                       __              ________  ________  _______  ",
                " /       \\                     /  |           |       \\ |/        |/       \\",
                " $$$$$$$  |  ______    ______  $$ |   __     _ \\$$$$$$$$ $$$$$$$$/ $$$$$$$  |",
                " $$ |  $$ | /      \\  /      \\ $$ |  /  |   \\$$\\     $$ |__    $$ |__$$ |",
                " $$ |  $$ | $$$$$$  |/$$$$$$  |$$ |_/$$/        \\$$\\ __$$    |   $$    $$< ",
                "$$ |  $$ | /    $$ |$$ |  $$/ $$   $$<          \\$$\\ $$$$$/    $$$$$$$  |",
                " $$ |__$$ |/$$$$$$$ |$$ |      $$$$$$  \\    ____   \\$$\\$$ |_____ $$ |__$$ |",
                " $$    $$/ $$    $$ |$$ |      $$ | $$  |     \\    $$\\|$$       |$$    $$/ ",
                " $$$$$$$/   $$$$$$$/ $$/       $$/   $$/      \\$$$$$$$$ $$$$$$$$/ $$$$$$$/  ",
                "                                                                             ",
                "                                                                             "
            };

            for (int i = 0; i < 5; i++) Console.WriteLine();

            Console.ForegroundColor = ConsoleColor.Red;
            foreach (string line in art)
            {
                int padding = Math.Max(0, (Console.WindowWidth - line.Length) / 2);
                Console.SetCursorPosition(padding, Console.CursorTop);
                Console.WriteLine(line);
                Thread.Sleep(50);
            }
            Console.ResetColor();
        }
    }
}