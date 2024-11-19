using Tk.Somnia.Model.Entities;

namespace Tk.Somnia.Interface.Repositories;

public interface IJournalRepository
{
    Task MigrateAsync();
    
    Task AddAsync(Journal journal);
}