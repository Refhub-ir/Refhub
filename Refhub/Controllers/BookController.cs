using Amazon.S3;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using Refhub.Models.Enums;
using Refhub.Service.Implement;
using Refhub.Service.Interface;
using Refhub.Tools.Exceptions;
using Refhub.Tools.Static;

namespace Refhub.Controllers;

public class BookController(IBookService bookService, IFileUploaderService s3FileUploaderService, ILogger<BookController> logger, IMessageService messageService) : Controller
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
    [HttpGet("download")]
    public async Task<IActionResult> DownloadFile([FromQuery] string fileUrl, CancellationToken ct)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(fileUrl) || !Uri.TryCreate(fileUrl, UriKind.Absolute, out _))
            {
                return NotFound(messageService.Get("InvalidFileName"));
            }
            // دریافت فایل از S3
            var stream = await s3FileUploaderService.DownloadFileAsync(fileUrl, ct, BucketNameStatic.GetName(BucketNames.BookPdf));

            // The file name for the user should be extracted from the URL
            var fileName = Path.GetFileName(new Uri(fileUrl).AbsolutePath);

            // تعیین نوع فایل با توجه به پسوند
            var contentTypeProvider = new FileExtensionContentTypeProvider();
            if (!contentTypeProvider.TryGetContentType(fileName, out string contentType))
            {
                contentType = "application/octet-stream"; // پیش‌فرض اگر پسوند ناشناس باشد
            }

            return File(stream, contentType, fileName);
        }
        catch (FileDownloadException s3Ex)
        {
            logger.LogError(s3Ex, "Error downloading file from S3: {Message}", s3Ex.Message);

            return NotFound(messageService.Get("FileNotFound"));
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Unexpected error occurred while downloading the file.");
            return StatusCode(500,messageService.Get("DownloadError"));
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
