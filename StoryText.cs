using static week3.UIManager;

namespace week3
{
    public class StoryText
    {
        UIManager _uiManager;

        public StoryText(UIManager uiManager)
        {
            _uiManager = new UIManager();
        }

        // 메인 텍스트 출력 메서드
        public void IntroText()
        {
            _uiStoryText.BeginTextSet();
            string[] introLines = new string[]
            {
                "저주와 악으□ 가득 찬,",
                "Ne※o에 오신 내을 환영합니다?",
                "",
                "※ 시스템 연결 중..."
            };
            _uiStoryText.TypeEffectCenteredLines(introLines, 30, ConsoleColor.DarkRed);
            _uiStoryText.EndTextSet();
        }
    }
}
