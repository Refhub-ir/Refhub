using Refhub_Ir.Models;

namespace Refhub_Ir.Data.Models
{
    public class Author
    {
        public int Id { get; set; }

        public string FullName { get; set; }
        public string Slug { get; set; }

        // Navigation Property
        public List<BookAuthor> BookAuthors { get; set; } = new();
    }
}
