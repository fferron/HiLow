
using HiLow.Infrastructure.SeedWorks;

namespace HiLow.Application.SeedWorks
{
    public abstract class BaseService
    {
        protected readonly IUnitOfWork UnitOfWork;

        protected BaseService(IUnitOfWork unitOfWork)
        {
            UnitOfWork = unitOfWork;
        }
    }
}