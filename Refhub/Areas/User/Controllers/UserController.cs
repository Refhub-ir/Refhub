using Microsoft.AspNetCore.Mvc;

namespace Refhub_Ir.Areas.User.Controllers
{
    [Area("User")]
    public class UserController : Controller
    {
        public IActionResult Dashboard()
        {
            return View();
        }
    }
}
