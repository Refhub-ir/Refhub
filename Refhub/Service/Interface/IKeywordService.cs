using Refhub_Ir.Data.Context;
using Refhub_Ir.Models;
using Refhub_Ir.Models.Keywords;

namespace Refhub_Ir.Service.Interface
{
    public interface IKeywordService
    {
        Task<List<KeywordListVM>> GetAllKeywordForListAsync(CancellationToken ct);
        Task AddKeywordAsync(CreateKeywordVM model, CancellationToken ct);
        Task<EditKeywordVM> GetForEdit(int id, CancellationToken ct);
        Task UpdateAsync(EditKeywordVM vm, CancellationToken ct);
        Task DeleteAsync(int id, CancellationToken ct);
    }
}
