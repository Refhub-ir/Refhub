using Microsoft.AspNetCore.Mvc;
using Refhub_Ir.Models.Books;
using Refhub_Ir.Service.Interface;

namespace Refhub_Ir.Areas.Admin.Controllers;

[Area("Admin")]
public class ManageBookController(IBookService bookService) : Controller
{
    public async Task<IActionResult> Index(string? searchtext)
    {
        return View(await bookService.GetBooks(searchtext));
    }
    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(CreateBookVM model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }
        //model.UserId = "b0052a44-4253-4da6-8e26-0e42e7fac925";
        var res = await bookService.CreateBook(model);
        return res ? RedirectToAction("Index") : View(model);
    }




    [HttpGet]
    public async Task<IActionResult> Update(int Id)
    {
        var book = await bookService.GetBookDetialsForUpdate(Id);

        return book != null ? View(book) : RedirectToPage("NotFound");
    }

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Update(UpdateBookVM model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }
        //model.UserId = "b0052a44-4253-4da6-8e26-0e42e7fac925";
        var res = await bookService.UpdateBook(model);
        return res ? RedirectToAction("Index") : View(model);
    }



    [HttpGet]
    public async Task<IActionResult> Delete(int id)
    {
        return RedirectToAction("Index", bookService.DeleteBook(id));
    }

    [HttpGet("/CreateAnother/{Slug}/{FullName}")]
    public async Task<IActionResult> CreateAnother(string FullName, string Slug)
    {
        var res = bookService.CreateAnother(FullName, Slug);        //

        return RedirectToAction("Create");

    }
}
