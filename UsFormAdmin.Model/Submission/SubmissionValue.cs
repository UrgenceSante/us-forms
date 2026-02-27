namespace UsFormAdmin.Model.Submission;

public class SubmissionValue
{
    public Guid FieldId { get; private set; }
    public string Value { get; private set; } = string.Empty;

    private SubmissionValue() { }

    public SubmissionValue(Guid fieldId, string value)
    {
        if (fieldId == Guid.Empty)
            throw new ArgumentException("FieldId is required.");

        Value = value ?? ""; // on autorise vide, la validation "required" est au niveau Form
        FieldId = fieldId;
    }
}