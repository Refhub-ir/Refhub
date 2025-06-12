using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Refhub.Service.Implement.S3_Sample.Service;
using Refhub.Service.Interface;

namespace Refhub.Controllers;

public class BookController(IBookService bookService,IFileUploaderService _s3FileUploaderService) : Controller
{
    [HttpGet("BookDetails/{slug}")]
    public async Task<IActionResult> BookDetails(string slug, CancellationToken ct)
    {

        if (string.IsNullOrEmpty(slug))
        {
            return BadRequest("Slug is required.");
        }

        var bookDetails = await bookService.GetBookDetailsBySlugAsync(slug, ct);

        return bookDetails == null ? NotFound() : View(bookDetails);
    }


    [Authorize]
    [HttpGet("download/{fileName}")]
    public async Task<IActionResult> DownloadFile(string fileName, CancellationToken ct)
    {
        try
        {
            var stream = await _s3FileUploaderService.DownloadFileAsync(fileName);
            var contentType = "application/octet-stream"; // یا محتوای واقعی بر اساس پسوند

            return File(stream, contentType, fileName);
        }
        catch (Exception ex)
        {
            return NotFound($"خطا در دانلود فایل: {ex.Message}");
        }
    }

    private readonly int _pageSize = 3;
    [Route("/books")]
    public async Task<IActionResult> List(string searchText, string authorFilter, string categoryFilter, int page = 1, CancellationToken cancellationToken = default)
    {
        var viewModel = await bookService.GetListAsync(searchText, authorFilter, categoryFilter, _pageSize, page, cancellationToken);

        return View(viewModel);
    }


}
