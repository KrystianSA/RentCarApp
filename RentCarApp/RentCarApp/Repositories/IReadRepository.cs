using RentCarApp.Entities;
using RentCarApp.Repositories;

namespace RentCarApp.Repositories
{
    public interface IReadRepository<out T> where T : class, IEntity
    {
        IEnumerable<T> GetAll();
        T GetById(int id);
    }
}