using Company.API.Exceptions;
using Company.API.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Company.API.Controllers;

[ApiController]
[Route("API/Schedule")]
public class ScheduleController : ControllerBase
{
    private readonly IScheduleService _scheduleService;

    public ScheduleController(IScheduleService scheduleService)
    {
        _scheduleService = scheduleService;
    }

    [HttpGet("{companyId}")]
    public async Task<ActionResult> GetNotificationsByCompanyIdAsync(string companyId)
    {
        try
        {
            return Ok(await _scheduleService.GetScheduleAsync(companyId));
        }
        catch (ItemNotFoundException e)
        {
            return NotFound(e.Message);
        }
    }
}