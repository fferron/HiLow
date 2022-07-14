namespace HiLow.Entity.Exceptions.Game.Canceled
{
    public class InvalidEntityException : CanceledGameException
    {
        public InvalidEntityException(string? message) : base(message) { }

        public InvalidEntityException(string? message, Exception? innerException) : base(message, innerException) { }

    }
}
