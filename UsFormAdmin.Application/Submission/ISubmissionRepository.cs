using UsFormAdmin.Model.Submission;

namespace UsFormAdmin.Application.Submissions;

public interface ISubmissionRepository
{
    Task AddAsync(Submission submission);
    Task<Submission?> GetByIdAsync(Guid submissionId);
    Task<IReadOnlyList<Submission>> ListByFormIdAsync(Guid formId);
}