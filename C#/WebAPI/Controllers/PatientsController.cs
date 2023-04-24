﻿using Application.LogicInterfaces;
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
    
    [HttpGet("{id:long}")]
    public async Task<ActionResult<Patient>> GetById([FromRoute] int id)
    {
        try
        {
            Patient result = await patientLogic.GetByIdAsync(id);
            return Ok(result);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }
}