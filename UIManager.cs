using System.Numerics;

namespace week3
{
    public class UIManager
    {
        public static volatile bool stopFlicker = false;
        public static volatile bool stopLED = false;
        private SoundManager _soundManager = new SoundManager();
        private Thread? flickerThread;
        private bool isFlickering = false;

        // ========================================
        // 🎬 게임 인트로 UI 컨트롤러
        // ========================================
        public void ShowGameIntroUI()
        {
            WelcomeZEB();
            ShowMenu();
        }

        // ========================================
        // 🖥️ ZEB 타이틀 애니메이션 출력 (여기 좀 나눠야하는데 시간없어서 못함)
        // ========================================
        public void WelcomeZEB()
        {
            Console.Clear();
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.SetBufferSize(120, 40);
            Console.SetWindowSize(120, 40);
            Console.CursorVisible = false;
            _soundManager.PlayLoop("song.wav");

            

            for (int i = 0; i < 5; i++) Console.WriteLine();

            Console.ForegroundColor = ConsoleColor.DarkCyan;

            foreach (string line in Constants.WELCOME_ZEB_STRING)
            {
                int padding = Math.Max(0, (Console.WindowWidth - line.Length) / 2);
                Console.SetCursorPosition(padding, Console.CursorTop);
                Console.WriteLine(line);
                Thread.Sleep(30);
            }
                _soundManager.PlayOnce("blip.wav");
            //르탄이
            Console.ForegroundColor = ConsoleColor.Cyan;
            foreach (string line in Constants.SPARTA_IMAGE)
            {
                int padding = Math.Max(0, (Console.WindowWidth - line.Length) / 2);
                Console.SetCursorPosition(padding, Console.CursorTop);
                Console.WriteLine(line);
                Thread.Sleep(30);
            }
                _soundManager.PlayOnce("videogame.wav");
                
            Console.ResetColor();
            Console.WriteLine();
            string separator = new string('=', 100);
            int separatorPadding = Math.Max(0, (Console.WindowWidth - separator.Length) / 2);
            Console.SetCursorPosition(separatorPadding, Console.CursorTop);
            Console.WriteLine(separator);

            Thread.Sleep(2000);
            
            Console.ResetColor();

            for (int i = 0; i < 2; i++) Console.WriteLine();
            
            for (int flash = 0; flash < 3; flash++)
            {
                Console.Clear();
                Thread.Sleep(100);

                int startY = (Console.WindowHeight - Constants.WELCOME_ZEB_STRING.Length) / 2;
                for (int i = 0; i < Constants.WELCOME_ZEB_STRING.Length; i++)
                {
                    string line = Constants.WELCOME_ZEB_STRING[i];
                    int padding = Math.Max(0, (Console.WindowWidth - line.Length) / 2);
                    Console.SetCursorPosition(padding, startY + i - 9);
                    Console.ForegroundColor = ConsoleColor.DarkCyan;
                    Console.WriteLine(line);
                }
                _soundManager.PlayOnce("blip.wav");
                Console.ResetColor();
                Thread.Sleep(100);
            }

            // 여기서부터 ZEP 회전 애니메이션
            Thread ledThread = new Thread(() => AnimateZEPOnly());
            ledThread.IsBackground = true;
            ledThread.Start();

            // 여기서부터 팀 소개
            int introStartY = (Console.WindowHeight / 2) - (Constants.TEAM_INTRO.Length / 2);


            for (int i = 0; i < Constants.TEAM_INTRO.Length; i++)
            {
                string line = Constants.TEAM_INTRO[i];

                // 폭 계산 및 자르기
                int visualWidth = _uiStoryText.GetVisualWidth(line);
                if (visualWidth > Console.WindowWidth - 2)
                {
                    int maxWidth = Console.WindowWidth - 2;
                    int currentWidth = 0;
                    string trimmed = "";

                    foreach (char c in line)
                    {
                        int charWidth = (c >= 0xAC00 && c <= 0xD7A3) ? 2 : 1;
                        if (currentWidth + charWidth > maxWidth) break;

                        trimmed += c;
                        currentWidth += charWidth;
                    }

                    line = trimmed;
                    visualWidth = currentWidth;
                }

                int paddingX = Math.Max(0, (Console.WindowWidth - visualWidth) / 2);
                int paddingY = introStartY + i + 3;

                // 🔧 줄 클리어 후 안전 출력
                if (paddingY < Console.WindowHeight - 1)
                {
                    Console.SetCursorPosition(0, paddingY);
                    Console.Write(new string(' ', Console.WindowWidth));
                    Console.SetCursorPosition(paddingX, paddingY);
                    Console.ForegroundColor = line.Contains("5조 팀 프로젝트") ? ConsoleColor.Yellow : ConsoleColor.White;
                    Console.Write(line);
                    Console.ResetColor();
                }

                Thread.Sleep(500);
                if (i % 2 == 1)
                    _soundManager.PlayOnce("ding.wav");
            }


            // ✅ LED 애니메이션 중단 및 정리
            UIManager.stopLED = true;
            Thread.Sleep(700);
            Console.Clear();

            Thread.Sleep(1500);
            Console.ResetColor();
            Console.Clear();


            // 여기서부터 파이널 로고 sparta codingclub

            _soundManager.PlayOnce("electric.wav");
            int logoStartY = (Console.WindowHeight / 2) - (Constants.FINAL_LOGO.Length / 2);
            Console.ForegroundColor = ConsoleColor.Red;

            for (int i = 0; i < Constants.FINAL_LOGO.Length; i++)
            {
                string line = Constants.FINAL_LOGO[i];
                int paddingX = Math.Max(0, (_uiStoryText.GetVisualWidth(line) - line.Length) > 0
                    ? (Console.WindowWidth - _uiStoryText.GetVisualWidth(line)) / 2
                    : (Console.WindowWidth - line.Length) / 2);
                int lineY = logoStartY + i;
                Console.SetCursorPosition(paddingX, lineY);
                Console.WriteLine(line);
                Thread.Sleep(50);
                Console.SetCursorPosition(paddingX, lineY);
                Console.Write(new string(' ', line.Length));
                Thread.Sleep(50);
                Console.SetCursorPosition(paddingX, lineY);
                Console.WriteLine(line);
                Thread.Sleep(30);
            }

            Console.ResetColor();
            Thread.Sleep(1000);

            _soundManager.StopCurrentLoop();
            _soundManager.PlayLoop("noise.wav");

            // 무서운 얼굴 노이즈 시작
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
        public static string TrimToConsoleWidth(string text)
        {
            int visualWidth = 0;
            string result = "";

            foreach (char c in text)
            {
                int w = (c >= 0xAC00 && c <= 0xD7A3) ? 2 : 1;
                if (visualWidth + w >= Console.WindowWidth - 2)
                    break;

                result += c;
                visualWidth += w;
            }
            return result;
        }

        // ========================================
        // 🔄 회전 애니메이션 함수 (리팩터 버전)
        // ========================================
        static void AnimateZEPOnly()
        {
            Console.SetWindowSize(120, 40);     // 콘솔 창 크기 설정
            Console.SetBufferSize(120, 40);     // 콘솔 버퍼 크기도 동일하게

            ConsoleColor[] colors = new[]
            {
                ConsoleColor.Red, ConsoleColor.Yellow, ConsoleColor.Green,
                ConsoleColor.Cyan, ConsoleColor.Blue, ConsoleColor.Magenta, ConsoleColor.White
            };

            string[] symbols = new[] { "*" };
            Random rand = new Random();

            int yTop = 3;
            int yBottom = Console.WindowHeight - 3;
            int width = Console.WindowWidth;

            for (int frame = 0; frame < width * 2; frame++)
            {
                if (UIManager.stopLED) break;

                // 🔧 클리어 LED 줄
                Console.SetCursorPosition(0, yTop);
                Console.Write(new string(' ', width));
                Console.SetCursorPosition(0, yBottom);
                Console.Write(new string(' ', width));

                int ledCount = width / 3; // 전체 너비의 1/3만큼만 출력

                for (int j = 0; j < ledCount; j++)
                {
                    int x = rand.Next(width); // 랜덤한 x 위치

                    Console.SetCursorPosition(x, yTop);
                    Console.ForegroundColor = colors[rand.Next(colors.Length)];
                    Console.Write(symbols[rand.Next(symbols.Length)]);

                    Console.SetCursorPosition(x, yBottom);
                    Console.ForegroundColor = colors[rand.Next(colors.Length)];
                    Console.Write(symbols[rand.Next(symbols.Length)]);
                }

                Console.SetCursorPosition(0, 0); // 커서 깜빡임 방지용
                Thread.Sleep(40);
            }

            Console.ResetColor();
        }

        public static Vector3 Rotate3D_Y(Vector3 v, float angleDegrees)
        {
            double radians = angleDegrees * Math.PI / 180;
            float x = (float)(v.X * Math.Cos(radians) - v.Z * Math.Sin(radians));
            float z = (float)(v.X * Math.Sin(radians) + v.Z * Math.Cos(radians));
            return new Vector3(x, v.Y, z);
        }
        public static Vector3 Rotate3D_X(Vector3 v, float angleDegrees)
        {
            double radians = angleDegrees * Math.PI / 180;
            float y = (float)(v.Y * Math.Cos(radians) - v.Z * Math.Sin(radians));
            float z = (float)(v.Y * Math.Sin(radians) + v.Z * Math.Cos(radians));
            return new Vector3(v.X, y, z);
        }



        // ========================================
        // 🌒 어두운 ZEB 텍스트 (연출 포함)
        // ========================================
        public void PrintDarkZEBUI()
        {
            _soundManager.StopCurrentLoop();
            UIManager.stopFlicker = false;
            // 👉 기존 단일 루프 중지 대신 모든 루프 중지
            _soundManager.StopAllLoopEx();

            Console.SetBufferSize(120, 40);
            Console.SetWindowSize(120, 40);
            // 👉 두 개의 루프 배경음 동시 재생
            _soundManager.PlayLoopEx("intro", "intro.wav");
            _soundManager.PlayLoopEx("glitch", "glitch.wav", 0.1f);

            int startY1 = 4;
            int startY2 = startY1 + Constants.DARK_ZEB_STRING.Length;
            int startY3 = startY2 + Constants.DARK_ZEB_STRING2.Length - 1;

            // 지지직 루프
            StartFlickerLoop(Constants.DARK_ZEB_STRING, startY1, new[] { 1000, 500, 1000, 500, 500, 300, 400, 200, 1000 });
            StartFlickerLoop(Constants.DARK_ZEB_STRING2, startY2, new[] { 1500, 500, 1500, 500, 500, 300, 400, 200, 1500 });
            StartFlickerLoop(Constants.DARK_ZEB_STRING3, startY3, new[] { 2000, 500, 2000, 500, 500, 300, 400, 200, 1000 });
        }

        private void StartFlickerLoop(string[] lines, int startY, int[] delays)
        {
            new Thread(() =>
            {
                while (true)
                {
                    if (stopFlicker) break;

                    FlickerPattern(lines, startY, delays); // 깜빡
                    if (stopFlicker) break;

                    PrintAscii(lines, startY);             // 고정 출력
                    if (stopFlicker) break;

                    Thread.Sleep(1500);                    // 대기 중 중지 확인 못함
                }

                // 🔥 끝나면 줄 단위로 클리어
                for (int i = 0; i < lines.Length; i++)
                {
                    int y = startY + i;
                    if (y >= Console.WindowHeight) break;
                    Console.SetCursorPosition(0, y);
                    Console.Write(new string(' ', Console.WindowWidth));
                }

            })
            { IsBackground = true }.Start();
        }


        private void PrintAscii(string[] lines, int startY)
        {
            for (int i = 0; i < lines.Length; i++)
            {
                string line = lines[i];
                int padding = Math.Max(0, (Console.WindowWidth - line.Length) / 2);
                Console.SetCursorPosition(padding, startY + i);
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(line);
            }
        }

        // ========================================
        // 🔁 ASCII 블록 단위 깜빡임 효과
        // ========================================
        public static void FlickerPattern(string[] lines, int startY, int[] delays)
        {
            ConsoleColor color = ConsoleColor.Red;
            int maxY = Console.WindowHeight - 8;

            while (!UIManager.stopFlicker)
            {
                foreach (int delay in delays)
                {
                    if (UIManager.stopFlicker) return; // 🔥 중간 탈출까지 안전하게

                    // 출력
                    for (int i = 0; i < lines.Length; i++)
                    {
                        int y = startY + i;
                        if (y >= maxY) break;

                        string line = lines[i];
                        int paddingX = Math.Max(0, (Console.WindowWidth - line.Length) / 2);

                        Console.SetCursorPosition(0, y);
                        Console.Write(new string(' ', Console.WindowWidth)); // 클리어 후 출력
                        Console.SetCursorPosition(paddingX, y);
                        Console.ForegroundColor = color;
                        Console.Write(line);
                    }

                    Thread.Sleep(delay);

                    // 클리어
                    for (int i = 0; i < lines.Length; i++)
                    {
                        int y = startY + i;
                        if (y >= maxY) break;
                        Console.SetCursorPosition(0, y);
                        Console.Write(new string(' ', Console.WindowWidth));
                    }

                    Thread.Sleep(50);
                }
            }
        }

        // ========================================
        // 🎮 메뉴 출력 및 선택 루프 (테두리+가운데 정렬+구분선 포함)
        // ========================================
        public void ShowMenu()
        {
            PrintDarkZEBUI();
            Thread.Sleep(200); // 타이밍 보정용 짧은 딜레이

            string[] menuItems = {
                " 게임 시작",
                " 시스템 설정",
                " 게임 종료"
            };

            ConsoleKey key;
            int selectedIndex = 0;
            bool firstDraw = true;

            int boxHeight = menuItems.Length * 3 + 2;
            int startRow = (Console.WindowHeight - boxHeight) / 2 + 10; // ✅ DarkZEB와 간격 확보
            int separatorLength = 100;
            string separator = new string('═', separatorLength);
            int separatorPadding = Math.Max(0, (Console.WindowWidth - separatorLength) / 2);

            while (true)
            {
                if (firstDraw)
                {
                    Console.ForegroundColor = ConsoleColor.Gray;
                    Console.SetCursorPosition(separatorPadding, startRow - 2);
                    Console.WriteLine(separator); // 🔼 위쪽 구분선
                    Console.SetCursorPosition(separatorPadding, startRow + boxHeight + 1);
                    Console.WriteLine(separator); // 🔽 아래쪽 구분선
                    Console.ResetColor();
                    firstDraw = false;
                }
                
                for (int i = 0; i < menuItems.Length; i++)
                {
                    int maxTextLength = menuItems.Max(item => ("▶ " + item).Length);
                    string text = "▶ " + menuItems[i];
                    text = text.PadRight(maxTextLength); // 길이 통일!

                    int maxWidth = Console.WindowWidth - 4; // 🔧 여유 공간 확보
                    int boxWidth = separatorLength - 10;
                    int boxLeft = (Console.WindowWidth - boxWidth) / 2;
                    int boxTop = startRow + i * 4;

                    // 🔒 텍스트 잘림 방지 (길면 자름)
                    if (text.Length > boxWidth - 2)
                    {
                        text = text.Substring(0, boxWidth - 5) + "...";
                    }

                    // 🔧 가운데 정렬용 패딩 계산
                    int innerWidth = boxWidth - 2;
                    int leftOffset = 4;
                    int paddingTotal = innerWidth - text.Length;
                    int paddingLeft = paddingTotal / 2;
                    int paddingRight = paddingTotal - paddingLeft;
                    string paddedText = new string(' ', paddingLeft) + text + new string(' ', paddingRight);

                    // 🔲 상단 테두리
                    Console.SetCursorPosition(boxLeft, boxTop);
                    Console.ForegroundColor = (i == selectedIndex) ? ConsoleColor.Red : ConsoleColor.White;
                    Console.Write("■" + new string('─', innerWidth) + "■");

                    // ▶ 텍스트 라인
                    Console.SetCursorPosition(boxLeft, boxTop + 1);
                    Console.Write("■" + paddedText + " ");

                    // 🔻 하단 테두리
                    Console.SetCursorPosition(boxLeft, boxTop + 2);
                    Console.Write("■" + new string('─', innerWidth) + "■");


                    Console.ResetColor();
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
                    UIManager.stopFlicker = true;             // 🔴 깜빡임 스레드 종료 요청
                    Thread.Sleep(200);                        // ⏳ 스레드 완전 종료 대기
                    Console.Clear();                          // 💣 잔상 제거
                    _soundManager.StopAllLoopEx();            // 🔇 모든 배경음 멀티루프 중지
                    StoryText storyText = new StoryText(this);
                    storyText.IntroText();                    // 🧠 본격 스토리 진입
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
}


