using System.ComponentModel.DataAnnotations;

namespace Refhub.Models.Authors;

public class AuthorVM
{
    [Required(ErrorMessage = "نام کامل الزامی است")]
    public string FullName { get; set; }

    [Required(ErrorMessage = "اسلاگ الزامی است")]
    public string Slug { get; set; }

    public bool IsSelected { get; set; }
}
