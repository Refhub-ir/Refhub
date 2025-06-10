using Microsoft.EntityFrameworkCore;
using Refhub.Data.Context;

namespace Refhub.Tools.ExtentionMethod;

public static class ContextConfigureExtentionMethod
{

    public static IServiceCollection ConfigureContext(this IServiceCollection service, ConfigurationManager configuration)
    {


        service.AddDbContext<AppDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
        return service;
    }
}
