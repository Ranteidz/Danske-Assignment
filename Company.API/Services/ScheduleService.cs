﻿using Company.API.Models;
using Company.API.Services.Interfaces;

namespace Company.API.Services;

public class ScheduleService : IScheduleService
{
    private readonly ICompanyService _companyService;

    public ScheduleService(ICompanyService companyService)
    {
        _companyService = companyService;
    }

    /// <summary>
    ///     Retrieves a schedule based on companyId and returns new CompanyScheduleDto.
    /// </summary>
    /// <param name="companyId"></param>
    /// <returns></returns>
    public async Task<CompanyScheduleDto> GetScheduleAsync(string companyId)
    {
        var schedule = await _companyService.GetCompanyByIdAsync(companyId);
        return new CompanyScheduleDto {Id = schedule.Id, Notifications = schedule.Notifications};
    }
}