using Application.LogicInterfaces;
using Domain.DTOs;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class RequestsController : ControllerBase
{
    private readonly IRequestLogic requestLogic;

    public RequestsController(IRequestLogic requestLogic)
    {
        this.requestLogic = requestLogic;
    }
    
    [HttpPost]
    public async Task<ActionResult<Request>> CreateAsync(RequestCreationDto dto)
    {
        try
        {
            Request request = await requestLogic.CreateAsync(dto);
            return Created($"/requests/{request.Id}", request);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }
    
    
    [HttpDelete("{id:int}")]
    public async Task<ActionResult> DeleteRequestAsync([FromRoute] int id)
    {
        try
        {
            await requestLogic.DeleteAsync(id);
            return Ok();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }
    
    [HttpGet("{id:int}")]
    public async Task<ActionResult<Request>> GetRequestById([FromRoute] int id)
    {
        try
        {
            Request? request = await requestLogic.GetByIdAsync(id);
            return Ok(request);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }
    
    [HttpGet("/ToMove")]
    public async Task<ActionResult<IEnumerable<Request>>> GetAllRequestsToMovePatients()
    {
        try
        {
            IEnumerable<Request>? requests = await requestLogic.GetAllRequestsToMovePatients();
            return Ok(requests);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }
    
    [HttpGet("/Additional")]
    public async Task<ActionResult<IEnumerable<Request>>> GetAllAdditionalRequests()
    {
        try
        {
            IEnumerable<Request>? requests = await requestLogic.GetAllAdditionalRequests();
            return Ok(requests);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }
}