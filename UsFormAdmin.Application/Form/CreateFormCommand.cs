using UsFormAdmin.Models;

namespace UsFormAdmin.Application.Forms;

public record CreateFormCommand(
    string Name,
    List<CreateTextFieldCommand> Fields
);

public record CreateTextFieldCommand(
    string Label,
    bool Required,
    EFieldTypes FieldType,
    string Placeholder
);