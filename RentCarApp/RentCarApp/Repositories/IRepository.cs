using RentCarApp.Entities;
using RentCarApp.Repositories;

namespace RentCarApp.Repositories
{
    public interface IRepository<T> : IWriteRepository<T>, IReadRepository<T> where T : class, IEntity
    {
    }
}