using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

public class GetServicesAccessFastService : IGetServicesAccessFastService
{
    private readonly RecepcionDbContext _context;
    public IAppLogger<GetServicesAccessFastService> Logger { get; }

    public GetServicesAccessFastService(RecepcionDbContext context, IAppLogger<GetServicesAccessFastService> logger)
    {
        _context = context;
        Logger = logger;
    }


    public async Task<IEnumerable<GetServicesAccessFastResponseDto>> GetServicesAccessFastAsync()
    {
        this.Logger.LogInformacion("EXEC GetServicesAccessFastAsync()");

        var datos =  await _context.ServiceAccessFasts
               .AsNoTracking()
               .Where(saf => saf.Status == "A")
               .Join(_context.ProdServices,
                 saf => saf.ProdServiceID ,
                 ps => ps.Id,
                 (saf, ps) => new { saf, ps.Price, ps.CatServiceId, ps.IsPeso, ps.IsLavado })
               .Join(_context.CatServices,
                 saf => saf.CatServiceId,
                 cs => cs.Id,
                 (saf, cs) => new { saf.saf, saf.Price, cs.Name, saf.IsPeso, saf.IsLavado })
               .OrderBy(saf => saf.saf.Order)
               .Select(saf => new GetServicesAccessFastResponseDto
               (
                   saf.saf.Id,
                   saf.saf.ProdServiceID,
                   saf.saf.ShortName,
                   saf.saf.IconName,
                   saf.saf.Status,
                   saf.Price,
                   saf.Name,
                   saf.IsPeso,
                   saf.IsLavado
               ))
               .ToListAsync();
        return datos;
    }
}