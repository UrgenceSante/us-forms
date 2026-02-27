using Microsoft.AspNetCore.Mvc;
using UsFormAdmin.Application.Submissions.Create;
using UsFormAdmin.Application.Submissions.Queries;

namespace UsFormAdmin.Api.Submissions;

[ApiController]
[Route("forms/{formId:guid}/submissions")]
public class SubmissionsController : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> Create(
        Guid formId,
        [FromBody] CreateSubmissionRequest request,
        [FromServices] CreateSubmissionService service)
    {
        var cmd = new CreateSubmissionCommand(
            formId,
            request.Values.Select(v => new CreateSubmissionValueCommand(v.FieldId, v.Value)).ToList()
        );

        var submissionId = await service.ExecuteAsync(cmd);
        return CreatedAtAction(nameof(GetById), new { formId, submissionId }, null);
    }

    [HttpGet]
    public async Task<IActionResult> List(
        Guid formId,
        [FromServices] ListSubmissionsService service)
    {
        var submissions = await service.ExecuteAsync(formId);
        return Ok(submissions.Select(SubmissionMapping.ToResponse));
    }

    [HttpGet("{submissionId:guid}")]
    public async Task<IActionResult> GetById(
        Guid formId,
        Guid submissionId,
        [FromServices] GetSubmissionService service)
    {
        var submission = await service.ExecuteAsync(submissionId);
        if (submission is null || submission.FormId != formId)
            return NotFound();

        return Ok(SubmissionMapping.ToResponse(submission));
    }
}
