using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Tk.Somnia.Application.Providers;
using Tk.Somnia.Data.Extensions;
using Tk.Somnia.Interface.Providers;

namespace Tk.Somnia.Application.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddTransient<IDateTimeProvider, DefaultDateTimeProvider>();
        services.AddTransient<IGuidProvider, DefaultGuidProvider>();
        
        services.AddData(configuration);
        
        return services;
    }
}