namespace HiLow.Entity.Exceptions
{
    public class GameBadRequestException : Exception
    {
        public GameBadRequestException(string? message) : base(message) { }

        public GameBadRequestException(string? message, Exception? innerException) : base(message, innerException) { }
    }
}
