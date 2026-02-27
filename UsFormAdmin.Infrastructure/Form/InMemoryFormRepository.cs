using System.Collections.Concurrent;
using UsFormAdmin.Application.Form;
using UsFormAdmin.Model.Form;

namespace UsFormAdmin.Infrastructure.Forms;

public class InMemoryFormRepository : IFormRepository
{
    private readonly ConcurrentDictionary<Guid, Form> _store = new();

    public Task AddAsync(Form form)
    {
        _store[form.Id] = form;
        return Task.CompletedTask;
    }

    public Task<Form?> GetByIdAsync(Guid id)
    {
        _store.TryGetValue(id, out var form);
        return Task.FromResult(form);
    }

    public Task<IReadOnlyList<Form>> ListAsync()
    {
        IReadOnlyList<Form> result = _store.Values.ToList();
        return Task.FromResult(result);
    }

    public Task<bool> DeleteAsync(Guid id)
    {
        var removed = _store.TryRemove(id, out _);
        return Task.FromResult(removed);
    }
}
