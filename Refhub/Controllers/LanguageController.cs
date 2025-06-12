using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Refhub.Service.Implement;
using Refhub.Service.Interface;

namespace Refhub.Controllers;

public class LanguageController(IMessageService messageService) : Controller
{


    [Route("/fa")]
    public IActionResult persian()
    {
        Response.Cookies.Append(CookieRequestCultureProvider.DefaultCookieName,
            CookieRequestCultureProvider.MakeCookieValue(new RequestCulture("fa-IR")),
            new CookieOptions() { Expires = DateTimeOffset.UtcNow.AddYears(1) });
        return RedirectToAction("Index", "Home");
    }
    [Route("/en")]
    public IActionResult english()
    {
        Response.Cookies.Append(CookieRequestCultureProvider.DefaultCookieName,
            CookieRequestCultureProvider.MakeCookieValue(new RequestCulture("en-US")),
            new CookieOptions() { Expires = DateTimeOffset.UtcNow.AddYears(1) });
        return RedirectToAction("Index","Home");
    }


}
