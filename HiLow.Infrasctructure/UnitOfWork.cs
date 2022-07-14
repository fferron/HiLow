using HiLow.Infrastructure.SeedWorks;

namespace HiLow.Infrasctructure
{
    public sealed class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly AppDbContext _appDbContext;

        public UnitOfWork(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<int> SaveChangeAsync()
        {
            return _appDbContext.SaveChanges();
        }
        
        private bool _disposed;

        private void Dispose(bool disposing)
        {
            if (!_disposed && disposing)
            {
                _appDbContext.Dispose();
            }
            _disposed = true;
        }
        
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}