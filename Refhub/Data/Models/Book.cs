using Refhub_Ir.Data.Models;

namespace Refhub_Ir.Data.Models
{
    public class Book
    {
        public int Id { get; set; }

        public string Title { get; set; }
        public string Slug { get; set; }
        public int PageCount { get; set; }
        public string FilePath { get; set; }
        public string ImagePath { get; set; }

        // Foreign Key
        public int CategoryId { get; set; }

        public string? UserId { get; set; }

        // Navigation Properties
        // Preventing NullReferenceException by initializing collections.
        public Category Category { get; set; }
        public ApplicationUser User { get; set; }
        public List<BookKeyword> BookKeywords { get; set; } = new();
        public List<BookAuthor> BookAuthors { get; set; } = new();
        public List<BookRelation> RelatedTo { get; set; } = new();
        public List<BookRelation> RelatedFrom { get; set; } = new();
    }
}
