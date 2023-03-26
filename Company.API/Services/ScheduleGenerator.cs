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

    /// <summary>
    ///     Checks if the company provided meets the notification requirements, generates notifications and returns a
    ///     notifications list.
    ///     If the company doesn't meet the requirements, an empty notification list will be returned.
    /// </summary>
    /// <param name="company"></param>
    /// <returns></returns>
    public List<NotificationDto> CreateSchedule(CompanyDto company)
    {
        if (_scheduleProvider.TryGetNotificationIntervals(company.Type!, company.Market!, out var intervals))
            return GenerateNotifications(intervals!);
        return new List<NotificationDto>();
    }

    /// <summary>
    ///     Converts a list of integer intervals to notifications.
    /// </summary>
    /// <param name="intervals"></param>
    /// <returns></returns>
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