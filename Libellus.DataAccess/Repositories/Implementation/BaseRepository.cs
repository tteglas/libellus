using Libellus.DataAccess.Database;
using Libellus.DataAccess.Repositories.Interface;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;

namespace Libellus.DataAccess.Repositories.Implementation
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        private readonly LibellusDbContext DbContext;
        private IDbSet<T> DbSet
        {
            get { return DbContext.Set<T>(); }
        }

        public BaseRepository(IObjectContextAdapter dbContext)
        {
            DbContext = (LibellusDbContext)dbContext;
        }

        public List<T> GetAll()
        {
            return DbSet.Distinct().ToList();
        }

        public void Add(T entity)
        {
            //DbContext.Entry(entity).State = EntityState.Added;
            DbSet.Add(entity);
        }

        public void Delete(T entity)
        {
            DbSet.Remove(entity);
        }

        public void Update(T entity)
        {
            DbContext.Entry(entity).State = EntityState.Modified;
        }

        public void Save()
        {
            DbContext.SaveChanges();
        }
    }
}
