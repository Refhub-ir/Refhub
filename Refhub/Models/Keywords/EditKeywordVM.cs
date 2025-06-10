using System.ComponentModel.DataAnnotations;

namespace Refhub_Ir.Models.Keywords
{
    public class EditKeywordVM
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "کلمه کلیدی الزامی است")]
        public string Word { get; set; }
    }
}
