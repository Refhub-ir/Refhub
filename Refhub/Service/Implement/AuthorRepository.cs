using Microsoft.EntityFrameworkCore;
using Refhub.Data.Context;
using Refhub.Data.Models;
using Refhub.Service.Interface;

namespace Refhub.Service.Implement;

public class AuthorRepository : IAuthorRepository
{

    private readonly AppDbContext _context;

    public AuthorRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<Author>> GetAllAsync(CancellationToken ct)
    {
        return await _context.Authors.ToListAsync(ct);
    }

    public async Task<Author> GetAuthorWithBooksBySlugAsync(string slug, CancellationToken ct)
    {
        return await _context.Authors
               .Include(a => a.BookAuthors)
               .ThenInclude(ba => ba.Book)
               .ThenInclude(b => b.BookAuthors)
               .ThenInclude(ba => ba.Author)
               .FirstOrDefaultAsync(a => a.Slug == slug, ct);
    }

    public async Task<Author> GetBySlugAsync(string slug, CancellationToken ct)
    {
        return await _context.Authors.FirstOrDefaultAsync(a => a.Slug == slug, ct);
    }

    public async Task AddAsync(Author author, CancellationToken ct)
    {
        await _context.Authors.AddAsync(author, ct);
        await _context.SaveChangesAsync(ct);
    }

    public async Task UpdateAsync(Author author, CancellationToken ct)
    {
        _context.Authors.Update(author);
        await _context.SaveChangesAsync(ct);
    }

    public async Task DeleteAsync(string slug, CancellationToken ct)
    {
        var author = await GetBySlugAsync(slug, ct);
        if (author != null)
        {
            _context.Authors.Remove(author);
            await _context.SaveChangesAsync(ct);
        }
    }
    public async Task<bool> SlugExistsAsync(string slug, string? excludeSlug, CancellationToken ct)
    {
        return !string.IsNullOrEmpty(excludeSlug)
            ? await _context.Authors.AnyAsync(a => a.Slug == slug && a.Slug != excludeSlug, ct)
            : await _context.Authors.AnyAsync(a => a.Slug == slug, ct);
    }
}
