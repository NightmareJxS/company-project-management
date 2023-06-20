using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class RepositoryBase<T> where T : class
    {
        private readonly CompanyXDbContext _context;
        protected readonly DbSet<T> _dbSet;

        public RepositoryBase()
        {
            _context = new CompanyXDbContext();
            _dbSet = _context.Set<T>();
        }

        public void Create(T entity)
        {
            _dbSet.Add(entity);
            _context.SaveChanges();
        }

        public List<T> GetAll()
        {
            return _dbSet.ToList();
        }

        public T Get(Expression<Func<T, bool>> predicate)
        {
            return _dbSet.Where(predicate).FirstOrDefault();
        }

        public bool Update(T entity)
        {
            _dbSet.Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
            return _context.SaveChanges() > 0;
        }

        public bool UpdateTrackedEntity<T>(T entity) // Note: Watch out for concurrency issue
        {
            var entityType = _context.Model.FindEntityType(typeof(T));
            var primaryKey = entityType.FindPrimaryKey();
            var primaryKeyPropertyName = primaryKey.Properties.First().Name; // Only work with table have 1 primary key, could change it to get 2 different primary key name

            var existingEntity = _dbSet.Local.FirstOrDefault(e => _context.Entry(e).Property(primaryKeyPropertyName).CurrentValue.Equals(_context.Entry(entity).Property(primaryKeyPropertyName).CurrentValue));

            if (existingEntity != null)
            {
                _context.Entry(existingEntity).State = EntityState.Detached;
            }

            _context.Entry(entity).State = EntityState.Modified;

            return _context.SaveChanges() > 0;
        }


        public bool Delete(Expression<Func<T, bool>> predicate)
        {
            _dbSet.Remove(Get(predicate));
            return _context.SaveChanges() > 0;
        }
    }
}
