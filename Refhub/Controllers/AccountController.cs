using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Refhub_Ir.Data.Models;
using Refhub_Ir.Models.Users;
using Refhub_Ir.Service.Interface;

namespace Refhub_Ir.Controllers
{
    public class AccountController(IUserService userService) : Controller
    {

        #region Register
        [HttpGet]
        public IActionResult Register() => View();
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterVM model,CancellationToken ct)
        {
            if (ModelState.IsValid)
            {
                var result = await userService.Register(model, ct);
                if (!result.IsError)
                    return RedirectToAction(nameof(Login));
                ModelState.AddModelError("Email", string.Join(',', result.Errors.Select(a => a.Code)));

            }
            return View(model);
        }
        #endregion

        #region Login
        [HttpGet]
        public IActionResult Login() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginVM model, CancellationToken ct)
        {
            if (ModelState.IsValid)
            {
                var result = await userService.Login(model, ct);
                if (!result.IsError)
                    return RedirectToAction("Index", "Home");
                ModelState.AddModelError("Email", string.Join(',', result.Errors.Select(a=>a.Code)));
            }
            return View(model);
        }
        #endregion

        #region Logout
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout(CancellationToken ct)
        {
            await userService.Logout(ct);
            return RedirectToAction("Index", "Home");
        }
        #endregion

        public IActionResult AccessDenied() => View();
    }
}


