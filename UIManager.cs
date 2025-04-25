using week3;
namespace week3
{
    public class UIManager
    {
        private SoundManager _soundManager = new SoundManager();

        // ========================================
        // 🎬 게임 인트로 UI 컨트롤러
        // ========================================
        public void ShowGameIntroUI()
        {
            WelcomeZEB();
            ShowMenu();
        }

        // ========================================
        // 🖥️ ZEB 타이틀 애니메이션 출력
        // ========================================
        public void WelcomeZEB()
        {
            Console.Clear();
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.SetBufferSize(120, 40);
            Console.SetWindowSize(120, 40);
            Console.CursorVisible = false;
            _soundManager.PlayLoop("welcomeZEB.wav"); // 🎵 인트로 음악 시작

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
            string separator = new string('=', 100);
            int separatorPadding = Math.Max(0, (Console.WindowWidth - separator.Length) / 2);
            Console.SetCursorPosition(separatorPadding, Console.CursorTop);
            Console.WriteLine(separator);
            Thread.Sleep(2000);
            Console.ResetColor();

            for (int i = 0; i < 2; i++) Console.WriteLine();



            int introStartY = (Console.WindowHeight / 2) - (Constants.TEAM_INTRO.Length / 2);
            Console.ForegroundColor = ConsoleColor.White;

            for (int i = 0; i < Constants.TEAM_INTRO.Length; i++)
            {
                string line = Constants.TEAM_INTRO[i];
                int paddingX = Math.Max(0, (Console.WindowWidth - _uiStoryText.GetVisualWidth(line)) / 2);
                int paddingY = introStartY + i + 3;

                Console.SetCursorPosition(paddingX, paddingY);
                Console.WriteLine(line);
                Thread.Sleep(500);
            }

            Console.ResetColor();
            Thread.Sleep(1000);
            _soundManager.StopCurrentLoop();
            _soundManager.PlayLoop("noise.wav");

            Random rnd = new Random();
            Console.SetWindowSize(120, 40);
            Console.SetBufferSize(120, 100);
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
                        string noiseLine = new string(Enumerable.Repeat(0, Console.WindowWidth)
                            .Select(_ => (char)new Random().Next(33, 126)).ToArray());

                        Console.ForegroundColor = ConsoleColor.DarkGray;
                        Console.SetCursorPosition(0, Console.CursorTop);
                        Console.WriteLine(noiseLine);
                    }
                }

