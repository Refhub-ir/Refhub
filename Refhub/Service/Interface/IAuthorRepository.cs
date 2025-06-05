namespace Refhub_Ir.Service.Interface;

public interface IAuthorRepository
{
    Task<List<Author>> GetAllAsync();
    Task<Author> GetBySlugAsync(string slug);
    Task AddAsync(Author author);
    Task UpdateAsync(Author author);
    Task DeleteAsync(string slug);
    Task<bool> SlugExistsAsync(string slug, string? excludeSlug = null);
}
