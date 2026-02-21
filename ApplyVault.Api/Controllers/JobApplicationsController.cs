using ApplyVault.Application.Commands.JobApplications;
using ApplyVault.Application.Dtos.JobApplications;
using ApplyVault.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ApplyVault.Api.Controllers;

[ApiController]
[Route("api/job-applications")]
public sealed class JobApplicationsController : ControllerBase
{
    private readonly IJobApplicationService _service;
    private readonly ILogger<JobApplicationsController> _logger;

    public JobApplicationsController(IJobApplicationService service, ILogger<JobApplicationsController> logger)
    {
        _service = service;
        _logger = logger;
    }

    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<JobApplicationResponseDto>>> GetAll(CancellationToken ct)
    {
        var items = await _service.GetAllAsync(ct);
        return Ok(items);
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<JobApplicationResponseDto>> GetById(Guid id, CancellationToken ct)
    {
        var item = await _service.GetByIdAsync(id, ct);
        return item is null ? NotFound() : Ok(item);
    }

    [HttpPost]
    public async Task<ActionResult<JobApplicationResponseDto>> Create([FromBody] CreateJobApplicationRequestDto dto, CancellationToken ct)
    {
        try
        {
            var command = new CreateJobApplicationCommand(
                dto.Company,
                dto.Role,
                dto.Location,
                dto.IsRemote,
                dto.Referral,
                dto.ContactPerson,
                dto.DateApplied,
                dto.Status,
                dto.CompensationRange,
                dto.LastTouch,
                dto.NextAction,
                dto.NextActionDate,
                dto.Notes,
                dto.Link
            );

            var created = await _service.CreateAsync(command, ct);

            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }
        catch (ArgumentException ex)
        {
            _logger.LogWarning(ex, "Create job application validation failed.");
            return BadRequest(ex.Message);
        }
    }

    [HttpPut("{id:guid}")]
    public async Task<ActionResult<JobApplicationResponseDto>> Update(Guid id, [FromBody] UpdateJobApplicationRequestDto dto, CancellationToken ct)
    {
        try
        {
            var command = new UpdateJobApplicationCommand(
                id,
                dto.Company,
                dto.Role,
                dto.Location,
                dto.IsRemote,
                dto.Referral,
                dto.ContactPerson,
                dto.DateApplied,
                dto.Status,
                dto.CompensationRange,
                dto.LastTouch,
                dto.NextAction,
                dto.NextActionDate,
                dto.Notes,
                dto.Link
            );

            var updated = await _service.UpdateAsync(command, ct);
            return updated is null ? NotFound() : Ok(updated);
        }
        catch (ArgumentException ex)
        {
            _logger.LogWarning(ex, "Update job application validation failed for {Id}.", id);
            return BadRequest(ex.Message);
        }
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id, CancellationToken ct)
    {
        var success = await _service.DeleteAsync(id, ct);
        return success ? NoContent() : NotFound();
    }
}
