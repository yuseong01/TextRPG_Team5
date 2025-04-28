using System.Numerics;
using System.Threading;
using week3;

namespace week3
{
    public class UIManager
    {
        public SoundManager soundManager;
        public Thread? flickerThread;
        public bool isFlickering = false;
        public TextEffect textEffect = new TextEffect();
        private volatile bool stopLED = false;
        private Thread ledThread;

        // ========================================
        // 🧩 플레이어 스탯 창 출력
        // ========================================
        public void ShowStatus(Player player)
        {
            Console.SetWindowSize(120, 40);
            Console.SetBufferSize(120, 40);
            Console.Clear();

            int boxWidth = 100;
            int statLines = 6;
            int boxHeight = statLines * 2 + 4;
            int leftPadding = (Console.WindowWidth - boxWidth) / 2;
            int centerStartY = (Console.WindowHeight - boxHeight) / 2;

            // 🔴 STATUS 로고 출력
            Console.ForegroundColor = ConsoleColor.Red;
            foreach (string line in Constants.STATUS_LOGO)
            {
                int padding = Math.Max(0, (Console.WindowWidth - textEffect.GetVisualWidth(line)) / 2);
                Console.SetCursorPosition(padding, Console.CursorTop);
                Console.WriteLine(line);
                Thread.Sleep(30);
            }
            Console.ResetColor();
            for (int i = 0; i < 5; i++) Console.WriteLine();

            // 🔷 LED 라인
            DrawCenteredLEDLine(boxWidth, centerStartY - 2);
            Console.SetCursorPosition(0, Console.CursorTop + 2);
            Console.WriteLine();

            // 🔶 박스 윗 테두리
            Console.SetCursorPosition(leftPadding, centerStartY);
            Console.WriteLine("┌" + new string('─', boxWidth - 2) + "┐");

            PrintEmptyBoxLine(boxWidth, leftPadding);
            PrintCenteredBoxLine($"이름: {player.Name}", boxWidth, leftPadding);
            PrintEmptyBoxLine(boxWidth, leftPadding);
            PrintCenteredBoxLine($"체력: {player.CurrentHp}", boxWidth, leftPadding);
            PrintEmptyBoxLine(boxWidth, leftPadding);
            PrintCenteredBoxLine($"공격력: {player.Attack} (+{player.AdditionalAttackPower})", boxWidth, leftPadding);
            PrintEmptyBoxLine(boxWidth, leftPadding);
            PrintCenteredBoxLine($"방어력: {player.Defense} (+{player.AdditionalDefensePower})", boxWidth, leftPadding);
            PrintEmptyBoxLine(boxWidth, leftPadding);
            PrintCenteredBoxLine($"ZEB 코인: {player.ZebCoin}", boxWidth, leftPadding);
            PrintEmptyBoxLine(boxWidth, leftPadding);
            PrintCenteredBoxLine($"골드: {player.Gold}", boxWidth, leftPadding);
            PrintEmptyBoxLine(boxWidth, leftPadding);

            // 🔶 박스 아랫 테두리
            Console.SetCursorPosition(leftPadding, Console.CursorTop);
            Console.WriteLine("└" + new string('─', boxWidth - 2) + "┘");

            // 🔷 LED 라인
            Console.SetCursorPosition(0, Console.CursorTop + 1);
            DrawCenteredLEDLine(boxWidth, Console.CursorTop);
            Console.SetCursorPosition(0, Console.CursorTop + 2);
            Console.WriteLine();

            // ✨ 나가기 버튼
            Console.SetCursorPosition(leftPadding, Console.CursorTop);
            Console.WriteLine("0. 나가기");
        }

        // 📄 한 줄 출력 (스탯 텍스트)
        void PrintCenteredBoxLine(string text, int boxWidth, int leftPadding)
        {
            Console.SetCursorPosition(leftPadding, Console.CursorTop);
            int innerWidth = boxWidth - 2;
            int paddingTotal = innerWidth - textEffect.GetVisualWidth(text);
            int paddingLeft = paddingTotal / 2;
            int paddingRight = paddingTotal - paddingLeft;
            Console.WriteLine("│" + new string(' ', paddingLeft) + text + new string(' ', paddingRight) + "│");
        }

        // 📄 빈 줄 출력
        void PrintEmptyBoxLine(int boxWidth, int leftPadding)
        {
            Console.SetCursorPosition(leftPadding, Console.CursorTop);
            Console.WriteLine("│" + new string(' ', boxWidth - 2) + "│");
        }

        // 📄 LED 중앙 출력
        void DrawCenteredLEDLine(int width, int yPos)
        {
            ConsoleColor[] colors = { ConsoleColor.Red, ConsoleColor.Yellow, ConsoleColor.Green, ConsoleColor.Cyan, ConsoleColor.Blue, ConsoleColor.Magenta, ConsoleColor.White };
            Random rand = new Random();
            int padding = (Console.WindowWidth - width) / 2;

            Console.SetCursorPosition(padding, yPos);
            for (int i = 0; i < width; i++)
            {
                Console.ForegroundColor = colors[rand.Next(colors.Length)];
                Console.Write("*");
            }
            Console.ResetColor();
        }

