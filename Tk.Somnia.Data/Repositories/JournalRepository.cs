using Microsoft.EntityFrameworkCore;
using Tk.Somnia.Data.Context.Journals;
using Tk.Somnia.Interface.Repositories;
using Tk.Somnia.Model.Entities;

namespace Tk.Somnia.Data.Repositories;

public class JournalRepository : IJournalRepository
{
    private readonly JournalDbContext _context;
    
    public JournalRepository(JournalDbContext context)
    {
        _context = context;
    }

    public async Task MigrateAsync()
    {
        var pendingMigrations = await _context.Database.GetPendingMigrationsAsync();
        if (pendingMigrations.Any())
        {
            await _context.Database.MigrateAsync();
        }
    }
    
    public async Task AddAsync(Journal journal)
    {
        await _context.Journals.AddAsync(journal);
        await _context.SaveChangesAsync();
    }
}