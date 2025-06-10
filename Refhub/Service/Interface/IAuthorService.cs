using Refhub.Models.Authors;
using Refhub.Models.Books;

namespace Refhub.Service.Interface;

public interface IAuthorService
{
    Task<List<AuthorVM>> GetAllAuthorsAsync(CancellationToken ct);
    Task<BooksList_VM> GetAllAuthorsBooksAsync(string slug, CancellationToken ct);
    Task<AuthorVM> GetAuthorBySlugAsync(string slug, CancellationToken ct);
    Task CreateAuthorAsync(AuthorVM authorVm, CancellationToken ct);
    Task UpdateAuthorAsync(AuthorVM authorVm, string originalSlug, CancellationToken ct);
    Task DeleteAuthorAsync(string slug, CancellationToken ct);
}


