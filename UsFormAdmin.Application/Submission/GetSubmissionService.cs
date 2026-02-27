using UsFormAdmin.Application.Submissions;
using UsFormAdmin.Model.Submission;


namespace UsFormAdmin.Application.Submissions.Queries;

public class GetSubmissionService
{
    private readonly ISubmissionRepository _repo;

    public GetSubmissionService(ISubmissionRepository repo)
    {
        _repo = repo;
    }

    public Task<Submission?> ExecuteAsync(Guid submissionId)
        => _repo.GetByIdAsync(submissionId);
}
