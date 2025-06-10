using Refhub_Ir.Service.Interface;

namespace Refhub_Ir.Service.Implement
{
    public class LocalFileUploaderService : IFileUploaderService
    {
        public async Task<string> UpdloadFile(IFormFile file, string directoryName, string Type, string Name)
        {
            if (!System.IO.Directory.Exists(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Files", Type)))
            {
                System.IO.Directory.CreateDirectory(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Files", Type));
            }

            if (!System.IO.Directory.Exists(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Files", Type, directoryName)))
            {
                System.IO.Directory.CreateDirectory(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Files", Type, directoryName));
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
            if (System.IO.File.Exists(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Files", Type, directoryName, Name)))
            {
                System.IO.File.Delete(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Files", Type, directoryName, Name));
            }

        }
    }

}
