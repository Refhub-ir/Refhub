using Microsoft.AspNetCore.Mvc;
using Refhub_Ir.Service.Interface;

namespace Refhub_Ir.Components.Handlers.Admin.Category
{
    public class DropDownCategoryAdminPanelViewComponent
        (IBookService bookService)
        : ViewComponent
    {

        public async Task<IViewComponentResult> InvokeAsync(int Id, CancellationToken ct)
        {
            string viewPath = this.GetDefaultViewPath();
            return View(viewPath, await bookService.GetCategoriesAsync(Id, ct));
        }

    }
}
