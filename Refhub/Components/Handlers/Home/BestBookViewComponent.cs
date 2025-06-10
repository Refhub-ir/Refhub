using Microsoft.AspNetCore.Mvc;
using Refhub.Service.Interface;

namespace Refhub.Components.Handlers.Home;

public class BestBookViewComponent
    (IBookService bookService)
    : ViewComponent
{

    public async Task<IViewComponentResult> InvokeAsync(CancellationToken ct)
    {
        string viewPath = this.GetDefaultViewPath();
        return View(viewPath, await bookService.GetBestBooksAsync(ct));
    }

}
