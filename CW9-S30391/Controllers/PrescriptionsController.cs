using CW9_S30391.DTOs;
using CW9_S30391.Exceptions;
using CW9_S30391.Services;
using Microsoft.AspNetCore.Mvc;

namespace CW9_S30391.Controllers;

[ApiController]
[Route("[controller]")]
public class PrescriptionsController(IDbService service) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> AddPrescription([FromBody] PrescriptionCreateDto prescriptionData)
    {
        try
        {
            var prescription = await service.CreatePrescriptionAsync(prescriptionData);
            return Created($"prescriptions/{prescription.IdPrescription}",prescription);
        } catch (NotFoundException e)
        {
            return NotFound(e.Message);
        }
    }
}