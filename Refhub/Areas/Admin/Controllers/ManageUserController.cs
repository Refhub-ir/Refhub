using Microsoft.AspNetCore.Mvc;
using Refhub_Ir.Models.Users;
using Refhub_Ir.Service.Interface;

namespace Refhub_Ir.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ManageUserController(IUserService userService) : Controller
    {
        public async Task<IActionResult> Index(string? name, CancellationToken ct)
            => View(await userService.GetListUserAdminPanelAsync(name, ct));


        public async Task<IActionResult> AddAdminRole(string id, CancellationToken ct)
        {

           await userService.AddToRoleForUserInAdminPanelAsync(id, ct);
            return RedirectToAction(nameof(Index));

        }
    }
}
