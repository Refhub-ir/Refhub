using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Refhub.Models.Books;
using Refhub.Service.Interface;
using System.ComponentModel.DataAnnotations;

namespace Refhub.Controllers.Api;

/// <summary>
/// API Controller for managing books
/// </summary>
[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public class BooksController(IBookService bookService, IMessageService messageService) : ControllerBase
{
    /// <summary>
    /// Get a list of books with optional filtering
    /// </summary>
    /// <param name="searchText">Search text for book title, description, etc.</param>
    /// <param name="authorFilter">Filter by author name</param>
    /// <param name="categoryFilter">Filter by category</param>
    /// <param name="page">Page number (starting from 1)</param>
    /// <param name="pageSize">Number of items per page (default: 10, max: 50)</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>List of books with pagination info</returns>
    [HttpGet]
    [ProducesResponseType(typeof(ListBooksVM), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<ListBooksVM>> GetBooks(
        [FromQuery] string? searchText = null,
        [FromQuery] string? authorFilter = null,
        [FromQuery] string? categoryFilter = null,
        [FromQuery][Range(1, int.MaxValue)] int page = 1,
        [FromQuery][Range(1, 50)] int pageSize = 10,
        CancellationToken cancellationToken = default)
    {
        try
        {
            var result = await bookService.GetListAsync(searchText, authorFilter, categoryFilter, pageSize, page, cancellationToken);
            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = messageService.Get("GeneralError"), details = ex.Message });
        }
    }

    /// <summary>
    /// Get detailed information about a specific book by its slug
    /// </summary>
    /// <param name="slug">Book slug (URL-friendly identifier)</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Detailed book information</returns>
    [HttpGet("{slug}")]
    [ProducesResponseType(typeof(BookDetailsVM), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<BookDetailsVM>> GetBookBySlug(
        [Required] string slug,
        CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(slug))
        {
            return BadRequest(new { message = "Slug is required" });
        }

        try
        {
            var book = await bookService.GetBookDetailsBySlugAsync(slug, cancellationToken);

            if (book == null)
            {
                return NotFound(new { message = messageService.Get("BookNotFound") });
            }

            return Ok(book);
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = messageService.Get("GeneralError"), details = ex.Message });
        }
    }

    /// <summary>
    /// Get download URL for a book (requires authentication)
    /// </summary>
    /// <param name="slug">Book slug</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Download information</returns>
    [HttpGet("{slug}/download-info")]
    [Authorize]
    [ProducesResponseType(typeof(object), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> GetBookDownloadInfo(
        [Required] string slug,
        CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(slug))
        {
            return BadRequest(new { message = "Slug is required" });
        }

        try
        {
            var book = await bookService.GetBookDetailsBySlugAsync(slug, cancellationToken);

            if (book == null)
            {
                return NotFound(new { message = messageService.Get("BookNotFound") });
            }

            if (string.IsNullOrWhiteSpace(book.FilePath))
            {
                return NotFound(new { message = messageService.Get("FileNotFound") });
            }

            // Return download URL that can be used with the existing download endpoint
            var downloadUrl = Url.Action("DownloadFile", "Book", new { fileUrl = book.FilePath });

            return Ok(new
            {
                downloadUrl = downloadUrl,
                fileName = Path.GetFileName(book.FilePath),
                message = messageService.Get("DownloadReady")
            });
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = messageService.Get("GeneralError"), details = ex.Message });
        }
    }
}
