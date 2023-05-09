
using Application.LogicInterfaces;
using Domain.DTOs;
using Domain.Models;
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
    
    [HttpPost]
    public async Task<ActionResult<Patient>> CreateAsync(DoctorCreationDto dto)
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

    [HttpDelete("{id:int}")]
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
    
    [HttpGet("{id:int}")]
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
    
    [HttpPatch("{id:int}")]
    public async Task<ActionResult> DoctorUpdateAsync([FromRoute] int id, string name, string password, string phoneNumber)
    {
        try
        {
            await doctorLogic.DoctorUpdateAsync(id, name, password, phoneNumber);
            return Ok();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }
}