namespace UsFormAdmin.Api.Submissions;

public class CreateSubmissionRequest
{
    public List<CreateSubmissionValueRequest> Values { get; set; } = new();
}

public class CreateSubmissionValueRequest
{
    public Guid FieldId { get; set; }
    public string? Value { get; set; }
}
