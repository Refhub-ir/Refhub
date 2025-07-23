# Refhub Copilot Instructions

## Project Overview

Refhub is a book management web application built with ASP.NET Core 9.0, using Entity Framework with SQL Server, ArvanCloud S3-compatible storage for file management, and a multi-language system (Persian/English).

## Architecture & Key Patterns

### Service Layer Architecture

- **Pattern**: Repository/Service pattern with dependency injection
- **Registration**: All services registered in `Tools/ExtentionMethod/AddServiceExtentionMethod.cs`
- **Interfaces**: Located in `Service/Interface/`, implementations in `Service/Implement/`
- **Example**: `IBookService` handles all book-related business logic, injected via DI
- **Critical**: Always register new services in `AddServiceExtentionMethod.cs` following existing patterns

### Database & Entity Framework

- **Context**: `AppDbContext` inherits from `IdentityDbContext<ApplicationUser>`
- **Configuration**: Entity configurations in `Data/Configuration/` (e.g., `BookConfiguration.cs`)
- **Migration Strategy**: Auto-migration on startup in `Program.cs` using `db.Database.Migrate()`
- **Soft Deletes**: Books use `IsDeleted` flag with filtered unique indexes (`HasFilter("IsDeleted = 0")`)
- **Relationships**: Complex many-to-many via junction entities (`BookAuthor`, `BookKeyword`)

### File Storage (ArvanCloud S3)

- **Service**: `S3FileUploaderService` implements `IFileUploaderService`
- **Provider**: ArvanCloud S3-compatible storage (Iranian cloud provider)
- **Configuration**: Uses `ForcePathStyle = true` for ArvanCloud compatibility
- **Buckets**: Managed via `BucketNameStatic.GetName(BucketNames.BookPdf|BookImages)`
- **Downloads**: Protected route `[Authorize] /download?fileUrl=` in `BookController`
- **Error Handling**: Custom `FileDownloadException` for S3 operations

### Multi-Language Support

- **Localization**: Configured in `MultiLanguageExtensionMethod.cs`
- **Cultures**: Persian (fa-IR) as default, English (en-US) support
- **Resources**: Message keys in `Resources/Messages.resx` and `Messages.fa.resx`
- **Usage**: Access via `IMessageService.Get("MessageKey")` for localized error messages

### Area-Based Organization

- **Admin Area**: Controllers in `Areas/Admin/Controllers/` (ManageBook, ManageCategory, etc.)
- **User Area**: Placeholder structure in `Areas/User/`
- **Routing**: Areas-first routing configured in `Program.cs`

## Development Workflows

### Database Operations

```bash
# Add new migration
dotnet ef migrations add MigrationName

# Update database (auto-runs on startup, but manual option)
dotnet ef database update
```

### Running the Application

```bash
# Development with different launch modes
dotnet run --launch-profile https-with-app      # Opens main app only
dotnet run --launch-profile https-with-swagger  # Opens Swagger only
dotnet run --launch-profile https-with-both     # Opens both app and Swagger

# Docker (with SQL Server)
docker-compose up
```

### Browser Launch Configuration

- **Environment Variable**: `LAUNCH_MODE` controls browser opening behavior
- **Values**: `app`, `swagger`, `both`, or unset (no auto-launch)
- **Implementation**: `BrowserLaunchExtentionMethod.cs` with configurable delays
- **URLs**: Main app at `https://localhost:7065`, Swagger at `/swagger`

### Key Configuration Files

- `appsettings.json` - Base configuration
- `appsettings.Development.json` - Dev overrides (local SQL Server, ArvanCloud credentials)
- `appsettings.Docker.json` - Container configuration

## Code Conventions

### Extension Methods Pattern

- **Location**: All extension methods in `Tools/ExtentionMethod/` (note: "Extention" typo is intentional)
- **Naming**: End with `ExtentionMethod.cs` to match existing codebase convention
- **Purpose**: Service registration, configuration setup, middleware registration, browser launching
- **Examples**: `AddServiceExtentionMethod.cs`, `BrowserLaunchExtentionMethod.cs`

### ViewModel Naming

- **Pattern**: `{Entity}{Purpose}VM` (e.g., `CreateBookVM`, `UpdateBookVM`, `BookDetailsVM`)
- **Location**: Organized by entity in `Models/{Entity}/` folders
- **Collection Initialization**: Use `= []` syntax for navigation properties to prevent `NullReferenceException`

### Controller Patterns

