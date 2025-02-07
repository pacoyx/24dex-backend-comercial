public interface IDeleteCompanyService
{
    Task DeleteCompany(int id);
    Task<Company?> GetCompany(int id);
}