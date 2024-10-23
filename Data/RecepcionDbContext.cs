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
}