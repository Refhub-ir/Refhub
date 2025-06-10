using Microsoft.AspNetCore.Mvc;

namespace Refhub_Ir.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AdminController : Controller
    {
        public IActionResult Dashboard()
        {
            return View();
        }
    }
}
