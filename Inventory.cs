namespace week3
{
    public class Inventory
    {
        private List<Item> items = new List<Item>();

        // ▼ 외부에서 items 리스트를 읽기 전용으로 접근할 수 있게 해주는 C# 프로퍼티
        public IReadOnlyList<Item> Items => items;

        internal void Add(Item item) => items.Add(item);
        internal void Remove(Item item) => items.Remove(item);
    }
}
