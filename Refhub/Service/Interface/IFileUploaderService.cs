namespace Refhub.Service.Interface;

public interface IFileUploaderService
{
    Task<string> UploadFile(IFormFile file, string directoryName, string name);
    Task DeleteFile(string realUrl, string bucketName);
    Task<Stream> DownloadFileAsync(string fileName,CancellationToken ct, string? bucketName);
}
