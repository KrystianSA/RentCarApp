using Microsoft.VisualBasic;
using RentCarApp.Entities;
using RentCarApp.Repositories;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace RentCarApp.Repositories
{
    public class FileRepository<T> : IRepository<T> where T : class, IEntity, new()
    {
        public readonly List<T> _items = new();
        private readonly string _fileNameJson = $"{typeof(T).Name}_save.json";
        private readonly string _fileNameTxt = $"Lista Samochodów.txt";
        public event EventHandler<T>? ItemsSaveToFile;
        public event EventHandler<T>? ItemAdded;
        public event EventHandler<T>? ItemRemoved;

        public FileRepository()
        {
        }

        public IEnumerable<T> GetAll()
        {
            return _items.ToList();
        }

        public T GetById(int id)
        {
            return _items.Single(item => item.Id == id);
        }

        public void Add(T item)
        {
            item.Id = _items.Count + 1;
            _items.Add(item);
            ItemAdded?.Invoke(this, item);
        }

        public void Remove(T item)
        {
            _items.Remove(item);
            ItemRemoved?.Invoke(this, item);
        }

        public void Save()
        {
            var objectSerialized = JsonSerializer.Serialize<IEnumerable<T>>(_items);
            File.WriteAllText(_fileNameJson, objectSerialized);
        }

        public IEnumerable<T> Read()
        {
            if (File.Exists(_fileNameJson))
            {
                var objectsSerialized = File.ReadAllText(_fileNameJson);
                var deserializedObject = JsonSerializer.Deserialize<IEnumerable<T>>(objectsSerialized);
                if (deserializedObject != null)
                {
                    foreach (var item in deserializedObject)
                    {
                        _items.Add(item);
                    }
                }
            }
            return _items;
        }

        public void WriteToFileTxt()
        {
            if(File.Exists(_fileNameTxt))
            {
                File.WriteAllText(_fileNameTxt, string.Empty);
            }

            using (var writer = new StreamWriter(_fileNameTxt))
            {
                foreach (var item in _items)
                {
                    writer.WriteLine(item);
                }
            }
            ItemsSaveToFile?.Invoke(this, new T());
        }

        public void SortElements()
        {
            List<T> list = new List<T>();

            for (int i = 0; i < _items.Count; i++)
            {
                _items[i].Id = i + 1;
                list.Add(_items[i]);
            }

            _items.Clear();

            foreach (var item in list)
            {
                _items.Add(item);
            }
        }
    }
}
