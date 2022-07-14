using System.Linq.Expressions;

namespace HiLow.Infrastructure.SeedWorks
{
    public interface IRepository<T> where T : class   
    {   
        void Add(T entity);   
        void Delete(T entity);   
        void Update(T entity);
        Task<T> GetAsync(int id, params Expression<Func<T, object>>[] includes);
    }
}