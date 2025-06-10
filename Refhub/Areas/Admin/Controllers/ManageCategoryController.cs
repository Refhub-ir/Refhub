using Microsoft.AspNetCore.Mvc;
using Refhub_Ir.Models.Categories;
using Refhub_Ir.Service.Interfaces;

namespace Refhub_Ir.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ManageCategoryController(ICategoryService _categoryService) : Controller
    {
    

        public async Task<IActionResult> Index(CancellationToken ct)
        {
            var categories = await _categoryService.GetAllCategoriesAsync(ct);
            return View(categories);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateCategoryVM model, CancellationToken ct)
        {
            if (ModelState.IsValid)
            {
                await _categoryService.CreateCategoryAsync(model,ct);
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        public async Task<IActionResult> Update(int id, CancellationToken ct)
        {
            var category = await _categoryService.GetCategoryByIdAsync(id, ct);
            if (category == null) return NotFound();

            var model = new UpdateCategoryVM
            {
                Id = category.Id,
                Name = category.Name,
                Slug = category.Slug,
                Description = category.Description
            };
            return View(model);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(UpdateCategoryVM model, CancellationToken ct)
        {
            if (ModelState.IsValid)
            {
                await _categoryService.UpdateCategoryAsync(model,ct);
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        public async Task<IActionResult> Delete(int id, CancellationToken ct)
        {
            var category = await _categoryService.GetCategoryByIdAsync(id, ct);
            if (category == null) return NotFound();

            var model = new DeleteCategoryVM
            {
                Id = category.Id,
                Name = category.Name
            };

            return View(model);
        }

        [HttpPost, ActionName("Delete"), ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id, CancellationToken ct)
        {
            await _categoryService.DeleteCategoryAsync(id,ct);
            return RedirectToAction(nameof(Index));
        }
    }
}
