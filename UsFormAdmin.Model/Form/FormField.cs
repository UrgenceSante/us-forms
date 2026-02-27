using UsFormAdmin.Models;

namespace UsFormAdmin.Model.Form;

public class FormField
{
    public Guid Id { get; set; }
    public string Label { get; set; } = "";
    public bool Required { get; set; }
    public EFieldTypes FieldType { get; set; } = EFieldTypes.Text;
    public string Placeholder { get; set; } = "";


    private FormField() { }

    private FormField(string label, bool required, EFieldTypes fieldType, string placeholder)
    {
        Id = Guid.NewGuid();
        Label = label;
        Required = required;
        FieldType = fieldType;
        Placeholder = placeholder;

    }

    public static FormField CreateText(string label, bool required, EFieldTypes fieldType, string placeholder)
        => new(label, required, fieldType, placeholder);

}