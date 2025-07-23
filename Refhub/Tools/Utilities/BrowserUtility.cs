using System.Diagnostics;
using System.Runtime.InteropServices;

namespace Refhub.Tools.Utilities;

/// <summary>
/// Utility class for browser operations
/// </summary>
public static class BrowserUtility
{
    /// <summary>
    /// Opens the specified URL in the default browser
    /// </summary>
    /// <param name="url">URL to open</param>
    /// <param name="delay">Delay in milliseconds before opening (default: 2000ms)</param>
    public static void OpenInBrowser(string url, int delay = 2000)
    {
        // Use a background task to avoid blocking the application startup
        Task.Run(async () =>
        {
            await Task.Delay(delay);
            try
            {
                OpenUrl(url);
            }
            catch (Exception ex)
            {
                // Log the error but don't crash the application
                Console.WriteLine($"Failed to open browser for URL {url}: {ex.Message}");
            }
        });
    }

    /// <summary>
    /// Opens multiple URLs in the default browser with staggered delays
    /// </summary>
    /// <param name="urls">Dictionary of URLs with their respective delays</param>
    public static void OpenMultipleInBrowser(Dictionary<string, int> urls)
    {
        foreach (var urlWithDelay in urls)
        {
            OpenInBrowser(urlWithDelay.Key, urlWithDelay.Value);
        }
    }

    /// <summary>
    /// Opens the specified URL using platform-specific method
    /// </summary>
    /// <param name="url">URL to open</param>
    private static void OpenUrl(string url)
    {
        try
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                // Windows
                Process.Start(new ProcessStartInfo
                {
                    FileName = url,
                    UseShellExecute = true
                });
            }
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
            {
                // macOS
                Process.Start("open", url);
            }
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            {
                // Linux
                Process.Start("xdg-open", url);
            }
            else
            {
                throw new PlatformNotSupportedException("Unsupported operating system");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error opening URL {url}: {ex.Message}");
            throw;
        }
    }
}
