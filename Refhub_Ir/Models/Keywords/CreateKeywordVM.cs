using System.ComponentModel.DataAnnotations;

namespace Refhub_Ir.Models.Keywords;

public class CreateKeywordVM
{
    [Required(ErrorMessage = "لطفا کلمه کلیدی را وارد کنید.")]
    public string Word { get; set; }
}
