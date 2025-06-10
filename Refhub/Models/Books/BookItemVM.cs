namespace Refhub.Models.Books;

public class BookItemVM
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Slug { get; set; }
    public string CategoryName { get; set; }
    public string Authores { get; set; }

    public string ImagePath { get; set; }

}