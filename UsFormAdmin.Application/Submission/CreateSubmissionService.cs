using UsFormAdmin.Application.Form;
using UsFormAdmin.Model.Submission;



namespace UsFormAdmin.Application.Submissions.Create;

public class CreateSubmissionService
{
    private readonly IFormRepository _forms;
    private readonly ISubmissionRepository _submissions;

    public CreateSubmissionService(IFormRepository forms, ISubmissionRepository submissions)
    {
        _forms = forms;
        _submissions = submissions;
    }

    public async Task<Guid> ExecuteAsync(CreateSubmissionCommand command)
    {
        var form = await _forms.GetByIdAsync(command.FormId);
        if (form is null)
            throw new KeyNotFoundException("Form not found.");

        if (command.Values is null || command.Values.Count == 0)
            throw new ArgumentException("Submission values are required.");

        // 1) Check fields exist + required fields are present
        var formFieldIds = form.Fields.Select(f => f.Id).ToHashSet();
        foreach (var v in command.Values)
        {
            if (!formFieldIds.Contains(v.FieldId))
                throw new ArgumentException($"Unknown fieldId: {v.FieldId}");
        }

        foreach (var requiredField in form.Fields.Where(f => f.Required))
        {
            var hasValue = command.Values.Any(v =>
                v.FieldId == requiredField.Id &&
                !string.IsNullOrWhiteSpace(v.Value)
            );

            if (!hasValue)
                throw new ArgumentException($"Missing required field: {requiredField.Label}");
        }

        // 2) Build submission
        var values = command.Values.Select(v =>
            new SubmissionValue(v.FieldId, v.Value ?? "")
        );

        var submission = new Submission(command.FormId, values);

        await _submissions.AddAsync(submission);
        return submission.Id;
    }
}
