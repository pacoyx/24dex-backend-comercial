using System.Collections.Generic;
using System.Threading.Tasks;

public interface IGetServicesAccessFastService
{
    Task<IEnumerable<GetServicesAccessFastResponseDto>> GetServicesAccessFastAsync();
}