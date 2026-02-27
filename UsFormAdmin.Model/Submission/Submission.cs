namespace UsFormAdmin.Model.Submission;

public class Submission
{
    public Guid Id { get; set; }
    public Guid FormId { get; set; }
    public DateTimeOffset CreatedAt { get; set; }

    private readonly List<SubmissionValue> _values = [];
    public IReadOnlyList<SubmissionValue> Values => _values.AsReadOnly();

    private Submission() { }

    public Submission(Guid formId, IEnumerable<SubmissionValue> values)
    {
        if (formId == Guid.Empty)
            throw new ArgumentException("FormId is required.");

        var list = values?.ToList() ?? throw new ArgumentNullException(nameof(values));
        if (list.Count == 0)
            throw new ArgumentException("A submission must contain at least one value.");

        Id = Guid.NewGuid();
        FormId = formId;
        CreatedAt = DateTimeOffset.UtcNow;

        _values.AddRange(list);
    }
}