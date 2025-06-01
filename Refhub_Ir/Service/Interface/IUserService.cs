using ErrorOr;
using Refhub_Ir.Models.Users;

namespace Refhub_Ir.Service.Interface
{
    public interface IUserService
    {
        Task Logout(CancellationToken ct);
        Task<IEnumerable<UserListAdminVM>> GetListUserAdminPanelAsync(string? name, CancellationToken ct);
        Task<ErrorOr<UserListAdminVM>> AddToRoleForUserInAdminPanelAsync(string id, CancellationToken ct);
        Task<ErrorOr<LoginVM>> Login(LoginVM model, CancellationToken ct);
        Task<ErrorOr<RegisterVM>> Register(RegisterVM model, CancellationToken ct);
    }
}

