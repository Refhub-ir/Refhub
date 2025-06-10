using Microsoft.AspNetCore.Mvc;
using Refhub_Ir.Service.Interface;

namespace Refhub_Ir.Components.Handlers.Admin.Another;

public class DropDownAnotherAdminPanelViewComponent
    (IBookService bookService)
    : ViewComponent
{

    public async Task<IViewComponentResult> InvokeAsync(List<int> Ids, CancellationToken ct)
    {
        string viewPath = this.GetDefaultViewPath();
        return View(viewPath, await bookService.GetAnothersAsync(Ids, ct));
    }

}
