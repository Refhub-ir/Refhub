using Microsoft.EntityFrameworkCore;
using Refhub_Ir.Data.Context;
using Refhub_Ir.Service.Interface;

namespace Refhub_Ir.Service.Implement;

public class AuthorRepository : IAuthorRepository
{

    private readonly AppDbContext _context;

    public AuthorRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<Author>> GetAllAsync()
    {
        return await _context.Authors.ToListAsync();
    }

    public async Task<Author> GetBySlugAsync(string slug)
    {
        return await _context.Authors.FirstOrDefaultAsync(a => a.Slug == slug);
    }

    public async Task AddAsync(Author author)
    {
        _context.Authors.Add(author);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Author author)
    {
        _context.Authors.Update(author);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(string slug)
    {
        var author = await GetBySlugAsync(slug);
        if (author != null)
        {
            _context.Authors.Remove(author);
            await _context.SaveChangesAsync();
        }
    }
    public async Task<bool> SlugExistsAsync(string slug, string excludeSlug = null)
    {
        return !string.IsNullOrEmpty(excludeSlug)
            ? await _context.Authors.AnyAsync(a => a.Slug == slug && a.Slug != excludeSlug)
            : await _context.Authors.AnyAsync(a => a.Slug == slug);
    }
}
