using Microsoft.AspNetCore.Mvc;
using Refhub.Models.Authors;
using Refhub.Models.Books;
using Refhub.Service.Interface;
using System.ComponentModel.DataAnnotations;

namespace Refhub.Controllers.Api;

/// <summary>
/// API Controller for managing authors
/// </summary>
[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public class AuthorsController(IAuthorService authorService, IMessageService messageService) : ControllerBase
{
    /// <summary>
    /// Get a list of all authors
    /// </summary>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>List of authors</returns>
    [HttpGet]
    [ProducesResponseType(typeof(List<AuthorVM>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<List<AuthorVM>>> GetAuthors(CancellationToken cancellationToken = default)
    {
        try
        {
            var authors = await authorService.GetAllAuthorsAsync(cancellationToken);
            return Ok(authors);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = messageService.Get("GeneralError"), details = ex.Message });
        }
    }

    /// <summary>
    /// Get author by slug
    /// </summary>
    /// <param name="slug">Author slug (URL-friendly identifier)</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Author details</returns>
    [HttpGet("{slug}")]
    [ProducesResponseType(typeof(AuthorVM), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<AuthorVM>> GetAuthor(
        [Required] string slug,
        CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(slug))
        {
            return BadRequest(new { message = "Author slug is required" });
        }

        try
        {
            var author = await authorService.GetAuthorBySlugAsync(slug, cancellationToken);

            if (author == null)
            {
                return NotFound(new { message = messageService.Get("AuthorNotFound") });
            }

            return Ok(author);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = messageService.Get("GeneralError"), details = ex.Message });
        }
    }

    /// <summary>
    /// Get all books by a specific author
    /// </summary>
    /// <param name="slug">Author slug</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>List of books by the author</returns>
    [HttpGet("{slug}/books")]
    [ProducesResponseType(typeof(BooksList_VM), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<BooksList_VM>> GetAuthorBooks(
        [Required] string slug,
        CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(slug))
        {
            return BadRequest(new { message = "Author slug is required" });
        }

        try
        {
            var authorBooks = await authorService.GetAllAuthorsBooksAsync(slug, cancellationToken);

            if (authorBooks == null)
            {
                return NotFound(new { message = messageService.Get("AuthorNotFound") });
            }

            return Ok(authorBooks);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = messageService.Get("GeneralError"), details = ex.Message });
        }
    }
}
