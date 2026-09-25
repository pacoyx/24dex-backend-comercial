using Microsoft.EntityFrameworkCore;

public class DeletePositionService : IDeletePositionService
{
    private readonly RecepcionDbContext _context;

    public DeletePositionService(RecepcionDbContext context)
    {
        _context = context;
    }

    public async Task<bool> DeletePositionAsync(int id)
    {
        var position = await _context.Positions.FirstOrDefaultAsync(x => x.Id == id);
        if (position == null)
        {
            return false;
        }

        // Baja lógica: los empleados referencian PositionId, un delete físico rompería la integridad referencial.
        position.IsActive = false;
        await _context.SaveChangesAsync();

        return true;
    }
}
