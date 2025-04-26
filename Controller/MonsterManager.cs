using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using week3;


namespace week3
{
    // 몬스터를 관리하는 매니저 클래스
    public class MonsterManager
    {
        Random random = new Random();
        Player player;
        Monster monster;
        List<Monster> normalMonsters = new List<Monster>(); //-lv.2 lv4 lv10 lv6  <-한전투에 계속 등장할 애들 (랜덤값으로 1-4마리가 나와요)  
        List<Monster> hardMonsters = new List<Monster>();



        // ========= 메서드 영역 =========

        // 몬스터 데이터를 초기화하는 함수
        public void AddMonsters()
        {

            string monster1question = @"
CS1002가 당신에게 다가옵니다. 
왠지 맞추지 않으면 여태껏 작성했던 코드에 오류가 발생할 것 같습니다.
Error Error Error Error . . . CS1□02. CS1002.
Console.WriteLine()에서 CS1002 발생.즉시, 해결.바람.

-> 답을 입력해주세요.
";

            string monster2question = @"
지옥에서 올라온 극한의 E가 당신에게 다가옵니다. 
왠지 당신은 이 상황을 기피하고 싶습니다..
우리 같이 스터디, 해요. 같,이 스※디 해요.
저 이거 뭔지 모르겠는데. 알려주세요. ■□
분명... 상속 클래스....에서만 접근할 수 있는 게 있었는데.. 기억이 나질 않아요...
알려주세요. 알려. 주■※§?

-> 답을 입력해주세요.
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

-> 답을 입력해주세요.
";

            string monster5question = @"
지금 Zeb코인을 공짜로 얻으시려고요?
이 세상에는 대가 없는 공짜는 없습니다. . . 저를 건들이셨다니..
■※§○◎...  . int[]arr = {{3, 2, 5, 1, 4}} 를
Array.Sort(arr); 을 하면 arr은 어떻게 정렬이. 되나.요? 

-> 답을 입력해주세요.
";

            string monster6question = @"
단서를 쉽게 찾으려하다니.
TIL 작성은요? 제출은요? 퇴실체크는요? .. .다 안 하셨군요. . .
그럼 유감스럽게도 대신 문제를 풀어 주셔야겠습니다. 
부모 클래스에서 이미 정의된 메서드를 자식 클래스에서 재정의하는 것은?

-> 답을 입력해주세요.
";

            string monster7question = @"
지이이잉 징, 지이이잉........... 지잉...
인식이 되질 않습니다. 인식이 되질 않습니다. 인식이 되질 않습니다.
않습니다. 않습니다. 않□니다. 않습니다...dksgtmqslek..
사용자 인식을 하기 위해서 답안을 적어주셔야 합□다.

마트에 가서 사과를 세 개를 사 오고 만약에 수박이 있으면 한 개 사와라고 할 때
어떤 것을 몇 개 사 가야할까?

-> □□ ? 개
";

            string monster8question = @"
형용할 수 없는 기괴한 비명소리가 들려옵니다 . . .
빠르게 처치하지 않으면 여태껏 열심히 작성했던 코드들이 망가질 것 같습니다. . .
출....력...결..과...작성...바람..

bool a = true;
bool b = false;

if (a || b && false)
    Console.WriteLine(""조건 통과!"");
else
    Console.WriteLine(""조건 실패!"");

-> 답을 입력해주세요.
";
            normalMonsters.Add(new Monster("컴파일에러(CS1002)",MonsterType.Normal, monster1question, ";", 39, 12, 9));

            normalMonsters.Add(new Monster("극한의 대문자 E", MonsterType.Normal, monster2question, "protected", 21, 17, 11));

            normalMonsters.Add(new Monster("※△ㅁ쀓?뚫.뚫/딻?띫", MonsterType.Normal, monster3question, "FindNumberOptimized -> FindNumber", 45, 10, 16));

            normalMonsters.Add(new Monster("{name}", MonsterType.Normal, monster4question, "!=", 12, 13, 6));

            hardMonsters.Add(new Monster("ZebC□in", MonsterType.Hard, monster5question, "1, 2, 3, 4, 5", 80, 25, 18));

            hardMonsters.Add(new Monster("{message}", MonsterType.Hard, monster6question, "override", 75, 36, 25));

            hardMonsters.Add(new Monster("FakeCam", MonsterType.Hard, monster7question, "사과 한 개", 80, 24, 35));

            hardMonsters.Add(new Monster("Codebraker", MonsterType.Hard, monster8question, "조건 통과!", 60, 28, 15));
        }



        // 랜덤으로 몬스터를 가져오는 함수
        // count: 가져올 몬스터 수
        // includeHard: true면 하드 몬스터도 포함

        public List<Monster> GetRandomMonsters(int count, bool includeHard = false)
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
    }
}
