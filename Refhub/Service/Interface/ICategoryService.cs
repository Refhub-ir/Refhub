using Refhub.Models.Category;

namespace Refhub.Service.Interface;

public interface ICategoryService
{
    Task<List<CategoryVM>> GetAllCategoriesAsync(CancellationToken ct);
    Task<List<CategoryItemHomePageVM>> GetAllCategoriesHomePageAsync(CancellationToken ct, int take = 8);
    Task<CategoryVM> GetCategoryByIdAsync(int id, CancellationToken ct);
    Task CreateCategoryAsync(CreateCategoryVM model, CancellationToken ct);
    Task UpdateCategoryAsync(UpdateCategoryVM model, CancellationToken ct);
    Task DeleteCategoryAsync(int id, CancellationToken ct);
}

