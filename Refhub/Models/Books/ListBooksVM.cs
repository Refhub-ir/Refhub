using Refhub_Ir.Areas.Admin.DTOs;
using Refhub_Ir.Models.Categories;

namespace Refhub_Ir.Models.Books
{
    public class ListBooksVM
    {
        public List<BookVM> Books { get; set; } = new();
        public List<CategoryVM> Categories { get; set; } = new();
        public List<AuthorVM> Authors { get; set; } = new();


        public string? AuthorFilter { get; set; }
        public string? CategoryFilter { get; set; }




        public int CurrentPage { get; set; } = 1;
        public int TotalPages { get; set; }
        public string? SearchText { get; set; }

        public bool HasPreviousPage => CurrentPage > 1;
        public bool HasNextPage => CurrentPage < TotalPages;

        public int PreviousPage => CurrentPage - 1;
        public int NextPage => CurrentPage + 1;
    }
}
