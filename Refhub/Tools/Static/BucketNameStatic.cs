using Refhub.Models.Enums;

namespace Refhub.Tools.Static;

public static class BucketNameStatic
{
    public static string GetName(BucketNames bucketNames)
    {
        return bucketNames switch
        {
            BucketNames.BookPdf => nameof(BucketNames.BookPdf),
            BucketNames.BookImages => nameof(BucketNames.BookImages),


            _ => throw new ArgumentOutOfRangeException(nameof(bucketNames), $"Unsupported bucket name: {bucketNames}")
        };
    }
}
