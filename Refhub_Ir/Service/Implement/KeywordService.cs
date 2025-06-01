using Microsoft.EntityFrameworkCore;
using Refhub_Ir.Data.Context;
using Refhub_Ir.Models;
using Refhub_Ir.Models.Keywords;
using Refhub_Ir.Service.Interface;

namespace Refhub_Ir.Service.Implement
{
    public class KeywordService : IKeywordService
    {
        private AppDbContext _context;
        public KeywordService(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddKeywordAsync(CreateKeywordVM model, CancellationToken ct)
        {
            var exists = await _context.Keywords.AnyAsync(k => k.Word.ToLower() == model.Word.ToLower(), ct);
            if (exists)
                throw new Exception("این کلیدواژه قبلاً ثبت شده است.");

            var keyword = new Keyword
            {
                Word = model.Word,
            };

            await _context.Keywords.AddAsync(keyword, ct);
            await _context.SaveChangesAsync(ct);
        }


        public async Task DeleteAsync(int id, CancellationToken ct)
        {
            var keyword = await _context.Keywords.FindAsync(id, ct);
            if (keyword == null) return;

            _context.Keywords.Remove(keyword);
            await _context.SaveChangesAsync(ct);
        }

        public Task<List<KeywordListVM>> GetAllKeywordForListAsync(CancellationToken ct)
        {
            return _context.Keywords.Select(x => new KeywordListVM
            {
                Id = x.Id,
                Word = x.Word,

            }).ToListAsync(ct);
        }

        public async Task<EditKeywordVM> GetForEdit(int id, CancellationToken ct)
        {
            var keyword = await _context.Keywords.FindAsync(id, ct);
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
            var keyword = await _context.Keywords.FindAsync(vm.Id, ct);
            if (keyword == null) return;

            keyword.Word = vm.Word;
            _context.Keywords.Update(keyword);
            await _context.SaveChangesAsync(ct);
        }
    }
}
