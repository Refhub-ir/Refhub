using Microsoft.AspNetCore.Mvc;
using Refhub.Data.Context;
using Refhub.Service.Interface;

public class AuthorController(IAuthorService authorService, AppDbContext _context) : Controller
{
    [Route("/book-author/{slug}")]
    [HttpGet]
    public async Task<IActionResult> Books(string slug, CancellationToken cancellationToken)
    {
        var viewModel = await authorService.GetAllAuthorsBooksAsync(slug, cancellationToken);

        return View("Books", viewModel);
    }
}
