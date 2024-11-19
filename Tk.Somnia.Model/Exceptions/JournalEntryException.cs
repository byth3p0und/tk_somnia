namespace Tk.Somnia.Model.Exceptions;

internal class JournalEntryException : Exception
{
    private JournalEntryException(string message) : base(message)
    {
    }
    
    public static void ThrowIfTitleIsInvalid(string? title)
    {
        if (string.IsNullOrWhiteSpace(title))
        {
            throw new JournalEntryException("Title must be provided.");
        }
    }

    public static void ThrowIfContentsIsInvalid(string contents)
    {
        if (string.IsNullOrWhiteSpace(contents))
        {
            throw new JournalEntryException("Contents must be provided.");
        }
    }

    public static void ThrowIfDateIsInvalid(DateOnly date)
    {
        if(date == default)
        {
            throw new JournalEntryException("Date must be provided.");
        }
    }
}