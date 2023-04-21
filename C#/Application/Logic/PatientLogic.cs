using Application.DaoInterfaces;
using Application.LogicInterfaces;
using Domain.DTOs;
using Domain.Models;

namespace Application.Logic;

public class PatientLogic : IPatientLogic
{
    private readonly IRoomDao roomDao;

    public PatientLogic(IRoomDao roomDao)
    {
        this.roomDao = roomDao;
    }
    public async Task<Patient> CreateAndAddToRoomAsync(int roomId, PatientCreationDto dto)
    {
        ValidatePatient(dto);
        Patient toCreate = new Patient()
        {
            Name = dto.Name
        };
        Patient created = await roomDao.CreateAndAddToRoomAsync(roomId, toCreate);
        return created;
    }
    private void ValidatePatient(PatientCreationDto dto)
    {
        if (dto.Name == "")
            throw new ArgumentException("The name cannot be empty!");
        if (dto.Name.Length < 3)
            throw new ArgumentException("The name cannot be smaller than 3 characters!");
        if (dto.Name.Length > 255)
            throw new ArgumentException("The name is too long!");
    }
}