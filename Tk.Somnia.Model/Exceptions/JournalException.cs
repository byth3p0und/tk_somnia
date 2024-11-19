using Tk.Somnia.Model.Entities;

namespace Tk.Somnia.Model.Exceptions;

public class JournalException : Exception
{
    private JournalException(string message) : base(message)
    {
    }
    
    public static void ThrowIfNameIsInvalid(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new JournalException("Name must be provided.");
        }
    }

    public static void ThrowIfDescriptionIsInvalid(string description)
    {
        if (string.IsNullOrWhiteSpace(description))
        {
            throw new JournalException("Description must be provided.");
        }
    }

    public static void ThrowIfEntryIsNull(JournalEntry? entry)
    {
        if (entry is null)
        {
            throw new JournalException("Journal entry must be provided.");
        }
    }
    
    public static void ThrowIfEntryDateExists(IEnumerable<JournalEntry> entries, DateOnly entryDate)
    {
        using var enumerator = entries.GetEnumerator();
        
        while (enumerator.MoveNext())
        {
            if (enumerator.Current.Date == entryDate)
            {
                throw new JournalException($"Entry with the same date ({entryDate}) already exists.");
            }
        }
    }

    public static void ThrowIfEntryDateDoesNotExist(IEnumerable<JournalEntry> entries, DateOnly entryDate, out JournalEntry? entry)
    {
        using var enumerator = entries.GetEnumerator();
        
        while (enumerator.MoveNext())
        {
            if (enumerator.Current.Date == entryDate)
            {
                entry = enumerator.Current;
                return;
            }
        }
        
        throw new JournalException($"Entry with the date ({entryDate}) does not exist.");
    }
}