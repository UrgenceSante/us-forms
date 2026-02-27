namespace UsFormAdmin.Application.Submissions.Create;

public record CreateSubmissionCommand(
    Guid FormId,
    List<CreateSubmissionValueCommand> Values
);

public record CreateSubmissionValueCommand(
    Guid FieldId,
    string? Value
);
