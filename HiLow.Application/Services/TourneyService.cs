using HiLow.Application.Constants;
using HiLow.Application.SeedWorks;
using HiLow.Application.Services.Interfaces;
using HiLow.Entity.Entities.Models.Responses;
using HiLow.Entity.Entities.TourneyRounds;
using HiLow.Entity.Entities.Tourneys;
using HiLow.Entity.Enums;
using HiLow.Entity.Exceptions;
using HiLow.Entity.Exceptions.Game.Canceled;
using HiLow.Infrastructure.SeedWorks;
using Microsoft.Extensions.Logging;

namespace HiLow.Application.Services
{
    public class TourneyService : BaseService, ITourneyService
    {
        private readonly ILogger<TourneyService> _logger;
        private readonly IRepository<Tourney> _tourneyRepo;
        private readonly ITourneyRoundService _tourneyRoundService;
        private readonly ICardService _cardService;

        public TourneyService(IUnitOfWork unitOfWork, IRepository<Tourney> tourneyRepo, ITourneyRoundService tourneyRoundService, ICardService cardService) : base(unitOfWork)
        {
            _tourneyRepo = tourneyRepo;
            _tourneyRoundService = tourneyRoundService;
            _cardService = cardService;
        }

        public async Task<GetTourneyByIdResponseDTO> GetScore(int id)
        {
            try
            {
                GetTourneyByIdResponseDTO responseTourneyDTO = new GetTourneyByIdResponseDTO();

                var tourney = await _tourneyRepo.GetAsync(id, r => r.Rounds);

                if (tourney != null)
                {
                    if (IsTheLastRound(tourney))
                    {
                        tourney = await UpdateWinnerPlayerInfos(tourney);

                        responseTourneyDTO.Message = $"The final result for the game{tourney.Id} is {tourney.Player1}: {tourney.Player1ScoreCount()} and {tourney.Player2}: {tourney.Player2ScoreCount()}";
                    }
                    else
                    {
                        responseTourneyDTO.Message = $"The current score for the game{tourney.Id} is {tourney.Player1}: {tourney.Player1ScoreCount()} and {tourney.Player2}: {tourney.Player2ScoreCount()}";
                    }
                }
                else
                {
                    throw new EntityNotFoundException($"Cannot find game with id {id}");
                }

                return responseTourneyDTO;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<CreateStartGameResponseDTO> CreateGame(StatusTourney statusTourney, string player1, string player2, DateTime createDate)
        {
            try
            {
                CreateStartGameResponseDTO responseStartGame = new CreateStartGameResponseDTO();

                #region Dealer shuffles the cards and then choose one 
                var dealerCard = _cardService.CardMaker();

                var tourney = new Tourney(statusTourney, player1, player2, createDate);

                _tourneyRepo.Add(tourney);
                #endregion

                // Save the start game into Tourney table
                await UnitOfWork.SaveChangeAsync();

                if (tourney.Id > 0)
                {
                    // Save the first round into Tourney Round table
                    await _tourneyRoundService.AddRound(
                           new TourneyRound(
                               (int)Round.First,
                               0,
                               0,
                               tourney.Id,
                               dealerCard.Number,
                               dealerCard.Suit
                               )
                           );

                    responseStartGame.GameId = tourney.Id;
                    responseStartGame.Message = $"The first card from Game {tourney.Id} is {dealerCard.Number} of {dealerCard.Suit}";
                }
                else
                {
                    throw new GameBadRequestException($"Cannot create game for players {player1} and {player2}.");
                }

                return responseStartGame;
            }
            catch (Exception)
            {
                throw;
            }
            
        }

        public async Task<CreateTourneyByGameIdResponseDTO> CreateRound(int gameId, string guess)
        {
            try
            {
                CreateTourneyByGameIdResponseDTO responseCreateRound = new CreateTourneyByGameIdResponseDTO();

                var tourney = await _tourneyRepo.GetAsync(gameId, r => r.Rounds);

                if (tourney == null)
                {
                    throw new EntityNotFoundException($"Cannot find game with id {gameId}");
                }

                if (tourney.Rounds.Count() < (int)Round.Last)
                {
                    var nextCard = _cardService.CardMaker();

                    // Get the next card (should be a card that was not showned)
                    while (tourney.Rounds.ToList().Any(t => t.CardFullname == nextCard.CardFullname))
                    {
                        nextCard = _cardService.CardMaker();
                    }

                    var previousCard = tourney.Rounds.Last();

                    var matchGuess = _cardService.CardValidateGuess(guess, nextCard.CardFullname, previousCard.CardFullname);

                    // Get the current round and sum with 1
                    var currentRound = tourney.Rounds.Count() + 1;

                    // Identifies who is the turn of ?
                    if (currentRound % 2 == 0)
                    {
                        // Player 2
                        await _tourneyRoundService.AddRound(new TourneyRound(currentRound, 0, matchGuess, tourney.Id, nextCard.Number, nextCard.Suit));
                    }
                    else
                    {
                        // Player 1
                        await _tourneyRoundService.AddRound(new TourneyRound(currentRound, matchGuess, 0, tourney.Id, nextCard.Number, nextCard.Suit));
                    }

                    var tourneyRoundId = await UnitOfWork.SaveChangeAsync();

                    var answerDescription = (matchGuess.Equals(1)) ? "Correct" : "Incorrect";

                    responseCreateRound.Message = $"The card in game{gameId} is {nextCard.Number} of {nextCard.Suit}. {answerDescription}";
                }
                else
                {
                    throw new GameBadRequestException($"Endgame! Consult score's game {gameId}!");
                }

                return responseCreateRound;
            }
            catch (Exception)
            {
                throw;
            }
        }

        #region Private methods
        private bool IsTheLastRound(Tourney tourney)
        {
            if (tourney.Rounds.Count() == HiLowConstants.NumberOfRounds)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private async Task<Tourney> UpdateWinnerPlayerInfos(Tourney tourney)
        {
            if (tourney.Player1ScoreCount() > tourney.Player2ScoreCount())
            {
                tourney.Winner = Winner.Player1;
            }
            else if (tourney.Player2ScoreCount() > tourney.Player1ScoreCount())
            {
                tourney.Winner = Winner.Player2;
            }
            else
            {
                tourney.Winner = Winner.BreakEven;
            }

            tourney.Status = StatusTourney.Finished; // End Game
            tourney.Player1ScoreFinal = tourney.Player1ScoreCount();
            tourney.Player2ScoreFinal = tourney.Player2ScoreCount();

            _tourneyRepo.Update(tourney);
            await UnitOfWork.SaveChangeAsync();

            return tourney;
        }
        #endregion
    }
}