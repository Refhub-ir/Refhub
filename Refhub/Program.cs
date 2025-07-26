using Microsoft.EntityFrameworkCore;
using Refhub.Data.Context;
using Refhub.Data.Models;
using Refhub.Data.Seed;
using Refhub.Tools.ExtensionMethod;
using Refhub.Tools.Utilities;



namespace Refhub;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddControllersWithViews().AddMultiLanguage();


        //Add  CUstomServices





        #region CustomExtentionMethod 
        builder.BindS3Model();
        builder.Services.AddCustomService();
        builder.Services.ConfigureContext(builder.Configuration);
        builder.Services.ConfigureCookie();
        builder.Services.ConfigureIdentity();
        builder.Configuration
                            .SetBasePath(Directory.GetCurrentDirectory())
                            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                            .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true)
                            .AddEnvironmentVariables();
        #endregion
        var app = builder.Build();
        using (var scope = app.Services.CreateScope())
        {
            var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
            db.Database.Migrate(); // این خط باعث اجرای مایگریشن‌ها می‌شه
        }

        // Configure the HTTP request pipeline.
        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Home/Error");
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }

        // Configure Swagger
        app.UseSwaggerWithUI(app.Environment);

        app.UseHttpsRedirection();
        app.UseStaticFiles();
        app.UseMultiLanguage();
        app.UseRouting();

        app.UseAuthentication();
        app.UseAuthorization();



        app.MapControllerRoute(
            name: "areas",
            pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
        );

        app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

        // Configure browser launching based on environment variables
        app.UseBrowserLaunchMode();

        // Seed initial data from Excel files
        try
        {
            DataSeeder.SeedInitialData(app.Services);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Failed to seed data: {ex.Message}");
            // Continue startup even if seeding fails in development
        }

        app.Run();
    }
}
