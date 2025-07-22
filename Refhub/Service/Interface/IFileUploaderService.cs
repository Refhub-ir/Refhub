namespace Refhub.Service.Interface;

public interface IFileUploaderService
{
    Task<string> UploadFile(IFormFile file, string bucketName, string name);
    Task DeleteFile(string realUrl, string bucketName);
    Task<Stream> DownloadFileAsync(string fileUrl, CancellationToken ct, string bucketName);
}
