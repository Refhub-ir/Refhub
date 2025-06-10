using Refhub.Service.Implement;
using Refhub.Service.Interface;

namespace Refhub.Tools.ExtentionMethod;

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

        collection.AddScoped<IFileUploaderService, LocalFileUploaderService>();
        return collection;
    }
}
