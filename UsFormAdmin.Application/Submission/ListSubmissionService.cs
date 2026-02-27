using UsFormAdmin.Application.Submissions;
using UsFormAdmin.Model.Submission;


namespace UsFormAdmin.Application.Submissions.Queries;

public class ListSubmissionsService
{
    private readonly ISubmissionRepository _repo;

    public ListSubmissionsService(ISubmissionRepository repo)
    {
        _repo = repo;
    }

    public Task<IReadOnlyList<Submission>> ExecuteAsync(Guid formId)
        => _repo.ListByFormIdAsync(formId);
}
