using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Refhub.Service.Implement;
using Refhub.Service.Interface;

namespace Refhub.Controllers;

public class LanguageController : Controller
{


    [Route("/fa")]
    public IActionResult Persian()
    {
        Response.Cookies.Append(CookieRequestCultureProvider.DefaultCookieName,
            CookieRequestCultureProvider.MakeCookieValue(new RequestCulture("fa-IR")),
            new CookieOptions() { Expires = DateTimeOffset.UtcNow.AddYears(1) });
        return RedirectToAction(nameof(HomeController.Index), nameof(HomeController).Replace("Controller", ""));
    }
    [Route("/en")]
    public IActionResult English()
    {
        Response.Cookies.Append(CookieRequestCultureProvider.DefaultCookieName,
            CookieRequestCultureProvider.MakeCookieValue(new RequestCulture("en-US")),
            new CookieOptions() { Expires = DateTimeOffset.UtcNow.AddYears(1) });
        return RedirectToAction(nameof(HomeController.Index), nameof(HomeController).Replace("Controller", ""));
    }


}
