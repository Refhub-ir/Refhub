using Refhub_Ir.Models.Books;

namespace Refhub_Ir.Service.Interface;

public interface IBookService
{
    Task<IEnumerable<CategoryDropDownVM>> GetCategories(int Id, CancellationToken cancellationToken);
    Task<IEnumerable<CategoryDropDownVM>> GetAnothers(List<int> Id);
    Task<bool> CreateAnother(string fullname, string slug);
    Task<IEnumerable<BookVM>> GetBooks(string? searchText);
    Task<UpdateBookVM> GetBookDetialsForUpdate(int Id);
    Task<IEnumerable<BookVM>> GetBook(int Id);
    Task<bool> CreateBook(CreateBookVM book);
    Task<bool> UpdateBook(UpdateBookVM book);
    Task<bool> DeleteBook(int Id);

}
