using Application.LogicInterfaces;
using Domain.DTOs;
using Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[ApiController]
[Route("[controller]")]
[Authorize]
public class PatientsController : ControllerBase
{
    private readonly IPatientLogic patientLogic;

    public PatientsController(IPatientLogic patientLogic)
    {
        this.patientLogic = patientLogic;
    }
    
    [HttpPost]
    public async Task<ActionResult<Patient>> CreateAndAddToRoomAsync(int roomId, PatientCreationDto dto)
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

    [HttpDelete("{id:int}")]
    public async Task<ActionResult> DeleteAsync([FromRoute] int id)
    {
        try
        {
            await patientLogic.DeleteAsync(id);
            return Ok();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }
    
    [HttpGet("{id:int}")]
    public async Task<ActionResult<Patient>> GetById([FromRoute] int id)
    {
        try
        {
            Patient? result = await patientLogic.GetByIdAsync(id);
            return Ok(result);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }
    
    [HttpPatch("{patientId:int}+{roomId}")]
    public async Task<ActionResult> MovePatientToGivenRoom([FromRoute]int patientId, [FromRoute]int roomId)
    {
        try
        {
            await patientLogic.MovePatientToGivenRoom(patientId, roomId);
            return Ok();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }
    
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Patient>>> GetAllPatients()
    {
        try
        {
            IEnumerable<Patient?> patients= await patientLogic.GetAllPatientsAsync();
            return Ok(patients);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }
}