/*
namespace RentCarApp.Repositories
{
    public class ListRepository<T> : IRepository<T> where T : class,IEntity, new()
    {
        public readonly List<T> items = new();

        public IEnumerable<T> GetAll()
        {
            return items.ToList();
        }
        public T GetById(int id)
        {
           return items.Single(items => items.Id == id);
        }
        public void Add(T item)
        {
            item.Id = items.Count + 1;
            items.Add(item);
        }
        public void Remove(T item)
        {
            items.Remove(item);
        }
        public void Save()
        {

        }

        public void deleteAllFromFile()
        {
            throw new NotImplementedException();
        }

        public void getAllToFile()
        {
            throw new NotImplementedException();
        }
    }
}
*/