using Application.LogicInterfaces;
using Domain.DTOs;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class RoomsController : ControllerBase
{
    private readonly IRoomLogic roomLogic;

    public RoomsController(IRoomLogic roomLogic)
    {
        this.roomLogic = roomLogic;
    }

    [HttpPost]
    public async Task<ActionResult<Room>> CreateAsync(RoomCreationDto dto)
    {
        try
        {
            Room room = await roomLogic.CreateAsync(dto);
            return Created($"/rooms/{room.Id}", room);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }
    
    [HttpGet]
    public async Task<ActionResult<IEnumerable<string>>> GetAllRoomsName()
    {
        try
        {
            IEnumerable<string> names = roomLogic.GetAllNames();
            return Ok(names);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }
    
    [HttpGet("Empty")]
    public async Task<ActionResult<IEnumerable<Room>>> GetAllEmptyRoomsIdToUpdate()
    {
        try
        {
            IEnumerable<Room> rooms = await roomLogic.GetAllEmptyRooms();
            return Ok(rooms);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }

    [HttpGet ("{id:int}")]
    public async Task<ActionResult<Room?>> GetRoomDetailsByIdAsync([FromRoute] int id)
    {
        try
        {
            Room? result = await roomLogic.GetRoomDetailsByIdAsync(id);
            return Ok(result);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }
    
    [HttpPatch("{id:int}")]
    public async Task<ActionResult> RoomUpdateAsync([FromRoute] int id, int capacity, string availability)
    {
        try
        {
            await roomLogic.RoomUpdateAsync(id, capacity, availability);
            return Ok();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }
    
    
    
}