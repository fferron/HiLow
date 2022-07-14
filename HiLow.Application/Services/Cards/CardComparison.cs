namespace HiLow.Application.Services.Cards
{
    public static class CardComparison
    {
        public static string IsCardLarger(string cardA, string cardB)
        {
            string[] suits = { "diamonds", "spades", "hearts", "clubs" };

            //Card A
            string[] cardASplit = cardA.Split(" ", 2);
            string cardANum = cardASplit[0];
            string cardASuit = cardASplit[1];
            int cardASuitVal = 0;

            for (int k = 0; k < 4; k++)
            {
                if (suits[k].Equals(cardASuit))
                    cardASuitVal = k + 1;
            }

            //Card B
            string[] cardBSplit = cardB.Split(" ", 2);
            string cardBNum = cardBSplit[0];
            string cardBSuit = cardBSplit[1];
            int cardBSuitVal = 0;

            for (int k = 0; k < 4; k++)
            {
                if (suits[k].Equals(cardBSuit))
                    cardBSuitVal = k + 1;
            }

            //Comparator
            if (int.Parse(cardANum) > int.Parse(cardBNum))
            {
                return (cardA);
            }

            else if (int.Parse(cardANum) < int.Parse(cardBNum))
            {
                return (cardB);
            }

            else
            {
                if (cardASuitVal > cardBSuitVal)
                    return (cardA);
                else
                    return (cardB);
            }
        }

        public static int CardNum(string card)
        {
            string[] cardSplit = card.Split(" ", 2);
            return (int.Parse(cardSplit[0]));
        }

        public static string CardSuit(string card)
        {
            string[] cardSplit = card.Split(" ", 2);
            return (cardSplit[1]);
        }

        public static bool IsHigher(string cardA, string cardB)
        {
            if (cardA.Equals(CardComparison.IsCardLarger(cardA, cardB)))
                return true;
            else
                return false;
        }

        public static bool IsLower(string cardA, string cardB)
        {
            if (cardA.Equals(CardComparison.IsCardLarger(cardA, cardB)))
                return false;
            else
                return true;
        }
    }
}
