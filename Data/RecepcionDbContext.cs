using DexterCompany.Models;
using Microsoft.EntityFrameworkCore;


public class RecepcionDbContext : DbContext
{
    public RecepcionDbContext(DbContextOptions<RecepcionDbContext> options) : base(options)
    {

    }

    public DbSet<User> Users { get; set; }
    public DbSet<NumbersDocument> NumbersDocuments { get; set; }
    public DbSet<CatService> CatServices { get; set; }
    public DbSet<BranchSales> BranchSales { get; set; }
    public DbSet<TypeDocument> TypeDocuments { get; set; }
    public DbSet<ProdService> ProdServices { get; set; }
    public DbSet<Customer> Customers { get; set; }
    public DbSet<WorkGuideMain> WorkGuideMains { get; set; }
    public DbSet<WorkGuideDetail> WorkGuideDetails { get; set; }
    public DbSet<ExpenseBox> ExpenseBoxMains { get; set; }
    public DbSet<WorkShift> WorkShifts { get; set; }
    public DbSet<CashBoxMain> CashBoxMains { get; set; }
    public DbSet<CashBoxDetail> CashBoxDetails { get; set; }
    public DbSet<BrachSalesUser> BrachSalesUsers { get; set; }
    public DbSet<AlertMsg> AlertMsgs { get; set; }
    public DbSet<LocationClothes> LocationClothes { get; set; }
    public DbSet<LocationWorkGuide> LocationWorkGuides { get; set; }
    public DbSet<ServiceAccessFast> ServiceAccessFasts { get; set; }
    public DbSet<Company> Companies { get; set; }

    public DbSet<ClothingItem>  ClothingItems { get; set; }
    public DbSet<ClothingObservations>  ClothingObservations { get; set; }
    public DbSet<ClothingWorker>  ClothingWorkers { get; set; }
    public DbSet<CollectionGuide>  CollectionGuides { get; set; }
    public DbSet<CollectionGuideTicket>  CollectionGuideTickets { get; set; }
    public DbSet<ObservationSection>  ObservationSections { get; set; }
    public DbSet<Ticket>  Tickets { get; set; }
    public DbSet<TicketClothe>  TicketClothes { get; set; }
    public DbSet<TypeObservation>  TypeObservations { get; set; }
    
    



    public override int SaveChanges()
    {
        UpdateAuditFields();
        return base.SaveChanges();
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        UpdateAuditFields();
        return base.SaveChangesAsync(cancellationToken);
    }

    private void UpdateAuditFields()
    {
        DateTime fechaOperacion = TimeZoneInfo.ConvertTime(DateTime.Now, TimeZoneInfo.FindSystemTimeZoneById("SA Pacific Standard Time"));

        var entries = ChangeTracker.Entries()
            .Where(e => e.Entity is BaseAuditableEntity && (e.State == EntityState.Added || e.State == EntityState.Modified));

        foreach (var entry in entries)
        {
            var auditableEntity = (BaseAuditableEntity)entry.Entity;
            var currentUser = "system"; // Aquí puedes obtener el usuario actual de tu contexto de autenticación
            

            if (entry.State == EntityState.Added)
            {
                auditableEntity.Created = fechaOperacion;
                auditableEntity.CreatedBy = currentUser;
            }
            else
            {
                Entry(auditableEntity).Property(x => x.Created).IsModified = false;
                Entry(auditableEntity).Property(x => x.CreatedBy).IsModified = false;
                auditableEntity.LastModified = fechaOperacion;
                auditableEntity.LastModifiedBy = currentUser;
            }
        }
    }
}