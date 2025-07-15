using Microsoft.EntityFrameworkCore;
using Refhub.Data.Context;
using Refhub.Data.Models;
using Refhub.Models.Authors;
using Refhub.Models.Books;
using Refhub.Models.Category;
using Refhub.Models.DTO;
using Refhub.Models.Enums;
using Refhub.Service.Interface;
using Refhub.Tools.Static;

namespace Refhub.Service.Implement;

public class BookService(AppDbContext context, IFileUploaderService uploaderService) : IBookService
{
    public async Task<IEnumerable<CategoryDropDownVM>> GetCategoriesAsync(int selectedCategoryId, CancellationToken ct)
    {
        var categories = await context.Categories
            .Select(a => new CategoryDropDownVM
            {
                Id = a.Id,
                CategoryName = a.Name,
                IsSelected = a.Id == selectedCategoryId
            })
            .ToListAsync(ct);

        return categories;
    }

    public async Task<IEnumerable<CategoryDropDownVM>> GetAnothersAsync(List<int> selectedAuthorIds, CancellationToken ct)
    {
        // بررسی ورودی
        if (selectedAuthorIds == null || !selectedAuthorIds.Any())
        {
            selectedAuthorIds = [];
        }

        //todo
        var authors = await context.Authors
            .Select(a => new CategoryDropDownVM
            {
                Id = a.Id,
                CategoryName = a.FullName,
                IsSelected = selectedAuthorIds.Contains(a.Id)
            })
            .ToListAsync(ct);
        return authors;
    }

    public async Task<IEnumerable<BookItemVM>> GetLastBooksAsync(CancellationToken ct)
    {
        var books = context.Books
            .Include(a => a.Category)
            .OrderByDescending(_ => _.Id)
            .Take(8).AsQueryable();


        var result = await books.Select(a => new BookItemVM()
        {
            Id = a.Id,
            Title = a.Title,
            //UserId = a.UserId,
            ImagePath = a.ImagePath,
            CategoryName = a.Category.Name,
            Authores = string.Join(',', a.BookAuthors.Select(_ => _.Author.FullName)),
            Slug = a.Slug
        }).ToListAsync(ct);

        return result;
    }

    public async Task<IEnumerable<BookItemVM>> GetBestBooksAsync(CancellationToken ct)
    {
        var books = context.Books
            .Include(a => a.Category)
            //order by view todo
            .Take(8).AsQueryable();


        var result = await books.Select(a => new BookItemVM()
        {
            Id = a.Id,
            Title = a.Title,
            //UserId = a.UserId,
            ImagePath = a.ImagePath,
            CategoryName = a.Category.Name,
            Authores = string.Join(',', a.BookAuthors.Select(_ => _.Author.FullName)),
            Slug = a.Slug
        }).ToListAsync(ct);

        return result;
    }

    public async Task<bool> CreateAnotherAsync(string fullname, string slug, CancellationToken ct)
    {


        var author = new Author { FullName = fullname, Slug = slug };
        await context.Authors.AddAsync(author, ct);
        await context.SaveChangesAsync(ct);
        return true;
    }

    public async Task<IEnumerable<BookVM>> GetBooksAsync(string? searchText, CancellationToken ct)
    {
        var books = context.Books.AsQueryable();

        if (!string.IsNullOrWhiteSpace(searchText))
        {
            books = books.Where(a => EF.Functions.Like(a.Title, $"%{searchText}%"));   // for best Translate in Sql
        }

        var result = await books.Select(a => new BookVM
        {
            Id = a.Id,
            Title = a.Title,
            ImagePath = a.ImagePath,
            Slug = a.Slug
        }).ToListAsync(ct);

        return result;
    }

    public async Task<UpdateBookVM> GetBookDetailsForUpdateAsync(int bookId, CancellationToken ct)
    {
        var book = await context.Books
            .Include(b => b.BookAuthors)
            .FirstOrDefaultAsync(b => b.Id == bookId, ct);

        return book == null
            ? null
            : new UpdateBookVM
            {
                Slug = book.Slug,
                Title = book.Title,
                CategoryId = book.CategoryId,
                FilePath = book.FilePath,
                ImagePath = book.ImagePath,
                PageCount = book.PageCount,
                AnotherId = book.BookAuthors.Select(a => a.AuthorId).ToList()
            };
    }



