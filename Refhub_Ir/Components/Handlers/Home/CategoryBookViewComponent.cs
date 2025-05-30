using Microsoft.AspNetCore.Mvc;
using Refhub_Ir.Service.Interface;

namespace Refhub_Ir.Components.Handlers.Home
{
    public class CategoryBookViewComponent
        (IBookService bookService)
        : ViewComponent
    {

        public async Task<IViewComponentResult> InvokeAsync( CancellationToken ct)
        {
            string viewPath = this.GetDefaultViewPath();
            return View(viewPath);
        }

    }
}
