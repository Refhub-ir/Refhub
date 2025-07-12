using Refhub.Models.AppSetting;
using Refhub.Service.Implement;
using Refhub.Service.Interface;

namespace Refhub.Tools.ExtensionMethod;

public static class BindAppSettingToModelExtentionMethod
{
    public static WebApplicationBuilder BindS3Model(this WebApplicationBuilder builder)
    {
        builder.Services.Configure<S3Configuration>(
            builder.Configuration.GetSection("AWS:S3"));
        return builder;
    }
}
