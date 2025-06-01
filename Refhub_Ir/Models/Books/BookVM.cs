namespace Refhub_Ir.Models.Books
{
    public class BookVM
    {
        public int Id { get; set; }


        public string? Title { get; set; }
        public string? Slug { get; set; }
        //public int PageCount { get; set; }

        public string FilePath { get; set; }


        //public string FilePath { get; set; }
        public string? ImagePath { get; set; }

        public string? AuthorFullName { get; set; }
        public string? AuthorSlug { get; set; }



        // Foreign Key
        //public int CategoryId { get; set; }

        public string? UserId { get; set; }
    }

    public class CategoryDropDownVM
    {
        public int Id { get; set; }

        public string CategoryName { get; set; }
        public bool IsSelected { get; set; } = false;
    }
}
