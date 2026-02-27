using UsFormAdmin.Application.Form;
using UsFormAdmin.Application.Forms;



namespace UsFormAdmin.Application.Form;


public class CreateFormService
{
    private readonly IFormRepository _repo;

    public CreateFormService(IFormRepository repo)
    {
        _repo = repo;
    }

    public async Task<Guid> ExecuteAsync(CreateFormCommand command)
    {
        var form = new Model.Form.Form(command.Name);

        if (command.Fields is null || command.Fields.Count == 0)
            throw new ArgumentException("A form must contain at least one field.");

        foreach (var f in command.Fields)
            form.AddTextField(f.Label, f.Required, f.FieldType, f.Placeholder);

        await _repo.AddAsync(form);
        return form.Id;
    }
}
