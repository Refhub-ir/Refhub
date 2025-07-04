using Refhub.Resources;
using System.ComponentModel.DataAnnotations;

namespace Refhub.Models.Keywords;

public class CreateKeywordVM
{
    [Required(ErrorMessageResourceType = typeof(Messages),
   ErrorMessageResourceName = nameof(Messages.Keyword_Required))]
    public string Word { get; set; }
}
