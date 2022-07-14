using HiLow.Application.Services.Interfaces;
using HiLow.Entities.Models.Request;
using HiLow.Entity.Entities.Models.Responses;
using HiLow.Entity.Enums;
using HiLow.Entity.Exceptions;
using HiLow.Entity.Exceptions.Models;
using Microsoft.AspNetCore.Mvc;

namespace HiLow.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HiLowController : ControllerBase
    {
        private readonly ITourneyService _tourneyService;

        public HiLowController(ITourneyService tourneyService)
        {
            _tourneyService = tourneyService;
        }

        // Get api/<HiLowController>/{id}
        /// <summary>
        /// Get the score of the game
        /// </summary>
        /// <param name="gameId"></param>
        /// <response code="200">Returns score of the game by id</response>
        /// <response code="404">Game id doesn't exist</response>
        [ProducesResponseType(typeof(GetTourneyByIdResponseDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorDetails), StatusCodes.Status404NotFound)]
        [Produces("application/json")]
        [HttpGet("{gameId:int}")]
        public async Task<GetTourneyByIdResponseDTO> GetScore(int gameId)
        {
            //_logger.LogInformation($"Add item basket id {id}");
            return await _tourneyService.GetScore(gameId);
        }

        // POST api/<HiLowController>
        /// <summary>
        /// Start Game
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     {
        ///         "player1": "John", // Name of the Player 1. The service generates automatically a player name.
        ///         "player2": "James", // Name of the Player 2. The service generates automatically a player name.
        ///     }
        ///
        /// </remarks>
        /// <param name="tourneyRequest"></param>
        /// <response code="201">Successfull call: returns the newly created game id</response>
        /// <response code="400">Invalid Post Body</response>    
        [ProducesResponseType(typeof(CreateStartGameResponseDTO), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ErrorDetails), StatusCodes.Status400BadRequest)]
        [HttpPost]
        [Route("/startgame")]
        public async Task<CreateStartGameResponseDTO> StartGame([FromBody] CreateTourneyDTO tourneyRequest)
        {
            return await _tourneyService.CreateGame(StatusTourney.Started, tourneyRequest.Player1, tourneyRequest.Player2, CreateDate: DateTime.Now);
        }

        // PUT api/<HiLowController>/{id}/rounds
        /// <summary>
        /// Create a round with Player's guess
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     {
        ///         "guess": "Higher", // Player's guess. The value should be 'Higher' or 'Lower'.
        ///     }
        ///
        /// </remarks>
        /// <param name="roundRequest"></param>
        /// <response code="202">Successfull call: returns the newly created round by gameId</response>
        /// <response code="400">Invalid Post Body</response>    
        [ProducesResponseType(typeof(CreateStartGameResponseDTO), StatusCodes.Status202Accepted)]
        [ProducesResponseType(typeof(ErrorDetails), StatusCodes.Status400BadRequest)]
        [HttpPut("{gameId:int}/rounds")]
        public async Task<CreateTourneyByGameIdResponseDTO> AddRound(int gameId, [FromBody] CreateRoundDTO roundRequest)
        {
            //_logger.LogInformation($"Add item basket id {id}");

            if (ModelState.IsValid)
            {
                return await _tourneyService.CreateRound(gameId, roundRequest.Guess);
            }

            throw new GameBadRequestException(ModelState.ToString());
        }
    }
}
