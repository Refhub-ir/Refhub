using System.ComponentModel.DataAnnotations;

namespace Refhub_Ir.Areas.Admin.DTOs;

public class AuthorDTO
{
    [Required(ErrorMessage = "نام کامل الزامی است")]
    public string FullName { get; set; }

    [Required(ErrorMessage = "اسلاگ الزامی است")]
    public string Slug { get; set; }
}
