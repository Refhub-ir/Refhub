using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Refhub_Ir.Data.Context;
using Refhub_Ir.Tools.Static;

namespace Refhub_Ir.Tools.ExtentionMethod
{
    public static class ContextConfigureExtentionMethod
    {

        public static IServiceCollection ConfigureContext(this IServiceCollection service,ConfigurationManager configuration)
        {


            service.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
            return service;
        }
    }
}
