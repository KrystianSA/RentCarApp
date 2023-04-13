/*using Microsoft.EntityFrameworkCore;
using RentCarApp.Entities;
using System.Collections.Generic;

namespace RentCarApp.Repositories
{
    public class SqlRepository<T> : IRepository<T> where T : class, IEntity, new()
    {
        public readonly DbSet<T> dbSet;
        private readonly DbContext dbContext;
        public SqlRepository(DbContext DbContext)
        {
            dbContext = DbContext;
            dbSet = dbContext.Set<T>();
        }
        public IEnumerable<T> GetAll()
        {
            return dbSet.ToList();
        }
        public T GetById(int id)
        {
            return dbSet.Find(id);
        }
        public void Add(T item)
        {
            ; dbSet.Add(item);
        }
        public void Remove(T item)
        {
            dbSet.Remove(item);
        }
        public void Save()
        {
            dbContext.SaveChanges();
        }
    }
}
*/