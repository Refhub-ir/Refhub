using Refhub.Models.Books;

namespace Refhub.Models.Category;

public class CategoryVM
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Slug { get; set; }
    public string? Description { get; set; }
    public List<BookVM> Books { get; set; } = [];

    public bool IsSelected { get; set; }
}

public class CategoryItemHomePageVM
{

    public string? Name { get; set; }
    public string? Slug { get; set; }
    public int? BookCount { get; set; }


}
