namespace HiLow.Application.Services.Cards.Strategy
{
    public class CardStrategy
    {
        public GuessCard Guess { get; set; } 

        public int Validate(string guess, string nextCard, string previousCard)
        {
            return Guess.MatchRound(guess, nextCard, previousCard);
        }
    }
}
