using Microsoft.EntityFrameworkCore;
using Refhub_Ir.Data.Context;
using Refhub_Ir.Models;
using Refhub_Ir.Models.Keywords;
using Refhub_Ir.Service.Interface;

namespace Refhub_Ir.Service.Implement
{
    public class KeywordService : IKeywordService
    {
        private AppDbContext _Context;
        public KeywordService(AppDbContext context)
        {
            _Context = context;
        }

        public async Task AddKeywordAsync(CreateKeywordVM model, CancellationToken ct)
        {
            var keyword = new Keyword
            {
                Word = model.Word,

            };

            await _Context.Keywords.AddAsync(keyword, ct);
            await _Context.SaveChangesAsync(ct);
        }

        public async Task DeleteAsync(int id, CancellationToken ct)
        {
            var keyword = await _Context.Keywords.FindAsync(id, ct);
            if (keyword == null) return;

            _Context.Keywords.Remove(keyword);
            await _Context.SaveChangesAsync(ct);
        }

        public Task<List<KeywordListVM>> GetAllKeywordForListAsync(CancellationToken ct)
        {
            return _Context.Keywords.Select(x => new KeywordListVM
            {
                Id = x.Id,
                Word = x.Word,

            }).ToListAsync(ct);
        }

        public async Task<EditKeywordVM> GetForEdit(int id, CancellationToken ct)
        {
            var keyword = await _Context.Keywords.FindAsync(id, ct);
            if (keyword == null) return null;

            var model = new EditKeywordVM
            {
                Id = keyword.Id,
                Word = keyword.Word
            };

            return model;
        }

        public async Task UpdateAsync(EditKeywordVM vm, CancellationToken ct)
        {
            var keyword = await _Context.Keywords.FindAsync(vm.Id, ct);
            if (keyword == null) return;

            keyword.Word = vm.Word;
            _Context.Keywords.Update(keyword);
            await _Context.SaveChangesAsync(ct);
        }
    }
}
