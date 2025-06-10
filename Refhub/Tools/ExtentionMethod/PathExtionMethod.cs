using Refhub_Ir.Tools.Static;

namespace Refhub_Ir.Tools.ExtentionMethod
{
    public static class PathImageExtionMethod
    {
        public static string ConvertForBookPathImage(this string imageName)
        {
            if (string.IsNullOrEmpty(imageName))
                return string.Empty;
            return
                $"/{FolderNameStatic.GetDirectoryFiles}/{FolderNameStatic.GetDirectoryImages}/{FolderNameStatic.GetDirectoryBooks}/{imageName}";
        }
        public static string ConvertForBookPathFile(this string fileName)
        {
            if (string.IsNullOrEmpty(fileName))
                return string.Empty;
            return
                $"\\{FolderNameStatic.GetDirectoryFiles}\\{FolderNameStatic.GetDirectoryImages}\\{FolderNameStatic.GetDirectoryBooks}\\{fileName}";
        }
    }
}
