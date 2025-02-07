public interface IUpdateCompanyService
{
    Task<Company?> UpdateCompany(UpdateCompanyDto companyDto);
    Task<Company?> GetCompany(int id);
}