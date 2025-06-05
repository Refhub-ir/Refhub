using Refhub_Ir.Tools.Static;

namespace Refhub_Ir.Tools.ExtentionMethod;

public static class PathImageExtionMethod
{
    public static string ConvertForBookPathImage(this string imageName)
    {
        return string.IsNullOrEmpty(imageName)
            ? string.Empty
            : $"/{FolderNameStatic.GetDirectoryFiles}/{FolderNameStatic.GetDirectoryImages}/{FolderNameStatic.GetDirectoryBooks}/{imageName}";
    }
}
