using Refhub_Ir.Areas.Admin.DTOs;
using Refhub_Ir.Models.Books;

namespace Refhub_Ir.Service.Interface
{
    public interface IAuthorService
    {
        Task<List<AuthorVM>> GetAllAuthorsAsync( CancellationToken ct);
        Task<BooksList_VM> GetAllAuthorsBooksAsync(string slug, CancellationToken ct);
        Task<AuthorVM> GetAuthorBySlugAsync(string slug, CancellationToken ct);
        Task CreateAuthorAsync(AuthorVM authorVm, CancellationToken ct);
        Task UpdateAuthorAsync(AuthorVM authorVm, string originalSlug, CancellationToken ct);
        Task DeleteAuthorAsync(string slug, CancellationToken ct);
    }
}
    

