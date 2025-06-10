using Microsoft.AspNetCore.Identity;
using Refhub_Ir.Data.Context;
using Refhub_Ir.Tools.Static;

namespace Refhub_Ir.Tools.ExtentionMethod
{
    public static class CookieConfigureExtentionMethod
    {

        public static IServiceCollection ConfigureCookie(this IServiceCollection service)
        {
       

            service.ConfigureApplicationCookie(options =>
            {
                options.LoginPath = "/Account/Login";
                options.AccessDeniedPath = "/Account/AccessDenied";
                options.Cookie.Name = "RefHub.AuthCookie";
            });
            return service;
        }
    }
}
