using HiLow.Application.SeedWorks;
using HiLow.Application.Services.Cards.Strategy;
using HiLow.Application.Services.Interfaces;
using HiLow.Entity.Entities.Cards;
using HiLow.Infrastructure.SeedWorks;

namespace HiLow.Application.Services
{
    public class CardService : BaseService, ICardService
    {
        public CardService(IUnitOfWork unitOfWork) : base(unitOfWork) { }

        public int CardValidateGuess(string guess, string nextCard, string previousCard)
        {
            CardStrategy card = new CardStrategy();

            try
            {
                if (guess.ToLower().Equals("higher"))
                {
                    card.Guess = new Higher();
                    return card.Validate(guess, nextCard, previousCard);
                }
                else
                { 
                    card.Guess = new Lower();
                    return card.Validate(guess, nextCard, previousCard);
                }
            }
            catch(Exception ex)
            {
                throw new Exception($"Failed to validate hint of the player. Error: ", ex);
            }
        }

        public Card CardMaker()
        {
            string[] suits = { "diamonds", "spades", "hearts", "clubs" };

            int cardNum = (int)(new Random().Next(1, 14));
            string cardSuit = suits[(int)(new Random().Next(0, 4))];

            return new Card(cardNum, cardSuit);
        }
    }
}
