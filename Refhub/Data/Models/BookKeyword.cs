namespace Refhub_Ir.Data.Models
{
    public class BookKeyword
    {
        public int BookId { get; set; }
        public Book Book { get; set; }
        public int KeywordId { get; set; }
        public Keyword Keyword { get; set; }
    }
}
