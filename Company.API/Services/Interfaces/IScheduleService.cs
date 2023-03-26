using Company.API.Models;

namespace Company.API.Services.Interfaces;

public interface IScheduleService
{
    Task<CompanyScheduleDto> GetScheduleAsync(string companyId);
}