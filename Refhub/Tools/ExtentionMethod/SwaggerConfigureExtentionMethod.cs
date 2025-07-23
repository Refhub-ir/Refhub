using Microsoft.OpenApi.Models;

namespace Refhub.Tools.ExtensionMethod;

public static class SwaggerConfigureExtentionMethod
{
    /// <summary>
    /// Configure Swagger/OpenAPI services
    /// </summary>
    /// <param name="services">Service collection</param>
    /// <returns>Service collection for chaining</returns>
    public static IServiceCollection ConfigureSwagger(this IServiceCollection services)
    {
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "Refhub API",
                Version = "v1",
                Description = "Book management web application API",
                Contact = new OpenApiContact
                {
                    Name = "Refhub Team",
                    Email = "info@refhub.ir",
                    Url = new Uri("https://refhub.ir")
                }
            });

            // Include XML comments if available
            var xmlFile = $"{System.Reflection.Assembly.GetExecutingAssembly().GetName().Name}.xml";
            var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
            if (File.Exists(xmlPath))
            {
                c.IncludeXmlComments(xmlPath);
            }

            // Add JWT Authentication support in Swagger
            c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
                Name = "Authorization",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.ApiKey,
                Scheme = "Bearer"
            });

            c.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    Array.Empty<string>()
                }
            });
        });

        return services;
    }

    /// <summary>
    /// Configure Swagger middleware and UI
    /// </summary>
    /// <param name="app">Web application</param>
    /// <param name="environment">Environment</param>
    /// <returns>Web application for chaining</returns>
    public static WebApplication UseSwaggerWithUI(this WebApplication app, IWebHostEnvironment environment)
    {
        // Enable Swagger in development and Docker environments
        if (environment.IsDevelopment() || environment.EnvironmentName == "Docker")
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Refhub API v1");
                c.RoutePrefix = "swagger"; // Set Swagger UI at /swagger
                c.DocumentTitle = "Refhub API Documentation";
                c.DisplayRequestDuration();
                c.EnableDeepLinking();
                c.EnableFilter();
                c.ShowExtensions();
                c.EnableValidator();
            });
        }

        return app;
    }
}
