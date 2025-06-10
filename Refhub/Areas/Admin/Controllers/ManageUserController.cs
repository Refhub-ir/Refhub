using Microsoft.AspNetCore.Mvc;
using Refhub.Service.Interface;

namespace Refhub.Areas.Admin.Controllers;

[Area("Admin")]
public class ManageUserController(IUserService userService) : Controller
{
    public async Task<IActionResult> Index(string? name, CancellationToken ct)
    {
        return View(await userService.GetListUserAdminPanelAsync(name, ct));
    }

    public async Task<IActionResult> AddAdminRole(string id, CancellationToken ct)
    {

        await userService.AddToRoleForUserInAdminPanelAsync(id, ct);
        return RedirectToAction(nameof(Index));

    }
}
