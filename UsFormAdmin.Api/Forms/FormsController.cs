using Microsoft.AspNetCore.Mvc;
using UsFormAdmin.Application.Forms;
using UsFormAdmin.Application.Forms.Queries;
using UsFormAdmin.Application.Forms.Delete;
using UsFormAdmin.Application.Form;
using Microsoft.AspNetCore.Http.HttpResults;
using UsFormAdmin.Models;



namespace UsFormAdmin.Api.Forms;

[ApiController]
[Route("forms")]
public class FormsController : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> Create(
        [FromServices] CreateFormService service,
        [FromBody] CreateFormRequest request)
    {
        // Mapping API -> Application command
        var cmd = new CreateFormCommand(
            request.Name,
            request.Fields.Select(f =>
                new CreateTextFieldCommand(f.Label, f.Required, f.FieldType, f.Placeholder)
            ).ToList()
        );

        var id = await service.ExecuteAsync(cmd);

        return CreatedAtAction(nameof(GetById), new { id }, null);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(
        [FromServices] GetFormService service,
        Guid id)
    {
        var form = await service.ExecuteAsync(id);
        if (form is null) return NotFound();

        return Ok(FormMapping.ToResponse(form));
    }

    [HttpGet]
    public async Task<IActionResult> List(
        [FromServices] ListFormsService service)
    {
        var forms = await service.ExecuteAsync();
        return Ok(forms.Select(FormMapping.ToResponse));
    }

    [HttpGet("fieldTypes")]
    public ActionResult<List<EFieldTypes>> GetFieldTypes()
    {
        List<string> fieldTypes = [.. Enum.GetValues(typeof(EFieldTypes)).Cast<EFieldTypes>().Select(f => f.ToString())];
        return Ok(fieldTypes);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(
        [FromServices] DeleteFormService service,
        Guid id)
    {
        var removed = await service.ExecuteAsync(id);
        return removed ? NoContent() : NotFound();
    }
}
