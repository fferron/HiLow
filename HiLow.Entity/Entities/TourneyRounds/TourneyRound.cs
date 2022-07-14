using HiLow.Entity.Entities.Tourneys;
using HiLow.Entity.SeedWorks;
using System.ComponentModel.DataAnnotations.Schema;

namespace HiLow.Entity.Entities.TourneyRounds
{
    [Table("TourneyRound")]
    public class TourneyRound : EntityBase
    {
        public int Round { get; set; }
        public int Player1Score { get; set; }
        public int Player2Score { get; set; }
        public int CardNumber { get; set; }
        public string CardSuit { get; set; }

        public int TourneyId { get; set; }
        public virtual Tourney Tourney { get; set; }

        public virtual string CardFullname { get { return ($"{CardNumber} {CardSuit}"); } }

        public TourneyRound()
        {
        }

        public TourneyRound(int round, int player1Score, int player2Score, int tourneyId, int numberCard, string suitCard)
        {
            Round = round;
            Player1Score = player1Score;
            Player2Score = player2Score;
            TourneyId = tourneyId;
            CardNumber = numberCard;
            CardSuit = suitCard;
        }
    }
}