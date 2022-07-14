using HiLow.Entity.SeedWorks;
using HiLow.Infrastructure.SeedWorks;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Linq.Expressions;

namespace HiLow.Infrasctructure
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly AppDbContext _dbContext;
        private DbSet<T> _dbSet;

        private DbSet<T> DbSet => _dbSet ??= _dbContext.Set<T>();

        public Repository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Add(T entity)
        {
            DbSet.Add(entity);
        }

        public void Delete(T entity)
        {
            DbSet.Remove(entity);
        }

        public async Task<T> GetAsync(int id, params Expression<Func<T, object>>[] includes)
        {
            var query = _dbContext.Set<T>().AsQueryable();

            return await includes.Aggregate(query, (q, w) => q.Include(w)).FirstOrDefaultAsync(_ => (_ as EntityBase).Id == id);
        }

        public void Update(T entity)
        {
            DbSet.Update(entity);
        }
    }
}