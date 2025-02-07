public interface ICreateCompanyService
{
    Task<Company> CreateCompany(CreateCompanyDto companyDto);
    Task<bool> ExistUser(int usuarioId);
}