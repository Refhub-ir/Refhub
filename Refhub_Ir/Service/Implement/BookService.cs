using Microsoft.EntityFrameworkCore;
using Refhub_Ir.Data.Context;
using Refhub_Ir.Models.Books;
using Refhub_Ir.Service.Interface;
using Refhub_Ir.Tools.Static;

namespace Refhub_Ir.Service.Implement;

public class BookService(AppDbContext context, IFileUploaderService uploaderService) : IBookService
{
    public async Task<IEnumerable<CategoryDropDownVM>> GetCategories(int Id)
    {
        var category = context.Categories.AsQueryable();

        return category.Select(a => new CategoryDropDownVM()
        {
            Id = a.Id,
            Name = a.Name,
            IsSelected = a.Id.Equals(Id)

        }).ToList();
    }

    public async Task<IEnumerable<CategoryDropDownVM>> GetAnothers(List<int> Ids)
    {
        // بررسی ورودی
        if (Ids == null || !Ids.Any())
        {
            Ids = [];
        }

        //todo
        var anothers = context.Authors.AsQueryable();

        return anothers.Select(a => new CategoryDropDownVM()
        {
            Id = a.Id,
            Name = a.FullName,
            IsSelected = Ids.Contains(a.Id),

        }).ToList();
    }

    public async Task<bool> CreateAnother(string fullname, string slug)
    {
        context.Authors.Add(new Author() { Slug = slug, FullName = fullname });
        context.SaveChanges();
        return true;
    }

    public async Task<IEnumerable<BookVM>> GetBooks(string? searchText)
    {
        var books = context.Books.AsQueryable();

        if (!string.IsNullOrEmpty(searchText))
        {
            books = books.Where(a => a.Title.Contains(searchText));
        }

        return await books
           .Select(a => new BookVM()
           {
               Id = a.Id,
               Title = a.Title,
               UserId = a.UserId,
               ImagePath = a.ImagePath,
               Slug = a.Slug,

           }).ToListAsync();
    }

    public async Task<UpdateBookVM> GetBookDetialsForUpdate(int Id)
    {
        UpdateBookVM model = new();
        var book = context.Books.Include(a => a.BookAuthors).FirstOrDefault(a => a.Id.Equals(Id));
        if (book != null)
        {
            model.Slug = book.Slug;
            model.Title = book.Title;
            model.CategoryId = book.CategoryId;
            model.FilePath = book.FilePath;
            model.ImagePath = book.ImagePath;
            model.PageCount = book.PageCount;
            model.UserId = book.UserId;
            model.AnotherId = book.BookAuthors.Select(a => a.AuthorId).ToList();
        }

        return model;
    }

    public Task<IEnumerable<BookVM>> GetBook(int Id)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> CreateBook(CreateBookVM book)
    {
        try
        {
            var BookAuthors = book.AnotherId.Select(a => new BookAuthor()
            {
                AuthorId = a
            });
            var _book = new Book()
            {
                CategoryId = book.CategoryId,
                Slug = book.Slug,

                PageCount = book.PageCount,
                FilePath = await uploaderService.UpdloadFile(book.File, FolderNameStatic.GetDirectoryBooks, FolderNameStatic.GetDirectoryImages, book.Slug),
                ImagePath = await uploaderService.UpdloadFile(book.Image, FolderNameStatic.GetDirectoryBooks, FolderNameStatic.GetDirectoryImages, book.Slug),

                Title = book.Title,
                UserId = book.UserId,
                BookAuthors = BookAuthors.ToList()
            };
            context.Books.Add(_book);
            context.SaveChanges();
            return true;
        }
        catch (Exception e)
        {
            return false;
        }

    }

    public async Task<bool> UpdateBook(UpdateBookVM book)
    {
        try
        {
            var _book = context.Books.FirstOrDefault(a => a.Id.Equals(book.Id));

            _book.CategoryId = book.CategoryId;
            _book.Slug = book.Slug;

            _book.PageCount = book.PageCount;
            if (book.File != null)
            {

                await uploaderService.DeleteFile(FolderNameStatic.GetDirectoryBooks,
                    FolderNameStatic.GetDirectoryImages, _book.FilePath);
                _book.FilePath = await uploaderService.UpdloadFile(book.File, FolderNameStatic.GetDirectoryBooks,
                    FolderNameStatic.GetDirectoryImages, book.Slug);

            }
            if (book.Image != null)
            {
                await uploaderService.DeleteFile(FolderNameStatic.GetDirectoryBooks,
                    FolderNameStatic.GetDirectoryImages, _book.ImagePath);
                _book.ImagePath = await uploaderService.UpdloadFile(book.Image, FolderNameStatic.GetDirectoryBooks,
                    FolderNameStatic.GetDirectoryImages, book.Slug);

            }
            _book.Title = book.Title;
            _book.UserId = book.UserId;
            context.BookAuthors.RemoveRange(context.BookAuthors.Where(a => a.BookId.Equals(book.Id)));
            context.SaveChanges();
            var BookAuthors = book.AnotherId.Select(a => new BookAuthor()
            {
                AuthorId = a
            });

            _book.BookAuthors = BookAuthors.ToList();

            context.Books.Update(_book);
            context.SaveChanges();
            return true;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return false;
        }

    }

    public async Task<bool> DeleteBook(int Id)
    {
        var book = context.Books.FirstOrDefault(a => a.Id.Equals(Id));
        if (book != null)
        {
            await uploaderService.DeleteFile(FolderNameStatic.GetDirectoryBooks,
                FolderNameStatic.GetDirectoryImages, book.ImagePath);
            await uploaderService.DeleteFile(FolderNameStatic.GetDirectoryBooks,
                FolderNameStatic.GetDirectoryImages, book.FilePath);
            context.Books.Remove(book);
            context.SaveChanges();
            return true;
        }

        return false;
    }
}
