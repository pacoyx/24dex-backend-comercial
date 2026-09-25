using Microsoft.EntityFrameworkCore;

public class DeleteAreaService : IDeleteAreaService
{
    private readonly RecepcionDbContext _context;

    public DeleteAreaService(RecepcionDbContext context)
    {
        _context = context;
    }

    public async Task<bool> DeleteAreaAsync(int id)
    {
        var area = await _context.Areas.FirstOrDefaultAsync(x => x.Id == id);
        if (area == null)
        {
            return false;
        }

        // Baja lógica: los empleados referencian AreaId, un delete físico rompería la integridad referencial.
        area.IsActive = false;
        await _context.SaveChangesAsync();

        return true;
    }
}
