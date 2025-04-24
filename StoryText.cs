using static week3.UIManager;
using static week3.Constants;

namespace week3
{
    public class StoryText
    {
        UIManager _uiManager;
        SoundManager _soundManager;

        public StoryText(UIManager uiManager)
        {
            _uiManager = new UIManager();
            _soundManager = new SoundManager();
        }

        // 텍스트 표현을 시작하는 메서드
        public void IntroText()
        {
            _uiStoryText.BeginTextSet();
            _soundManager.PlayBirdLoopFadeIn(); // 🐦 새소리 페이드 인

            // ✅ 첫 페이지 출력
            _uiStoryText.TypePage(INTRO_STORY_PAGES[0].ToList(), 70, ConsoleColor.White);
            ShowEnterPrompt();
            while (Console.KeyAvailable) Console.ReadKey(true);

            // ✅ 두 번째 페이지 출력 + 진동 시작
            _soundManager.PlayVibrationLoopUntilEnter();
            _uiStoryText.TypePage(INTRO_STORY_PAGES[1].ToList(), 70, ConsoleColor.White);
            ShowEnterPrompt();
            while (Console.KeyAvailable) Console.ReadKey(true);
            _soundManager.StopVibration();

            // ✅ 남은 스토리 출력
            for (int i = 2; i < INTRO_STORY_PAGES.Count; i++)
            {
                if (i == 5)
                {
                    _soundManager.StopBirdSound();
                    _soundManager.PlayOnce("body.wav");
                    _soundManager.PlayHorrorLoop();
                }

                // 📌 6번째 페이지만 흔들림 효과 적용
                bool isShaking = (i == 5);
                var textColor = i >= 5 ? ConsoleColor.Red : ConsoleColor.White;

                _uiStoryText.TypePage(INTRO_STORY_PAGES[i].ToList(), 70, textColor, isShaking);
                ShowEnterPrompt();
                while (Console.KeyAvailable) Console.ReadKey(true);
            }
            _soundManager.StopCurrentLoop(); //임시로 horror.wav 2번 재생 방지
            _soundManager.StopBirdSound();
        }


        //다음으로 넘어가는 문구
        private void ShowEnterPrompt()
        {
            string msg = ">> [Enter] 다음으로";
            int x = (Console.WindowWidth - msg.Length) / 2;
            Console.SetCursorPosition(x, Console.WindowHeight - 2);
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.Write(msg);
            Console.ResetColor();
        }
    }
}