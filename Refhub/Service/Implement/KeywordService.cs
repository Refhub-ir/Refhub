using Microsoft.EntityFrameworkCore;
using Refhub_Ir.Data.Context;
using Refhub_Ir.Models.Keywords;
using Refhub_Ir.Service.Interface;

namespace Refhub_Ir.Service.Implement;

public class KeywordService : IKeywordService
{
    private readonly AppDbContext _Context;
    public KeywordService(AppDbContext context)
    {
        _Context = context;
    }

    public async Task AddKeywordAsync(CreateKeywordVM model)
    {
        var keyword = new Keyword
        {
            Word = model.Word,

        };

        _Context.Keywords.Add(keyword);
        _Context.SaveChanges();
    }

    public async Task DeleteAsync(int id)
    {
        var keyword = await _Context.Keywords.FindAsync(id);
        if (keyword == null)
        {
            return;
        }

        _Context.Keywords.Remove(keyword);
        await _Context.SaveChangesAsync();
    }

    public Task<List<KeywordListVM>> GetAllKeywordForListAsync()
    {
        return _Context.Keywords.Select(x => new KeywordListVM
        {
            Id = x.Id,
            Word = x.Word,

        }).ToListAsync();
    }

    public async Task<EditKeywordVM> GetForEdit(int id)
    {
        var keyword = _Context.Keywords.Find(id);
        if (keyword == null)
        {
            return null;
        }

        var model = new EditKeywordVM
        {
            Id = keyword.Id,
            Word = keyword.Word
        };

        return model;
    }

    public async Task UpdateAsync(EditKeywordVM vm)
    {
        var keyword = await _Context.Keywords.FindAsync(vm.Id);
        if (keyword == null)
        {
            return;
        }

        keyword.Word = vm.Word;
        _Context.Keywords.Update(keyword);
        await _Context.SaveChangesAsync();
    }
}
