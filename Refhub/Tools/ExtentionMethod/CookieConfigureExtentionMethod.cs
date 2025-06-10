namespace Refhub.Tools.ExtentionMethod;

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
