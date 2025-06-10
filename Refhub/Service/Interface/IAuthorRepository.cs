using Refhub.Data.Models;

namespace Refhub.Service.Interface;

public interface IAuthorRepository
{
    Task<List<Author>> GetAllAsync(CancellationToken ct);
    Task<Author> GetAuthorWithBooksBySlugAsync(string slug, CancellationToken ct);
    Task<Author> GetBySlugAsync(string slug, CancellationToken ct);
    Task AddAsync(Author author, CancellationToken ct);
    Task UpdateAsync(Author author, CancellationToken ct);
    Task DeleteAsync(string slug, CancellationToken ct);
    Task<bool> SlugExistsAsync(string slug, string? excludeSlug, CancellationToken ct);
}
