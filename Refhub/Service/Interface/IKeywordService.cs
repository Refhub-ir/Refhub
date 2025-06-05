using Refhub_Ir.Models.Keywords;


namespace Refhub_Ir.Service.Interface;


public interface IKeywordService
{
    Task<List<KeywordListVM>> GetAllKeywordForListAsync();
    Task AddKeywordAsync(CreateKeywordVM model);
    Task<EditKeywordVM> GetForEdit(int id);
    Task UpdateAsync(EditKeywordVM vm);
    Task DeleteAsync(int id);
}
