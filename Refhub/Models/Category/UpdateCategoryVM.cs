using Refhub.Resources;
using System.ComponentModel.DataAnnotations;

namespace Refhub.Models.Category;

public class UpdateCategoryVM
{
    public int Id { get; set; }
    [Required(ErrorMessageResourceType = typeof(Messages),
        ErrorMessageResourceName = nameof(Messages.Category_NameRequired))]
    [MaxLength(50, ErrorMessageResourceType = typeof(Messages),
        ErrorMessageResourceName = nameof(Messages.Name_Max_50))]
    public string Name { get; set; }
    [Required(ErrorMessageResourceType = typeof(Messages),
        ErrorMessageResourceName = nameof(Messages.Slug_NameRequired))]
    [MaxLength(450, ErrorMessageResourceType = typeof(Messages),
        ErrorMessageResourceName = nameof(Messages.Slug_Max_450))]
    public string Slug { get; set; }
    [Required(ErrorMessageResourceType = typeof(Messages),
       ErrorMessageResourceName = nameof(Messages.Category_Description))]
    public string Description { get; set; }
}