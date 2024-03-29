﻿using Domain.DTOs;
using Domain.Models;

namespace Application.LogicInterfaces;

public interface IRequestLogic
{
    Task<Request> CreateAsync(RequestCreationDto requestToCreate);
    Task<Request?> GetByIdAsync(int id);
    Task DeleteAsync(int id);
    Task<IEnumerable<Request>> GetAllRequestsToMovePatients();
    Task<IEnumerable<Request>> GetAllAdditionalRequests();
}