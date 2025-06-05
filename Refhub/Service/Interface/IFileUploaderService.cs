namespace Refhub_Ir.Service.Interface;

public interface IFileUploaderService
{


    Task<string> UpdloadFile(IFormFile file, string directoryName, string Type, string Name);
    Task DeleteFile(string directoryName, string Type, string Name);
}
