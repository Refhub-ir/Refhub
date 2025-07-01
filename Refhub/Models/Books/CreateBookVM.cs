using System.ComponentModel.DataAnnotations;
using Refhub.Resources;

namespace Refhub.Models.Books;

public class CreateBookVM
{
    [Required(ErrorMessageResourceType = typeof(Messages),
        ErrorMessageResourceName = nameof(Messages.Book_TitleRequired))]
    public string Title { get; set; }

    [Required(ErrorMessageResourceType = typeof(Messages),
        ErrorMessageResourceName = nameof(Messages.Book_SlugRequired))]
    [MaxLength(450, ErrorMessageResourceType = typeof(Messages),
        ErrorMessageResourceName = nameof(Messages.Book_SlugMaxLength))]
    public string Slug { get; set; }

    [Range(1, int.MaxValue,
           ErrorMessageResourceType = typeof(Messages),
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
    [MinLength(1, ErrorMessageResourceType = typeof(Messages),
        ErrorMessageResourceName = nameof(Messages.Book_AnotherIdMinLength))]
    public List<int> AnotherId { get; set; }= new();
}
