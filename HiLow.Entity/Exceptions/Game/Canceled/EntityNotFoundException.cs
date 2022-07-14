namespace HiLow.Entity.Exceptions.Game.Canceled
{
    public class EntityNotFoundException : CanceledGameException
    {
        public EntityNotFoundException(string? message) : base(message) { }

        public EntityNotFoundException(string? message, Exception? innerException) : base(message, innerException) { }

    }
}
