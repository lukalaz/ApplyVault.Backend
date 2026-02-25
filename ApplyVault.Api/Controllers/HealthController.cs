using Microsoft.AspNetCore.Mvc;

namespace ApplyVault.Api.Controllers;

[ApiController]
[Route("health")]
public class HealthController : ControllerBase
{
    [HttpGet]
    public IActionResult Get() => Ok();
}