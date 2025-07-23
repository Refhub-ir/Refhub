# Refhub Copilot Instructions

## Project Overview
Refhub is a book management web application built with ASP.NET Core 9.0, using Entity Framework with SQL Server, AWS S3 for file storage, and a multi-language system (Persian/English).

## Architecture & Key Patterns

### Service Layer Architecture
- **Pattern**: Repository/Service pattern with dependency injection
- **Registration**: All services registered in `Tools/ExtentionMethod/AddServiceExtentionMethod.cs`
- **Interfaces**: Located in `Service/Interface/`, implementations in `Service/Implement/`
- **Example**: `IBookService` handles all book-related business logic, injected via DI

### Database & Entity Framework
- **Context**: `AppDbContext` inherits from `IdentityDbContext<ApplicationUser>`
- **Configuration**: Entity configurations in `Data/Configuration/` (e.g., `BookConfiguration.cs`)
- **Migration Strategy**: Auto-migration on startup in `Program.cs` using `db.Database.Migrate()`
- **Soft Deletes**: Books use `IsDeleted` flag with filtered unique indexes

### File Storage (AWS S3)
- **Service**: `S3FileUploaderService` implements `IFileUploaderService`
- **Buckets**: Managed via `BucketNameStatic.GetName(BucketNames.BookPdf)`
- **Downloads**: Protected route `/download?fileUrl=` requires authentication
- **Error Handling**: Custom `FileDownloadException` for S3 operations

### Multi-Language Support
- **Localization**: Configured in `MultiLanguageExtensionMethod.cs`
- **Cultures**: Persian (fa-IR) as default, English (en-US) support
- **Resources**: Message keys in `Resources/Messages.resx` and `Messages.fa.resx`
- **Usage**: Access via `IMessageService.Get("MessageKey")`

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
# Development
dotnet run

# Docker (with SQL Server)
docker-compose up
```

### Key Configuration Files
- `appsettings.json` - Base configuration
- `appsettings.Development.json` - Dev overrides
- `appsettings.Docker.json` - Container configuration

## Code Conventions

### Extension Methods Pattern
- **Location**: All extension methods in `Tools/ExtentionMethod/`
- **Naming**: End with `ExtentionMethod.cs` (note: typo is intentional, matches codebase)
- **Purpose**: Service registration, configuration setup, middleware registration

### ViewModel Naming
- **Pattern**: `{Entity}{Purpose}VM` (e.g., `CreateBookVM`, `UpdateBookVM`, `BookDetailsVM`)
- **Location**: Organized by entity in `Models/{Entity}/`

### Controller Patterns
- **Dependency Injection**: Primary constructor pattern with all dependencies
- **Error Handling**: Custom exceptions with localized messages
- **Authorization**: `[Authorize]` attribute for protected actions

### Entity Relationships
- **Many-to-Many**: Junction entities (`BookAuthor`, `BookKeyword`)
- **Self-Referencing**: `BookRelation` for related books with restricted delete behavior
- **Soft Deletes**: Use `IsDeleted` property, filter in queries and indexes

## Testing & Docker

### Test Project
- **Location**: `Refhub.Test/` (basic structure, expand as needed)
- **Framework**: Standard .NET test project

### Docker Setup
- **SQL Server**: Runs on port 1433 with password `TheSecrutyKey@1213`
- **Web App**: Exposed on port 8080
- **Environment**: Uses `ASPNETCORE_ENVIRONMENT=Docker`

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

When working with this codebase, always check existing patterns in the `Tools/ExtentionMethod/` folder before adding new configuration, and follow the established ViewModel naming conventions.
