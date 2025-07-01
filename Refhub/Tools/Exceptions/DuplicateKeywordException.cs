namespace Refhub.Tools.Exceptions
{
    public class DuplicateKeywordException : Exception
    {
        public DuplicateKeywordException(string keyword)
            : base($"The keyword '{keyword}' already exists.") { }
    }

}
