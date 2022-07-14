namespace HiLow.Entity.Entities.Cards
{
    public class Card
    {
        public int Number { get; set; }
        public string Suit { get; set; }
        public string CardFullname { get { return ($"{Number} {Suit}"); } }

        public Card(int number, string suit)
        {
            Number = number;
            Suit = suit;
        }
    }
}
