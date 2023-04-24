using Microsoft.EntityFrameworkCore;
using RentCarApp.Entities;
using System.Collections.Generic;
using RentCarApp.Data;

namespace RentCarApp.Repositories
{
    public class SqlRepository<T> : IRepository<T> where T : class, IEntity, new()
    {
        public readonly DbSet<T> dbSet;
        private readonly DbContext dbContext;

        public event EventHandler<T>? ItemAdded;
        public event EventHandler<T>? ItemRemoved;
        public event EventHandler<T>? ItemsSaveToFile;

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
            dbSet.Add(item);
        }
        public void Remove(T item)
        {
            dbSet.Remove(item);
        }
        public void Save()
        {
            dbContext.SaveChanges();
        }

        public void SortElements()
        {
            throw new NotImplementedException();
        }

        public void WriteToFileTxt()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<T> Read()
        {
            throw new NotImplementedException();
        }
    }
}
