using Microsoft.AspNetCore.Identity;
using Refhub_Ir.Data.Context;
using Refhub_Ir.Tools.Static;

namespace Refhub_Ir.Tools.ExtentionMethod
{
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
}
