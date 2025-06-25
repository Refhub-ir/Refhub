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

            // Authors
            //if (!db.Authors.Any())
            //{
                var authorPath = Path.Combine(Directory.GetCurrentDirectory(), "SeedData", "AuthorData.xlsx");

            var authors = ExcelSeeder.ReadAuthorsFromExcel(authorPath);
            db.Authors.AddRange(authors);
            //}

            // Books
            //if (!db.Books.Any())
            //{
            //    var bookPath = Path.Combine(Directory.GetCurrentDirectory(), "SeedData", "BookData.xlsx");
            //    var books = ExcelSeeder.ReadBooksFromExcel(bookPath);
            //    db.Books.AddRange(books);
            //}
                db.SaveChanges();
        }
    }
}
