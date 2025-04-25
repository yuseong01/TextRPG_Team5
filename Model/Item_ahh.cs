using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using week3;

namespace week3
{
    public class item_ahh
    {
        public string Name {  get; private set; }
        public string Description {  get; private set; }
        public string Type { get; private set; }
        public int Point {  get; private set; }

        public static Dictionary<string, item_ahh> ItemData = new Dictionary<string, item_ahh>() // 아이템 데이터
        {
            { "캠 수리", new item_ahh {Name = "캠 수리", Description = "ZEP에서 참여할 때 화면을 보여주는 웹캠이다.\n사용 시 1턴동안 캠을 수리하여 켤 수 있다.", Type = "공격", Point = 0} },
            { "마이크 수리", new item_ahh {Name = "마이크 수리", Description = "ZEP에서 참여할 때 목소리를 송출하는 마이크다.\n사용 시 1턴동안 마이크를 수리하여 켤 수 있다.", Type = "공격", Point = 0} },
            { "완전 방어", new item_ahh {Name = "완전 방어", Description = "딱 1번만 적의 공격을 막을 수 있는 ZEb 코인으로 구매 가능한 아이템.\n이런 아이템이 왜 존재하는지 모르겠다.", Type = "방어", Point = 1000} },
            { "서약서", new item_ahh {Name = "서약서", Description = "ZEP의 법에 복종하기로 한 피의 계약서.", Type = "기타", Point = 0} },
            { "TIL", new item_ahh {Name = "TIL", Description = "이 곳에서 있었던 일을 꼭 남겨야 한다. 같은 처지를 겪을지도 모르는 이를 위해...", Type = "기타", Point = 0} },
            { "구글폼", new item_ahh {Name = "구글폼", Description = "그들의 부름에 대답해야만 한다.", Type = "기타", Point = 0} },
            { "캠 발광", new item_ahh {Name = "캠 발광", Description = "ZEP에서 참여할 때 화면을 보여주는 웹캠에서 강한 빛을 발광시킨다.", Type = "공격", Point = 0} },
            { "소음", new item_ahh {Name = "소음", Description = "ZEP에서 참여할 때 목소리를 송출하는 마이크에서 엄청난 크기의 소음을 발생시킨다.", Type = "공격", Point = 0} },
            { "회복약", new item_ahh {Name = "회복약 (TEST)", Description = "테스트용 회복 아이템", Type = "회복", Point = 0} }
        };
    }
}
