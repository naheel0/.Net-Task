using task4.Models;

namespace task4.Services
{
    public interface IItemService
    {
        List<Item> GetAll();
        Item? GetById(int id);
        Item Add(Item item);
        bool Update(int id, Item item);
    }
    public class ItemService : IItemService
    {
        private readonly List<Item> _items = new();
        private int _nextId = 1;
        public List <Item> GetAll()=> _items;
        public Item GetById(int id) => _items.FirstOrDefault(i => i.id == id);
        public Item Add(Item item)
        {
            item.id = _nextId++;
            _items.Add(item);
            return item;
        }
        public bool Update(int id, Item item)
        {
            var existing = _items.FirstOrDefault(i => i.id == id);
            if (existing == null) return false;   

            existing.Title = item.Title;          
            existing.Description = item.Description;
            return true;                          
        }
    }
}
