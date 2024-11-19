using Microsoft.EntityFrameworkCore;
using Tk.Somnia.Interface.Interceptors;
using Tk.Somnia.Model.Entities;

namespace Tk.Somnia.Data.Context.Journals;

public class JournalDbContext : DbContext
{
    private readonly IPersistenceInterceptor _persistenceInterceptor;
    
    public DbSet<Journal> Journals { get; set; }
    
    public JournalDbContext(DbContextOptions<JournalDbContext> options, IPersistenceInterceptor persistenceInterceptor) : base(options)
    {
        _persistenceInterceptor = persistenceInterceptor;
    }
    
    private static void _applyGeneralSettings(ModelBuilder modelBuilder)
    {
        foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
        {
            relationship.DeleteBehavior = DeleteBehavior.Restrict;
        }
    }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
        optionsBuilder.AddInterceptors(_persistenceInterceptor);
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);

        _applyGeneralSettings(modelBuilder);
    }
}