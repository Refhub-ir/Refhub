using Microsoft.AspNetCore.Mvc;
using Refhub_Ir.Models.Keywords;
using Refhub_Ir.Service.Interface;

namespace Refhub_Ir.Areas.Admin.Controllers;

[Area("Admin")]
public class ManageKeywordController : Controller
{
    #region Constructor
    private readonly IKeywordService _keywordService;

    public ManageKeywordController(IKeywordService keywordService)
    {
        _keywordService = keywordService;
    }

    #endregion

    #region ListKeyword

    public async Task<IActionResult> ListKeyword()
    {
        var keywords = await _keywordService.GetAllKeywordForListAsync();
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
    public async Task<IActionResult> CreateKeyword(CreateKeywordVM model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        await _keywordService.AddKeywordAsync(model);
        return RedirectToAction("ListKeyword");
    }

    #endregion


    #region EditKeyword

    [HttpGet]
    public async Task<IActionResult> EditKeyword(int id)
    {
        var vm = await _keywordService.GetForEdit(id);
        return vm == null ? NotFound() : View(vm);
    }


    [HttpPost]
    public async Task<IActionResult> EditKeyword(EditKeywordVM vm)
    {
        if (!ModelState.IsValid)
        {
            return View(vm);
        }

        await _keywordService.UpdateAsync(vm);
        return RedirectToAction("ListKeyword");
    }

    #endregion


    #region DeleteKeyword

    public async Task<IActionResult> DeleteKeyword(int id)
    {
        await _keywordService.DeleteAsync(id);
        return RedirectToAction("ListKeyword");
    }
    #endregion

}
