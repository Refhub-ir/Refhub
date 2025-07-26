using Refhub.Service.Implement;

using Refhub.Service.Interface;

namespace Refhub.Tools.ExtensionMethod;

public static class AddServiceExtentionMethod
{
    public static IServiceCollection AddCustomService(this IServiceCollection collection)
    {
        collection.AddScoped<IBookService, BookService>();
        collection.AddScoped<ICategoryService, CategoryService>();
        collection.AddScoped<IAuthorRepository, AuthorRepository>();
        collection.AddScoped<IAuthorService, AuthorService>();
        collection.AddScoped<IKeywordService, KeywordService>();
        collection.AddScoped<IUserService, UserService>();
        collection.AddScoped<IMessageService, MessageService>();

        collection.AddScoped<IFileUploaderService, S3FileUploaderService>();

        // Configure Swagger
        collection.ConfigureSwagger();

        return collection;
    }
}
