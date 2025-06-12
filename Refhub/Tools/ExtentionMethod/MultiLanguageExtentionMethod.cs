using System.Globalization;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc.Razor;

namespace Refhub.Tools.ExtensionMethod;

public static class MultiLanguageExtensionMethod
{
    public static WebApplication UseMultiLanguage(this WebApplication app)
    {
        var supportedCultures = new List<CultureInfo>()
        {
            new CultureInfo("fa-IR"),
            new CultureInfo("en-US")
        };
        var options = new RequestLocalizationOptions()
        {
            DefaultRequestCulture = new RequestCulture("fa-IR"),
            SupportedCultures = supportedCultures,
            SupportedUICultures = supportedCultures,
            RequestCultureProviders = new List<IRequestCultureProvider>()
            {
                new QueryStringRequestCultureProvider(),
                new CookieRequestCultureProvider()
            }
        };
        app.UseRequestLocalization(options);
        return app;
    }

    public static IMvcBuilder AddMultiLanguage(this IMvcBuilder mvc)
    {
       
        mvc.AddViewLocalization(LanguageViewLocationExpanderFormat.Suffix, option =>
        {
            option.ResourcesPath = "Resources";
        }).AddDataAnnotationsLocalization();
        
        return mvc;
    }
}