    public async Task<bool> CreateBookAsync(CreateBookVM book, CancellationToken ct)
    {
        try
        {
            if (await context.Books.AnyAsync(a => a.Slug.Equals(book.Slug), ct))
                return false;
            var bookAuthors = book.AnotherId.Select(a => new BookAuthor
            {
                AuthorId = a
            }).ToList();

            var filePath = await uploaderService.UploadFile(book.File, BucketNameStatic.GetName(BucketNames.BookPdf), book.Slug);
            var imagePath = await uploaderService.UploadFile(book.Image, BucketNameStatic.GetName(BucketNames.BookImages), book.Slug);

            if (string.IsNullOrWhiteSpace(filePath) || string.IsNullOrWhiteSpace(imagePath))
            {
                return false;
            }

            var newBook = new Book
            {
                CategoryId = book.CategoryId,
                Slug = book.Slug,
                PageCount = book.PageCount,
                FilePath = filePath,
                ImagePath = imagePath,
                Title = book.Title,
                BookAuthors = bookAuthors
            };

            await context.Books.AddAsync(newBook, ct);
            await context.SaveChangesAsync(ct);

            return true;
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            return false;
        }

    }

    public async Task<bool> UpdateBookAsync(UpdateBookVM book, CancellationToken ct)
    {
        try
        {
            var existingBook = await context.Books
                .Include(b => b.BookAuthors)
                .FirstOrDefaultAsync(b => b.Id == book.Id, ct);

            if (existingBook == null)
            {
                return false;
            }

            existingBook.CategoryId = book.CategoryId;
            existingBook.Slug = book.Slug;
            existingBook.PageCount = book.PageCount;
            existingBook.Title = book.Title;
            existingBook.UserId = book.UserId;

            if (book.File != null)
            {
                if (!string.IsNullOrWhiteSpace(existingBook.FilePath))
                {
                    await uploaderService.DeleteFile(existingBook.FilePath, BucketNameStatic.GetName(BucketNames.BookPdf));
                }

                existingBook.FilePath = await uploaderService.UploadFile(book.File, BucketNameStatic.GetName(BucketNames.BookPdf), book.Slug);
            }

            if (book.Image != null)
            {
                if (!string.IsNullOrWhiteSpace(existingBook.ImagePath))
                {
                    await uploaderService.DeleteFile(existingBook.ImagePath, BucketNameStatic.GetName(BucketNames.BookImages));
                }

                existingBook.ImagePath = await uploaderService.UploadFile(book.Image, BucketNameStatic.GetName(BucketNames.BookImages), book.Slug);
            }

            // حذف نویسنده‌های قبلی و اضافه کردن جدید
            context.BookAuthors.RemoveRange(context.BookAuthors.Where(a => a.BookId == book.Id));

            var bookAuthors = book.AnotherId.Select(authorId => new BookAuthor
            {
                AuthorId = authorId,
                BookId = book.Id
            }).ToList();

            existingBook.BookAuthors = bookAuthors;

            context.Books.Update(existingBook);
            await context.SaveChangesAsync(ct);

            return true;
        }
        catch (Exception e)
        {
            Console.WriteLine(e); // بهتره از ILogger استفاده کنی
            return false;
        }
    }


    public async Task<bool> DeleteBookAsync(int bookId, CancellationToken ct)
    {
        try
        {
            var book = await context.Books
                                    .Include(b => b.BookAuthors)
                                    .FirstOrDefaultAsync(b => b.Id == bookId, ct);
            if (book == null)
            {
                return false;
            }

            if (!string.IsNullOrWhiteSpace(book.ImagePath))
            {
                await uploaderService.DeleteFile(book.ImagePath, BucketNameStatic.GetName(BucketNames.BookImages));
            }

            if (!string.IsNullOrWhiteSpace(book.FilePath))
            {
                await uploaderService.DeleteFile(book.FilePath, BucketNameStatic.GetName(BucketNames.BookPdf));
            }

            var relations = await context.BookRelations
                                           .Where(br => br.BookId == bookId || br.RelatedBookId == bookId)
                                           .ToListAsync(ct);
            if (relations.Any())
                context.BookRelations.RemoveRange(relations);

            context.Books.Remove(book);
            await context.SaveChangesAsync(ct);
            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return false;
        }
    }


