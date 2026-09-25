using Microsoft.EntityFrameworkCore;

public class GetAreasService : IGetAreasService
{
    private readonly RecepcionDbContext _context;

    public GetAreasService(RecepcionDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<GetAreasResponseDto>> GetAreasAsync(bool? onlyActive)
    {
        var query = _context.Areas.AsNoTracking().AsQueryable();

        if (onlyActive == true)
        {
            query = query.Where(x => x.IsActive);
        }

        return await query
            .OrderBy(x => x.Name)
            .Select(x => new GetAreasResponseDto
            {
                Id = x.Id,
                Name = x.Name,
                IsActive = x.IsActive
            })
            .ToListAsync();
    }
}
