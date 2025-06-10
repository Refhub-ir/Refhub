using Refhub.Models.DTO;

namespace Refhub.Models.Books;

public class BookDetailsVM
{
    public string Title { get; set; }
    public string Slug { get; set; }
    public string FilePath { get; set; }
    public string ImagePath { get; set; }
    public List<BookKeywordVM> BookKeywordsVM { get; set; } = [];
    public List<BookAuthorVM> BookAuthorsVM { get; set; } = [];
    public List<BookRelationVM> RelatedToVM { get; set; } = [];
    public List<BookRelationVM> RelatedFromVM { get; set; } = [];
}