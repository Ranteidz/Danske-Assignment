using Company.API.Exceptions;
using Company.API.Models;
using Company.API.Persistence;
using Company.API.Services.Interfaces;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

namespace Company.API.Services;

public class CompanyService : ICompanyService
{
    private readonly CompanyDatabase _db;
    private readonly IScheduleGenerator _scheduleGenerator;

    public CompanyService(CompanyDatabase db, IScheduleGenerator scheduleGenerator)
    {
        _db = db;
        _scheduleGenerator = scheduleGenerator;
    }

    public async Task<CompanyDto> CreateCompanyAsync(CompanyDto companyDto)
    {
        try
        {
            companyDto.Notifications = _scheduleGenerator.CreateSchedule(companyDto);
            var tempCompany = await _db.Companies.AddAsync(companyDto);
            await _db.SaveChangesAsync();
            return tempCompany.Entity;
        }
        catch (DbUpdateException e) when (e.InnerException is SqliteException {SqliteErrorCode: 19})
        {
            throw new DatabaseConstraintViolationException();
        }
    }

    public async Task<List<CompanyDto>> GetCompaniesAsync()
    {
        var companies = await _db.Companies.Include(x => x.Notifications.OrderBy(e => e.DateTime)).ToListAsync();
        return companies;
    }

    public async Task<CompanyDto> GetCompanyByIdAsync(string companyId)
    {
        var company = await _db.Companies
            .Include(x => x.Notifications
                .OrderBy(e => e.DateTime))
            .FirstOrDefaultAsync(x => x.Id == companyId);
        if (company != null) return company;

        throw new ItemNotFoundException($"Company with id: {companyId} not found in the database.");
    }
}