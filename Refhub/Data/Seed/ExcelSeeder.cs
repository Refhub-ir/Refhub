using Microsoft.CodeAnalysis.CSharp.Syntax;
using Refhub.Data.Models;
using OfficeOpenXml;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;

namespace Refhub.Data.Seed
{
    public static class ExcelSeeder
    {
        // Read Data from Author File
        public static List<Author> ReadAuthorsFromExcel(string filePath)
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            var authors = new List<Author>();

            using (var package = new ExcelPackage(new FileInfo(filePath)))
            {
                var workSheet = package.Workbook.workSheets[0];
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
            ExcelPackage.LicenseContext= LicenseContext.NonCommercial;
            var books = new List<Book>();
            using (var package = new ExcelPackage(new FileInfo(filePath)))
            {
                var workSheet = package.Workbook.Worksheets[0];
                int rowCount = workSheet.Dimension.Rows;

                for(int row = 2; row <= rowCount; row++)
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
    }
}