    public async Task<BookDetailsVM> GetBookDetailsBySlugAsync(string slug, CancellationToken ct)
    {
        var book = await context.Books
            .Include(b => b.BookAuthors).ThenInclude(ba => ba.Author)
            .Include(b => b.BookKeywords).ThenInclude(bk => bk.Keyword)
            .Include(b => b.RelatedTo).ThenInclude(r => r.RelatedBook)
            .Include(b => b.RelatedFrom).ThenInclude(r => r.RelatedBook)
            .FirstOrDefaultAsync(b => b.Slug == slug, ct);

        return book == null
            ? null
            : new BookDetailsVM
            {
                Title = book.Title,
                Slug = book.Slug,
                FilePath = book.FilePath,
                ImagePath = book.ImagePath,

                BookAuthorsVM = book.BookAuthors
                .Where(ba => ba.Author != null)
                .Select(ba => new BookAuthorVM
                {
                    BookId = ba.BookId,
                    AuthorId = ba.AuthorId,
                    Author = new AuthorDTO
                    {
                        Id = ba.Author.Id,
                        FullName = ba.Author.FullName
                    }
                }).ToList(),

                BookKeywordsVM = book.BookKeywords
                .Where(bk => bk.Keyword != null)
                .Select(bk => new BookKeywordVM
                {
                    BookId = bk.BookId,
                    KeywordId = bk.KeywordId,
                    Keyword = new KeywordDTO
                    {
                        Id = bk.Keyword.Id,
                        Word = bk.Keyword.Word
                    }
                }).ToList(),

                RelatedToVM = book.RelatedTo
                .Where(r => r.RelatedBook != null)
                .Select(r => new BookRelationVM
                {
                    BookId = r.BookId,
                    RelatedBookId = r.RelatedBookId,
                    RelatedBook = new BookVM
                    {
                        Id = r.RelatedBook.Id,
                        Title = r.RelatedBook.Title,
                        Slug = r.RelatedBook.Slug
                    }
                }).ToList(),

                RelatedFromVM = book.RelatedFrom
                .Where(r => r.RelatedBook != null)
                .Select(r => new BookRelationVM
                {
                    BookId = r.BookId,
                    RelatedBookId = r.RelatedBookId,
                    RelatedBook = new BookVM
                    {
                        Id = r.RelatedBook.Id,
                        Title = r.RelatedBook.Title,
                        Slug = r.RelatedBook.Slug
                    }
                }).ToList()
            };
    }


    public async Task<ListBooksVM> GetListAsync(string searchText, string authorFilter, string categoryFilter, int pageSize, int page, CancellationToken ct)
    {
        var booksQuery = context.Books
                                .Include(b => b.Category)
                                .Include(x => x.BookAuthors)
                                .ThenInclude(x => x.Author)
                                .AsQueryable();

        if (!string.IsNullOrEmpty(searchText))
        {
            var normalizedSearch = searchText.Trim();
            booksQuery = booksQuery.Where(b =>
                EF.Functions.Like(b.Title, $"%{normalizedSearch}%"));
        }

        if (!string.IsNullOrEmpty(authorFilter))
        {
            var normalizedAuthor = authorFilter.Trim();
            booksQuery = booksQuery.Where(b =>
                b.BookAuthors.Any(ba =>
                    EF.Functions.Like(ba.Author.FullName, $"%{normalizedAuthor}%")));
        }

        if (!string.IsNullOrEmpty(categoryFilter))
        {
            var normalizedCategory = categoryFilter.Trim();
            booksQuery = booksQuery.Where(b =>
                EF.Functions.Like(b.Category.Name, $"%{normalizedCategory}%"));
        }


        var totalItems = await booksQuery.CountAsync();
        var totalPages = (int)Math.Ceiling((double)totalItems / pageSize);
        page = Math.Max(1, Math.Min(page, Math.Max(1, totalPages)));

        var books = await booksQuery
                          .OrderBy(x => x.Title)
                          .Skip((page - 1) * pageSize)
                          .Take(pageSize)
                          .Select(a => new BookVM
                          {
                              Id = a.Id,
                              Title = a.Title,
                              Slug = a.Slug,
                              ImagePath = a.ImagePath,
                              UserId = a.UserId
                          })
                          .ToListAsync(cancellationToken: ct);

        var authors = await context.Authors
             .OrderBy(a => a.FullName)
             .Select(a => new AuthorVM
             {
                 FullName = a.FullName,
                 IsSelected = !string.IsNullOrWhiteSpace(authorFilter) &&
                              a.FullName.Contains(authorFilter.Trim(), StringComparison.OrdinalIgnoreCase)
             }).ToListAsync(ct);



        var categories = await context.Categories
            .OrderBy(c => c.Name)
            .Select(c => new CategoryVM
            {
                Name = c.Name,
                IsSelected = !string.IsNullOrWhiteSpace(categoryFilter) &&
                  c.Name.Contains(categoryFilter.Trim(), StringComparison.OrdinalIgnoreCase)
            }).ToListAsync(ct);


        return new ListBooksVM
        {
            Books = books,
            Categories = categories,
            Authors = authors,
            CurrentPage = page,
            TotalPages = totalPages,
            SearchText = searchText,
            AuthorFilter = authorFilter,
            CategoryFilter = categoryFilter
        };
    }
}
