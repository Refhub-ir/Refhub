using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Refhub_Ir.Models.Books;
using Refhub_Ir.Service.Implement;
using Refhub_Ir.Service.Interface;

namespace Refhub_Ir.Controllers
{
    public class BookController(IBookService bookService) : Controller
    {
        [HttpGet("BookDetails/{slug}")]
        public async Task<IActionResult> BookDetails(string slug, CancellationToken ct)
        {

            if (string.IsNullOrEmpty(slug))
            {
                return BadRequest("Slug is required.");
            }

            var bookDetails = await bookService.GetBookDetailsBySlugAsync(slug, ct);

            if (bookDetails == null)
                return NotFound();

            return View(bookDetails);
        }
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> DownloadFile(string filePath, CancellationToken ct)
        {
            filePath = Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot{filePath}");
            if (string.IsNullOrEmpty(filePath) || !System.IO.File.Exists(filePath))
                return NotFound();
            var fileBytes = await System.IO.File.ReadAllBytesAsync(filePath, ct);
            return File(fileBytes, "application/octet-stream", Path.GetFileName(filePath));
        }

        private readonly int _pageSize = 3;
        [Route("/books")]
        public async Task<IActionResult> List(string searchText, string authorFilter, string categoryFilter, int page = 1, CancellationToken cancellationToken = default)
        {
            var viewModel = await bookService.GetListAsync(searchText, authorFilter, categoryFilter, _pageSize, page, cancellationToken);

            return View(viewModel);
        }
    }
}
