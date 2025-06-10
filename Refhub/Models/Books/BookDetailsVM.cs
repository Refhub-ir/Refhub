using Refhub_Ir.Models.DTO;

namespace Refhub_Ir.Models.Books
{
    public class BookDetailsVM
    {
        public string Title { get; set; }
        public string Slug { get; set; }
        public string FilePath { get; set; }
        public string ImagePath { get; set; }
        public List<BookKeywordVM> BookKeywordsVM { get; set; } = new();
        public List<BookAuthorVM> BookAuthorsVM { get; set; } = new();
        public List<BookRelationVM> RelatedToVM { get; set; } = new();
        public List<BookRelationVM> RelatedFromVM { get; set; } = new();
    }
}