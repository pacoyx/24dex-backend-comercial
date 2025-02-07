public interface IGetCompanyService
{
    Task<Company?> GetCompany(int id);
}