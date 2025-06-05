using Refhub_Ir.Models.Categories;

namespace Refhub_Ir.Service.Interfaces;

public interface ICategoryService
{
    Task<List<CategoryVM>> GetAllCategoriesAsync();
    Task<CategoryVM> GetCategoryByIdAsync(int id);
    Task CreateCategoryAsync(CreateCategoryVM model);
    Task UpdateCategoryAsync(UpdateCategoryVM model);
    Task DeleteCategoryAsync(int id);
}

