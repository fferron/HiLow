using System.ComponentModel.DataAnnotations;
using System.Text;

namespace HiLow.Entities.Models.Request
{
    public class CreateTourneyDTO
    {
        /// <summary>
        /// Name of the player 1. The service generates automatically a player name
        /// </summary>
        [MaxLength(150, ErrorMessage = "The name field should have a maximum size of 150 characters")]
        public string Player1 { get; set; }

        /// <summary>
        /// Name of the player 2. The service generates automatically a player name
        /// </summary>
        [MaxLength(150, ErrorMessage = "The name field should have a maximum size of 150 characters")]
        public string Player2 { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            sb.Append($"Player 1 Name: {Player1}");
            sb.Append($"Player 2 Name: {Player2}");

            return sb.ToString();
        }
    }
}
