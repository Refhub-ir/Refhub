using Microsoft.EntityFrameworkCore;
using Refhub.Data.Context;

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
            var authorBookPath = Path.Combine(Directory.GetCurrentDirectory(), "SeedData", "AuthorBookData");
            var authorBook = ExcelSeeder.ReadBooksFromExcel(authorBookPath);
            db.BookAuthors.AddRange(authorBook);


            // BookKeyword
            var bookKeywordPath = Path.Combine(Directory.GetCurrentDirectory(), "SeedData", "BookKeywordData");
            var bookKetword = ExcelSeeder.ReadBooksFromExcel(bookKeywordPath);
            db.BookKeywords.AddRange(bookKetword);

            // BookRelation
            var bookRelationPath = Path.Combine(Directory.GetCurrentDirectory(), "SeedData", "BookRelationData");
            var bookRelation = ExcelSeeder.ReadBooksFromExcel(bookKeywordPath);
            db.BookKeywords.AddRange(bookRelation);

            // Category
            var categoryPath = Path.Combine(Directory.GetCurrentDirectory(), "SeedData", "CategorynData");
            var Category = ExcelSeeder.ReadBooksFromExcel(bookKeywordPath);
            db.BookKeywords.AddRange(Category);

            // Keyword
            var keywordPath = Path.Combine(Directory.GetCurrentDirectory(), "SeedData", "KeywordData");
            var Keyword = ExcelSeeder.ReadBooksFromExcel(bookKeywordPath);
            db.BookKeywords.AddRange(Keyword);

            db.SaveChanges();
        }
    }
}
