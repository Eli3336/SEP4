using Application.LogicInterfaces;
using Domain.DTOs;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[ApiController]
[Route("[controller]")]

public class ReceptionistsController : ControllerBase
{
    
    private readonly IReceptionistLogic receptionistLogic;

    public ReceptionistsController(IReceptionistLogic receptionistLogic)
    {
        this.receptionistLogic = receptionistLogic;
    }
    
    [HttpPost]
    public async Task<ActionResult<Patient>> CreateAsync(ReceptionistCreationDto dto)
    {
        try
        {
            Receptionist receptionist = await receptionistLogic.CreateAsync(dto);
            return Created($"/receptionist/{receptionist.Id}", receptionist);
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
            await receptionistLogic.DeleteAsync(id);
            return Ok();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }
    
    [HttpPatch("{id:int}")]
    public async Task<ActionResult> ReceptionistUpdateAsync([FromRoute] int id, string name, string phoneNumber)
    {
        try
        {
            await receptionistLogic.ReceptionistUpdateAsync(id, name, phoneNumber);
            return Ok();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }
    
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Receptionist>>> GetAllReceptionists()
    {
        try
        {
            IEnumerable<Receptionist?> receptionists= await receptionistLogic.GetAllReceptionistsAsync();
            return Ok(receptionists);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }
}