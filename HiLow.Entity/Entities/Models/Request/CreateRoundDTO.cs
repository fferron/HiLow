using System.ComponentModel.DataAnnotations;
using System.Text;

namespace HiLow.Entities.Models.Request
{
    public class CreateRoundDTO
    {
        /// <summary>
        /// Guess is the answer the player will answer.
        /// </summary>
        [Required(ErrorMessage = "Field guess is required")]
        [RegularExpression("HIGHER|LOWER|higher|lower|Higher|Lower", ErrorMessage = "Please insert a valid value between 'HIGHER' or 'LOWER' in capital or lower case letters for the guess field")]
        [MaxLength(6, ErrorMessage = "The hint field should have a maximum size of 6 characters")]
        public string Guess { get; set; }


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            sb.Append($"Hint: {Guess}");

            return sb.ToString();
        }
    }
}
