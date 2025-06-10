using System.ComponentModel.DataAnnotations;

namespace Refhub_Ir.Models.Users
{
    public class LoginVM
    {
        [Required(ErrorMessage = "ایمیل خود را وارد کنید")]
        [EmailAddress(ErrorMessage = "فرمت ایمیل درست نیست")]
        public string Email { get; set; }

        [RegularExpression(@"^(?=.*[A-Za-z])(?=.*\d)[A-Za-z\d]{6,}$", ErrorMessage = "حداقل یک حرف انگلیسی داشته باشه|حداقل یک عدد هم داشته باشه|طولش حداقل ۶ کاراکتر باشه")]
        [Required(ErrorMessage = "رمز عبورفراموش نشه ")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "مرا به خاطر بسپار ! ")]
        public bool RememberMe { get; set; }
    }
}
