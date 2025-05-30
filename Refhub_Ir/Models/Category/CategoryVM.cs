using Refhub_Ir.Models.Books;

namespace Refhub_Ir.Models.Categories
{
    public class CategoryVM
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Slug { get; set; }
        public string? Description { get; set; }
        public List<BookVM> Books { get; set; } = new();

        public bool IsSelected { get; set; }
    }
}
