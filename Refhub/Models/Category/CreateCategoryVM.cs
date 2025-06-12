using Refhub.Resources;
using System.ComponentModel.DataAnnotations;

namespace Refhub.Models.Category;

public class CreateCategoryVM
{
    [Required(ErrorMessageResourceType = typeof(Messages),
        ErrorMessageResourceName = nameof(Messages.Category_NameRequired))]
    [MaxLength(50, ErrorMessageResourceType = typeof(Messages),
        ErrorMessageResourceName = nameof(Messages.Name_Max_50))]
    public string Name { get; set; }

    [MaxLength(450, ErrorMessageResourceType = typeof(Messages),
        ErrorMessageResourceName = nameof(Messages.Slug_Max_450))]
    [Required(ErrorMessageResourceType = typeof(Messages),
       ErrorMessageResourceName = nameof(Messages.Slug_NameRequired))]
    public string Slug { get; set; }

    [Required(ErrorMessageResourceType = typeof(Messages),
       ErrorMessageResourceName = nameof(Messages.Category_Description))]
    public string Description { get; set; }
}