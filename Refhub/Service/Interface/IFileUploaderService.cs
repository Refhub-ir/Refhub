namespace Refhub.Service.Interface;

public interface IFileUploaderService
{
    Task<string> UploadFile(IFormFile file, string directoryName, string Type, string Name);
  
    Task DeleteFile(string realUrl);
}
