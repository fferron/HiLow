namespace HiLow.Infrastructure.SeedWorks
{
    public interface IUnitOfWork    
    {   
        Task<int> SaveChangeAsync();   
    }  
}