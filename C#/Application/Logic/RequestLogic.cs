using Application.DaoInterfaces;
using Application.LogicInterfaces;
using Domain.Models;

namespace Application.Logic;

public class RequestLogic : IRequestLogic
{
    private readonly IRequestDao requestDao;

    public RequestLogic(IRequestDao requestDao)
    {
        this.requestDao = requestDao;
    }

    public async Task<Request?> GetByIdAsync(int id)
    {
        Request? request = await requestDao.GetByIdAsync(id);
        if (request == null)
        {
            throw new Exception(
                $"Request with ID {id} not found!");
        }
        return request;
    }

    public async Task DeleteAsync(int id)
    {
        Request? request = await requestDao.GetByIdAsync(id);
        if (request == null)
        {
            throw new Exception(
                $"Request with ID {id} not found!");
        }
        await requestDao.DeleteAsync(request);
    }

    public async Task<IEnumerable<Request>> GetAllRequestsToMovePatients()
    {
        List<Request> requests = requestDao.GetAllRequests().Result.ToList();
        List<Request> result = new List<Request>();
        for (int i = 0; i < requests.Count; i++)
        {
            if(requests[i].Type.Equals("Move"))
                result.Add(requests[i]);
        }
        return result;
    }

    public async Task<IEnumerable<Request>> GetAllAdditionalRequests()
    {
        List<Request> requests = requestDao.GetAllRequests().Result.ToList();
        List<Request> result = new List<Request>();
        for (int i = 0; i < requests.Count; i++)
        {
            if(requests[i].Type.Equals("Additional"))
                result.Add(requests[i]);
        }
        return result;
    }
}