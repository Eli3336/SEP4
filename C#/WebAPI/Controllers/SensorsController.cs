using Application.LogicInterfaces;
using Domain.DTOs;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class SensorsController : ControllerBase
{
    private readonly ISensorLogic sensorLogic;

    public SensorsController(ISensorLogic sensorLogic)
    {
        this.sensorLogic = sensorLogic;
    }

    [HttpPost]
    public async Task<ActionResult<Sensor>> CreateAsync(SensorCreationDto dto)
    {
        try
        {
            Sensor sensor = await sensorLogic.CreateAsync(dto);
            return Created($"/sensors/{sensor.Type}", sensor);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }
    
    [HttpGet]
    public async Task<ActionResult<IEnumerable<SensorValue>>> GetSensorsValuesAsync([FromQuery] int roomId)
    {
        try
        {
            IEnumerable<SensorValue> values = await sensorLogic.GetSensorsValuesAsync(roomId);
            return Ok(values);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }

    [HttpGet("{sensorId:int}")]
    public async Task<ActionResult<IEnumerable<SensorValue>>> GetLogOfSensorValuesAsync([FromRoute] int? sensorId)
    {
        try
        {
            IEnumerable<SensorValue> log = await sensorLogic.GetLogOfSensorValuesAsync(sensorId);
            return Ok(log);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }
    [HttpPatch("{id:int}")]
    public async Task<ActionResult> SensorUpdateAsync([FromRoute] int id, double upbreakpoint, double downbreakpoint)
    {
        try
        {
            await sensorLogic.SensorUpdateAsync(id, upbreakpoint, downbreakpoint);
            return Ok();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }
}