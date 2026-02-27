using UsFormAdmin.Models;

namespace UsFormAdmin.Api.Forms;

public class CreateFormRequest
{
    public string Name { get; set; } = default!;
    public List<CreateTextFieldRequest> Fields { get; set; } = [];
}



public class CreateTextFieldRequest
{
    public string Label { get; set; } = default!;
    public bool Required { get; set; } = false;
    public EFieldTypes FieldType { get; set; } = EFieldTypes.Text;
    public string Placeholder { get; set; } = "";

}
