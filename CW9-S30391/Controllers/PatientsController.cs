using CW9_S30391.Services;
using Microsoft.AspNetCore.Mvc;

namespace CW9_S30391.Controllers;

[ApiController]
[Route("[controller]")]
public class PatientsController(IDbService service) : ControllerBase
{
    [HttpGet("{id}")]
    public async Task<IActionResult> GetPatient([FromRoute] int id)
    {
        try
        {
            var patient = await service.GetPatientByIdAsync(id);
            return Ok(patient);
        } catch (Exception e)
        {
            return NotFound(e.Message);
        }
    }
}