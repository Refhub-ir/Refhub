using Microsoft.AspNetCore.Mvc;

namespace Refhub.Areas.Admin.Controllers;

[Area("Admin")]
public class AdminController : Controller
{
    public IActionResult Dashboard()
    {
        return View();
    }
}
