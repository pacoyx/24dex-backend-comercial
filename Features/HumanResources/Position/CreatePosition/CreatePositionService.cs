public class CreatePositionService : ICreatePositionService
{
    private readonly RecepcionDbContext _context;

    public CreatePositionService(RecepcionDbContext context)
    {
        _context = context;
    }

    public async Task<CreatePositionResponseDto> CreatePositionAsync(CreatePositionRequestDto request)
    {
        var position = new Position
        {
            Name = request.Name,
            IsActive = true
        };

        _context.Positions.Add(position);
        await _context.SaveChangesAsync();

        return new CreatePositionResponseDto
        {
            Id = position.Id,
            Name = position.Name,
            IsActive = position.IsActive
        };
    }
}
