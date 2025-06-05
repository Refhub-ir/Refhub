using Refhub_Ir.Areas.Admin.DTOs;

namespace Refhub_Ir.Service.Interface;

public interface IAuthorService
{
    Task<List<AuthorDTO>> GetAllAuthorsAsync();
    Task<AuthorDTO> GetAuthorBySlugAsync(string slug);
    Task CreateAuthorAsync(AuthorDTO authorDto);
    Task UpdateAuthorAsync(AuthorDTO authorDto, string originalSlug);
    Task DeleteAuthorAsync(string slug);
}