                Thread.Sleep(30);
                Console.Clear();
                Thread.Sleep(300);
            }
            Console.ResetColor();
        }


        // ========================================
        // 🌒 어두운 ZEB 텍스트
        // ========================================
        public void PrintDarkZEBUI()
        {
            _soundManager.StopCurrentLoop();
            Console.SetBufferSize(120, 40);
            Console.SetWindowSize(120, 40);
            _soundManager.PlayLoop("intro.wav");
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


        // ========================================
        // 🎮 메뉴 출력 및 선택 루프
        // ========================================
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
            string separator = new string('=', 100);
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
                {
                    selectedIndex--;
                    _soundManager.PlayOnce("click.wav");
                }
                else if ((key == ConsoleKey.DownArrow || key == ConsoleKey.RightArrow) && selectedIndex < menuItems.Length - 1)
                {
                    selectedIndex++;
                    _soundManager.PlayOnce("click.wav");
                }
                else if (key == ConsoleKey.Enter)
                {
                    Console.Clear();
                    SelectMenuUI(selectedIndex);
                    firstDraw = true;
                }
            }
        }

        // ========================================
        // 🧭 선택한 메뉴 동작 처리
        // ========================================
        public void SelectMenuUI(int index)
        {
            switch (index)
            {
                case 0:
                    Console.Clear();
                    StoryText storyText = new StoryText(this);
                    storyText.IntroText();
                    Console.ReadKey();
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

        // ========================================
        // 📊 플레이어 상태 표시
        // ========================================
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

    // ========================================
    // 📝 스토리 출력용 UI 텍스트 클래스 (정적)
    // ========================================
    public static class _uiStoryText
    {
        // 🔧 기본 텍스트 환경 설정
        public static void BeginTextSet()
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.SetWindowSize(120, 40);
            Console.SetBufferSize(120, 40);
            Console.Clear();
        }

        // ⛔ 텍스트 종료 시 메시지 출력
        public static void EndTextSet(string message = "아무 키나 누르세요...")
        {
            int msgX = (Console.WindowWidth - GetVisualWidth(message)) / 2;
            int msgY = Console.WindowHeight - 2;

            Console.SetCursorPosition(0, msgY);
            Console.Write(new string(' ', Console.WindowWidth));
            Console.SetCursorPosition(msgX, msgY);

            Console.ForegroundColor = ConsoleColor.White;
            Console.Write(message);
            Console.ResetColor();

            Console.ReadKey(true);
        }

        // 🔎 시각적 너비 계산 (한글 2폭)
        public static int GetVisualWidth(string text)
        {
            int width = 0;
            foreach (char c in text)
            {
                width += (c >= 0xAC00 && c <= 0xD7A3) ? 2 : 1;
            }
            return width;
        }

        // 💬 단일 문장 타자기 출력
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

        // 📄 초반 스토리 전용 효과
        public static void TypePage(List<string> lines, int delay = 70, ConsoleColor color = ConsoleColor.DarkRed, bool shake = false)
        {
            SoundManager sound = new SoundManager();
            Console.Clear();

            int startY = (Console.WindowHeight - lines.Count) / 3;
            sound.ResumeTypingSound();

            for (int i = 0; i < lines.Count; i++)
            {
                string line = lines[i];
                int baseX = (Console.WindowWidth - GetVisualWidth(line)) / 2;
                int baseY = startY + i;

                // 줄 클리어
                Console.SetCursorPosition(0, baseY);
                Console.Write(new string(' ', Console.WindowWidth));

                int finalX = baseX;
                int finalY = baseY;

                Console.ForegroundColor = color;

                if (shake)
                {
                    Console.SetCursorPosition(finalX, finalY);
                    ShakeWrite(line, delay);
                }
                else
                {
                    Console.SetCursorPosition(finalX, finalY);
                    foreach (char c in line)
                    {
                        Console.Write(c);
                        Thread.Sleep(delay);
                    }
                }

                Console.ResetColor();
            }
            sound.PauseTypingSound();

            // 안내 메시지 출력
            string msg = ">> [Enter] 다음으로";
            int msgX = (Console.WindowWidth - GetVisualWidth(msg)) / 2;
            int msgY = Console.WindowHeight - 2;

            Console.SetCursorPosition(0, msgY);
            Console.Write(new string(' ', Console.WindowWidth));
            Console.SetCursorPosition(msgX, msgY);

            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.Write(msg);
            Console.ResetColor();

            ConsoleKeyInfo key = Console.ReadKey(true);
            if (key.Key == ConsoleKey.Enter)
                sound.PlayOnce("enterKey.wav");

            sound.StopTypingSound();
        }

        // 흔들림 효과
        public static void ShakeWrite(string text, int delay = 70)
        {
            int baseX = Console.CursorLeft;
            int baseY = Console.CursorTop;

            // 흔들리는 위치 순서대로 리스트
            List<(int dx, int dy)> shakePattern = new()
                {
                    (0, 0),   // 중심
                    (1, 1),   // 오른쪽 아래
                    (-1, -1), // 왼쪽 위
                    (0, 0)    // 복귀
                };

            foreach (var (dx, dy) in shakePattern)
            {
                int x = Math.Clamp(baseX + dx, 0, Console.WindowWidth - 1);
                int y = Math.Clamp(baseY + dy, 0, Console.WindowHeight - 2);

                Console.SetCursorPosition(x, y);
                Console.Write(text);
                Thread.Sleep(delay);

                // 흔들림 텍스트 지우기
                Console.SetCursorPosition(x, y);
                Console.Write(new string(' ', text.Length));
            }

            // 최종적으로 원위치에 텍스트 출력
            Console.SetCursorPosition(baseX, baseY);
            Console.Write(text);
        }
    }

    public class MapUI
    {
        public void ShowMap(MapManager.mapType mapType)
        {
            switch(mapType)
            {
                case MapManager.mapType.GroupFiveMap:
                    Console.OutputEncoding = System.Text.Encoding.UTF8;
                    Console.WriteLine(value: Constants.GROUP_FIVE_UI_STRING[0]);
                    break;
                case MapManager.mapType.PassageMap:
                    Console.OutputEncoding = System.Text.Encoding.UTF8;
                    Console.WriteLine(value: Constants.PASSAGE_UI_STRING[0]);
                    break;
                case MapManager.mapType.Manager1RoomMap:
                    Console.OutputEncoding = System.Text.Encoding.UTF8;
                    Console.WriteLine(value: Constants.MANAGER_ROOM_UI_STRING[0]);
                    break;
                case MapManager.mapType.Manager2RoomMap:
                    Console.OutputEncoding = System.Text.Encoding.UTF8;
                    Console.WriteLine(value: Constants.MANAGER_ROOM_UI_STRING[0]);
                    break;
                case MapManager.mapType.Manager3RoomMap:
                    Console.OutputEncoding = System.Text.Encoding.UTF8;
                    Console.WriteLine(value: Constants.MANAGER_ROOM_UI_STRING[0]);
                    break;
                default:
                    break;
        }

    }

    }
}




