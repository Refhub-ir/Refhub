using Refhub_Ir.Models.Books;

namespace Refhub_Ir.Service.Interface
{
    public interface IBookService
    {
        Task<IEnumerable<CategoryDropDownVM>> GetCategoriesAsync(int Id, CancellationToken ct);
        Task<IEnumerable<CategoryDropDownVM>> GetAnotherAsync(List<int> Id, CancellationToken ct);
        Task<bool> CreateAnotherAsync(string fullname, string slug, CancellationToken ct);
        Task<IEnumerable<BookVM>> GetBooksAsync(string? searchText, CancellationToken ct);
        Task<UpdateBookVM> GetBookDetialsForUpdateAsync(int Id, CancellationToken ct);
        Task<bool> CreateBookAsync(CreateBookVM book, CancellationToken ct);
        Task<bool> UpdateBookAsync(UpdateBookVM book, CancellationToken ct);
        Task<bool> DeleteBookAsync(int Id, CancellationToken ct);


        Task<ListBooksVM> GetListAsync(string searchText, string authorFilter, string categoryFilter, int pageSize, int page, CancellationToken ct);
        Task<BookDetailsVM> GetBookDetailsBySlugAsync(string slug, CancellationToken ct);
    }
}
