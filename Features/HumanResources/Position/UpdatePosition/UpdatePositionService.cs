using Microsoft.EntityFrameworkCore;

public class UpdatePositionService : IUpdatePositionService
{
    private readonly RecepcionDbContext _context;

    public UpdatePositionService(RecepcionDbContext context)
    {
        _context = context;
    }

    public async Task<UpdatePositionResponseDto?> UpdatePositionAsync(int id, UpdatePositionRequestDto request)
    {
        var position = await _context.Positions.FirstOrDefaultAsync(x => x.Id == id);
        if (position == null)
        {
            return null;
        }

        position.Name = request.Name;
        position.IsActive = request.IsActive;

        await _context.SaveChangesAsync();

        return new UpdatePositionResponseDto
        {
            Id = position.Id,
            Name = position.Name,
            IsActive = position.IsActive
        };
    }
}
