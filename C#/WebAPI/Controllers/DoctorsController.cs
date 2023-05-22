using Application.LogicInterfaces;
using Domain.DTOs;
using Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[ApiController]
[Route("[controller]")]

public class DoctorsController : ControllerBase
{
    private readonly IDoctorLogic doctorLogic;

    public DoctorsController(IDoctorLogic doctorLogic)
    {
        this.doctorLogic = doctorLogic;
    }
    
    [HttpPost, Authorize("BeDoctor")]
    public async Task<ActionResult<Doctor>> CreateAsync(DoctorCreationDto dto)
    {
        try
        {
            Doctor doctor = await doctorLogic.CreateAsync(dto);
            return Created($"/doctors/{doctor.Id}", doctor);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }

    [HttpDelete("{id:int}"), Authorize("BeDoctor")]
    public async Task<ActionResult> DeleteAsync([FromRoute] int id)
    {
        try
        {
            await doctorLogic.DeleteAsync(id);
            return Ok();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }
    
    [HttpGet("{id:int}"), Authorize("BeDoctor")]
    public async Task<ActionResult<Doctor>> GetById([FromRoute] int id)
    {
        try
        {
            Doctor? result = await doctorLogic.GetByIdAsync(id);
            return Ok(result);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }
    
    [HttpGet, Authorize("BeDoctor")]
    public async Task<ActionResult<IEnumerable<Doctor>>> GetAllDoctors()
    {
        try
        {
            IEnumerable<Doctor?> doctors= await doctorLogic.GetAllDoctorsAsync();
            return Ok(doctors);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }
    
    [HttpPatch("{id:int}"), Authorize("BeDoctor")]
    public async Task<ActionResult> DoctorUpdateAsync([FromRoute] int id, string name, string phoneNumber)
    {
        try
        {
            await doctorLogic.DoctorUpdateAsync(id, name, phoneNumber);
            return Ok();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }
}
