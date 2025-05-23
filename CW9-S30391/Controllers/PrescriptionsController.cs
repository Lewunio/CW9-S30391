using CW9_S30391.DTOs;
using CW9_S30391.Models;
using CW9_S30391.Services;
using Microsoft.AspNetCore.Mvc;

namespace CW9_S30391.Controllers;

[ApiController]
[Route("[controller]")]
public class PrescriptionsController(IDbService service) : ControllerBase
{
    // [HttpPost]
    // public async Task<IActionResult> AddPerscription([FromBody] PrescriptionCreateDto prescriptionData)
    // {
    //     try
    //     {
    //         var perscription = await service.CreatePerscriptionAsync(prescriptionData);
    //         return Created($"prescriptions/{perscription.IdPerscription}",perscription);
    //     } catch (Exception e)
    //     {
    //         return NotFound(e.Message);
    //     }
    // }
}