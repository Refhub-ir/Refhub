using Refhub_Ir.Areas.Admin.DTOs;
using Refhub_Ir.Models.Categories;

namespace Refhub_Ir.Models.Books
{
    public class BooksList_VM
    {
        public List<BookVM> Books { get; set; } = new();
        public List<CategoryVM> Categories { get; set; } = new();
       

        public string AuthorSlug { get; set; }
        public string? AuthorFilter { get; set; }
        public string? CategoryFilter { get; set; }
        public string? SearchText { get; set; }
    }
}
