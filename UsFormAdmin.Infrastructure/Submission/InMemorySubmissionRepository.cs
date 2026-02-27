using System.Collections.Concurrent;
using UsFormAdmin.Application.Submissions;
using UsFormAdmin.Model.Submission;


namespace UsFormAdmin.Infrastructure.Submissions;

public class InMemorySubmissionRepository : ISubmissionRepository
{
    private readonly ConcurrentDictionary<Guid, Submission> _byId = new();
    private readonly ConcurrentDictionary<Guid, ConcurrentBag<Guid>> _indexByFormId = new();

    public Task AddAsync(Submission submission)
    {
        if (submission.Id == Guid.Empty)
            throw new InvalidOperationException("Submission Id must not be empty.");

        _byId[submission.Id] = submission;

        var bag = _indexByFormId.GetOrAdd(submission.FormId, _ => new ConcurrentBag<Guid>());
        bag.Add(submission.Id);

        return Task.CompletedTask;
    }

    public Task<Submission?> GetByIdAsync(Guid submissionId)
    {
        _byId.TryGetValue(submissionId, out var submission);
        return Task.FromResult(submission);
    }

    public Task<IReadOnlyList<Submission>> ListByFormIdAsync(Guid formId)
    {
        if (!_indexByFormId.TryGetValue(formId, out var ids))
            return Task.FromResult<IReadOnlyList<Submission>>([]);

        var list = ids
            .Select(id => _byId.TryGetValue(id, out var s) ? s : null)
            .Where(s => s is not null)
            .Cast<Submission>()
            .OrderByDescending(s => s.CreatedAt)
            .ToList();

        return Task.FromResult<IReadOnlyList<Submission>>(list);
    }
}
