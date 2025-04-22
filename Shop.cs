namespace week3
{
    public class Shop
    {
        private List<Item> shopItems = new List<Item>();

        public IReadOnlyList<Item> ShopItems => shopItems;

        public Shop()
        {
            // 초기 상점 아이템 구성
            shopItems.Add(new Item("이름1", "설명1", 1100));
            shopItems.Add(new Item("이름2", "설명2", 1200));
            shopItems.Add(new Item("이름3", "설명3", 1300));
        }
    }
}
