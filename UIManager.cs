namespace week3
{
    public class UIManager
    {
        //GameIntroUI
        public void ShowGameIntroUI() 
        {
            WelcomeZEB();
            ShowMenu();
        }
        
        public void WelcomeZEB()
        {
            Console.Clear();

            //SCARED_FACE 아스키코드 깨짐방지
            Console.OutputEncoding = System.Text.Encoding.UTF8; 

            //콘솔창 사이즈 조정
            Console.SetWindowSize(120, 40);
            Console.SetBufferSize(120, 40);
            Console.CursorVisible = false;

            for (int i = 0; i < 5; i++) Console.WriteLine();

            Console.ForegroundColor = ConsoleColor.DarkCyan;
            
            foreach (string line in Constants.WELCOME_ZEB_STRING)
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

            //SCARED_FACE 연출 효과
            int noiseHeight = 70;
            int asciiStartY = (noiseHeight - Constants.SCARED_FACE_STRING.Length) / 8;

            for (int i = 0; i < 6; i++)
            {
                Console.SetCursorPosition(0, 0);

                for (int y = 0; y < noiseHeight; y++)
                {
                    if (y >= asciiStartY && y < asciiStartY + Constants.SCARED_FACE_STRING.Length)
                    {
                        int index = y - asciiStartY;
                        string artLine = Constants.SCARED_FACE_STRING[index];
                        int padding = (Console.WindowWidth - artLine.Length) / 2;
                        Console.SetCursorPosition(padding, Console.CursorTop);
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine(artLine);
                    }
                    else
                    {
                        string noiseLine = "";
                        for (int x = 0; x < Console.WindowWidth; x++)
                            noiseLine += (char)new Random().Next(33, 126);

                        Console.ForegroundColor = ConsoleColor.DarkGray;
                        Console.SetCursorPosition(0, Console.CursorTop);
                        Console.WriteLine(noiseLine);
                    }
                }

                Thread.Sleep(20);
                Console.Clear();
                Thread.Sleep(300);
            }
            Console.ResetColor();
        }
        
        public void PrintDarkZEBUI()
        {

            for (int i = 0; i < 5; i++) Console.WriteLine();

            Console.ForegroundColor = ConsoleColor.Red;
            foreach (string line in Constants.DARK_ZEB_STRING)
            {
                int padding = Math.Max(0, (Console.WindowWidth - line.Length) / 2);
                Console.SetCursorPosition(padding, Console.CursorTop);
                Console.WriteLine(line);
                Thread.Sleep(50);
            }
            Console.ResetColor();
        }
        
        public void ShowMenu()
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
                    PrintDarkZEBUI();

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
                    SelectMenuUI(selectedIndex);
                    firstDraw = true; // 메뉴로 돌아왔을 때 다시 전체 그림
                }
            }
        }
        
        //GameManager에서 불러오는걸로 변경
        public void SelectMenuUI(int index)
        {
            switch (index)
            {
                case 0:
                    //샵으로감
                    Console.Clear();
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

        public static class _uiStoryText //텍스트 세팅에 관한 설계도
        {
            // 텍스트 시작 설정
            public static void BeginTextSet()
            {
                Console.OutputEncoding = System.Text.Encoding.UTF8;
                Console.SetWindowSize(120, 40);
                Console.SetBufferSize(120, 40);
                Console.Clear();
            }

            // 텍스트 종료 입력 대기
            public static void EndTextSet()
            {
                Thread.Sleep(1000);
                string pressKeyMsg = "아무 키나 누르세요...";
                int msgX = (Console.WindowWidth - GetVisualWidth(pressKeyMsg)) / 2;
                int msgY = Console.WindowHeight - 3;
                Console.SetCursorPosition(msgX, msgY);
                Console.WriteLine(pressKeyMsg);
                Console.ReadKey();
            }
            // 타자기 효과
            public static void TypeEffect(string text, int delay = 50, ConsoleColor color = ConsoleColor.White)
            {
                Console.ForegroundColor = color;
                foreach (char c in text)
                {
                    Console.Write(c);
                    Thread.Sleep(delay);
                }
                Console.ResetColor();
            }

            // 시각적 폭 계산 (한글 2폭 처리)
            public static int GetVisualWidth(string text)
            {
                int width = 0;
                foreach (char c in text)
                {
                    //한글 유니코드 범위
                    width += (c >= 0xAC00 && c <= 0xD7A3) ? 2 : 1;
                }
                return width;
            }

            // 여러 줄 텍스트를 중앙 정렬 + 타자기 효과
            public static void TypeEffectCenteredLines(string[] lines, int delay = 50, ConsoleColor color = ConsoleColor.White)
            {
                Console.ForegroundColor = color;
                int startY = (Console.WindowHeight - lines.Length) / 3;

                for (int i = 0; i < lines.Length; i++)
                {
                    string line = lines[i];
                    int centerX = (Console.WindowWidth - GetVisualWidth(line)) / 2;
                    int posY = startY + i;

                    Console.SetCursorPosition(centerX, posY);
                    foreach (char c in line)
                    {
                        Console.Write(c);
                        Thread.Sleep(delay);
                    }
                }
                Console.ResetColor();
            }
        }
        //플레이어 UI
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