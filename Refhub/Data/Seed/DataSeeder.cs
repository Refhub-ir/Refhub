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
            try
            {
                using var scope = serviceProvider.CreateScope();
                var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();

                db.Database.Migrate();

                // Check if data already exists to avoid duplicates
                if (db.Authors.Any())
                {
                    Console.WriteLine("Database already seeded. Skipping...");
                    return;
                }

                // Author
                var authorPath = Path.Combine(Directory.GetCurrentDirectory(), "SeedData", "AuthorData.xlsx");
                if (File.Exists(authorPath))
                {
                    var authors = ExcelSeeder.ReadAuthorsFromExcel(authorPath);
                    if (authors.Any())
                    {
                        db.Authors.AddRange(authors);
                    }
                }

                // Category (seed before books since books reference categories)
                var categoryPath = Path.Combine(Directory.GetCurrentDirectory(), "SeedData", "CategoryData.xlsx");
                if (File.Exists(categoryPath))
                {
                    var categories = ExcelSeeder.ReadCategoryFromExcel(categoryPath);
                    if (categories.Any())
                    {
                        db.Categories.AddRange(categories);
                    }
                }

                // Keyword (seed before BookKeyword)
                var keywordPath = Path.Combine(Directory.GetCurrentDirectory(), "SeedData", "KeywordData.xlsx");
                if (File.Exists(keywordPath))
                {
                    var keywords = ExcelSeeder.ReadKeywordFromExcel(keywordPath);
                    if (keywords.Any())
                    {
                        db.Keywords.AddRange(keywords);
                    }
                }

                // Save basic entities first
                db.SaveChanges();

                // Book
                var bookPath = Path.Combine(Directory.GetCurrentDirectory(), "SeedData", "BookData.xlsx");
                if (File.Exists(bookPath))
                {
                    var books = ExcelSeeder.ReadBooksFromExcel(bookPath);
                    if (books.Any())
                    {
                        db.Books.AddRange(books);
                    }
                }

                // Save books before relationships
                db.SaveChanges();

                // AuthorBook
                var authorBookPath = Path.Combine(Directory.GetCurrentDirectory(), "SeedData", "AuthorBookData.xlsx");
                if (File.Exists(authorBookPath))
                {
                    var authorBooks = ExcelSeeder.ReadBookAuthorFromExcel(authorBookPath);
                    if (authorBooks.Any())
                    {
                        db.BookAuthors.AddRange(authorBooks);
                    }
                }

                // BookKeyword
                var bookKeywordPath = Path.Combine(Directory.GetCurrentDirectory(), "SeedData", "BookKeywordData.xlsx");
                if (File.Exists(bookKeywordPath))
                {
                    var bookKeywords = ExcelSeeder.ReadBookKeywordFromExcel(bookKeywordPath);
                    if (bookKeywords.Any())
                    {
                        db.BookKeywords.AddRange(bookKeywords);
                    }
                }

                // BookRelation
                var bookRelationPath = Path.Combine(Directory.GetCurrentDirectory(), "SeedData", "BookRelationData.xlsx");
                if (File.Exists(bookRelationPath))
                {
                    var bookRelations = ExcelSeeder.ReadBookRelationFromExcel(bookRelationPath);
                    if (bookRelations.Any())
                    {
                        db.BookRelations.AddRange(bookRelations);
                    }
                }

                db.SaveChanges();
                Console.WriteLine("Database seeding completed successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error during database seeding: {ex.Message}");
                throw;
            }
        }
    }
}
