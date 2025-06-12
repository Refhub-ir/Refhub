namespace Refhub.Tools.Exceptions
{
    public class DuplicateSlugException: Exception
    {
        public DuplicateSlugException(string message): base(message)
        {
        }
        public DuplicateSlugException(string message, Exception innerException) : base(message, innerException)
        {
        }
     
    }

    public class EntityNotFoundException : Exception
    {
        public EntityNotFoundException (string message) : base(message)
        {
        }
        public EntityNotFoundException(string message, Exception innerException) : base(message, innerException)
        {
        }

    }
}
