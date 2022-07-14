using System.ComponentModel;

namespace HiLow.Entity.Enums
{
    public enum Winner
    {
        [Description("In Progress")]
        Unknown = 0,
        [Description("Player 1")]
        Player1 = 1,
        [Description("Player 2")]
        Player2 = 2,
        [Description("Break-Even")]
        BreakEven = 3,   
    }
    public enum StatusTourney
    {
        [Description("Started")]
        Started = 1,
        [Description("In Progress")]
        InProgress = 2,
        [Description("Finished")]
        Finished = 3,
    }

    public enum Round
    {
        First = 1,
        Last = 52
    }

    public enum Guess
    {
        Higher = 0,
        Lower = 1
    }
}