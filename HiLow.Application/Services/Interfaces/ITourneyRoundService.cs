using HiLow.Entity.Entities.TourneyRounds;

namespace HiLow.Application.Services.Interfaces
{
    public interface ITourneyRoundService
    {
        public Task AddRound(TourneyRound tourneyRound);
    }
}