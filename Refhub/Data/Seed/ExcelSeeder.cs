using Microsoft.CodeAnalysis.CSharp.Syntax;
using Refhub.Data.Models;
using OfficeOpenXml;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;

namespace Refhub.Data.Seed
{
    public static class ExcelSeeder
    {
        public static List<Author> ReadAuthorsFromExcel(string filePath)
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            var authors = new List<Author>();

            using (var package = new ExcelPackage(new FileInfo(filePath)))
            {
                var worksheet = package.Workbook.Worksheets[0];
                int rowCount = worksheet.Dimension.Rows;

                for (int row = 2; row <= rowCount; row++)
                {
                    var fullName = worksheet.Cells[row, 1].Text;
                    var slug = worksheet.Cells[row, 2].Text;

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
    }
}
