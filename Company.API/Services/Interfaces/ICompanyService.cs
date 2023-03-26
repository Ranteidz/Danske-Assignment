using Company.API.Models;

namespace Company.API.Services.Interfaces;

public interface ICompanyService
{
    Task<CompanyDto> CreateCompanyAsync(CompanyDto companyDto);
    Task<List<CompanyDto>> GetCompaniesAsync();
    Task<CompanyDto> GetCompanyByIdAsync(string companyId);
}