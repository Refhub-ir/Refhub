using System.ComponentModel.DataAnnotations;
using Refhub.Resources;

namespace Refhub_Ir.Models.Books;

public class CreateBookVM
{
    [Required(ErrorMessageResourceType = typeof(Messages),
        ErrorMessageResourceName = nameof(Messages.Book_TitleRequired))]
    public string Title { get; set; }

    [Required(ErrorMessageResourceType = typeof(Messages),
        ErrorMessageResourceName = nameof(Messages.Book_SlugRequired))]
    public string Slug { get; set; }

    [Required(ErrorMessageResourceType = typeof(Messages),
        ErrorMessageResourceName = nameof(Messages.Book_PageCountRequired))]
    public int PageCount { get; set; }

    [Required(ErrorMessageResourceType = typeof(Messages),
        ErrorMessageResourceName = nameof(Messages.Book_FileRequired))]
    public IFormFile File { get; set; }

    [Required(ErrorMessageResourceType = typeof(Messages),
        ErrorMessageResourceName = nameof(Messages.Book_ImageRequired))]
    public IFormFile Image { get; set; }

    public string? UserId { get; set; }
    // Foreign Key
    [Required(ErrorMessageResourceType = typeof(Messages),
        ErrorMessageResourceName = nameof(Messages.Book_CategoryIdRequired))]
    public int CategoryId { get; set; }

    [Required(ErrorMessageResourceType = typeof(Messages),
        ErrorMessageResourceName = nameof(Messages.Book_AnotherIdRequired))]
    public List<int> AnotherId { get; set; }
}
