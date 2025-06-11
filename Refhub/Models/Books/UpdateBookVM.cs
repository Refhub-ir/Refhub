using System.ComponentModel.DataAnnotations;
using Refhub.Resources;

namespace Refhub.Models.Books;

public class UpdateBookVM
{

    public int Id { get; set; }

    [Required(ErrorMessageResourceType = typeof(Messages),
        ErrorMessageResourceName = nameof(Messages.Book_TitleRequired))]
    public string Title { get; set; }

    [Required(ErrorMessageResourceType = typeof(Messages),
        ErrorMessageResourceName = nameof(Messages.Book_SlugRequired))]
    public string Slug { get; set; }

    [Required(ErrorMessageResourceType = typeof(Messages),
        ErrorMessageResourceName = nameof(Messages.Book_PageCountRequired))]
    public int PageCount { get; set; }

    public IFormFile? File { get; set; }
    public string? FilePath { get; set; }

    public IFormFile? Image { get; set; }
    public string? ImagePath { get; set; }

    public string? UserId { get; set; }
    // Foreign Key
    [Required(ErrorMessageResourceType = typeof(Messages),
        ErrorMessageResourceName = nameof(Messages.Book_CategoryIdRequired))]
    public int CategoryId { get; set; }

    [Required(ErrorMessageResourceType = typeof(Messages),
        ErrorMessageResourceName = nameof(Messages.Book_AnotherIdRequired))]
    public List<int> AnotherId { get; set; }
}
