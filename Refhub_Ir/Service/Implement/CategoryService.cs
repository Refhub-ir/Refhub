using Microsoft.EntityFrameworkCore;
using Refhub_Ir.Data.Context;
using Refhub_Ir.Models.Books;
using Refhub_Ir.Models.Categories;
using Refhub_Ir.Service.Interfaces;

namespace Refhub_Ir.Service.Implement;

public class CategoryService : ICategoryService
{
    private readonly AppDbContext _context;

    public CategoryService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<CategoryVM>> GetAllCategoriesAsync()
    {
        return await _context.Categories
            .Include(c => c.Books)
            .Select(c => new CategoryVM
            {
                Id = c.Id,
                Name = c.Name,
                Slug = c.slug,
                Description = c.Description,
                Books = c.Books.Select(b => new BookVM
                {
                    Id = b.Id,
                    Title = b.Title,
                    Slug = b.Slug,
                    ImagePath = b.ImagePath
                }).ToList()
            }).ToListAsync();
    }

    public async Task<CategoryVM> GetCategoryByIdAsync(int id)
    {
        var category = await _context.Categories
            .Include(c => c.Books)
            .FirstOrDefaultAsync(c => c.Id == id);

        return category == null
            ? null
            : new CategoryVM
            {
                Id = category.Id,
                Name = category.Name,
                Slug = category.slug,
                Description = category.Description,
                Books = category.Books.Select(b => new BookVM
                {
                    Id = b.Id,
                    Title = b.Title,
                    Slug = b.Slug,
                    ImagePath = b.ImagePath
                }).ToList()
            };
    }

    public async Task CreateCategoryAsync(CreateCategoryVM model)
    {
        var category = new Category
        {
            Name = model.Name,
            slug = model.Slug,
            Description = model.Description
        };
        _context.Categories.Add(category);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateCategoryAsync(UpdateCategoryVM model)
    {
        var category = await _context.Categories.FindAsync(model.Id);
        if (category == null)
        {
            return;
        }

        category.Name = model.Name;
        category.slug = model.Slug;
        category.Description = model.Description;

        await _context.SaveChangesAsync();
    }

    public async Task DeleteCategoryAsync(int id)
    {
        var category = await _context.Categories.FindAsync(id);
        if (category == null)
        {
            return;
        }

        _context.Categories.Remove(category);
        await _context.SaveChangesAsync();
    }
}