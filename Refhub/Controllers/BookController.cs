using Amazon.S3;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using Refhub.Service.Implement.S3_Sample.Service;
using Refhub.Service.Interface;

namespace Refhub.Controllers;

public class BookController(IBookService bookService,IFileUploaderService _s3FileUploaderService,ILogger _logger) : Controller
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
            // دریافت فایل از S3
            var stream = await _s3FileUploaderService.DownloadFileAsync(fileName, ct);

            // تعیین نوع فایل با توجه به پسوند
            var contentTypeProvider = new FileExtensionContentTypeProvider();
            if (!contentTypeProvider.TryGetContentType(fileName, out string contentType))
            {
                contentType = "application/octet-stream"; // پیش‌فرض اگر پسوند ناشناس باشد
            }

            return File(stream, contentType, fileName);
        }
        catch (AmazonS3Exception s3Ex)
        {
            _logger.LogError(s3Ex, "خطا در دانلود فایل از S3: {Message}", s3Ex.Message);
            return NotFound("خطا در دریافت فایل. لطفاً بعداً تلاش کنید.");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "خطای پیش‌بینی‌نشده هنگام دانلود فایل.");
            return StatusCode(500, "خطای غیرمنتظره‌ای رخ داده است. لطفاً با پشتیبانی تماس بگیرید.");
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
