using Refhub.Resources;
using System.ComponentModel.DataAnnotations;

namespace Refhub.Models.Authors;

public class AuthorVM
{
    [Required(ErrorMessageResourceType = typeof(Messages),
        ErrorMessageResourceName = nameof(Messages.FullNameRequired))]
    public string FullName { get; set; }


    [Required(ErrorMessageResourceType = typeof(Messages),
        ErrorMessageResourceName = nameof(Messages.SlugRequired))]
    public string Slug { get; set; }

    public bool IsSelected { get; set; }
}
