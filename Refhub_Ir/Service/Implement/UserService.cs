using ErrorOr;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Refhub_Ir.Models.Users;
using Refhub_Ir.Service.Interface;
using Refhub_Ir.Tools.Static;

namespace Refhub_Ir.Service.Implement
{
    public class UserService(
            UserManager<ApplicationUser> _userManager,
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
                return Error.NotFound();

            if (!await _roleManager.RoleExistsAsync(RolesNameStatic.Admin))
                await _roleManager.CreateAsync(new IdentityRole() { Name = RolesNameStatic.Admin, NormalizedName = RolesNameStatic.Admin });

            IdentityResult res;
            if (await _userManager.IsInRoleAsync(user, RolesNameStatic.Admin))
                res = await _userManager.RemoveFromRoleAsync(user, RolesNameStatic.Admin);
            else
                res = await _userManager.AddToRoleAsync(user, RolesNameStatic.Admin);
            if (res.Succeeded)
                return new UserListAdminVM();
            else
                return Error.Validation(ErrorMessageAuthenticationStatic.Error_AddToRole);
        }

        public async Task<ErrorOr<LoginVM>> Login(LoginVM model, CancellationToken ct)
        {
            var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, lockoutOnFailure: false);
            if (result.Succeeded)
                return model;
            else
                return Error.NotFound(ErrorMessageAuthenticationStatic.Error_Login);
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
                if (error.Code == "DuplicateUserName" || error.Code == "DuplicateEmail")
                    return Error.Validation(ErrorMessageAuthenticationStatic.Error_Invalid_Email);
                else
                    return Error.Validation(ErrorMessageAuthenticationStatic.Error_Register);
            }

            return Error.Conflict();
        }
    }
}