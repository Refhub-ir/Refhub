using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Refhub_Ir.Areas.Admin.DTOs;
using Refhub_Ir.Data.Context;
using Refhub_Ir.Models.Books;
using Refhub_Ir.Service.Interface;

public class AuthorController(IAuthorService authorService, AppDbContext _context) : Controller
{
    [Route("/book-author/{slug}")]
    public async Task<IActionResult> Books(string slug, CancellationToken cancellationToken)
    {
        var viewModel = await authorService.GetAllAuthorsBooksAsync(slug, cancellationToken);

        return View("Books", viewModel);
    }
}
