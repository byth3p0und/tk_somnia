using Tk.Somnia.Model.Base;
using Tk.Somnia.Model.Exceptions;

namespace Tk.Somnia.Model.Entities;

public class JournalEntry : Entity
{
    public DateOnly Date { get; }
    
    public string? Title { get; private set; }
    public string? Contents { get; private set; }

    public Journal Parent { get; private set; } = null!;
    
    private JournalEntry()
    {
    }
    
    private JournalEntry(DateOnly date)
    {
        JournalEntryException.ThrowIfDateIsInvalid(date);
        Date = date;
    }
    
    public static JournalEntry Create(DateOnly date)
    {
        var entry = new JournalEntry(date);
        return entry;
    }
    
    public void SetTitle(string title)
    {
        JournalEntryException.ThrowIfTitleIsInvalid(title);
        
        Title = title;
    }
    
    public void SetContents(string contents)
    {
        JournalEntryException.ThrowIfContentsIsInvalid(contents);
        
        Contents = contents;
    }
}