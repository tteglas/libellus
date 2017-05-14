using Libellus.DataAccess.Database;
using Libellus.DataAccess.Repositories.Interface;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Libellus.DataAccess.Repositories.Implementation
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        private readonly LibellusDbContext _dbContext;
        private IDbSet<T> DbSet => _dbContext.Set<T>();

        public BaseRepository(LibellusDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Attach(T entity)
        {
            DbSet.Attach(entity);
        }

        public void Detach(T entity)
        {
            _dbContext.Entry(entity).State = EntityState.Detached;
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
            _dbContext.Entry(entity).State = EntityState.Modified;
        }

        public void Save()
        {
            _dbContext.SaveChanges();
        }
    }
}
