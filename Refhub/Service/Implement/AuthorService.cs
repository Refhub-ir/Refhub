using Refhub_Ir.Areas.Admin.DTOs;
using Refhub_Ir.Service.Interface;

namespace Refhub_Ir.Service.Implement;

public class AuthorService : IAuthorService
{
    private readonly IAuthorRepository _authorRepository;

    public AuthorService(IAuthorRepository authorRepository)
    {
        _authorRepository = authorRepository;
    }

    public async Task<List<AuthorDTO>> GetAllAuthorsAsync()
    {
        var authors = await _authorRepository.GetAllAsync();
        return authors.Select(a => new AuthorDTO
        {
            FullName = a.FullName,
            Slug = a.Slug
        }).ToList();
    }


    public async Task<AuthorDTO> GetAuthorBySlugAsync(string slug)
    {
        var author = await _authorRepository.GetBySlugAsync(slug);
        return author == null
            ? null
            : new AuthorDTO
            {
                FullName = author.FullName,
                Slug = author.Slug
            };
    }

    public async Task CreateAuthorAsync(AuthorDTO authorDto)
    {
        // چک کردن منحصربه‌فرد بودن Slug
        if (await _authorRepository.SlugExistsAsync(authorDto.Slug))
        {
            throw new Exception("اسلاگ قبلاً استفاده شده است");
        }

        var author = new Author
        {
            FullName = authorDto.FullName,
            Slug = authorDto.Slug
        };
        await _authorRepository.AddAsync(author);
    }

    public async Task UpdateAuthorAsync(AuthorDTO authorDto, string originalSlug)
    {
        var author = await _authorRepository.GetBySlugAsync(originalSlug);
        if (author == null)
        {
            throw new Exception("نویسنده پیدا نشد");
        }

        if (authorDto.Slug != originalSlug && await _authorRepository.SlugExistsAsync(authorDto.Slug))
        {
            throw new Exception("اسلاگ قبلاً استفاده شده است");
        }

        author.FullName = authorDto.FullName;
        author.Slug = authorDto.Slug;

        await _authorRepository.UpdateAsync(author);
    }


    public async Task DeleteAuthorAsync(string slug)
    {
        var author = await _authorRepository.GetBySlugAsync(slug);
        if (author == null)
        {
            throw new Exception("نویسنده پیدا نشد");
        }

        await _authorRepository.DeleteAsync(slug);
    }
}