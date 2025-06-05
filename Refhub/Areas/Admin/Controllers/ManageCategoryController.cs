using Microsoft.AspNetCore.Mvc;
using Refhub_Ir.Models.Categories;
using Refhub_Ir.Service.Interfaces;

namespace Refhub_Ir.Areas.Admin.Controllers;

[Area("Admin")]
public class ManageCategoryController : Controller
{
    private readonly ICategoryService _categoryService;

    public ManageCategoryController(ICategoryService categoryService)
    {
        _categoryService = categoryService;
    }

    public async Task<IActionResult> Index()
    {
        var categories = await _categoryService.GetAllCategoriesAsync();
        return View(categories);
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(CreateCategoryVM model)
    {
        if (ModelState.IsValid)
        {
            await _categoryService.CreateCategoryAsync(model);
            return RedirectToAction(nameof(Index));
        }
        return View(model);
    }

    public async Task<IActionResult> Update(int id)
    {
        var category = await _categoryService.GetCategoryByIdAsync(id);
        if (category == null)
        {
            return NotFound();
        }

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
    public async Task<IActionResult> Update(UpdateCategoryVM model)
    {
        if (ModelState.IsValid)
        {
            await _categoryService.UpdateCategoryAsync(model);
            return RedirectToAction(nameof(Index));
        }
        return View(model);
    }

    public async Task<IActionResult> Delete(int id)
    {
        var category = await _categoryService.GetCategoryByIdAsync(id);
        if (category == null)
        {
            return NotFound();
        }

        var model = new DeleteCategoryVM
        {
            Id = category.Id,
            Name = category.Name
        };

        return View(model);
    }

    [HttpPost, ActionName("Delete"), ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        await _categoryService.DeleteCategoryAsync(id);
        return RedirectToAction("Index");
    }
}
