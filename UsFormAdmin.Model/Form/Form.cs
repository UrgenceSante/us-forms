using UsFormAdmin.Models;

namespace UsFormAdmin.Model.Form;

public class Form
{
    public Guid Id { get; set; }
    public string Name { get; set; } = "";
    private readonly List<FormField> _fields = [];
    public IReadOnlyList<FormField> Fields => _fields.AsReadOnly();


    public Form(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Form name is required.");


        Id = Guid.NewGuid();
        Name = name.Trim();
    }

    public void AddTextField(string label, bool required, EFieldTypes fieldType, string placeholder)
    {
        if (string.IsNullOrWhiteSpace(label))
            throw new ArgumentException("Field label is required.");

        _fields.Add(FormField.CreateText(label.Trim(), required, fieldType, placeholder));

    }
}