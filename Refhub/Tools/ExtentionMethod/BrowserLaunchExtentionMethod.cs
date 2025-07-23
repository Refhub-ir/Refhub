using Refhub.Tools.Utilities;

namespace Refhub.Tools.ExtensionMethod;

/// <summary>
/// Extension methods for configuring browser launch behavior based on environment variables
/// </summary>
public static class BrowserLaunchExtentionMethod
{
    /// <summary>
    /// Configures browser launching behavior based on LAUNCH_MODE environment variable
    /// </summary>
    /// <param name="app">The WebApplication instance</param>
    /// <returns>The WebApplication instance for method chaining</returns>
    public static WebApplication UseBrowserLaunchMode(this WebApplication app)
    {
        // Only launch browsers in development environment
        if (!app.Environment.IsDevelopment())
        {
            return app;
        }

        // Wait for app to be ready before launching browsers
        _ = Task.Run(async () =>
        {
            await Task.Delay(1500); // Give the app time to start

            // Determine the base URL (prefer HTTPS)
            string baseUrl;
            if (app.Urls.Any())
            {
                baseUrl = app.Urls.FirstOrDefault(u => u.StartsWith("https")) ?? app.Urls.First();
            }
            else
            {
                baseUrl = "https://localhost:7065"; // fallback
            }

            // Check the launch mode from environment variable
            var launchMode = Environment.GetEnvironmentVariable("LAUNCH_MODE")?.ToLower();

            switch (launchMode)
            {
                case "app":
                    // Open only the main app
                    BrowserUtility.OpenInBrowser(baseUrl, 500);
                    break;

                case "swagger":
                    // Open only Swagger
                    BrowserUtility.OpenInBrowser($"{baseUrl}/swagger", 500);
                    break;

                case "both":
                    // Open both app and Swagger with staggered delays
                    BrowserUtility.OpenMultipleInBrowser(new Dictionary<string, int>
                    {
                        { baseUrl, 500 },                     // Open main app after 0.5 seconds
                        { $"{baseUrl}/swagger", 1000 }         // Open Swagger after 1 second
                    });
                    break;

                default:
                    // Default behavior: don't open any browser windows
                    // This preserves the existing "https" profile behavior
                    break;
            }
        });

        return app;
    }
}
