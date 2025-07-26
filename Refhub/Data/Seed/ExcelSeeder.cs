using Microsoft.CodeAnalysis.CSharp.Syntax;
using Refhub.Data.Models;
using OfficeOpenXml;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;

namespace Refhub.Data.Seed
{
    public static class ExcelSeeder
    {
        static ExcelSeeder()
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
        }

        public static void License()
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
        }


        // Read Data from Author File
        public static List<Author> ReadAuthorsFromExcel(string filePath)
        {
            try
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
                }
                return authors;
            }
            catch (FileNotFoundException)
            {
                throw new InvalidOperationException($"Excel file not found: {filePath}");
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Error reading authors from Excel: {ex.Message}", ex);
            }
        }

        // Read Data from Book File
        public static List<Book> ReadBooksFromExcel(string filePath)
        {
            try
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
            catch (FileNotFoundException)
            {
                throw new InvalidOperationException($"Excel file not found: {filePath}");
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Error reading books from Excel: {ex.Message}", ex);
            }
        }

        // Read Data from BookAuthor Excel file
        public static List<BookAuthor> ReadBookAuthorFromExcel(string filePath)
        {
            try
            {
                License();
                var bookAuthors = new List<BookAuthor>();

                using (var package = new ExcelPackage(new FileInfo(filePath)))
                {
                    var workSheet = package.Workbook.Worksheets[0];
                    int rowCount = workSheet.Dimension.Rows;
                    for (int row = 2; row <= rowCount; row++)
                    {
                        var authorId = workSheet.Cells[row, 1].Text;
                        var bookId = workSheet.Cells[row, 2].Text;

                        if (!String.IsNullOrWhiteSpace(authorId) && !String.IsNullOrWhiteSpace(bookId)
                            && int.TryParse(authorId, out int authorIdValue) && int.TryParse(bookId, out int bookIdValue))
                        {
                            bookAuthors.Add(new BookAuthor
                            {
                                AuthorId = authorIdValue,
                                BookId = bookIdValue
                            });
                        }
                    }
                }
                return bookAuthors;
            }
            catch (FileNotFoundException)
            {
                throw new InvalidOperationException($"Excel file not found: {filePath}");
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Error reading book authors from Excel: {ex.Message}", ex);
            }
        }

        // Read Data from KeyWord Excel file
        public static List<Keyword> ReadKeywordFromExcel(string filePath)
        {
            try
            {
                License();
                var keywords = new List<Keyword>();

                using (var package = new ExcelPackage(new FileInfo(filePath)))
                {
                    var workSheet = package.Workbook.Worksheets[0];
                    int rowCount = workSheet.Dimension.Rows;
                    for (int row = 2; row <= rowCount; row++)
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
            catch (FileNotFoundException)
            {
                throw new InvalidOperationException($"Excel file not found: {filePath}");
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Error reading keywords from Excel: {ex.Message}", ex);
            }
        }

        // Read Data from Category Excel file
        public static List<Category> ReadCategoryFromExcel(string filePath)
        {
            try
            {
                License();
                var categories = new List<Category>();

                using (var package = new ExcelPackage(new FileInfo(filePath)))
                {
                    var workSheet = package.Workbook.Worksheets[0];
                    int rowCount = workSheet.Dimension.Rows;

                    for (int row = 2; row <= rowCount; row++)
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
            catch (FileNotFoundException)
            {
                throw new InvalidOperationException($"Excel file not found: {filePath}");
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Error reading categories from Excel: {ex.Message}", ex);
            }
        }

        // Read Data from BookRelation Excel file
        public static List<BookRelation> ReadBookRelationFromExcel(string filePath)
        {
            try
            {
                License();
                var bookRelations = new List<BookRelation>();

                using (var package = new ExcelPackage(new FileInfo(filePath)))
                {
                    var workSheet = package.Workbook.Worksheets[0];
                    var rowCount = workSheet.Dimension.Rows;

                    for (int row = 2; row <= rowCount; row++)
                    {
                        var bookId = workSheet.Cells[row, 1].Text;
                        var relationBookId = workSheet.Cells[row, 2].Text;

                        if (!String.IsNullOrWhiteSpace(bookId) && !String.IsNullOrWhiteSpace(relationBookId)
                            && int.TryParse(bookId, out int bookIdValue) && int.TryParse(relationBookId, out int relationBookIdValue))
                        {
                            bookRelations.Add(new BookRelation
                            {
                                BookId = bookIdValue,
                                RelatedBookId = relationBookIdValue
                            });
                        }
                    }
                }
                return bookRelations;
            }
            catch (FileNotFoundException)
            {
                throw new InvalidOperationException($"Excel file not found: {filePath}");
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Error reading book relations from Excel: {ex.Message}", ex);
            }
        }

        // Read Data from BookKeyword Excel file
        public static List<BookKeyword> ReadBookKeywordFromExcel(string filePath)
        {
            try
            {
                License();
                var keywords = new List<BookKeyword>();
                using (var package = new ExcelPackage(new FileInfo(filePath)))
                {
                    var workSheet = package.Workbook.Worksheets[0];
                    var rowCount = workSheet.Dimension.Rows;

                    for (int row = 2; row <= rowCount; row++)
                    {
                        var bookId = workSheet.Cells[row, 1].Text;
                        var keywordId = workSheet.Cells[row, 2].Text;

                        if (!String.IsNullOrWhiteSpace(bookId) && !String.IsNullOrWhiteSpace(keywordId)
                            && int.TryParse(bookId, out int bookIdValue) && int.TryParse(keywordId, out int keywordIdValue))
                        {
                            keywords.Add(new BookKeyword
                            {
                                BookId = bookIdValue,
                                KeywordId = keywordIdValue
                            });
                        }
                    }
                }
                return keywords;
            }
            catch (FileNotFoundException)
            {
                throw new InvalidOperationException($"Excel file not found: {filePath}");
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Error reading book keywords from Excel: {ex.Message}", ex);
            }
        }
    }
}