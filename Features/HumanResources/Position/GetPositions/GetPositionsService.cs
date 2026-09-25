using Microsoft.EntityFrameworkCore;

public class GetPositionsService : IGetPositionsService
{
    private readonly RecepcionDbContext _context;

    public GetPositionsService(RecepcionDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<GetPositionsResponseDto>> GetPositionsAsync(bool? onlyActive)
    {
        var query = _context.Positions.AsNoTracking().AsQueryable();

        if (onlyActive == true)
        {
            query = query.Where(x => x.IsActive);
        }

        return await query
            .OrderBy(x => x.Name)
            .Select(x => new GetPositionsResponseDto
            {
                Id = x.Id,
                Name = x.Name,
                IsActive = x.IsActive
            })
            .ToListAsync();
    }
}
