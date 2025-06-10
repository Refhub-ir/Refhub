using Refhub_Ir.Data.Models;

namespace Refhub_Ir.Data.Models
{
    public class BookAuthor
    {

        public int BookId { get; set; }
        public Book Book { get; set; }
        public int AuthorId { get; set; }
        public Author Author { get; set; }

    }
}
