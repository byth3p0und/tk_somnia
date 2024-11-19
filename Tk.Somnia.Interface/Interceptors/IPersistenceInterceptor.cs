using Microsoft.EntityFrameworkCore.Diagnostics;

namespace Tk.Somnia.Interface.Interceptors;

public interface IPersistenceInterceptor : ISaveChangesInterceptor
{
}