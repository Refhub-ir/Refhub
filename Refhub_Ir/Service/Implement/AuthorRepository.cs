using Microsoft.EntityFrameworkCore;
using Refhub_Ir.Data.Context;
using Refhub_Ir.Data.Models;
using Refhub_Ir.Models;
using Refhub_Ir.Service.Interface;
using System.Threading;

namespace Refhub_Ir.Service.Implement
{
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
         return   await _context.Authors
                .Include(a => a.BookAuthors)
                .ThenInclude(ba => ba.Book)
                .ThenInclude(b => b.BookAuthors)
                .ThenInclude(ba => ba.Author)
                .FirstOrDefaultAsync(a => a.Slug == slug, ct);
        }

        public async Task<Author> GetBySlugAsync(string slug, CancellationToken ct)
        {
            return await _context.Authors.FirstOrDefaultAsync(a => a.Slug == slug,ct);
        }

        public async Task AddAsync(Author author, CancellationToken ct)
        {
           await _context.Authors.AddAsync(author,ct);
            await _context.SaveChangesAsync(ct);
        }

        public async Task UpdateAsync(Author author, CancellationToken ct)
        {
            _context.Authors.Update(author);
            await _context.SaveChangesAsync(ct);
        }

        public async Task DeleteAsync(string slug, CancellationToken ct)
        {
            var author =await GetBySlugAsync(slug,ct);
            if (author != null)
            {
                _context.Authors.Remove(author);
                await _context.SaveChangesAsync(ct);
            }
        }
        public async Task<bool> SlugExistsAsync( CancellationToken ct,string slug, string excludeSlug = null)
        {
            if (!string.IsNullOrEmpty(excludeSlug))
            {
                return await _context.Authors.AnyAsync(a => a.Slug == slug && a.Slug != excludeSlug,ct);
            }
            return await _context.Authors.AnyAsync(a => a.Slug == slug, ct);
        }
    }
}
