using Microsoft.AspNetCore.Mvc;

namespace Refhub.Areas.User.Controllers;

[Area("User")]
public class UserController : Controller
{
    public IActionResult Dashboard()
    {
        return View();
    }
}