        // ========================================
        // 📦 인벤토리 표시
        // ========================================
        public void ShowInventory(List<Item> items)
        {
            Console.WriteLine("=== 인벤토리 ===");
            if (items.Count == 0)
            {
                Console.WriteLine("인벤토리가 비어 있습니다.");
                return;
            }

            foreach (Item item in items)
            {
                Console.WriteLine($"- {item.Name} ({item.Price}G) : {item.Description}");
            }
        }

        // ========================================
        // 🗺️ 맵 표시
        // ========================================
        public void ShowMap(MapManager.mapType mapType)
        {
            Console.Clear();
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.SetWindowSize(120, 40); // 🔥 40줄로 줄임
            Console.SetBufferSize(120, 40);

            soundManager.PlayLoopEx("bgm1", "bgm1.wav", 0.6f);

            if (mapType == MapManager.mapType.GroupFiveMap)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                int offsetX = 5;
                int totalLines = Constants.GROUP_FIVE_UI_STRING.Length;
                int startY = Math.Max(0, (Console.WindowHeight - totalLines) / 2 - 2); // 살짝 조정

                for (int i = 0; i < totalLines; i++)
                {
                    if (startY + i >= Console.WindowHeight) break;

                    string line = Constants.GROUP_FIVE_UI_STRING[i];
                    int visualWidth = textEffect.GetVisualWidth(line);
                    int padding = Math.Max(0, (Console.WindowWidth - visualWidth) / 2);
                    Console.SetCursorPosition(padding + offsetX, startY + i);
                    Console.WriteLine(line);
                    Thread.Sleep(20);
                }
                Console.ResetColor();

                // 아트 끝나고 공백 1~2줄만 추가
                if (Console.CursorTop + 2 < Console.WindowHeight)
                    Console.SetCursorPosition(0, Console.CursorTop + 2);

                // 구분선 출력
                if (Console.CursorTop < Console.WindowHeight)
                {
                    string separator = new string('=', 100);
                    int separatorPadding = Math.Max(0, (Console.WindowWidth - separator.Length) / 2);
                    Console.SetCursorPosition(separatorPadding, Console.CursorTop);
                    Console.WriteLine(separator);
                }

                // 🎯 여기서 끝! "5조 텍스트"는 출력 안함
            }
            else
            {
                string line = mapType switch
                {
                    MapManager.mapType.PassageMap => Constants.PASSAGE_UI_STRING[0],
                    MapManager.mapType.Manager1RoomMap => Constants.MANAGER1_ROOM_UI_STRING[0],
                    MapManager.mapType.Manager2RoomMap => Constants.MANAGER2_ROOM_UI_STRING[0],
                    MapManager.mapType.Manager3RoomMap => Constants.MANAGER3_ROOM_UI_STRING[0],
                    _ => ""
                };

                int padding = Math.Max(0, (Console.WindowWidth - textEffect.GetVisualWidth(line)) / 2);
                Console.SetCursorPosition(padding, Console.CursorTop);
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine(line);
                Console.ResetColor();
            }
        }

        // ========================================
        // 📋 맵 설명 UI
        // ========================================
        // ========================================
        // 📋 맵 설명 UI (메뉴 2열 가로 출력 버전)
        // ========================================
        public void ShowMapDescriptionUI(string mapName, List<MapObject> mapObjects)
        {
            Console.WriteLine();
            Console.WriteLine();

            string title = $"Dark ZEB {mapName} 자리에 도착했다. 도움될만한 물건을 찾아보자!";
            int padding = (Console.WindowWidth - textEffect.GetVisualWidth(title)) / 2;
            Console.SetCursorPosition(padding, Console.CursorTop);
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(title);
            Console.ResetColor();
            Console.WriteLine();

            int totalOptions = mapObjects.Count;
            int columnSpacing = 20; // 좌우 메뉴 간격 (스페이스 몇 칸 띄울지)

            for (int i = 0; i < totalOptions; i += 2)
            {
                string leftOption = $"{i + 1}. {mapObjects[i].Name}";
                string rightOption = "";

                if (i + 1 < totalOptions)
                {
                    rightOption = $"{i + 2}. {mapObjects[i + 1].Name}";
                }

                // 왼쪽 + (빈칸) + 오른쪽 연결
                string line = leftOption.PadRight(columnSpacing) + rightOption;

                int linePadding = (Console.WindowWidth - textEffect.GetVisualWidth(line)) / 2;
                Console.SetCursorPosition(linePadding, Console.CursorTop);

                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine(line);
                Console.ResetColor();
                Thread.Sleep(30);
            }

            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.White;
            Console.ResetColor();

           //// ✅ 입력 받을 때마다 효과음 재생
           //while (true)
           //{
           //    var keyInfo;
           //    soundManager.PlayOnceForce("enterKey.wav");

           //    if (keyInfo.Key == ConsoleKey.D1 || keyInfo.Key == ConsoleKey.NumPad1 ||
           //        keyInfo.Key == ConsoleKey.D2 || keyInfo.Key == ConsoleKey.NumPad2 ||
           //        keyInfo.Key == ConsoleKey.D3 || keyInfo.Key == ConsoleKey.NumPad3 ||
           //        keyInfo.Key == ConsoleKey.D4 || keyInfo.Key == ConsoleKey.NumPad4)
           //    {
           //        break;
           //    }
           //}
        }
    }
}
