using Microsoft.EntityFrameworkCore;
using Refhub.Data.Context;
using Refhub.Data.Models;
using Refhub.Models.Keywords;
using Refhub.Resources;
using Refhub.Service.Interface;
using Refhub.Tools.Exceptions;

namespace Refhub.Service.Implement;

public class KeywordService : IKeywordService
{
    private readonly AppDbContext _context;
    private readonly IMessageService _messageService;

    public KeywordService(AppDbContext context, IMessageService messageService)
    {
        _context = context;
        _messageService = messageService;
    }

    public async Task AddKeywordAsync(CreateKeywordVM model, CancellationToken ct)
    {
        var exists = await _context.Keywords
                          .AnyAsync(k => EF.Functions.Collate(k.Word, "SQL_Latin1_General_CP1_CI_AI") == model.Word, ct);
        if (exists)
        {
            throw new DuplicateKeywordException(_messageService.Get("Keyword_AlreadyExists"));
        }

        var keyword = new Keyword
        {
            Word = model.Word,
        };

        await _context.Keywords.AddAsync(keyword, ct);
        await _context.SaveChangesAsync(ct);
    }


    public async Task DeleteAsync(int id, CancellationToken ct)
    {
        var keyword = await _context.Keywords.FindAsync(new object[] { id }, ct);
        if (keyword == null)
        {
            return;
        }

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

    public async Task UpdateAsync(EditKeywordVM vm, CancellationToken ct)
    {
        var keyword = await _context.Keywords.FindAsync(vm.Id, ct);
        if (keyword == null)
        {
            return;
        }

        keyword.Word = vm.Word;
        _context.Keywords.Update(keyword);
        await _context.SaveChangesAsync(ct);
    }
}
