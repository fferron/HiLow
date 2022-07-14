using HiLow.Entity.Entities.Cards;

namespace HiLow.Application.Services.Interfaces
{
    public interface ICardService
    {
        int CardValidateGuess(string guess, string nextCard, string previousCard);
        Card CardMaker();
    }
}
