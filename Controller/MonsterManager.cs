using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using week3;


namespace week3
{
    // 몬스터를 관리하는 매니저 클래스
    public class MonsterManager
    {
        // 필드 영역

        // 일반 몬스터 리스트
        private List<Monster> normalMonsters { get; } = new List<Monster>(); //-lv.2 lv4 lv10 lv6  <-한전투에 계속 등장할 애들 (랜덤값으로 1-4마리가 나와요)  
        // 하드 몬스터 리스트
        private List<Monster> hardMonsters { get; } = new List<Monster>();

        // 랜덤 숫자 생성을 위한 Random 인스턴스
        private Random random = new Random();

        // ========= 메서드 영역 =========

        // 몬스터 데이터를 초기화하는 함수
        public void InitializeMonsters()
        {
            // 일반 몬스터 추가

            string monster1question = @"
CS1002가 당신에게 다가옵니다. 
왠지 맞추지 않으면 여태껏 작성했던 코드에 오류가 발생할 것 같습니다.
Error Error Error Error . . . CS1□02. CS1002.
Console.WriteLine()에서 CS1002 발생.즉시, 해결.바람.
";

            string monster2question = @"
지옥에서 올라온 극한의 E가 당신에게 다가옵니다. 
왠지 당신은 이 상황을 기피하고 싶습니다..
우리 같이 스터디, 해요. 같,이 스※디 해요.
저 이거 뭔지 모르겠는데. 알려주세요. ■□
분명... 상속 클래스....에서만 접근할 수 있는 게 있었는데.. 기억이 나질 않아요...
알려주세요. 알려. 주■※§?
";

            string monster3question = @"
형체를 알아볼 수 없는 무언가가 당신에게 다□옵니다.
이 이상 가까워지면 큰일이 날 것 같습니다.
뚫ㅁ띫딻.띫똛뚧.띏?딻.＆※..
저 이거 뭔지 모르겠는데. 알려주세요. ■□
배열이 정렬된 상태일 때, 이진 탐색을 사용해 성능을 개선을 하라해서 해봤는데.. 틀렸어. 고쳐줘. .
알려주세요. 알려. 주■※§?

public static bool FindNumberOptimized (int[] array, int target)

    for (int i = 0; i < array.Length; i++)

        if(array[i] == target)

            return true;

    return false;

답안 예시 : ㅇㅇ -> ㅇㅇ
";

            string monster4question = @"
{{name}} {{name}} {{name}} {{name}} {{name}} {{name}}
뭔가 부족해... 부족해.. 부족해... 부족해 {{name}}이 부족해...
너가 내 {{name}} 좀 정해 주지 않을 래? . . . 알려줘※려줘 알려줘 알려줘 알려줘.
난 주로 두 값이 다를 때 true .. . 난 누구야 ..?
";

            string monster5question = @"
지금 Zeb코인을 공짜로 얻으시려고요?
이 세상에는 대가 없는 공짜는 없습니다. . . 저를 건들이셨다니..
■※§○◎...  . int[]arr = {{3, 2, 5, 1, 4}} 를
Array.Sort(arr); 을 하면 arr은 어떻게 정렬이. 되나.요? 
";

            string monster6question = @"
단서를 쉽게 찾으려하다니.
TIL 작성은요? 제출은요? 퇴실체크는요? .. .다 안 하셨군요. . .
그럼 유감스럽게도 대신 문제를 풀어 주셔야겠습니다. 
부모 클래스에서 이미 정의된 메서드를 자식 클래스에서 재 정의하는 것은?
";

            string monster8question = @"
-
";

            string monster7question = @"
-
";
            normalMonsters.Add(MonsterFactory.CreateNormal("컴파일에러(CS1002)",monster1question, ";", 50, 5));

            normalMonsters.Add(MonsterFactory.CreateNormal("극한의 대문자 E", monster2question, ";", 50, 5));

            normalMonsters.Add(MonsterFactory.CreateNormal("※△ㅁ쀓?뚫.뚫/딻?띫", monster3question, "FindNumberOptimized -> FindNumber", 50, 5));

            normalMonsters.Add(MonsterFactory.CreateNormal("{name}", monster4question, "!=", 50, 5));

            hardMonsters.Add(MonsterFactory.CreateHard("ZebC□in", monster5question, "1, 2, 3, 4, 5", 50, 5));

            hardMonsters.Add(MonsterFactory.CreateHard("{message}", monster6question, " ", 50, 5));

            hardMonsters.Add(MonsterFactory.CreateHard("FakeCam", monster7question, " ", 50, 5));

            hardMonsters.Add(MonsterFactory.CreateHard("Codebraker", monster8question, " ", 50, 5));
        }

        // 랜덤으로 몬스터를 가져오는 함수
        // count: 가져올 몬스터 수
        // includeHard: true면 하드 몬스터도 포함

        public static List<Monster> GetRandomMonsters(int count, bool includeHard = false)
        {
            List<Monster> allMonsters = new List<Monster>(normalMonsters);


            // 하드 몬스터를 포함해야 한다면 추가
            if (includeHard)
            {
                allMonsters.AddRange(hardMonsters);
            }
            List<Monster> selectedMonsters = new List<Monster>();

            // allMonsters.AddRange(hardMonsters); // 하드 몬스터 항상 추가

            // 랜덤으로 몬스터 뽑기
            List<Monster> tempList = new List<Monster>(allMonsters);

            for (int i = 0; i < count; i++)
            {
                // 뽑을 몬스터가 더 이상 없으면 중단
                if (tempList.Count == 0) break;

                int index = random.Next(tempList.Count); // 0~tempList.Count -1 중 하나 뽑기
                selectedMonsters.Add(tempList[index]); // 몬스터 추가
                tempList.RemoveAt(index); // 중복 방지를 위해 제거
            }
            return selectedMonsters;
        }
        
        public class MonsterFactory
        {
            public Monster CreateNormal(string name, string question, string correctAnswer, int maxHealth, int attackPower)
            {
                return new Monster(name, MonsterType.Normal, question, correctAnswer, maxHealth, attackPower);
            }
            public Monster CreateHard(string name, string question, string correctAnswer, int maxHealth, int attackPower)
            {
                return new Monster(name, MonsterType.Hard, question, correctAnswer, maxHealth, attackPower);
            }
        }
    }
}
