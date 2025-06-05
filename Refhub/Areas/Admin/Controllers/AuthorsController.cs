using Microsoft.AspNetCore.Mvc;
using Refhub_Ir.Areas.Admin.DTOs;
using Refhub_Ir.Service.Interface;

namespace Refhub_Ir.Areas.Admin.Controllers;

[Area("Admin")]
public class AuthorsController : Controller
{
    private readonly IAuthorService _authorService;
    #region Ctor
    public AuthorsController(IAuthorService authorService)
    {
        _authorService = authorService;
    }
    #endregion

    #region Authors
    [HttpGet]

    public async Task<IActionResult> ListAuthors()
    {
        var authors = await _authorService.GetAllAuthorsAsync();
        return View(authors);
    }
    #endregion

    #region AuthorDetails

    // GET: /Admin/Authors/Details/john-doe
    [HttpGet]
    public async Task<IActionResult> Details(string slug)
    {
        var author = await _authorService.GetAuthorBySlugAsync(slug);

        return author == null ? View("Details", null) : View(author);
    }

    #endregion

    #region AuthorCreate
    // GET: /Admin/Authors/Create
    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }

    // POST: /Admin/Authors/Create
    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(AuthorDTO authorDTO)
    {
        if (!ModelState.IsValid)
        {
            return View(authorDTO);
        }

        try
        {
            await _authorService.CreateAuthorAsync(authorDTO);
            return RedirectToAction(nameof(ListAuthors));
        }
        catch (Exception ex)
        {
            ModelState.AddModelError("", ex.Message);
            return View(authorDTO);
        }
    }
    #endregion

    #region AuthorEdit
    [HttpGet]
    // GET: /Admin/Authors/Edit/john-doe
    public async Task<IActionResult> Edit(string slug)
    {
        var author = await _authorService.GetAuthorBySlugAsync(slug);
        if (author == null)
        {
            return NotFound();
        }

        var dto = new AuthorDTO
        {
            FullName = author.FullName,
            Slug = author.Slug
        };

        ViewData["OriginalSlug"] = author.Slug;
        return View(dto);
    }


    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(AuthorDTO authorDto, string originalSlug)
    {
        if (!ModelState.IsValid)
        {
            ViewData["OriginalSlug"] = originalSlug;
            return View(authorDto);
        }

        try
        {
            await _authorService.UpdateAuthorAsync(authorDto, originalSlug);
            return RedirectToAction(nameof(ListAuthors));
        }
        catch (Exception ex)
        {
            ModelState.AddModelError("", ex.Message);
            ViewData["OriginalSlug"] = originalSlug;
            return View(authorDto);
        }
    }

    #endregion

    #region AuthorDelete
    [HttpGet]
    public async Task<IActionResult> Delete(string slug)
    {
        var author = await _authorService.GetAuthorBySlugAsync(slug);
        return author == null ? NotFound() : View(author);
    }

    // POST: /Admin/Authors/Delete/john-doe
    [HttpPost, ActionName("Delete"), ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(string slug)
    {
        var author = await _authorService.GetAuthorBySlugAsync(slug);
        if (author == null)
        {
            return NotFound();
        }

        await _authorService.DeleteAuthorAsync(author.Slug);
        return RedirectToAction(nameof(ListAuthors));
    }
    #endregion
}