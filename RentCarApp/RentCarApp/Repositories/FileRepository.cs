using RentCarApp.Entities;
using RentCarApp.Repositories;
using System.Collections.Immutable;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace RentCarApp.Repositories
{
    public class FileRepository<T> : IRepository<T> where T : class, IEntity, new()
    {
        public readonly List<T> _items = new();
        public readonly List<T> _updatedItems = new();
        public event EventHandler<T>? ItemAdded;
        public event EventHandler<T>? ItemRemoved;
        private readonly string _fileNameJson = $"{typeof(T).Name}_save.json";

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
        public void WriteToFileJson()
        {
            var objectSerialized = JsonSerializer.Serialize<IEnumerable<T>>(_items);
            File.WriteAllText(_fileNameJson, objectSerialized);
        }
        public IEnumerable<T> ReadFromFileJsonAndAddToUpdatedList()
        {
            if (File.Exists(_fileNameJson))
            {
                var objectsSerialized = File.ReadAllText(_fileNameJson);
                var deserializedObject = JsonSerializer.Deserialize<IEnumerable<T>>(objectsSerialized);
                if (deserializedObject != null)
                {
                    _updatedItems.Clear();
                    foreach (var item in deserializedObject)
                    {
                        _updatedItems.Add(item);
                    }
                }
            }
            return _updatedItems;
        }
        public void SaveToFileTxt(string fileName)
        {
            using (var writer = File.AppendText(fileName))
            {
                foreach (var item in _updatedItems)
                {
                    writer.WriteLine(item);
                }
            }
        }
        public void DeleteFileTxt(string fileName)
        {
            File.Delete(fileName);
        }
    }
}
