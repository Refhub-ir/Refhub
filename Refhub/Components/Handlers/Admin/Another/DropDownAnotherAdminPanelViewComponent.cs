using Microsoft.AspNetCore.Mvc;
using Refhub_Ir.Service.Interface;

namespace Refhub_Ir.Components.Handlers.Admin.Another;

public class DropDownAnotherAdminPanelViewComponent
    (IBookService bookService)
    : ViewComponent
{

    public async Task<IViewComponentResult> InvokeAsync(List<int> Ids)
    {

        return View("/Components/Views/Admin/Another/DropDownAnotherAdminPanel.cshtml", await bookService.GetAnothers(Ids)); //
    }

}
