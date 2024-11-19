using Tk.Somnia.Interface.Providers;

namespace Tk.Somnia.Application.Providers;

public class DefaultDateTimeProvider : IDateTimeProvider
{
    public DateTimeOffset Now => DateTimeOffset.Now;
    public DateOnly Today => DateOnly.FromDateTime(Now.Date);
}