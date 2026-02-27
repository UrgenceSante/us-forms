using UsFormAdmin.Model.Submission;

namespace UsFormAdmin.Api.Submissions;

public record SubmissionResponse(
    Guid Id,
    Guid FormId,
    DateTimeOffset CreatedAt,
    List<SubmissionValueResponse> Values
);

public record SubmissionValueResponse(
    Guid FieldId,
    string Value
);

public static class SubmissionMapping
{
    public static SubmissionResponse ToResponse(Submission s) =>
        new(
            s.Id,
            s.FormId,
            s.CreatedAt,
            s.Values.Select(v => new SubmissionValueResponse(v.FieldId, v.Value)).ToList()
        );
}
