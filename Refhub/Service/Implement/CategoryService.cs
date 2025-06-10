using Microsoft.EntityFrameworkCore;
using Refhub.Data.Context;
using Refhub.Data.Models;
using Refhub.Models.Category;
using Refhub.Service.Interface;

namespace Refhub.Service.Implement;

public class CategoryService : ICategoryService
{
    private readonly AppDbContext _context;

    public CategoryService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<CategoryVM>> GetAllCategoriesAsync(CancellationToken ct)
    {
        return await _context.Categories
            .AsNoTracking()
            .OrderBy(c => c.Name) // مرتب‌سازی اختیاری برای نظم
            .Select(c => new CategoryVM
            {
                Id = c.Id,
                Name = c.Name,
                Slug = c.slug,
                Description = c.Description,
            })
            .ToListAsync(ct);
    }

    public async Task<List<CategoryItemHomePageVM>> GetAllCategoriesHomePageAsync(CancellationToken ct, int take = 8)
    {
        return await _context.Categories
            .AsNoTracking()
            .OrderBy(c => c.Name) // مرتب‌سازی اختیاری برای نظم
            .Select(c => new CategoryItemHomePageVM
            {

                Name = c.Name,
                Slug = c.slug,
                BookCount = c.Books.Count,
            })
            .ToListAsync(ct);
    }


    public async Task<CategoryVM> GetCategoryByIdAsync(int id, CancellationToken ct)
    {
        var category = await _context.Categories
            .AsNoTracking()
            .FirstOrDefaultAsync(c => c.Id == id, ct);

        return category is null
            ? null
            : new CategoryVM
            {
                Id = category.Id,
                Name = category.Name,
                Slug = category.slug, // اطمینان از PascalCase بودن
                Description = category.Description
            };
    }


    public async Task CreateCategoryAsync(CreateCategoryVM model, CancellationToken ct)
    {
        var category = new Category
        {
            Name = model.Name,
            slug = model.Slug,
            Description = model.Description
        };

        await _context.Categories.AddAsync(category, ct);
        await _context.SaveChangesAsync(ct);
    }


    public async Task UpdateCategoryAsync(UpdateCategoryVM model, CancellationToken ct)
    {
        var category = await _context.Categories.FindAsync(new object[] { model.Id }, ct);
        if (category == null)
        {
            return;
        }

        category.Name = model.Name;
        category.slug = model.Slug;
        category.Description = model.Description;

        await _context.SaveChangesAsync(ct);
    }


    public async Task DeleteCategoryAsync(int id, CancellationToken ct)
    {
        var category = await _context.Categories.FindAsync(new object[] { id }, ct);
        if (category == null)
        {
            return;
        }

        _context.Categories.Remove(category);
        await _context.SaveChangesAsync(ct);
    }

}