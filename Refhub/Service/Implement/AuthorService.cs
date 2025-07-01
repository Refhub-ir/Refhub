using Refhub.Data.Models;
using Refhub.Models.Authors;
using Refhub.Models.Books;
using Refhub.Service.Interface;
using Refhub.Tools.Exceptions;


namespace Refhub.Service.Implement;

public class AuthorService : IAuthorService
{
    private readonly IAuthorRepository _authorRepository;
    private readonly IMessageService _messageService;

    public AuthorService(IAuthorRepository authorRepository, IMessageService messageService)
    {
        _authorRepository = authorRepository;
        _messageService = messageService;
    }

    public async Task<List<AuthorVM>> GetAllAuthorsAsync(CancellationToken ct)
    {
        var authors = await _authorRepository.GetAllAsync(ct);

        return authors == null || authors.Count == 0
            ? []
            : authors.Select(a => new AuthorVM
            {
                FullName = a.FullName,
                Slug = a.Slug
            }).ToList();
    }

    public async Task<BooksList_VM> GetAllAuthorsBooksAsync(string slug, CancellationToken ct)
    {
        var viewModel = new BooksList_VM();

        var author = await _authorRepository.GetAuthorWithBooksBySlugAsync(slug, ct);
        if (author == null)
        {
            return viewModel;
        }

        var bookVMs = author.BookAuthors.Select(ba => new BookVM
        {
            Id = ba.Book.Id,
         
            Title = ba.Book.Title,
            ImagePath = ba.Book.ImagePath,
            AuthorFullName = string.Join(", ", ba.Book.BookAuthors
            .Select(x => x.Author.FullName)
            .Where(n => !string.IsNullOrWhiteSpace(n)))
            .Trim() switch
            {
                "" => _messageService.Get("Error_NotDefined"),
                var s => s
            },
        }).ToList();



        viewModel = new BooksList_VM
        {
            Books = bookVMs,

            AuthorFilter = author.FullName,

        };
        return viewModel;
    }



    public async Task<AuthorVM?> GetAuthorBySlugAsync(string slug, CancellationToken ct)
    {
        if (string.IsNullOrWhiteSpace(slug))
        {
            return null;
        }

        var author = await _authorRepository.GetBySlugAsync(slug.Trim(), ct);
        return author == null
            ? null
            : new AuthorVM
            {
                FullName = author.FullName,
                Slug = author.Slug
            };
    }


    public async Task CreateAuthorAsync(AuthorVM authorVm, CancellationToken ct)
    {
        // چک کردن منحصربه‌فرد بودن Slug
        if (await _authorRepository.SlugExistsAsync(slug: authorVm.Slug, excludeSlug: null, ct: ct))
        {
            throw new DuplicateSlugException(_messageService.Get("Error_SlugExists"));
        }

        var author = new Author
        {
            FullName = authorVm.FullName,
            Slug = authorVm.Slug
        };

        await _authorRepository.AddAsync(author, ct);
    }


    public async Task UpdateAuthorAsync(AuthorVM authorVm, string originalSlug, CancellationToken ct)
    {
        var author = await _authorRepository.GetBySlugAsync(originalSlug, ct);

        if (author == null)
        {
            throw new Exception(_messageService.Get("Error_AuthorNotFound"));
        }

        if (authorVm.Slug != originalSlug &&
            await _authorRepository.SlugExistsAsync(slug: authorVm.Slug, excludeSlug: originalSlug, ct: ct))
        {
            throw new Exception(_messageService.Get("Error_SlugExists"));
        }

        author.FullName = authorVm.FullName;
        author.Slug = authorVm.Slug;

        await _authorRepository.UpdateAsync(author, ct);
    }

    public async Task DeleteAuthorAsync(string slug, CancellationToken ct)
    {

        var author = await _authorRepository.GetBySlugAsync(slug, ct);
        if (author == null)
        {
            throw new Exception(_messageService.Get("Error_AuthorNotFound"));
        }

        await _authorRepository.DeleteAsync(slug, ct);
    }

}