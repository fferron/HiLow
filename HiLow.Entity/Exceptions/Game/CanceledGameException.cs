namespace HiLow.Entity.Exceptions.Game
{
    public class CanceledGameException : Exception
    {
        public CanceledGameException(string? message) : base(message) { }

        public CanceledGameException(string? message, Exception? innerException) : base(message, innerException) { }

    }
}
