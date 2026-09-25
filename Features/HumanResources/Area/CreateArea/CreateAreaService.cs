public class CreateAreaService : ICreateAreaService
{
    private readonly RecepcionDbContext _context;

    public CreateAreaService(RecepcionDbContext context)
    {
        _context = context;
    }

    public async Task<CreateAreaResponseDto> CreateAreaAsync(CreateAreaRequestDto request)
    {
        var area = new Area
        {
            Name = request.Name,
            IsActive = true
        };

        _context.Areas.Add(area);
        await _context.SaveChangesAsync();

        return new CreateAreaResponseDto
        {
            Id = area.Id,
            Name = area.Name,
            IsActive = area.IsActive
        };
    }
}
