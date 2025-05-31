using Microsoft.AspNetCore.Mvc;
using Refhub_Ir.Models.Books;
using Refhub_Ir.Service.Interface;

namespace Refhub_Ir.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ManageBookController(IBookService bookService) : Controller
    {
        public async Task<IActionResult> Index(string? searchtext, CancellationToken ct)
        {
            var books = await bookService.GetBooksAsync(searchtext, ct);
            return View(books);
        }

        [HttpGet]
        public IActionResult Create() => View();

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateBookVM model, CancellationToken ct)
        {
            if (!ModelState.IsValid)
                return View(model);
            //model.UserId = "b0052a44-4253-4da6-8e26-0e42e7fac925";
            var res = await bookService.CreateBookAsync(model, ct);
            if (res)
                return RedirectToAction("Index");

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Update(int Id, CancellationToken ct)
        {
            var book = await bookService.GetBookDetailsForUpdateAsync(Id, ct);
            if (book == null)
            {
                return NotFound();
            }
            return View(book);
   
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(UpdateBookVM model, CancellationToken ct)
        {
            if (!ModelState.IsValid)
                return View(model);
            //model.UserId = "b0052a44-4253-4da6-8e26-0e42e7fac925";
            var res = await bookService.UpdateBookAsync(model, ct);
            if (res)
                return RedirectToAction("Index");

            return View(model);
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
}
