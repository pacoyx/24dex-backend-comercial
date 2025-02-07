public interface IGetCompanyByUserService
{
    Task<Company?> GetCompanyByUser(int userId);
}
