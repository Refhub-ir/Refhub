using Refhub.Service.Interface;

namespace Refhub.Service.Implement;

public class LocalFileUploaderService : IFileUploaderService
{
    private readonly string _rootPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Files");

    public async Task<string> UploadFile(IFormFile file, string directoryName, string type, string name)
    {
        // ایجاد مسیر دایرکتوری نهایی
        string targetDirectory = Path.Combine(_rootPath, type, directoryName);

        // ایجاد پوشه‌ها در صورت نبود
        if (!Directory.Exists(targetDirectory))
        {
            Directory.CreateDirectory(targetDirectory);
        }

        // تمیزسازی و ساخت نام یکتا
        name = name.Replace(" ", "-") + "_" + Path.GetRandomFileName().Replace(".", "");
        string fileName = name + Path.GetExtension(file.FileName);
        string fullPath = Path.Combine(targetDirectory, fileName);

        // ذخیره فایل به صورت async
        using (Stream stream = new FileStream(fullPath, FileMode.Create, FileAccess.Write, FileShare.None, 4096, useAsync: true))
        {
            await file.CopyToAsync(stream);
        }

        return fileName;
    }

    public Task DeleteFile(string realUrl)
    {
        string fullPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", realUrl.TrimStart('/').Replace("/", Path.DirectorySeparatorChar.ToString()));

        if (File.Exists(fullPath))
        {
            File.Delete(fullPath);
        }

        return Task.CompletedTask;
    }

    public Task<Stream> DownloadFileAsync(string fileName, CancellationToken ct)
    {
        string fullPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", fileName.TrimStart('/').Replace("/", Path.DirectorySeparatorChar.ToString()));

        if (!File.Exists(fullPath))
        {
            throw new FileNotFoundException("فایل پیدا نشد", fileName);
        }

        Stream fileStream = new FileStream(fullPath, FileMode.Open, FileAccess.Read, FileShare.Read, 4096, useAsync: true);
        return Task.FromResult(fileStream);
    }

  
}
