using ApplyVault.Api.Dtos.JobApplications;
using ApplyVault.Domain.Entities;
using ApplyVault.Infrastructure.Persistence.EFCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApplyVault.Api.Controllers;

[ApiController]
[Route("api/job-applications")]
public class JobApplicationsController : ControllerBase
{
    private readonly ApplicationDbContext _db;

    public JobApplicationsController(ApplicationDbContext db)
    {
        _db = db;
    }

    // GET api/job-applications
    [HttpGet]
    public async Task<ActionResult<List<JobApplicationResponseDto>>> GetAll(CancellationToken ct)
    {
        var items = await _db.JobApplications
            .AsNoTracking()
            .OrderByDescending(x => x.DateApplied ?? DateTime.MinValue)
            .ThenByDescending(x => x.LastTouch ?? DateTime.MinValue)
            .ToListAsync(ct);

        return Ok(items.Select(ToDto).ToList());
    }

    // GET api/job-applications/{id}
    [HttpGet("{id:guid}")]
    public async Task<ActionResult<JobApplicationResponseDto>> GetById(Guid id, CancellationToken ct)
    {
        var entity = await _db.JobApplications
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == id, ct);

        if (entity is null)
            return NotFound();

        return Ok(ToDto(entity));
    }

    // POST api/job-applications
    [HttpPost]
    public async Task<ActionResult<JobApplicationResponseDto>> Create(
        [FromBody] CreateJobApplicationRequestDto dto,
        CancellationToken ct)
    {
        if (string.IsNullOrWhiteSpace(dto.Company))
            return BadRequest("Company is required.");

        if (string.IsNullOrWhiteSpace(dto.Role))
            return BadRequest("Role is required.");

        var entity = JobApplication.Create(
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
        _db.JobApplications.Add(entity);
        await _db.SaveChangesAsync(ct);

        return CreatedAtAction(nameof(GetById), new { id = entity.Id }, ToDto(entity));
    }

    // PUT api/job-applications/{id}
    [HttpPut("{id:guid}")]
    public async Task<ActionResult<JobApplicationResponseDto>> Update(
        Guid id,
        [FromBody] UpdateJobApplicationRequestDto dto,
        CancellationToken ct)
    {
        if (string.IsNullOrWhiteSpace(dto.Company))
            return BadRequest("Company is required.");

        if (string.IsNullOrWhiteSpace(dto.Role))
            return BadRequest("Role is required.");

        var entity = await _db.JobApplications.FirstOrDefaultAsync(x => x.Id == id, ct);
        if (entity is null)
            return NotFound();

        entity.Update(
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

        await _db.SaveChangesAsync(ct);

        return Ok(ToDto(entity));
    }

    // DELETE api/job-applications/{id}
    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id, CancellationToken ct)
    {
        var entity = await _db.JobApplications.FirstOrDefaultAsync(x => x.Id == id, ct);
        if (entity is null)
            return NotFound();

        _db.JobApplications.Remove(entity);
        await _db.SaveChangesAsync(ct);

        return NoContent();
    }

    private static JobApplicationResponseDto ToDto(JobApplication e) =>
        new(
            e.Id,
            e.Company,
            e.Role,
            e.Location,
            e.IsRemote,
            e.Referral,
            e.ContactPerson,
            e.DateApplied,
            e.Status,
            e.CompensationRange,
            e.LastTouch,
            e.NextAction,
            e.NextActionDate,
            e.Notes,
            e.Link
        );
}
