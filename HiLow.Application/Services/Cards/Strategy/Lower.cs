namespace HiLow.Application.Services.Cards.Strategy
{
    public class Lower : GuessCard
    {
        public Lower(){ }

        public override int MatchRound(string guess, string nextCard, string previousCard)
        {
            if (CardComparison.IsLower(nextCard, previousCard))
            {
                //Correct
                return 1;
            }
            else
            {
                //Incorrect
                return 0;
            }
        }
    }
}
