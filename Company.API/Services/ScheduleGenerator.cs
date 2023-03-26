using Company.API.Models;
using Company.API.Services.Interfaces;

namespace Company.API.Services;

public class ScheduleGenerator : IScheduleGenerator
{
    private readonly IScheduleProvider _scheduleProvider;

    public ScheduleGenerator(IScheduleProvider scheduleProvider)
    {
        _scheduleProvider = scheduleProvider;
    }

    public List<NotificationDto> CreateSchedule(CompanyDto company)
    {
        if (_scheduleProvider.TryGetNotificationIntervals(company.Type, company.Market, out var intervals))
            return GenerateNotifications(intervals);
        return new List<NotificationDto>();
    }

    private static List<NotificationDto> GenerateNotifications(List<int> intervals)
    {
        var notifications = new List<NotificationDto>();

        foreach (var day in intervals)
        {
            var currentDate = DateTime.Now;
            var futureDate = currentDate.AddDays(day);
            notifications.Add(new NotificationDto {Id = Guid.NewGuid().ToString(), DateTime = futureDate});
        }

        return notifications;
    }
}