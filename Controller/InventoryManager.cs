namespace week3
{
    public class InventoryManager
    {
        Item item;

        //여기에 아이템이 있으면 "if 아이템있음?이렇게" 전투에서 사용 
        List<Item> playerItems;
        List<Item> healingItems;
        List<Item> equipmentItems;


        public InventoryManager()
        {
            playerItems = new List<Item>();
            healingItems = playerItems.Where(item => item.Type == "회복").ToList();
            equipmentItems = playerItems.Where(item => item.Type != "회복").ToList();
        }
        public void AllItemList()
        {
            for (int i = 0; i < playerItems.Count; i++)
            {
                string index = (playerItems[i].IsEquip)? "[E]" : $"{i + 1}.";

                string item = $"{index} {playerItems[i].Name,-10}| {playerItems[i].Description,-30}";
                Console.WriteLine(item);
            }
        }

        public void AddItem(int itemIndex)
        {
            playerItems.Add(ItemData[itemIndex]);
            Console.WriteLine($"{ItemData[itemIndex].Name}을(를) 인벤토리에 추가했습니다.");
        }

        void SeletEquipItem(int itemIndex)
        {
            if(ItemData[itemIndex].IsEquip)
            {
                item.ToggleSoldStatus(ItemData[itemIndex]);
            }
        }

        public void UseItem(int itemIndex)
        {

            playerItems.Remove(playerItems[itemIndex]);
            Console.WriteLine($"{playerItems[itemIndex]}을(를) 사용했습니다.");
        }

        public List<Item> ItemData = new List<Item>() // 아이템 데이터
        {
            // 보스 전투 전용 아이템(0 ~ 7)
            new Item ("캠 수리", "ZEP에서 참여할 때 화면을 보여주는 웹캠이다.\n사용 시 1턴동안 캠을 수리하여 켤 수 있다.","공격", 500),
            new Item("마이크 수리", "ZEP에서 참여할 때 목소리를 송출하는 마이크다.\n사용 시 1턴동안 마이크를 수리하여 켤 수 있다.", "공격", 700),
            new Item("완전 방어", "딱 1번만 적의 공격을 막을 수 있는 ZEb 코인으로 구매 가능한 아이템.\n이런 아이템이 왜 존재하는지 모르겠다.", "방어", 1000),
            new Item("서약서", "ZEP의 법에 복종하기로 한 피의 계약서.", "기타", 500),
            new Item("TIL", "이 곳에서 있었던 일을 꼭 남겨야 한다. 같은 처지를 겪을지도 모르는 이를 위해...", "기타", 500),
            new Item("구글폼", "그들의 부름에 대답해야만 한다.", "기타", 500),
            new Item("캠 발광", "ZEP에서 참여할 때 화면을 보여주는 웹캠에서 강한 빛을 발광시킨다.", "공격", 700),
            new Item("소음", "ZEP에서 참여할 때 목소리를 송출하는 마이크에서 엄청난 크기의 소음을 발생시킨다.", "공격", 700),
            // 소비 아이템(8 ~ 11)
            new Item("버그패치 정제", "코드 오류로 멈춘 시스템을 다시 돌아가게 해주는 약. 잠시의 안정감을 준다.", "회복", 15, 700),
            new Item("캐시 리프레셔", " 시스템 오류로 지친 마음을 되살려주는 음료. 피로를 풀어준다. ", "회복",35, 1000),
            new Item("코드 리팩토링 팩", "코드에서 발생한 오류를 완벽하게 해결하고, 시스템의 안정성을 회복시켜주는 아이템. 리팩토링의 피로를 말끔히 없애준다. 내가 갖고싶어서 넣은 건 아니다.", "회복", 100, 1300),
            new Item("멘탈 리부트", "정신적 과부하를 해소하는 코드 복구 프로그램. 집중력을 되찾게 해준다.", "회복", 700),
            //장착 가능한 장비(12 ~ 17)
            new Item("팝업 차단 고글", "눈앞에 뜨는 오류를 막아주는 아이템. 착용감이 좋다.", "방어구", 3, 1000),
            new Item("방화벽 조끼", "해킹과 바이러스로부터 상체를 보호해준다.", "방어구", 5, 1400),
            new Item("보안 토큰 반지", "손에 끼우면 강력한 인증 보호막이 생성됨. 최상급 방어 장비.", "방어구", 7, 2000),
            new Item("핑 해머", "지연되는 신호를 이용해 적의 움직임을 살짝 멈추게 함. 해머인데 가볍다.", "무기", 3, 1000),
            new Item("코드 인젝터", "악성 코드를 적에게 주입해 랜덤한 오류를 유발한다. 비도덕적이라 호불호가 갈린다.", "무기", 5, 1400),
            new Item("디도스 블레이드", "서버를 과부하시키는 에너지 검. 너무 강력해서 단종되었다.", "무기", 7, 2000)
        };

    }
}
