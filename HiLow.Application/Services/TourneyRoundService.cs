using HiLow.Application.SeedWorks;
using HiLow.Application.Services.Interfaces;
using HiLow.Entity.Entities.TourneyRounds;
using HiLow.Infrastructure.SeedWorks;

namespace HiLow.Application.Services
{
    public class TourneyRoundService : BaseService, ITourneyRoundService
    {
        private readonly IRepository<TourneyRound> _tourneyRoundRepo;

        public TourneyRoundService(IUnitOfWork unitOfWork, IRepository<TourneyRound> tourneyRoundRepo) : base(unitOfWork)
        {
            _tourneyRoundRepo = tourneyRoundRepo;
        }

        public async Task AddRound(TourneyRound tourneyRound)
        {
            _tourneyRoundRepo.Add(tourneyRound);

            await UnitOfWork.SaveChangeAsync();
        }
    }
}