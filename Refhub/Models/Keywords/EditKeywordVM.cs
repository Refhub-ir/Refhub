using Refhub.Resources;
using System.ComponentModel.DataAnnotations;

namespace Refhub.Models.Keywords;

public class EditKeywordVM
{
    public int Id { get; set; }

    [Required(ErrorMessageResourceType = typeof(Messages),
    ErrorMessageResourceName = nameof(Messages.Keyword_Required))]
    public string Word { get; set; }
}
