﻿using Company.API.Services.Interfaces;

namespace Company.API.Services;

public class ScheduleProvider : IScheduleProvider
{
    //Ideally this should be stored in the database because you could change the schedule without recompiling the application
    private readonly IDictionary<(string companyType, string market), List<int>?> _notificationsMap =
        new Dictionary<(string companyType, string market), List<int>?>
        {
            {("small", "Denmark"), new List<int> {1, 5, 10, 15, 20}},
            {("medium", "Denmark"), new List<int> {1, 5, 10, 15, 20}},
            {("large", "Denmark"), new List<int> {1, 5, 10, 15, 20}},

            {("small", "Norway"), new List<int> {1, 5, 10, 20}},
            {("medium", "Norway"), new List<int> {1, 5, 10, 20}},
            {("large", "Norway"), new List<int> {1, 5, 10, 20}},

            {("small", "Sweden"), new List<int> {1, 7, 14, 28}},
            {("medium", "Sweden"), new List<int> {1, 7, 14, 28}},

            {("large", "Finland"), new List<int> {1, 5, 10, 15, 20}}
        };

    /// <summary>
    ///     Tries to retrieve intervals.
    /// </summary>
    /// <param name="type"></param>
    /// <param name="market"></param>
    /// <param name="intervals"></param>
    /// <returns></returns>
    public bool TryGetNotificationIntervals(string type, string market, out List<int>? intervals)
    {
        return _notificationsMap.TryGetValue((type, market), out intervals);
    }
}