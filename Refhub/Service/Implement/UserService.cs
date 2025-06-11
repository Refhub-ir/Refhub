using ErrorOr;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Refhub.Data.Models;
using Refhub.Models.Users;
using Refhub.Service.Interface;
using Refhub.Tools.Static;

namespace Refhub.Service.Implement;

public class UserService(
        UserManager<ApplicationUser> _userManager,
        IMessageService _messageService,
        RoleManager<IdentityRole> _roleManager,
        SignInManager<ApplicationUser> _signInManager)
       : IUserService
{
    public async Task Logout(CancellationToken ct)
    {
        await _signInManager.SignOutAsync();
    }

    public async Task<IEnumerable<UserListAdminVM>> GetListUserAdminPanelAsync(string? name, CancellationToken ct)
    {

        var users = _userManager.Users.AsQueryable();

        if (!string.IsNullOrEmpty(name))
        {
            users = users.Where(a => a.UserName.Contains(name));
        }
        var result = new List<UserListAdminVM>();

        foreach (var user in users)
        {
            var isAdmin = await _userManager.IsInRoleAsync(user, "Admin");

            result.Add(new UserListAdminVM
            {
                Id = user.Id,
                FullName = user.UserName,
                Email = user.Email,
                UserName = user.UserName,
                IsAdmin = isAdmin
            });
        }
        return result;
    }

    public async Task<ErrorOr<UserListAdminVM>> AddToRoleForUserInAdminPanelAsync(string id, CancellationToken ct)
    {
        var user = await _userManager.Users.FirstOrDefaultAsync(a => a.Id.Equals(id), ct);
        if (user == null)
        {
            return Error.NotFound("User.NotFound", _messageService.Get("User_NotFound"));
        }

        if (!await _roleManager.RoleExistsAsync(RolesNameStatic.Admin).ConfigureAwait(false))
        {
            await _roleManager.CreateAsync(new IdentityRole(RolesNameStatic.Admin)).ConfigureAwait(false);
        }

        IdentityResult res = await _userManager.IsInRoleAsync(user, RolesNameStatic.Admin)
            ? await _userManager.RemoveFromRoleAsync(user, RolesNameStatic.Admin)
            : await _userManager.AddToRoleAsync(user, RolesNameStatic.Admin);
        return res.Succeeded ? (ErrorOr<UserListAdminVM>)new UserListAdminVM() : (ErrorOr<UserListAdminVM>)Error.Validation(_messageService.Get("Account_Error_AddToRole"));
    }

    public async Task<ErrorOr<LoginVM>> Login(LoginVM model, CancellationToken ct)
    {
        var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, lockoutOnFailure: false);
        return result.Succeeded ? (ErrorOr<LoginVM>)model : (ErrorOr<LoginVM>)Error.NotFound(_messageService.Get("Account_LoginInvalid"));
    }

    public async Task<ErrorOr<RegisterVM>> Register(RegisterVM model, CancellationToken ct)
    {
        var user = new ApplicationUser { UserName = model.Email, Email = model.Email };
        var result = await _userManager.CreateAsync(user, model.Password);

        if (result.Succeeded)
        {
            await _signInManager.SignInAsync(user, isPersistent: false);
            return model;
        }

        foreach (var error in result.Errors)
        {
            return error.Code is "DuplicateUserName" or "DuplicateEmail"
                ? (ErrorOr<RegisterVM>)Error.Validation(_messageService.Get("The email entered is already registered."))
                : (ErrorOr<RegisterVM>)Error.Validation(_messageService.Get("Account_RegisterInValid"));
        }

        return Error.Conflict();
    }
}