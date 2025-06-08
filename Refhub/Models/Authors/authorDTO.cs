using System.ComponentModel.DataAnnotations;
using Refhub.Resources;

namespace Refhub_Ir.Areas.Admin.DTOs;

public class AuthorDTO
{
    [Required(ErrorMessageResourceType = typeof(Messages),
        ErrorMessageResourceName = nameof(Messages.FullNameRequired))]
    public string FullName { get; set; }

    [Required(ErrorMessageResourceType = typeof(Messages),
        ErrorMessageResourceName = nameof(Messages.SlugRequired))]
    public string Slug { get; set; }
}
