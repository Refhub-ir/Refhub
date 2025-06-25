using Refhub.Models.Enums;

namespace Refhub.Tools.Static;

public static class FolderNameStatic
{
    public static string GetDirectoryName(DirectoryTypes folder)
    {
        return folder switch
        {
            DirectoryTypes.Images => nameof(DirectoryTypes.Images),
            DirectoryTypes.Books => nameof(DirectoryTypes.Books),
            DirectoryTypes.Files => nameof(DirectoryTypes.Files),

            _ => throw new ArgumentOutOfRangeException(nameof(folder), $"Unsupported directory type: {folder}")
        };
    }
}
