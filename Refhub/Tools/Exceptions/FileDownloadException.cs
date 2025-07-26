namespace Refhub.Tools.Exceptions
{
    public class FileDownloadException : Exception
    {
        public FileDownloadException (string message) : base(message)
        {
        }
        public FileDownloadException(string message, Exception innerException) : base(message, innerException)
        {
        }

    }
}
