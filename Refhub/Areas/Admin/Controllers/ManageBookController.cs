using Microsoft.AspNetCore.Mvc;
using Refhub.Models.Books;
using Refhub.Service.Interface;

namespace Refhub.Areas.Admin.Controllers;

[Area("Admin")]
public class ManageBookController(IBookService bookService) : Controller
{
    public async Task<IActionResult> Index(string? searchtext, CancellationToken ct)
    {
        var books = await bookService.GetBooksAsync(searchtext, ct);
        return View(books);
    }

    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(CreateBookVM model, CancellationToken ct)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        var res = await bookService.CreateBookAsync(model, ct);
        return res ? RedirectToAction("Index") : View(model);
    }

    [HttpGet]
    public async Task<IActionResult> Update(int Id, CancellationToken ct)
    {
        var book = await bookService.GetBookDetailsForUpdateAsync(Id, ct);
        return book == null ? NotFound() : View(book);
    }

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Update(UpdateBookVM model, CancellationToken ct)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        var res = await bookService.UpdateBookAsync(model, ct);
        return res ? RedirectToAction("Index") : View(model);
    }



    [HttpGet]
    public async Task<IActionResult> Delete(int id, CancellationToken ct)
    {
        var res = await bookService.DeleteBookAsync(id, ct);

        return RedirectToAction("Index");

    }

    [HttpGet("/CreateAnotherAsync/{Slug}/{FullName}")]
    public async Task<IActionResult> CreateAnother(string FullName, string Slug, CancellationToken ct)
    {
        var res = await bookService.CreateAnotherAsync(FullName, Slug, ct);

        return RedirectToAction("Create");

    }
}
