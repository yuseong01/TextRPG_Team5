using static week3.UIManager;
using static week3.Constants;


namespace week3
{
    // ========================================
    // 📝 스토리 출력용 UI 텍스트 클래스
    // ========================================

    public class TextEffect
    {
        // 텍스트 표현을 시작하는 메서드
        public void BeginTextSet()
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.SetWindowSize(120, 40);
            Console.SetBufferSize(120, 40);
            Console.Clear();
        }

        // ⛔ 텍스트 종료 시 메시지 출력
        public void EndTextSet(string message = "아무 키나 누르세요...")
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
        public int GetVisualWidth(string text)
        {
            int width = 0;
            foreach (char c in text)
            {
                width += (c >= 0xAC00 && c <= 0xD7A3) ? 2 : 1;
            }
            return width;
        }

        // 💬 단일 문장 타자기 출력
        public void TypeEffect(string text, int delay = 50, ConsoleColor color = ConsoleColor.White)
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
        public void TypePage(List<string> lines, int delay = 70, ConsoleColor color = ConsoleColor.DarkRed, bool shake = false)
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
        public void ShakeWrite(string text, int delay = 70)
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