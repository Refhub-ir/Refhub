using Microsoft.AspNetCore.Identity;
using Refhub.Data.Context;
using Refhub.Data.Models;

namespace Refhub.Tools.ExtensionMethod;

public static class IdentityConfigureExtentionMethod
{
    public static IServiceCollection ConfigureIdentity(this IServiceCollection service)
    {
        service.AddIdentity<ApplicationUser, IdentityRole>(options =>
            {
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireDigit = true;
                options.Password.RequiredLength = 6;
            })
            .AddEntityFrameworkStores<AppDbContext>()
            .AddDefaultTokenProviders();

        return service;
    }

}
