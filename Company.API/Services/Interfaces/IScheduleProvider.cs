namespace Company.API.Services.Interfaces;

public interface IScheduleProvider
{
    bool TryGetNotificationIntervals(string type, string market, out List<int> intervals);
}