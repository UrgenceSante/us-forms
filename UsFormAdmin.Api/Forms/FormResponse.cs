using UsFormAdmin.Model.Form;
using UsFormAdmin.Models;

namespace UsFormAdmin.Api.Forms;

public record FormResponse(
    Guid Id,
    string Name,
    List<FormFieldResponse> Fields
);

public record FormFieldResponse(
    Guid Id,
    string Label,
    EFieldTypes FieldType,
    string Placeholder
);

public static class FormMapping
{
    public static FormResponse ToResponse(Form form) =>
        new(
            form.Id,
            form.Name,
            [.. form.Fields.Select(f =>
                new FormFieldResponse(
                    f.Id,
                    f.Label,
                    f.FieldType,
                    f.Placeholder
                )
            )]
        );
}
