using System.ComponentModel.DataAnnotations;

namespace Refhub_Ir.Models.Categories
{
    public class UpdateCategoryVM
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "لطفا نام دسته بندی را وارد کنید.")]
        public string Name { get; set; }
        [Required(ErrorMessage = "لطفا نام اسلاگ را وارد کنید.")]
        public string Slug { get; set; }
        [Required(ErrorMessage = "لطفا توضیحات دسته بندی را وارد کنید.")]
        public string Description { get; set; }
    }
}