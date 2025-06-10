using System.ComponentModel.DataAnnotations;

namespace Refhub_Ir.Models.Books
{
    public class CreateBookVM
    {

        [Required(ErrorMessage = "عنوان کتاب را وارد کنید")]
        public string Title { get; set; }
        [Required(ErrorMessage = "عنوان در مرورگر را وارد کنید")]
        public string Slug { get; set; }
        [Required(ErrorMessage = "تعداد صفحه را وارد کنید انتخاب کنید")]
        public int PageCount { get; set; }
        [Required(ErrorMessage = "یک فایل انتخاب کنید")]
        public IFormFile File { get; set; }
        [Required(ErrorMessage = "یک تصویر انتخاب کنید")]
        public IFormFile Image { get; set; }

        public string? UserId { get; set; }
        // Foreign Key
        [Required(ErrorMessage = "یک دسته بندی انتخاب کنید")]
        public int CategoryId { get; set; }
        [Required(ErrorMessage = "یک نویسند انتخاب کنید")]
        public List<int> AnotherId { get; set; }
    }
}
