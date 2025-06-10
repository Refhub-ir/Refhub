using Microsoft.AspNetCore.Mvc;
using Refhub.Models.Keywords;
using Refhub.Service.Interface;

namespace Refhub.Areas.Admin.Controllers;

[Area("Admin")]
public class ManageKeywordController(IKeywordService _keywordService) : Controller
{

    #region ListKeyword

    public async Task<IActionResult> ListKeyword(CancellationToken ct)
    {
        var keywords = await _keywordService.GetAllKeywordForListAsync(ct);
        return View(keywords);
    }


    #endregion

    #region CreateKeyword

    [HttpGet]
    public async Task<IActionResult> CreateKeyword()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> CreateKeyword(CreateKeywordVM model, CancellationToken ct)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        await _keywordService.AddKeywordAsync(model, ct);
        return RedirectToAction("ListKeyword");
    }

    #endregion


    #region EditKeyword

    [HttpGet]
    public async Task<IActionResult> EditKeyword(int id, CancellationToken ct)
    {
        var vm = await _keywordService.GetForEdit(id, ct);
        return vm == null ? NotFound() : View(vm);
    }


    [HttpPost]
    public async Task<IActionResult> EditKeyword(EditKeywordVM vm, CancellationToken ct)
    {
        if (!ModelState.IsValid)
        {
            return View(vm);
        }

        await _keywordService.UpdateAsync(vm, ct);
        return RedirectToAction("ListKeyword");
    }

    #endregion


    #region DeleteKeyword

    public async Task<IActionResult> DeleteKeyword(int id, CancellationToken ct)
    {
        await _keywordService.DeleteAsync(id, ct);
        return RedirectToAction("ListKeyword");
    }
    #endregion

}
