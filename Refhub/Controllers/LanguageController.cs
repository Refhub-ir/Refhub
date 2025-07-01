using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;

namespace Refhub.Controllers;

public class LanguageController : Controller
{


    [Route("/fa")]
    public IActionResult Persian()
    {
        Response.Cookies.Append(CookieRequestCultureProvider.DefaultCookieName,
            CookieRequestCultureProvider.MakeCookieValue(new RequestCulture("fa-IR")),
            new CookieOptions
            {
                Expires = DateTimeOffset.UtcNow.AddYears(1),
                Secure = true,          // send only over HTTPS
                HttpOnly = true,        // not accessible to JS
                SameSite = SameSiteMode.Lax
            });
        return RedirectToAction(nameof(HomeController.Index), nameof(HomeController).Replace("Controller", ""));
    }
    [Route("/en")]
    public IActionResult English()
    {
        Response.Cookies.Append(CookieRequestCultureProvider.DefaultCookieName,
            CookieRequestCultureProvider.MakeCookieValue(new RequestCulture("en-US")),
            new CookieOptions
            {
                Expires = DateTimeOffset.UtcNow.AddYears(1),
                Secure = true,          // send only over HTTPS
                HttpOnly = true,        // not accessible to JS
                SameSite = SameSiteMode.Lax
            });
        return RedirectToAction(nameof(HomeController.Index), nameof(HomeController).Replace("Controller", ""));
    }


}
