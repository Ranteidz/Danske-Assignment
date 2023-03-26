using Company.API.Exceptions;
using Company.API.Models;
using Company.API.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Company.API.Controllers;

[ApiController]
[Route("API/company")]
public class CompanyController : ControllerBase
{
    private readonly ICompanyService _companyService;


    public CompanyController(ICompanyService companyService)
    {
        _companyService = companyService;
    }

    [HttpPost]
    public async Task<ActionResult> CreateCompanyAsync([FromBody] CompanyDto company)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        try
        {
            return Ok(await _companyService.CreateCompanyAsync(company));
        }
        catch (DatabaseConstraintViolationException e)
        {
            return BadRequest(e.Message);
        }
    }


    [HttpGet]
    public async Task<ActionResult<List<CompanyDto>>> GetCompaniesAsync()
    {
        return Ok(await _companyService.GetCompaniesAsync());
    }
}