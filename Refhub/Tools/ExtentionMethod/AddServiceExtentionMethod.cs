using Refhub_Ir.Service.Implement;
using Refhub_Ir.Service.Interface;
using Refhub_Ir.Service.Interfaces;
using Refhub_Ir.Tools.Static;

namespace Refhub_Ir.Tools.ExtentionMethod
{
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
}
