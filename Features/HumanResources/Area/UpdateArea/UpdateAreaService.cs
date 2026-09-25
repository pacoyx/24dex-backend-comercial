using Microsoft.EntityFrameworkCore;

public class UpdateAreaService : IUpdateAreaService
{
    private readonly RecepcionDbContext _context;

    public UpdateAreaService(RecepcionDbContext context)
    {
        _context = context;
    }

    public async Task<UpdateAreaResponseDto?> UpdateAreaAsync(int id, UpdateAreaRequestDto request)
    {
        var area = await _context.Areas.FirstOrDefaultAsync(x => x.Id == id);
        if (area == null)
        {
            return null;
        }

        area.Name = request.Name;
        area.IsActive = request.IsActive;

        await _context.SaveChangesAsync();

        return new UpdateAreaResponseDto
        {
            Id = area.Id,
            Name = area.Name,
            IsActive = area.IsActive
        };
    }
}
