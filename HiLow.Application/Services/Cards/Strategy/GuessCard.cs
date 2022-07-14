namespace HiLow.Application.Services.Cards.Strategy
{
    public abstract class GuessCard
    {
        public abstract int MatchRound(string guess, string nextCard, string previousCard);
    }
}
