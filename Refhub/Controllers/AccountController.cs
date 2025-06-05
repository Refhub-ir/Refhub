using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Refhub_Ir.Models.Users;

namespace Refhub_Ir.Controllers;

public class AccountController : Controller
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;

    public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
    {
        _userManager = userManager;
        _signInManager = signInManager;
    }

    #region Register
    [HttpGet]
    public IActionResult Register()
    {
        return View();
    }

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Register(RegisterViewModel model)
    {
        if (ModelState.IsValid)
        {
            var user = new ApplicationUser { UserName = model.Email, Email = model.Email };
            var result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                await _signInManager.SignInAsync(user, isPersistent: false);
                return RedirectToAction("Login");
            }

            foreach (var error in result.Errors)
            {
                if (error.Code is "DuplicateUserName" or "DuplicateEmail")
                {
                    ModelState.AddModelError("Email", "ایمیل وارد شده قبلاً ثبت شده است.");
                }
                else
                {
                    ModelState.AddModelError("Email", "ثبت نام با ارور مواجه شده است ");
                }
            }

        }
        return View(model);
    }
    #endregion

    #region Login
    [HttpGet]
    public IActionResult Login()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Login(LoginViewModel model)
    {
        if (ModelState.IsValid)
        {
            var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, lockoutOnFailure: false);
            if (result.Succeeded)
            {
                return RedirectToAction("Index", "Home");
            }

            ModelState.AddModelError("Email", "تلاش برای ورود نامعتبر است.");
        }
        return View(model);
    }
    #endregion

    #region Logout

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Logout()
    {
        await _signInManager.SignOutAsync();
        return RedirectToAction("Index", "Home");
    }
    #endregion

    public IActionResult AccessDenied()
    {
        return View();
    }
}


