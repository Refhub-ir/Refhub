using Microsoft.AspNetCore.Mvc;
using Refhub_Ir.Service.Interface;
using Refhub_Ir.Service.Interfaces;

namespace Refhub_Ir.Components.Handlers.Home
{
    public class CategoryBookViewComponent
        (ICategoryService categoryService)
        : ViewComponent
    {

        public async Task<IViewComponentResult> InvokeAsync( CancellationToken ct)
        {
            string viewPath = this.GetDefaultViewPath();
            return View(viewPath,await categoryService.GetAllCategoriesHomePageAsync(ct,8));
        }

    }
}
