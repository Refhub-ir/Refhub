using Microsoft.EntityFrameworkCore;
using Refhub.Data.Context;
using Refhub.Data.Models;
using Refhub.Models.DTO;

namespace Refhub.Data.Seed
{
    public static class DataSeeder
    {
        public static void SeedInitialData(IServiceProvider serviceProvider)
        {
            using var scope = serviceProvider.CreateScope();
            var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();

            db.Database.Migrate();

            // Author
            var authorPath = Path.Combine(Directory.GetCurrentDirectory(), "SeedData", "AuthorData.xlsx");
            var authors = ExcelSeeder.ReadAuthorsFromExcel(authorPath);
            db.Authors.AddRange(authors);

            // Book
            var bookPath = Path.Combine(Directory.GetCurrentDirectory(), "SeedData", "BookData.xlsx");
            var books = ExcelSeeder.ReadBooksFromExcel(bookPath);
            db.Books.AddRange(books);


            // AuthorBook
            var authorBookPath = Path.Combine(Directory.GetCurrentDirectory(), "SeedData", "AuthorBookData.xlsx");
            var authorBook = ExcelSeeder.ReadBookAuthorFromExcel(authorBookPath);
            db.BookAuthors.AddRange(authorBook);


            // BookKeyword
            var bookKeywordPath = Path.Combine(Directory.GetCurrentDirectory(), "SeedData", "BookKeywordData.xlsx");
            var bookKeyword = ExcelSeeder.ReadBookKeywordFromExcel(bookKeywordPath);
            db.BookKeywords.AddRange(bookKeyword);

            // BookRelation
            var bookRelationPath = Path.Combine(Directory.GetCurrentDirectory(), "SeedData", "BookRelationData.xlsx");
            var bookRelation = ExcelSeeder.ReadBookRelationFromExcel(bookRelationPath);
            db.BookRelations.AddRange(bookRelation);

            // Category
            var categoryPath = Path.Combine(Directory.GetCurrentDirectory(), "SeedData", "CategoryData.xlsx");
            var category = ExcelSeeder.ReadCategoryFromExcel(categoryPath);
            db.Categories.AddRange(category);

            // Keyword
            var keywordPath = Path.Combine(Directory.GetCurrentDirectory(), "SeedData", "KeywordData.xlsx");
            var keyword = ExcelSeeder.ReadKeywordFromExcel(keywordPath);
            db.Keywords.AddRange(keyword);

            db.SaveChanges();
        }
    }
}
