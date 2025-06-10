namespace Refhub_Ir.Models.DTO
{
    public class BookKeywordVM
    {
        public int BookId { get; set; }
        public int KeywordId { get; set; }
        public KeywordDTO Keyword { get; set; }
    }
    public class KeywordDTO
    {
        public string Word { get; set; }
        public int Id { get; internal set; }
    }
}
