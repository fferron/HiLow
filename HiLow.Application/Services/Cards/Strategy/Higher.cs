namespace HiLow.Application.Services.Cards.Strategy
{
    public class Higher : GuessCard
    {
        public Higher() { } 

        public override int MatchRound(string guess, string nextCard, string previousCard) 
        {
            if (CardComparison.IsHigher(nextCard, previousCard))
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
