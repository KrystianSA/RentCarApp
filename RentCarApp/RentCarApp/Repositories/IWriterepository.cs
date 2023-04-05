using RentCarApp.Entities;
using RentCarApp.Repositories;

namespace RentCarApp.Repositories
{
    public interface IWriteRepository<in T> where T : class, IEntity
    {
        void Add(T item);
        void Remove(T lineToRemove);
        void WriteToFileJson();
        void SaveToFileTxt(string fileName);
        void DeleteFileTxt(string fileName);
    }
}