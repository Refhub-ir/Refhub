using Refhub.Resources;
using System.ComponentModel.DataAnnotations;

namespace Refhub.Models.Category;

public class UpdateCategoryVM
{
    public int Id { get; set; }
    [Required(ErrorMessageResourceType = typeof(Messages),
        ErrorMessageResourceName = nameof(Messages.Category_NameRequired))]
    public string Name { get; set; }
    [Required(ErrorMessageResourceType = typeof(Messages),
        ErrorMessageResourceName = nameof(Messages.Name_Slug_Required))]
    public string Slug { get; set; }
    [Required(ErrorMessageResourceType = typeof(Messages),
       ErrorMessageResourceName = nameof(Messages.Category_Description))]
    public string Description { get; set; }
}