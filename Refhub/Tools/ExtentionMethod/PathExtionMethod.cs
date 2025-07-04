using Refhub.Tools.Static;

namespace Refhub.Tools.ExtensionMethod;

public static class PathImageExtionMethod
{
    public static string ConvertForBookPathImage(this string imageName)
    {
        return string.IsNullOrEmpty(imageName)
            ? string.Empty
            : $"/{FolderNameStatic.GetDirectoryFiles}/{FolderNameStatic.GetDirectoryImages}/{FolderNameStatic.GetDirectoryBooks}/{imageName}";
    }
    public static string ConvertForBookPathFile(this string fileName)
    {
        return string.IsNullOrEmpty(fileName)
            ? string.Empty
            : $"\\{FolderNameStatic.GetDirectoryFiles}\\{FolderNameStatic.GetDirectoryImages}\\{FolderNameStatic.GetDirectoryBooks}\\{fileName}";
    }
}
