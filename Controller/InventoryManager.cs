namespace week3
{
    public class InventoryManager
    {
        //여기에 아이템이 있으면 "if 아이템있음?이렇게" 전투에서 사용 
        private List<Item> playerItems = new List<Item>();
        
        public void AddItem(int itemIndex)
        {
            playerItems.Add(ItemData[itemIndex]);
            Console.WriteLine($"{ItemData[itemIndex].Name}을(를) 인벤토리에 추가했습니다.");
        }

        public void RemoveItem(int itemIndex)
        {
            playerItems.Remove(ItemData[itemIndex]);
            Console.WriteLine($"{ItemData[itemIndex]}을(를) 인벤토리에서 제거했습니다.");
        }

        public List<Item> ItemData = new List<Item>() // 아이템 데이터
        {
            new Item ("캠 수리", "ZEP에서 참여할 때 화면을 보여주는 웹캠이다.\n사용 시 1턴동안 캠을 수리하여 켤 수 있다.","공격", 500),
            new Item("마이크 수리", "ZEP에서 참여할 때 목소리를 송출하는 마이크다.\n사용 시 1턴동안 마이크를 수리하여 켤 수 있다.", "공격", 700),
            new Item("완전 방어", "딱 1번만 적의 공격을 막을 수 있는 ZEb 코인으로 구매 가능한 아이템.\n이런 아이템이 왜 존재하는지 모르겠다.", "방어", 1000),
            new Item("서약서", "ZEP의 법에 복종하기로 한 피의 계약서.", "기타", 500),
            new Item("TIL", "이 곳에서 있었던 일을 꼭 남겨야 한다. 같은 처지를 겪을지도 모르는 이를 위해...", "기타", 500),
            new Item("구글폼", "그들의 부름에 대답해야만 한다.", "기타", 500),
            new Item("캠 발광", "ZEP에서 참여할 때 화면을 보여주는 웹캠에서 강한 빛을 발광시킨다.", "공격", 700),
            new Item("소음", "ZEP에서 참여할 때 목소리를 송출하는 마이크에서 엄청난 크기의 소음을 발생시킨다.", "공격", 700),
            new Item("회복약 (TEST)", "테스트용 회복 아이템", "회복", 700)
        };

    }
}
