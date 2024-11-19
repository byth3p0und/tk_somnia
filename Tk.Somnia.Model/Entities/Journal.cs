using Tk.Somnia.Model.Base;
using Tk.Somnia.Model.Exceptions;

namespace Tk.Somnia.Model.Entities;

public class Journal : Entity
{
    public string Name { get; } = null!;
    public string? Description { get; private set; }

    private List<JournalEntry> _entries = new();
    public IReadOnlyList<JournalEntry> Entries => _entries;
    
    private Journal()
    {
    }
    
    private Journal(string name)
    {
        JournalException.ThrowIfNameIsInvalid(name);
        
        Name = name;
    }
    
    public static Journal Create(string name)
    {
        var journal = new Journal(name);
        return journal;
    }
    
    public void SetDescription(string description)
    {
        JournalException.ThrowIfDescriptionIsInvalid(description);
        
        Description = description;
    }
    
    public void AddEntry(JournalEntry entry)
    {
        JournalException.ThrowIfEntryIsNull(entry);
        JournalException.ThrowIfEntryDateExists(Entries, entry.Date);
        
        _entries.Add(entry);
    }
    
    public void RemoveEntry(DateOnly entryDate)
    {
        JournalException.ThrowIfEntryDateDoesNotExist(Entries, entryDate, out var existingEntry);
        
        _entries.Remove(existingEntry!);
    }
}