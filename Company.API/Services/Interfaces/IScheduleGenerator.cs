using Company.API.Models;

namespace Company.API.Services.Interfaces;

public interface IScheduleGenerator
{
    List<NotificationDto> CreateSchedule(CompanyDto company);
}