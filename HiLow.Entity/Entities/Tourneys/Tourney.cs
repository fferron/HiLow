using HiLow.Entity.Entities.TourneyRounds;
using HiLow.Entity.Enums;
using HiLow.Entity.SeedWorks;
using System.ComponentModel.DataAnnotations.Schema;

namespace HiLow.Entity.Entities.Tourneys
{
    [Table("Tourney")]
    public class Tourney : EntityBase
    {
        public StatusTourney Status { get; set; }
        public string Player1 { get; set; }
        public string Player2 { get; set; }
        public Winner Winner { get; set; }
        public int? Player1ScoreFinal { get; set; }
        public int? Player2ScoreFinal { get; set; }
        public virtual ICollection<TourneyRound> Rounds { get; }

        public virtual int Player1ScoreCount() => Rounds.Sum(_ => _.Player1Score);
        public virtual int Player2ScoreCount() => Rounds.Sum(_ => _.Player2Score);

        public Tourney()
        {
        }

        public Tourney(StatusTourney status, string player1, string player2, DateTime createDate)
        {
            Status = status;
            Player1 = player1;
            Player2 = player2;
            CreatedDate = createDate;  
            Rounds = new List<TourneyRound>();
        }

        public void AddTourneyRounds(int round, int player1Score, int player2Score, int tourneyId, int numberCard, string suitCard)
        {
            Rounds.Add(new TourneyRound(round, player1Score, player2Score, tourneyId, numberCard, suitCard));
        }
    }
}