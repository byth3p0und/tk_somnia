namespace Tk.Somnia.Interface.Providers;

public interface IDateTimeProvider
{
    DateTimeOffset Now { get; }
    DateOnly Today { get; }
}