namespace Refhub_Ir.Models.DTO
{
    public class BookAuthorVM
    {
        public int BookId { get; set; }
        public int AuthorId { get; set; }
        public AuthorDTO Author { get; set; }
    }
    public class AuthorDTO
    {
        public string FullName { get; set; }
        public int Id { get; internal set; }
    }
}
