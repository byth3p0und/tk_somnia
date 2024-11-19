using Tk.Somnia.Interface.Providers;

namespace Tk.Somnia.Application.Providers;

public class DefaultGuidProvider : IGuidProvider
{
    public Guid New() => Guid.NewGuid();
}