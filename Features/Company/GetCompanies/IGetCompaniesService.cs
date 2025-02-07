public interface IGetCompaniesService
{
    Task<IEnumerable<GetCompaniesDto>> GetCompanies();
}