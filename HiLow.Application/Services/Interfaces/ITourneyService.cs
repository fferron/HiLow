using HiLow.Entity.Entities.Models.Responses;
using HiLow.Entity.Enums;

namespace HiLow.Application.Services.Interfaces
{
    public interface ITourneyService
    {
        public Task<GetTourneyByIdResponseDTO> GetScore(int id);
        public Task<CreateStartGameResponseDTO> CreateGame(StatusTourney statusTourney, string player1, string player2, DateTime CreateDate);
        public Task<CreateTourneyByGameIdResponseDTO> CreateRound(int gameId, string hint);
    }
}