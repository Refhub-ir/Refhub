using Refhub.Service.Interface;

namespace Refhub.Service.Implement;

public class LocalFileUploaderService : IFileUploaderService
{
    public async Task<string> UploadFile(IFormFile file, string directoryName, string Type, string Name)
    {
        if (!Directory.Exists(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Files", Type)))
        {
            Directory.CreateDirectory(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Files", Type));
        }

        if (!Directory.Exists(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Files", Type, directoryName)))
        {
            Directory.CreateDirectory(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Files", Type, directoryName));
        }

        Name = Name.Replace(" ", "-") + new Random().Next(1111, 9999);
        string name = Name + Path.GetExtension(file.FileName);
        string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Files", Type, directoryName, name);
        using (Stream stream = new FileStream(path, FileMode.Create))
        {
            file.CopyTo(stream);
        }

        return name;
    }

    public async Task DeleteFile(string directoryName, string Type, string Name)
    {
        if (File.Exists(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Files", Type, directoryName, Name)))
        {
            File.Delete(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Files", Type, directoryName, Name));
        }

    }

    public Task DeleteFile(string realUrl)
    {
        throw new NotImplementedException();
    }
}
