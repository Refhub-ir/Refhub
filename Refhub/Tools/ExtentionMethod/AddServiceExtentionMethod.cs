using Refhub.Service.Implement;
using Refhub.Service.Interface;
using Refhub_Ir.Service.Implement;
using Refhub_Ir.Service.Interface;
using Refhub_Ir.Service.Interfaces;

namespace Refhub_Ir.Tools.ExtentionMethod;

public static class AddServiceExtentionMethod
{
    public static IServiceCollection AddCustomService(this IServiceCollection collection)
    {
        collection.AddScoped<IBookService, BookService>();
        collection.AddScoped<ICategoryService, CategoryService>();
        collection.AddScoped<IAuthorRepository, AuthorRepository>();
        collection.AddScoped<IAuthorService, AuthorService>();
        collection.AddScoped<IKeywordService, KeywordService>();

        collection.AddScoped<IFileUploaderService, LocalFileUploaderService>();

        collection.AddScoped<IMessageService, MessageService>();
        return collection;
    }
}