- **Dependency Injection**: Primary constructor pattern with all dependencies
- **Error Handling**: Custom exceptions with localized messages via `IMessageService`
- **Authorization**: `[Authorize]` attribute for protected actions (e.g., file downloads)
- **Route Patterns**: Use attribute routing for specific endpoints like `[HttpGet("download")]`

### Entity Relationships

- **Many-to-Many**: Junction entities (`BookAuthor`, `BookKeyword`) instead of direct relationships
- **Self-Referencing**: `BookRelation` for related books with `DeleteBehavior.Restrict`
- **Soft Deletes**: Use `IsDeleted` property, filter in queries and unique indexes
- **Foreign Keys**: Explicit FK properties with navigation properties for clarity

## Testing & Docker

### Test Project

- **Location**: `Refhub.Test/` (basic xUnit structure, expand as needed)
- **Framework**: xUnit with Visual Studio test runner
- **Target**: .NET 8.0 with nullable references enabled

### Docker Setup

- **SQL Server**: Runs on port 1433 with password `TheSecrutyKey@1213`
- **Web App**: Exposed on port 8080, internal port 8080
- **Environment**: Uses `ASPNETCORE_ENVIRONMENT=Docker`
- **Network**: Custom bridge network `refhub_network` for service communication

## Critical Integration Points

### File Upload/Download Flow

1. Files uploaded to S3 via `S3FileUploaderService`
2. URLs stored in database (`Book.FilePath`, `Book.ImagePath`)
3. Downloads authenticated and proxied through `/download` endpoint
4. Content-Type detection via `FileExtensionContentTypeProvider`

### Book Slug System

- **Uniqueness**: Enforced by filtered unique index (`IsDeleted = 0`)
- **URL Pattern**: `/BookDetails/{slug}` for public access
- **SEO-Friendly**: Used instead of IDs in public URLs

### Authentication Flow

- **Identity**: ASP.NET Core Identity with `ApplicationUser`
- **Cookie**: Configured in `CookieConfigureExtentionMethod.cs`
- **User-Book Relation**: Books can be owned by users (`Book.UserId`)

## Project Documentation & Future Roadmap

### Comprehensive Feature Documentation

- **Location**: `docs/future-docs/Features/` contains detailed specifications for planned features
- **Current Implementation**: Focus on Resource Management (01), Authors/Experts (02), and basic authentication (04)
- **Future Features**: Shopping cart (03), loyalty/points (07), smart search (08), analytics/ads (09), admin tools (10)

### Key Documentation Insights

#### Resource Management (Books/References)

- **Book Detail Pages**: Comprehensive specs in `docs/future-docs/Features/01-Resource-Management/`
- **View Tracking**: Auto-increment view counts on page access
- **Related Content**: Books linked by category, series, keywords, or authors
- **File Management**: Secure PDF downloads, audio file support
- **AI Integration**: Planned AI Q&A feature for book content
- **Social Features**: Comments, notes, like/dislike system

#### Database Design (ERD Reference)

- **Multi-entity Relationships**: See `docs/future-docs/Diagrams/erd.md`
- **Core Entities**: User, Order, Cart, Reference (Book), Author, Category, Keyword
- **Advanced Features**: Expert picks, organizational gifts, campaigns, experiences
- **Junction Tables**: Many-to-many via dedicated entities (matching current `BookAuthor`, `BookKeyword`)

#### Cache Strategy (Future Implementation)

- **Cache System**: Redis-based caching documented in `docs/future-docs/CacheSystem/`
- **Cache Types**: Page cache, data cache, query cache, context processor cache
- **Cache Keys**: Dynamic keys based on slug, page number, user authentication state
- **Cache Invalidation**: Smart invalidation after data changes
- **Timeouts**: 15 minutes for list data, 5 minutes for sensitive data

#### Planned Middleware Architecture

- **User Activity Tracking**: Log user behavior for analytics and security
- **Multi-language Support**: Auto-redirect based on IP/cookies, remove www prefix
- **Rate Limiting**: IP-based request throttling with crawler exceptions
- **Error Logging**: 404/500 error tracking with Sentry integration
- **Security Features**: Multiple middleware layers for protection

### Migration Considerations

- **SEO-Friendly URLs**: Maintain slug-based routing (`/BookDetails/{slug}`)
- **Multi-language**: Continue Persian (fa-IR) as default with English support
- **Performance**: Plan for Redis cache implementation to match Django predecessor
- **Security**: Rate limiting and user activity tracking for production

When working with this codebase, always check existing patterns in the `Tools/ExtentionMethod/` folder before adding new configuration, and follow the established ViewModel naming conventions. Refer to `docs/future-docs/` for comprehensive feature specifications and architectural decisions.
