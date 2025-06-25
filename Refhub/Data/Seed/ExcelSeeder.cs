using Microsoft.CodeAnalysis.CSharp.Syntax;
using Refhub.Data.Models;
using OfficeOpenXml;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;

namespace Refhub.Data.Seed
{
    public static class ExcelSeeder
    {
        public static void License()
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
        }


        // Read Data from Author File
        public static List<Author> ReadAuthorsFromExcel(string filePath)
        {
            License();
            var authors = new List<Author>();

            using (var package = new ExcelPackage(new FileInfo(filePath)))
            {
                var workSheet = package.Workbook.Worksheets[0];
                int rowCount = workSheet.Dimension.Rows;

                for (int row = 2; row <= rowCount; row++)
                {
                    var fullName = workSheet.Cells[row, 1].Text;
                    var slug = workSheet.Cells[row, 2].Text;

                    if (!String.IsNullOrWhiteSpace(fullName) && !String.IsNullOrWhiteSpace(slug))
                    {
                        authors.Add(new Author
                        {
                            FullName = fullName,
                            Slug = slug
                        });
                    }
                }
                return authors;
            }
        }

        // Read Data from Book File
        public static List<Book> ReadBooksFromExcel(string filePath)
        {
            License();
            var books = new List<Book>();
            using (var package = new ExcelPackage(new FileInfo(filePath)))
            {
                var workSheet = package.Workbook.Worksheets[0];
                int rowCount = workSheet.Dimension.Rows;

                for (int row = 2; row <= rowCount; row++)
                {
                    var title = workSheet.Cells[row, 1].Text;
                    var slug = workSheet.Cells[row, 2].Text;
                    var pageCountText = workSheet.Cells[row, 3].Text;
                    var filePathCell = workSheet.Cells[row, 4].Text;
                    var imagePath = workSheet.Cells[row, 5].Text;
                    var categoryIdText = workSheet.Cells[row, 6].Text;
                    var userId = workSheet.Cells[row, 7].Text;


                    if (string.IsNullOrWhiteSpace(title) || string.IsNullOrWhiteSpace(slug))
                        continue;

                    int.TryParse(pageCountText, out int pageCount);
                    int.TryParse(categoryIdText, out int categoryId);

                    books.Add(new Book
                    {
                        Title = title,
                        Slug = slug,
                        PageCount = pageCount,
                        FilePath = filePathCell,
                        ImagePath = imagePath,
                        CategoryId = categoryId,
                        UserId = string.IsNullOrWhiteSpace(userId) ? null : userId
                    });
                }
            }
            return books;
        }

        // Read Data from BookAuthor Excel file
        public static List<BookAuthor> ReadBookAuthorFromExcel(string filePath)
        {
            License();
            var bookAuthors = new List<BookAuthor>();

            using (var package = new ExcelPackage(new FileInfo(filePath)))
            {
                var workSheet = package.Workbook.Worksheets[0];
                int rowCount = workSheet.Dimension.Rows;
                for (int row = 2; row < rowCount; row++)
                {
                    var authorId = workSheet.Cells[row, 1].Text;
                    var bookId = workSheet.Cells[row, 2].Text;

                    if (!String.IsNullOrWhiteSpace(authorId) && !String.IsNullOrWhiteSpace(bookId))
                        bookAuthors.Add(new BookAuthor
                        {
                            AuthorId = Convert.ToInt32(authorId),
                            BookId = Convert.ToInt32(bookId)
                        });
                }
            }
            return bookAuthors;
        }

        // Read Data from KeyWord Excel file
        public static List<Keyword> ReadKeywordFromExcel(string filePath)
        {
            License();
            var keywords = new List<Keyword>();

            using (var package = new ExcelPackage(new FileInfo(filePath)))
            {
                var workSheet = package.Workbook.Worksheets[0];
                int rowCount = workSheet.Dimension.Rows;
                for (int row = 2; row < rowCount; row++)
                {
                    var keyword = workSheet.Cells[row, 1].Text;

                    if (!String.IsNullOrWhiteSpace(keyword))
                    {
                        keywords.Add(new Keyword
                        {
                            Word = keyword,
                        });
                    }
                }
            }
            return keywords;
        }

        // Read Data from Category Excel file
        public static List<Category> ReadCategoryFromExcel(string filePath)
        {
            License();
            var categories = new List<Category>();

            using (var package = new ExcelPackage(new FileInfo(filePath)))
            {
                var workSheet = package.Workbook.Worksheets[0];
                int rowCount = workSheet.Dimension.Rows;

                for (int row = 2; row < rowCount; row++)
                {
                    var name = workSheet.Cells[row, 1].Text;
                    var slug = workSheet.Cells[row, 2].Text;
                    var description = workSheet.Cells[row, 3].Text;

                    if (!String.IsNullOrWhiteSpace(name) && !String.IsNullOrWhiteSpace(slug) && !String.IsNullOrWhiteSpace(description))
                    {
                        categories.Add(new Category
                        {
                            Name = name,
                            Description = description,
                            slug = slug
                        });
                    }
                }
            }
            return categories;
        }

        // Read Data from BookRelation Excel file
        public static List<BookRelation> ReadBookRelationFromExcel(string filePath)
        {
            License();
            var bookRelations = new List<BookRelation>();

            using (var package = new ExcelPackage(new FileInfo(filePath)))
            {
                var workSheet = package.Workbook.Worksheets[0];
                var rowCount = workSheet.Dimension.Rows;

                for (int row = 2; row < rowCount; row++)
                {
                    var bookId = workSheet.Cells[row, 1].Text;
                    var relationBookId = workSheet.Cells[row, 2].Text;

                    if (!String.IsNullOrWhiteSpace(bookId) && !String.IsNullOrWhiteSpace(relationBookId))
                    {
                        bookRelations.Add(new BookRelation
                        {
                            BookId = Convert.ToInt32(bookId),
                            RelatedBookId = Convert.ToInt32(relationBookId)
                        });
                    }
                }
            }
            return bookRelations;
        }


   

    }
}