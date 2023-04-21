using Application.LogicInterfaces;
using Domain.DTOs;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class PatientsController : ControllerBase
{
    private readonly IPatientLogic patientLogic;

    public PatientsController(IPatientLogic patientLogic)
    {
        this.patientLogic = patientLogic;
    }
    
    [HttpPost]
    public async Task<ActionResult<Patient>> CreateAndAddToRoomAsync(PatientCreationDto dto, int roomId)
    {
        try
        {
            Patient patient = await patientLogic.CreateAndAddToRoomAsync(roomId, dto);
            return Created($"/patients/{patient.Id}", patient);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }
}