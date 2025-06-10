using Refhub.Models.Category;

namespace Refhub.Models.Books;

public class BooksList_VM
{
    public List<BookVM> Books { get; set; } = [];
    public List<CategoryVM> Categories { get; set; } = [];


    public string AuthorSlug { get; set; }
    public string? AuthorFilter { get; set; }
    public string? CategoryFilter { get; set; }
    public string? SearchText { get; set; }
}
